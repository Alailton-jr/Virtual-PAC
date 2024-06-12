using Newtonsoft.Json;
using System.Configuration;
using System.Diagnostics.Eventing.Reader;
using System.Net.Sockets;
using static Quality.Classes;
using static Quality.Classes.SampledValue;
using static Quality.MainForm;

namespace Quality
{
    public partial class MonitorForm : Form
    {

        private List<SampledValue> svConfig;
        private List<SvMonitor> svMonitors;
        private Button addButton;
        private List<CheckBox> CbxMenus;
        private int curSvSelected = -1;
        private SocketConnection socket;
        private bool monitoring;
        List<SampledValue.RegisteredEvents> curVtEvents;

        private MenuOptions curMenu;
        private enum MenuOptions
        {
            General = 0,
            Vtcd = 1,
            Vtld = 2,
            Harm = 3,
            Transient = 4,
            Unbalance = 5,
            Fluctuation = 6
        }

        public MonitorForm()
        {
            InitializeComponent();

            if (mainControl.sampledValues == null) mainControl.sampledValues = new List<SampledValue>();

            svConfig = MainForm.mainControl.sampledValues;

            socket = mainControl.socket;

            socket.ConnectionLost += serverDisconnected;

            curMenu = MenuOptions.General;
            svMonitors = new List<SvMonitor>();
            CbxMenus = new List<CheckBox>()
            {
                CbxGeneral,
                CbxVtcd,
                CbxVtld,
                CbxHarm,
                CbxTransient,
                CbxUnbalance,
                Cbxfluctuation
            };
            CbxGeneral_CheckedChanged(CbxGeneral, null);
            defineAddButton();
            updateTable();
            monitoring = false;
            socket.ConnectionLost += monitorDisconnected;


            CbGenMeasures.Items.Clear();
            CbGenMeasures.Items.Add("RMS");
            CbGenMeasures.Items.Add("Fundamental");
            for (int i = 2; i <= 40; i++)
            {
                CbGenMeasures.Items.Add(i + "º Harmônico");
            }
            CbGenMeasures.SelectedIndex = 0;
            abcOr012 = false;
        }

        public void ExtReload()
        {
            updateTable();

        }

        private void defineAddButton()
        {
            addButton = new Button()
            {
                Text = "Add SV",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 12.5F, FontStyle.Bold),
                BackColor = Color.FromArgb(0, 122, 204),
                ForeColor = Color.Lavender,
                Size = new Size(165, 238),
            };
        }

        private void updateTable()
        {
            TlpSvTable.Controls.Clear();
            svMonitors.Clear();
            int nSV = svConfig.Count;


            // Max columns is 4

            // First add the columns and row configuration
            if (nSV > 0)
            {
                TlpSvTable.ColumnCount = 4;
                TlpSvTable.RowCount = ((nSV + 1) / 4) + 1;
            }
            else
            {
                TlpSvTable.ColumnCount = 1;
                TlpSvTable.RowCount = 1;
            }

            int i_col, i_row;
            for (int i_sv = 0; i_sv < nSV; i_sv++)
            {
                svMonitors.Add(new SvMonitor()
                {
                    svID = svConfig[i_sv].SVID,
                    Tag = i_sv,
                    isRunning = svConfig[i_sv].running
                });
                TlpSvTable.Controls.Add(
                    svMonitors[i_sv],
                    i_sv % 4,
                    i_sv / 4
                );
                svMonitors[i_sv].myOnClick += SvSelectedChanged;
            }

            TlpSvTable.Controls.Add(addButton, svConfig.Count % 4, svConfig.Count / 4);

        }

