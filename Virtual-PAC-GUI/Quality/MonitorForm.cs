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
                    //PnQuality.Controls.Add(PnGeneral);
                    //PnGeneral.Dock = DockStyle.Fill;
                    //PnGeneral.BringToFront();
                    //PnGeneral.Visible = true;
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

        List<SampledValue.RegisteredEvents> curVtcdEvents;
        private void updateVtcdEvent()
        {
            if (!(curSvSelected >= 0)) return;

            var curSv = svConfig[curSvSelected];

            var curIdxCb = CbVtEvents.SelectedIndex;

            curVtcdEvents = new List<SampledValue.RegisteredEvents>();

            curVtcdEvents.AddRange(curSv.sag.RegisteredEvents);
            curVtcdEvents.AddRange(curSv.swell.RegisteredEvents);
            curVtcdEvents.AddRange(curSv.interruption.RegisteredEvents);

            CbVtEvents.Items.Clear();
            CbVtEvents.Items.AddRange(curVtcdEvents.Select(x => x.name).ToArray());

            TbVtDate.Text = "";
            TbVtMagnitude.Text = "";
            TbVtDuration.Text = "";
            TbVtType.Text = "";
            CbVtEvents.Text = "";

            if (curIdxCb >= 0 && curIdxCb < curVtcdEvents.Count)
                CbVtEvents.SelectedIndex = curIdxCb;
            else
            {
                if (CbVtEvents.Items.Count > 0)
                    CbVtEvents.SelectedIndex = 0;
                else CbVtEvents.SelectedIndex = -1;
            }


            //if (svConfig[curSvSelected].running)
            //    if (CbVtEvents.Items.Count > 0)
            //        CbVtEvents.SelectedIndex = 0;

        }

        private void CbVtEvents_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!(curSvSelected >= 0)) return;
            if (CbVtEvents.SelectedIndex < 0 || CbVtEvents.SelectedIndex > curVtcdEvents.Count - 1) return;

            SampledValue.RegisteredEvents curEvent = curVtcdEvents[CbVtEvents.SelectedIndex];

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
                var res = socket.SendData("ReceiveMonitorSetup", yamlFile);
                if (res == "success")
                {
                    monitoring = true;
                    BtnStartSearch.Text = "Parar Monitoramento";
                    TimerEvents.Start();
                }
            }
            else
            {
                BtnStartSearch.Text = "Iniciar Monitoramento";
                TimerEvents.Stop();
            }

        }

        private class JsonRegistedEvents
        {
            public string Type { get; set; }
            public string Duration { get; set; }
            public string MinValue { get; set; }
            public string MaxValue { get; set; }
            public string Name { get; set; }
            public string Date { get; set; }
        }

        private void GetEventsFromServer(int svidx)
        {
            var res = socket.SendData("getRegistedEvents", svConfig[svidx].SVID);

            if (res != null && res != "error")
            {
                var events = JsonConvert.DeserializeObject<List<JsonRegistedEvents>>(res);


                svConfig[svidx].sag.RegisteredEvents.Clear();
                svConfig[svidx].swell.RegisteredEvents.Clear();
                svConfig[svidx].interruption.RegisteredEvents.Clear();


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
                for (int i = 0; i < svConfig.Count; i++)
                {
                    GetEventsFromServer(i);
                }
            }
            catch
            {

            }
        }

        private void BtnVtDownload_Click(object sender, EventArgs e)
        {
            byte[] waveFormBytes = socket.ReceiveFile("getMonitorWaveForm", CbVtEvents.Text);
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
    }
}