        private void SvSelectedChanged(object? sender, EventArgs e)
        {
            SvMonitor local = (SvMonitor)sender;
            if (local == null || local.Tag == null) return;
            if (!local.selected)
            {
                local.myOnClick -= SvSelectedChanged;
                local.selected = true;
                local.myOnClick += SvSelectedChanged;
            }
            for (int i = 0; i < svMonitors.Count; i++)
            {
                if ((int)local.Tag != i) svMonitors[i].selected = false;
            }
            curSvSelected = (int)local.Tag;
            if (svConfig[curSvSelected].running)
            {
                LbSvStatus.Text = "       Sampled Value Encontrado";
                LbSvStatus.Image = Properties.Resources.running;
            }
            else
            {
                LbSvStatus.Text = "       Sampled Value não Encontrado";
                LbSvStatus.Image = Properties.Resources.stoped;
            }
            updateMenus();
        }

        private void updateMenus()
        {
            if (curMenu == MenuOptions.Vtcd)
                updateVtcdEvent();
            else if (curMenu == MenuOptions.Vtld)
                updateVtldEvent();
            else if (curMenu == MenuOptions.Harm)
                updateHarm();
            else if (curMenu == MenuOptions.Transient)
                updateTransient();
            else if (curMenu == MenuOptions.Unbalance)
                updateUnbalance();
            else if (curMenu == MenuOptions.Fluctuation)
                updateFluctuation();
            else if (curMenu == MenuOptions.General)
                updateGeneral();
        }

        private bool abcOr012;
        private void updateGeneral()
        {
            if (!(curSvSelected >= 0)) return;

            BtnAbcOr012.Enabled = true;
            int idx = CbGenMeasures.SelectedIndex;
            if (idx == 0) // RMS
            {
                if (svConfig[curSvSelected].generalInfo.Rms == null) return;
                TbGenIaMod.Text = svConfig[curSvSelected].generalInfo.Rms[0].ToString("0.0000");
                TbGenIbMod.Text = svConfig[curSvSelected].generalInfo.Rms[1].ToString("0.0000");
                TbGenIcMod.Text = svConfig[curSvSelected].generalInfo.Rms[2].ToString("0.0000");
                TbGenInMod.Text = svConfig[curSvSelected].generalInfo.Rms[3].ToString("0.0000");
                TbGenVaMod.Text = svConfig[curSvSelected].generalInfo.Rms[4].ToString("0.0000");
                TbGenVbMod.Text = svConfig[curSvSelected].generalInfo.Rms[5].ToString("0.0000");
                TbGenVcMod.Text = svConfig[curSvSelected].generalInfo.Rms[6].ToString("0.0000");
                TbGenVnMod.Text = svConfig[curSvSelected].generalInfo.Rms[7].ToString("0.0000");

                TbGenIaAng.Text = "-";
                TbGenIbAng.Text = "-";
                TbGenIcAng.Text = "-";
                TbGenInAng.Text = "-";
                TbGenVaAng.Text = "-";
                TbGenVbAng.Text = "-";
                TbGenVcAng.Text = "-";
                TbGenVnAng.Text = "-";
                BtnAbcOr012.Enabled = false;
            }
            else
            {
                double[][][] data;
                if (!abcOr012)
                {
                    data = svConfig[svConfig.Count - 1].generalInfo.PhasorPolar;
                    double angRef = data[4][idx][1];
                    for (int i = 0; i < 8; i++)
                    {
                        data[i][idx][1] -= angRef;
                        data[i][idx][1] = data[i][idx][1] * 180 / Math.PI;
                        if (data[i][idx][1] > 180) data[i][idx][1] -= 360;
                    }
                    TbGenIaMod.Text = data[0][idx][0].ToString("0.00");
                    TbGenIaAng.Text = data[0][idx][1].ToString("0.00");
                    TbGenIbMod.Text = data[1][idx][0].ToString("0.00");
                    TbGenIbAng.Text = data[1][idx][1].ToString("0.00");
                    TbGenIcMod.Text = data[2][idx][0].ToString("0.00");
                    TbGenIcAng.Text = data[2][idx][1].ToString("0.00");
                    TbGenVaMod.Text = data[4][idx][0].ToString("0.00");
                    TbGenVaAng.Text = data[4][idx][1].ToString("0.00");
                    TbGenVbMod.Text = data[5][idx][0].ToString("0.00");
                    TbGenVbAng.Text = data[5][idx][1].ToString("0.00");
                    TbGenVcMod.Text = data[6][idx][0].ToString("0.00");
                    TbGenVcAng.Text = data[6][idx][1].ToString("0.00");
                    TbGenVnMod.Text = data[7][idx - 1][0].ToString("0.00");
                    TbGenVnAng.Text = data[7][idx - 1][1].ToString("0.00");
                    TbGenInMod.Text = data[3][idx - 1][0].ToString("0.00");
                    TbGenInAng.Text = data[3][idx - 1][1].ToString("0.00");
                }
                else
                {
                    data = svConfig[svConfig.Count - 1].generalInfo.Symmetrical;

                    double angRef = data[1][0][1];
                    for (int i = 0; i < 2; i++)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            data[i][j][1] -= angRef;
                            data[i][j][1] = data[i][j][1] * 180 / Math.PI;
                            if (data[i][j][1] > 180) data[i][j][1] -= 360;
                        }
                    }

                    TbGenIaMod.Text = data[0][0][0].ToString("0.00");
                    TbGenIaAng.Text = data[0][0][1].ToString("0.00");
                    TbGenIbMod.Text = data[0][1][0].ToString("0.00");
                    TbGenIbAng.Text = data[0][1][1].ToString("0.00");
                    TbGenIcMod.Text = data[0][2][0].ToString("0.00");
                    TbGenIcAng.Text = data[0][2][1].ToString("0.00");

                    TbGenVaMod.Text = data[1][0][0].ToString("0.00");
                    TbGenVaAng.Text = data[1][0][1].ToString("0.00");
                    TbGenVbMod.Text = data[1][1][0].ToString("0.00");
                    TbGenVbAng.Text = data[1][1][1].ToString("0.00");
                    TbGenVcMod.Text = data[1][2][0].ToString("0.00");
                    TbGenVcAng.Text = data[1][2][1].ToString("0.00");
                    TbGenVnMod.Text = "-";
                    TbGenVnAng.Text = "-";
                    TbGenInMod.Text = "-";
                    TbGenInAng.Text = "-";
                }
            }

        }

        private void updateVtcdEvent()
        {
            if (!(curSvSelected >= 0)) return;

            var curSv = svConfig[curSvSelected];

            var curIdxCb = CbVtEvents.SelectedIndex;

            curVtEvents = new List<SampledValue.RegisteredEvents>();

            curVtEvents.AddRange(curSv.sag.RegisteredEvents);
            curVtEvents.AddRange(curSv.swell.RegisteredEvents);
            curVtEvents.AddRange(curSv.interruption.RegisteredEvents);

            CbVtEvents.Items.Clear();
            CbVtEvents.Items.AddRange(curVtEvents.Select(x => x.eventType.ToString()).ToArray());

            TbVtDate.Text = "";
            TbVtMagnitude.Text = "";
            TbVtDuration.Text = "";
            TbVtType.Text = "";
            CbVtEvents.Text = "";

            if (curIdxCb >= 0 && curIdxCb < curVtEvents.Count)
                CbVtEvents.SelectedIndex = curIdxCb;
            else
            {
                if (CbVtEvents.Items.Count > 0)
                    CbVtEvents.SelectedIndex = 0;
                else CbVtEvents.SelectedIndex = -1;
            }
        }

        private void updateFluctuation()
        {
            throw new NotImplementedException();
        }

        private void updateUnbalance()
        {
            throw new NotImplementedException();
        }

        private void updateTransient()
        {
            throw new NotImplementedException();
        }

        private void updateHarm()
        {
            throw new NotImplementedException();
        }

        private void updateVtldEvent()
        {
            //throw new NotImplementedException();
            if (!(curSvSelected >= 0)) return;

            var curSv = svConfig[curSvSelected];

            var curIdxCb = CbVtEvents.SelectedIndex;

            curVtEvents = new List<SampledValue.RegisteredEvents>();

            curVtEvents.AddRange(curSv.underVoltage.RegisteredEvents);
            curVtEvents.AddRange(curSv.overVoltage.RegisteredEvents);
            curVtEvents.AddRange(curSv.sustainedinterruption.RegisteredEvents);

            CbVtEvents.Items.Clear();
            CbVtEvents.Items.AddRange(curVtEvents.Select(x => x.name).ToArray());

            TbVtDate.Text = "";
            TbVtMagnitude.Text = "";
            TbVtDuration.Text = "";
            TbVtType.Text = "";
            CbVtEvents.Text = "";

            if (curIdxCb >= 0 && curIdxCb < curVtEvents.Count)
                CbVtEvents.SelectedIndex = curIdxCb;
            else
            {
                if (CbVtEvents.Items.Count > 0)
                    CbVtEvents.SelectedIndex = 0;
                else CbVtEvents.SelectedIndex = -1;
            }
        }

        private void CbxGeneral_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox local = (CheckBox)sender;
            if (local.Tag == null) return;
            if (!local.Checked)
            {
                local.CheckedChanged -= CbxGeneral_CheckedChanged;
                local.Checked = true;
                local.CheckedChanged += CbxGeneral_CheckedChanged;
                return;
            }

            foreach (CheckBox cbx in CbxMenus)
            {
                cbx.CheckedChanged -= CbxGeneral_CheckedChanged;
                if (cbx != local) cbx.Checked = false;
                cbx.CheckedChanged += CbxGeneral_CheckedChanged;
            }

            if (local.Checked)
            {
                //PnQuality.Controls.Clear();
                if (local == CbxGeneral)
                {
                    PnMenu.Controls.Add(PnGeneral);
                    PnGeneral.Dock = DockStyle.Fill;
                    PnGeneral.BringToFront();
                    PnGeneral.Visible = true;
                    curMenu = MenuOptions.General;
                }
                else if (local == CbxVtcd)
                {
                    PnMenu.Controls.Add(PnVt);
                    PnVt.Dock = DockStyle.Fill;
                    PnVt.BringToFront();
                    PnVt.Visible = true;
                    curMenu = MenuOptions.Vtcd;
                }
                //else if (local == CbxVtld)
                //{
                //    PnQuality.Controls.Add(PnVtld);
                //    PnVtld.Dock = DockStyle.Fill;
                //    PnVtld.BringToFront();
                //    PnVtld.Visible = true;
                //}
                //else if (local == CbxHarm)
                //{
                //    PnQuality.Controls.Add(PnHarm);
                //    PnHarm.Dock = DockStyle.Fill;
                //    PnHarm.BringToFront();
                //    PnHarm.Visible = true;
                //}
                //else if (local == CbxTransient)
                //{
                //    PnQuality.Controls.Add(PnTransient);
                //    PnTransient.Dock = DockStyle.Fill;
                //    PnTransient.BringToFront();
                //    PnTransient.Visible = true;
                //}
                //else if (local == CbxUnbalance)
                //{
                //    PnQuality.Controls.Add(PnUnbalance);
                //    PnUnbalance.Dock = DockStyle.Fill;
                //    PnUnbalance.BringToFront();
                //    PnUnbalance.Visible = true;
                //}
                //else if (local == Cbxfluctuation)
                //{
                //    PnQuality.Controls.Add(PnFluctuation);
                //    PnFluctuation.Dock = DockStyle.Fill;
                //    PnFluctuation.BringToFront();
                //    PnFluctuation.Visible = true;
                //}
            }

            updateMenus();

        }

        private void CbVtEvents_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!(curSvSelected >= 0)) return;
            if (CbVtEvents.SelectedIndex < 0 || CbVtEvents.SelectedIndex > curVtEvents.Count - 1) return;

            SampledValue.RegisteredEvents curEvent = curVtEvents[CbVtEvents.SelectedIndex];

            TbVtDate.Text = curEvent.date.ToString();
            TbVtMagnitude.Text = curEvent.magnitude.ToString("0.0000");
            TbVtDuration.Text = curEvent.duration.ToString("0.0000");

            if (curEvent.eventType == SampledValue.EventTypes.sag)
                TbVtType.Text = "Afundamento";
            else if (curEvent.eventType == SampledValue.EventTypes.swell)
                TbVtType.Text = "Eleveção";
            else if (curEvent.eventType == SampledValue.EventTypes.interruption)
                TbVtType.Text = "Interrupção";

        }

        private void BtnStartSearch_Click(object sender, EventArgs e)
        {
            if (!monitoring)
            {
                var yamlFile = mainControl.CreateYamlSampledValue();
                //var res = socket.SendData("ReceiveMonitorSetup", yamlFile);
                var res = socket.SendData(SocketConnection.entryType.NONE, yamlFile);
                if (res == "success")
                {
                    //res = socket.SendData("netMonitorStart", "");
                    res = socket.SendData(SocketConnection.entryType.NONE, "");
                    if (res == "success")
                    {
                        monitoring = true;
                        TimerEvents.Start();
                        BtnStartSearch.Text = "Parar Monitoramento";
                    }
                }
            }
            else
            {
                //var res = socket.SendData("netMonitorStop", "");
                var res = socket.SendData(SocketConnection.entryType.NONE, "");
                if (res == "success")
                {
                    BtnStartSearch.Text = "Iniciar Monitoramento";
                    TimerEvents.Stop();
                    monitoring = false;
                }
            }
        }

        private void monitorDisconnected(object? sender, EventArgs e)
        {
            BtnStartSearch.Text = "Iniciar Monitoramento";
            TimerEvents.Stop();
            monitoring = false;
        }

        private void GetEventsFromServer(int svidx)
        {
            //var res = socket.SendData("getRegistedEvents", svConfig[svidx].SVID);
            var res = socket.SendData(SocketConnection.entryType.NONE, svConfig[svidx].SVID);

            if (res != null && res != "error")
            {
                var events = JsonConvert.DeserializeObject<List<SampledValue.JsonServerRegistedEvents>>(res);


                svConfig[svidx].sag.RegisteredEvents.Clear();
                svConfig[svidx].swell.RegisteredEvents.Clear();
                svConfig[svidx].interruption.RegisteredEvents.Clear();

                if (events == null) return;
                foreach (var ev in events)
                {
                    var revent = new RegisteredEvents();
                    revent.date = DateTime.Parse(ev.Date);
                    revent.duration = double.Parse(ev.Duration);
                    revent.name = ev.Name;
                    revent.eventType = ev.Type == "sag" ? SampledValue.EventTypes.sag :
                        ev.Type == "swell" ? SampledValue.EventTypes.swell :
                        SampledValue.EventTypes.interruption;

                    if (revent.eventType == EventTypes.sag)
                    {
                        revent.magnitude = double.Parse(ev.MinValue) / svConfig[svidx].nominalVoltage;
                        svConfig[svidx].sag.RegisteredEvents.Add(revent);
                    }
                    else if (revent.eventType == EventTypes.swell)
                    {
                        revent.magnitude = double.Parse(ev.MaxValue) / svConfig[svidx].nominalVoltage;
                        svConfig[svidx].swell.RegisteredEvents.Add(revent);
                    }
                    else if (revent.eventType == EventTypes.interruption)
                    {
                        svConfig[svidx].interruption.RegisteredEvents.Add(revent);
                    }
                }
                updateVtcdEvent();
            }
        }

        
        private void TimerEvents_Tick(object sender, EventArgs e)
        {
            try
            {
                //return;
                for (int i = 0; i < svConfig.Count; i++)
                {
                    //getMonitorSvInfo
                    //var res = socket.ReceiveFile("getMonitorSvInfo", svConfig[i].SVID);
                    var res = socket.ReceiveFile(SocketConnection.entryType.NONE, svConfig[i].SVID);
                    try
                    {
                        //string resStr = System.Text.Encoding.UTF8.GetString(res);
                        //var events = JsonConvert.DeserializeObject<SampledValue.JsonSvServerInfo>(resStr);
                        //if (events == null) continue;

                        //svMonitors[i].isRunning = events.Found;
                        //svMonitors[i].vtcd = (events.flags.Sag + events.flags.Swell + events.flags.Interruption) > 0;
                        //svMonitors[i].vtld = (events.flags.OverVoltage + events.flags.UnderVoltage) > 0;

                        //svConfig[i].running = events.Found;
                        //svConfig[i].sag.flag = events.flags.Sag > 0;
                        //svConfig[i].swell.flag = events.flags.Swell > 0;
                        //svConfig[i].interruption.flag = events.flags.Interruption > 0;
                        //svConfig[i].overVoltage.flag = events.flags.OverVoltage > 0;
                        //svConfig[i].underVoltage.flag = events.flags.UnderVoltage > 0;

                        //svConfig[i].generalInfo.Rms = events.Rms;
                        //svConfig[i].generalInfo.PhasorPolar = events.Phasor;
                        //svConfig[i].generalInfo.Symmetrical = events.Symmetrical;
                        //svConfig[i].generalInfo.Unbalance = events.Unbalance;

                        updateGeneral();
                        GetEventsFromServer(i);
                    }
                    catch
                    {

                    }
                }
            }
            catch
            {

            }
        }

        private void BtnVtDownload_Click(object sender, EventArgs e)
        {
            //byte[] waveFormBytes = socket.ReceiveFile("getMonitorWaveForm", CbVtEvents.Text);
            byte[] waveFormBytes = socket.ReceiveFile(SocketConnection.entryType.NONE, CbVtEvents.Text);
            if (waveFormBytes != null && waveFormBytes.Length <= 0) return;
            try
            {
                string waveFormString = System.Text.Encoding.UTF8.GetString(waveFormBytes);
                List<List<double>> waveForm = new List<List<double>>();
                var rows = waveFormString.Trim().Split('\n');

                foreach (var value in rows[0].Trim().Split(','))
                {
                    if (string.IsNullOrEmpty(value)) continue;
                    waveForm.Add(new List<double>());
                }

                for (int i = 0; i < rows.Length; i++)
                {
                    var row = rows[i].Trim().Split(',');
                    if (row.Length - 1 != waveForm.Count) continue;
                    for (int j = 0; j < row.Length; j++)
                    {
                        if (string.IsNullOrEmpty(row[j])) continue;
                        waveForm[j].Add(double.Parse(row[j]));
                    }
                }
                if (waveForm.Count > 0)
                {
                    var showData = new ShowData();
                    showData.OpenExt(waveForm, CbVtEvents.Text, double.Parse(TbVtDuration.Text));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading file from Server, try capturing again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void serverDisconnected(object? sender, EventArgs e)
        {
            BtnStartSearch.Text = "Iniciar Monitoramento";
            TimerEvents.Stop();
            monitoring = false;
        }

        private void BtnDel_Click(object sender, EventArgs e)
        {
            if (curSvSelected >= 0 && curSvSelected <= svConfig.Count)
            {
                svConfig.RemoveAt(curSvSelected);
                curSvSelected = -1;
                updateTable();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            abcOr012 = !abcOr012;
            if (!abcOr012)
            {
                BtnAbcOr012.Text = "012";
                LbGenVa.Text = "Va";
                LbGenVb.Text = "Vb";
                LbGenVc.Text = "Vc";
                LbGenVn.Text = "Vn";
                LbGenIa.Text = "Ia";
                LbGenIb.Text = "Ib";
                LbGenIc.Text = "Ic";
                LbGenIn.Text = "In";
                CbGenMeasures.Enabled = true;
            }
            else
            {
                BtnAbcOr012.Text = "ABC";
                LbGenVa.Text = "V0";
                LbGenVb.Text = "V1";
                LbGenVc.Text = "V2";
                LbGenVn.Text = "-";
                LbGenIa.Text = "I0";
                LbGenIb.Text = "I1";
                LbGenIc.Text = "I2";
                LbGenIn.Text = "-";
                CbGenMeasures.Enabled = false;
            }
            updateGeneral();
        }

        private void CbGenMeasures_SelectedIndexChanged(object sender, EventArgs e)
        {
            updateGeneral();
        }
    }
}
