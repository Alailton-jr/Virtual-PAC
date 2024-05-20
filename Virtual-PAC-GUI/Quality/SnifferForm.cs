using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Quality.MainForm;
using static Quality.Classes;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;
using static Quality.Classes.YamlSnifferData;

namespace Quality
{
    public partial class SnifferForm : Form
    {
        private SocketConnection socket;
        List<SampledValue> curSvData;
        List<List<TextBox>> textBoxes;
        List<Label> labels;
        private int curSel;

        private double _duration;
        private string duration
        {
            get
            {
                return _duration.ToString("0.00") + " s";
            }
            set
            {
                // Try parse the value
                if (double.TryParse(value.Replace(" s", ""), out double result))
                {
                    _duration = result;
                }
                TbDuration.Text = duration;
            }
        }

        public SnifferForm()
        {
            InitializeComponent();
            CbxVtcd_CheckedChanged(CbxGeneral, null);
            socket = mainControl.socket;
            duration = "1.00 s";
            PnMainSniffer.Dock = DockStyle.Fill;
            curSvData = new List<SampledValue>();
            labels = new List<Label>();
            curSel = -1;
            SelectSv(-1);
        }

        private void CbxVtcd_CheckedChanged(object sender, EventArgs e)
        {

            CheckBox local = (CheckBox)sender;
            if (local.Tag == null) return;

            CbxGeneral.CheckedChanged -= CbxVtcd_CheckedChanged;
            CbxVtcd.CheckedChanged -= CbxVtcd_CheckedChanged;
            CbxVtld.CheckedChanged -= CbxVtcd_CheckedChanged;
            Cbxfluctuation.CheckedChanged -= CbxVtcd_CheckedChanged;
            CbxHarm.CheckedChanged -= CbxVtcd_CheckedChanged;
            CbxTransient.CheckedChanged -= CbxVtcd_CheckedChanged;
            CbxUnbalance.CheckedChanged -= CbxVtcd_CheckedChanged;


            CbxVtcd.Checked = false;
            CbxVtld.Checked = false;
            CbxGeneral.Checked = false;
            Cbxfluctuation.Checked = false;
            CbxHarm.Checked = false;
            CbxTransient.Checked = false;
            CbxUnbalance.Checked = false;


            local.Checked = true;
            if (int.Parse((string)local.Tag) == 1)
            {
                PnQuality.Controls.Add(PnGeneral);
                PnGeneral.Dock = DockStyle.Fill;
                PnGeneral.BringToFront();
                PnGeneral.Visible = true;
            }
            else if (int.Parse((string)local.Tag) == 2)
            {
                PnQuality.Controls.Add(PnVtcd);
                PnVtcd.Dock = DockStyle.Fill;
                PnVtcd.BringToFront();
                PnVtcd.Visible = true;
            }
            else if (int.Parse((string)local.Tag) == 3)
            {
                PnQuality.Controls.Clear();
                PnQuality.Controls.Add(PnVtld);
                PnVtld.Dock = DockStyle.Fill;
                PnVtld.BringToFront();
                PnVtld.Visible = true;
            }
            CbxGeneral.CheckedChanged += CbxVtcd_CheckedChanged;
            CbxVtcd.CheckedChanged += CbxVtcd_CheckedChanged;
            CbxVtld.CheckedChanged += CbxVtcd_CheckedChanged;
            Cbxfluctuation.CheckedChanged += CbxVtcd_CheckedChanged;
            CbxHarm.CheckedChanged += CbxVtcd_CheckedChanged;
            CbxTransient.CheckedChanged += CbxVtcd_CheckedChanged;
            CbxUnbalance.CheckedChanged += CbxVtcd_CheckedChanged;

        }

        private bool searching = false;

        private void BtnStartSearch_Click(object sender, EventArgs e)
        {
            socket.changeConProperties(mainControl.serverConfig.ipAddress, mainControl.serverConfig._port);
            socket.IsSocketConnected();
            if (!socket.isConnected)
            {
                socket.Connect();
                if (!socket.isConnected)
                {
                    stopSearch();
                    MessageBox.Show("Server is not connected", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            if (searching)
            {
                stopSearch();
            }
            else
            {
                startSearch();
            }
        }

        private void startSearch(bool sendCommand = true)
        {
            if (!searching)
            {
                if (sendCommand)
                {
                    //string res = socket.SendData("netCaptureStart", _duration.ToString());
                    string res = socket.SendData(SocketConnection.entryType.CAPT_ANY_SV, _duration.ToString());
                    if (res != null && res == "success")
                    {
                        curSel = -1;
                        LbLoading.Visible = true;
                        pBarSearch.Visible = true;
                        TbSVFound.Text = "-1";
                        searching = true;
                        TimerGetResults.Start();
                        LbSearchStatus.Text = "Buscando";
                        BtnStartSearch.Text = "Stop Search";
                    }
                }
                else
                {
                    searching = false;
                    //TimerGetResults.Start();
                }
            } 
        }

        private void stopSearch(bool sendCommand = true)
        {
            if (searching)
            {
                LbLoading.Visible = false;
                pBarSearch.Visible = false;
                pBarSearch.Value = 0;
                if (sendCommand)
                {
                    //string res = socket.SendData("netCaptureStop", "");
                    string res = socket.SendData(SocketConnection.entryType.CAPT_SV_STOP, "");
                    if (res != null && res == "success")
                    {
                        searching = false;
                        TimerGetResults.Stop();
                        LbSearchStatus.Text = "Busca Parada";
                    }
                }
                else
                {
                    BtnStartSearch.Text = "Start Search";
                    searching = false;
                    TimerGetResults.Stop();
                    LbSearchStatus.Text = "Busca Parada";
                }

            }
        }

        private void TbDuration_Validated(object sender, EventArgs e)
        {
            this.duration = TbDuration.Text;
        }

        private double[] getFreqAndSmpRate(double meanTime)
        {
            double[] freqs = { 50, 60 };
            double[] smpRate = { 80, 240, 256 };
            double minDifference = double.MaxValue;
            double closestFrequency = 0;
            double closestSampleRate = 0;
            foreach (double freq in freqs)
            {
                foreach (double smp in smpRate)
                {
                    double calculatedValue = 1 / (freq * smp);
                    double difference = Math.Abs(calculatedValue - meanTime);
                    if (difference < minDifference)
                    {
                        minDifference = difference;
                        closestFrequency = freq;
                        closestSampleRate = smp;
                    }
                }
            }
            return new double[] { closestFrequency, closestSampleRate };
        }

        private void SelectSv(int idx)
        {
            if (idx == curSel) return;
            //mainControl.sampledValues.Clear();
            if (idx == -1)
            {
                TbFreq.Text = "";
                TbMacDst.Text = "";
                TbSvID.Text = "";
                TbNAsdu.Text = "";
                TbNChannels.Text = "";
                TbSmpRate.Text = "";
                TbVLANID.Text = "";
                TbVLANPriority.Text = "";
                BtnMapSV.Enabled = false;

                TbUnderVoltageBottomThreshold.Text = "";
                TbUnderVoltageTopThreshold.Text = "";
                TbUnderVoltageMaxTime.Text = "";
                TbUnderVoltageMinTime.Text = "";

                TbOverVoltageBottomThreshold.Text = "";
                TbOverVoltageTopThreshold.Text = "";
                TbOverVoltageMaxTime.Text = "";
                TbOverVoltageMinTime.Text = "";

                TbSagBottonThreshold.Text = "";
                TbSagTopThreshold.Text = "";
                TbSagMaxTime.Text = "";
                TbSagMinTime.Text = "";

                TbSwellBottonThreshold.Text = "";
                TbSwellTopThreshold.Text = "";
                TbSwellMaxTime.Text = "";
                TbSwellMinTime.Text = "";

                TbInterruptionBottonThreshold.Text = "";
                TbInterruptionTopThreshold.Text = "";
                TbInterruptionMaxTime.Text = "";
                TbInterruptionMinTime.Text = "";

                TbSustainedBottomThreshold.Text = "";
                TbSustainedTopThreshold.Text = "";
                TbSustainedInterruptionMaxTime.Text = "";
                TbSustainedInterruptionMinTime.Text = "";


                return;
            }

            #region Fields
            TbFreq.Text = curSvData[idx].frequency.ToString();
            TbMacDst.Text = curSvData[idx].macDst;
            TbSvID.Text = curSvData[idx].SVID;
            TbNAsdu.Text = curSvData[idx].noAsdu.ToString();
            TbNChannels.Text = curSvData[idx].noChannels.ToString();
            TbSmpRate.Text = curSvData[idx].smpRate.ToString();
            TbVLANID.Text = curSvData[idx].vLANID.ToString();
            TbVLANPriority.Text = curSvData[idx].vLANPriority.ToString();

            TbUnderVoltageBottomThreshold.Text = curSvData[idx].underVoltage.bottomThreshold.ToString();
            TbUnderVoltageTopThreshold.Text = curSvData[idx].underVoltage.topThreshold.ToString();
            TbUnderVoltageMaxTime.Text = curSvData[idx].underVoltage.maxDuration.ToString();
            TbUnderVoltageMinTime.Text = curSvData[idx].underVoltage.minDuration.ToString();

            TbOverVoltageBottomThreshold.Text = curSvData[idx].overVoltage.bottomThreshold.ToString();
            TbOverVoltageTopThreshold.Text = curSvData[idx].overVoltage.topThreshold.ToString();
            TbOverVoltageMaxTime.Text = curSvData[idx].overVoltage.maxDuration.ToString();
            TbOverVoltageMinTime.Text = curSvData[idx].overVoltage.minDuration.ToString();

            TbSagBottonThreshold.Text = curSvData[idx].sag.bottomThreshold.ToString();
            TbSagTopThreshold.Text = curSvData[idx].sag.topThreshold.ToString();
            TbSagMaxTime.Text = curSvData[idx].sag.maxDuration.ToString();
            TbSagMinTime.Text = curSvData[idx].sag.minDuration.ToString();

            TbSwellBottonThreshold.Text = curSvData[idx].swell.bottomThreshold.ToString();
            TbSwellTopThreshold.Text = curSvData[idx].swell.topThreshold.ToString();
            TbSwellMaxTime.Text = curSvData[idx].swell.maxDuration.ToString();
            TbSwellMinTime.Text = curSvData[idx].swell.minDuration.ToString();

            TbInterruptionBottonThreshold.Text = curSvData[idx].interruption.bottomThreshold.ToString();
            TbInterruptionTopThreshold.Text = curSvData[idx].interruption.topThreshold.ToString();
            TbInterruptionMaxTime.Text = curSvData[idx].interruption.maxDuration.ToString();
            TbInterruptionMinTime.Text = curSvData[idx].interruption.minDuration.ToString();

            TbSustainedBottomThreshold.Text = curSvData[idx].sustainedinterruption.bottomThreshold.ToString();
            TbSustainedTopThreshold.Text = curSvData[idx].sustainedinterruption.topThreshold.ToString();
            TbSustainedInterruptionMaxTime.Text = curSvData[idx].sustainedinterruption.maxDuration.ToString();
            TbSustainedInterruptionMinTime.Text = curSvData[idx].sustainedinterruption.minDuration.ToString();

            TbNomCurrent.Text = curSvData[idx].nominalCurrent.ToString();
            TbNomVoltage.Text = curSvData[idx].nominalVoltage.ToString();

            #endregion

            // Check if exist the SV with the same ID
            if (mainControl.sampledValues.Exists(sv => sv.SVID == curSvData[idx].SVID))
                BtnMapSV.Text = "Atualizar Sampled Value";
            else
                BtnMapSV.Text = "Mapear Sampled Value";


            for (int i = 0; i < textBoxes[idx].Count; i++)
            {
                textBoxes[idx][i].ForeColor = Color.DeepSkyBlue;
                if (curSel >= 0)
                    textBoxes[curSel][i].ForeColor = Color.Lavender;
            }
            curSel = idx;
        }

        private void BtnVerCaptura_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            if (btn.Tag == null) return;

            int idx = (int)btn.Tag;
            if (idx < 0) return;

            //byte[] waveFormBytes = socket.ReceiveFile("netCaptureWaveForm", curSvData[idx].SVID);
            byte[] waveFormBytes = socket.ReceiveFile(SocketConnection.entryType.CAPT_SV_WAVEFORM, curSvData[idx].SVID);
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
                    showData.OpenExt(waveForm, curSvData[idx].SVID, _duration);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading file from Server, try capturing again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSelect_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int idx = (int)btn.Tag;
            SelectSv(idx);
        }

        private void UpdateTableFields()
        {
            //TlpData:
            //SvID | MacDst | MacSrc | Mapeado | Ver Captura | Selecionar
            //textBox | textBox | textBox | textBox | button | button

            TlpData.GrowStyle = TableLayoutPanelGrowStyle.AddRows;
            TlpData.Controls.Clear();
            TlpData.RowCount = curSvData.Count + 1;
            TlpData.ColumnCount = 6;

            TlpData.Controls.Add(LbSvID);
            TlpData.Controls.Add(LbMacDst);
            TlpData.Controls.Add(LbMacDst);
            TlpData.Controls.Add(LbMacOri);
            TlpData.Controls.Add(LbMaped);
            TlpData.Controls.Add(LbVerCaptura);
            TlpData.Controls.Add(LbSel);

            textBoxes = new List<List<TextBox>>();
            labels = new List<Label>();
            for (int i = 0; i < curSvData.Count; i++)
            {
                textBoxes.Add(new List<System.Windows.Forms.TextBox>());

                TextBox tbSvID = new TextBox();
                tbSvID.Text = curSvData[i].SVID;
                tbSvID.ReadOnly = true;
                tbSvID.Dock = DockStyle.Fill;
                tbSvID.BackColor = Color.FromArgb(31, 45, 56);
                tbSvID.ForeColor = Color.Lavender;
                tbSvID.TextAlign = HorizontalAlignment.Center;
                tbSvID.BorderStyle = BorderStyle.FixedSingle;
                TlpData.Controls.Add(tbSvID);

                TextBox tbMacDst = new TextBox();
                tbMacDst.Text = curSvData[i].macDst.ToUpper();
                tbMacDst.ReadOnly = true;
                tbMacDst.BackColor = Color.FromArgb(31, 45, 56);
                tbMacDst.ForeColor = Color.Lavender;
                tbMacDst.Dock = DockStyle.Fill;
                tbMacDst.TextAlign = HorizontalAlignment.Center;
                tbMacDst.BorderStyle = BorderStyle.FixedSingle;
                TlpData.Controls.Add(tbMacDst);

                TextBox tbMacSrc = new TextBox();
                tbMacSrc.Text = curSvData[i].macSrc.ToUpper();
                tbMacSrc.ReadOnly = true;
                tbMacSrc.BackColor = Color.FromArgb(31, 45, 56);
                tbMacSrc.ForeColor = Color.Lavender;
                tbMacSrc.Dock = DockStyle.Fill;
                tbMacSrc.TextAlign = HorizontalAlignment.Center;
                tbMacSrc.BorderStyle = BorderStyle.FixedSingle;
                TlpData.Controls.Add(tbMacSrc);

                Label tbMapeado = new Label();
                tbMapeado.Dock = DockStyle.Fill;
                tbMapeado.BackColor = Color.FromArgb(31, 45, 56);
                if (mainControl.sampledValues.Exists(sv => sv.SVID == curSvData[i].SVID))
                    tbMapeado.Image = Properties.Resources.running;
                else
                    tbMapeado.Image = Properties.Resources.stoped;
                TlpData.Controls.Add(tbMapeado);

                Button btnVerCaptura = new Button();
                btnVerCaptura.Text = " ";
                btnVerCaptura.Dock = DockStyle.Fill;
                btnVerCaptura.Tag = i;
                btnVerCaptura.Click += BtnVerCaptura_Click;
                btnVerCaptura.FlatStyle = FlatStyle.Flat;
                TlpData.Controls.Add(btnVerCaptura);

                Button btnSelect = new Button();
                btnSelect.Text = " ";
                btnSelect.Dock = DockStyle.Fill;
                btnSelect.Tag = i;
                btnSelect.Click += BtnSelect_Click;
                btnSelect.FlatStyle = FlatStyle.Flat;
                TlpData.Controls.Add(btnSelect);

                textBoxes[i].Add(tbSvID);
                textBoxes[i].Add(tbMacDst);
                textBoxes[i].Add(tbMacSrc);
                labels.Add(tbMapeado);
                //textBoxes[i].Add(tbMapeado);
            }

            foreach (RowStyle rowStyle in TlpData.RowStyles)
            {
                rowStyle.SizeType = SizeType.AutoSize;
            }
        }

        private void getResults()
        {
            byte[]? svData = socket.ReceiveFile(SocketConnection.entryType.CAPT_SV_DATA, "");
            if (svData != null)
            {
                string yamlString = System.Text.Encoding.UTF8.GetString(svData);
                try
                {
                    var deserializer = new DeserializerBuilder().Build();
                    var yamlRoot = deserializer.Deserialize<YamlSnifferData.YamlRoot>(yamlString);
                    if (yamlRoot == null) return;
                    if (yamlRoot.nSV > 0)
                    {
                        //curSvData = yamlRoot.svData;
                        curSvData.Clear();
                        foreach (var sv in yamlRoot.svData)
                        {
                            var freqAndSmpRate = getFreqAndSmpRate(sv.MeanTime / sv.nAsdu);
                            curSvData.Add(new SampledValue()
                            {
                                noAsdu = sv.nAsdu,
                                noChannels = sv.nChannels,
                                smpRate = (int)freqAndSmpRate[1],
                                SVID = sv.svID,
                                vLANID = sv.vLanId,
                                vLANPriority = sv.vLanPriority,
                                macDst = sv.macDst,
                                frequency = (int)freqAndSmpRate[0],
                                macSrc = sv.macSrc
                            });
                        }
                        UpdateTableFields();
                        SelectSv(0);
                    }
                    else
                    {
                        curSvData.Clear();
                        UpdateTableFields();
                        SelectSv(-1);
                    }
                    TbSVFound.Text = yamlRoot.nSV.ToString();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error parsing the results: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void TimerGetResults_Tick(object sender, EventArgs e)
        {
            if (searching)
            {
                if (!socket.isConnected) { 
                    TimerGetResults.Stop();
                    stopSearch(false);
                    MessageBox.Show("Server is not connected", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                //string res = socket.SendData("netCaptureStatus", "");
                string res = socket.SendData(SocketConnection.entryType.CAPT_SV_STATUS, "");
                if (res != null)
                {
                    string[] resLines = res.Split("|");
                    if (resLines[0] == "1")
                    {
                        try
                        {
                            double time = double.Parse(resLines[1]);
                            int nSv = int.Parse(resLines[2]);
                            TbSVFound.Text = nSv.ToString();
                            pBarSearch.Value = (int)(time / _duration * 100);
                        }
                        catch { }
                        
                    }
                    else if(resLines[0] == "0")
                    {
                        pBarSearch.Value = 100;
                        getResults();
                        stopSearch(false);
                    }
                }
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void BtnMapSV_Click(object sender, EventArgs e)
        {
            var svIdx = mainControl.sampledValues.FindIndex(sv => sv.SVID == curSvData[curSel].SVID);
            if (svIdx != null && svIdx >= 0)
            {
                mainControl.sampledValues[svIdx] = curSvData[curSel];
            }
            else
            {
                mainControl.sampledValues.Add(curSvData[curSel]);
            }
            BtnMapSV.Text = "Atualizar Sampled Value";
            labels[curSel].Image = Properties.Resources.running;
        }

        // This function will be used for all the textboxes
        private void TbSagBottonThreshold_Validated(object sender, EventArgs e)
        {
            if (curSel < 0) return;
            curSvData[curSel].sag.bottomThreshold = changeDouble(TbSagBottonThreshold.Text, curSvData[curSel].sag.bottomThreshold);
            curSvData[curSel].sag.topThreshold = changeDouble(TbSagTopThreshold.Text, curSvData[curSel].sag.topThreshold);
            curSvData[curSel].sag.maxDuration = changeDouble(TbSagMaxTime.Text, curSvData[curSel].sag.maxDuration);
            curSvData[curSel].sag.minDuration = changeDouble(TbSagMinTime.Text, curSvData[curSel].sag.minDuration);

            curSvData[curSel].swell.bottomThreshold = changeDouble(TbSwellBottonThreshold.Text, curSvData[curSel].swell.bottomThreshold);
            curSvData[curSel].swell.topThreshold = changeDouble(TbSwellTopThreshold.Text, curSvData[curSel].swell.topThreshold);
            curSvData[curSel].swell.maxDuration = changeDouble(TbSwellMaxTime.Text, curSvData[curSel].swell.maxDuration);
            curSvData[curSel].swell.minDuration = changeDouble(TbSwellMinTime.Text, curSvData[curSel].swell.minDuration);

            curSvData[curSel].underVoltage.bottomThreshold = changeDouble(TbUnderVoltageBottomThreshold.Text, curSvData[curSel].underVoltage.bottomThreshold);
            curSvData[curSel].underVoltage.topThreshold = changeDouble(TbUnderVoltageTopThreshold.Text, curSvData[curSel].underVoltage.topThreshold);
            curSvData[curSel].underVoltage.maxDuration = changeDouble(TbUnderVoltageMaxTime.Text, curSvData[curSel].underVoltage.maxDuration);
            curSvData[curSel].underVoltage.minDuration = changeDouble(TbUnderVoltageMinTime.Text, curSvData[curSel].underVoltage.minDuration);

            curSvData[curSel].overVoltage.bottomThreshold = changeDouble(TbOverVoltageBottomThreshold.Text, curSvData[curSel].overVoltage.bottomThreshold);
            curSvData[curSel].overVoltage.topThreshold = changeDouble(TbOverVoltageTopThreshold.Text, curSvData[curSel].overVoltage.topThreshold);
            curSvData[curSel].overVoltage.maxDuration = changeDouble(TbOverVoltageMaxTime.Text, curSvData[curSel].overVoltage.maxDuration);
            curSvData[curSel].overVoltage.minDuration = changeDouble(TbOverVoltageMinTime.Text, curSvData[curSel].overVoltage.minDuration);

            curSvData[curSel].interruption.bottomThreshold = changeDouble(TbInterruptionBottonThreshold.Text, curSvData[curSel].interruption.bottomThreshold);
            curSvData[curSel].interruption.topThreshold = changeDouble(TbInterruptionTopThreshold.Text, curSvData[curSel].interruption.topThreshold);
            curSvData[curSel].interruption.maxDuration = changeDouble(TbInterruptionMaxTime.Text, curSvData[curSel].interruption.maxDuration);
            curSvData[curSel].interruption.minDuration = changeDouble(TbInterruptionMinTime.Text, curSvData[curSel].interruption.minDuration);

            curSvData[curSel].sustainedinterruption.bottomThreshold = changeDouble(TbSustainedBottomThreshold.Text, curSvData[curSel].sustainedinterruption.bottomThreshold);
            curSvData[curSel].sustainedinterruption.topThreshold = changeDouble(TbSustainedTopThreshold.Text, curSvData[curSel].sustainedinterruption.topThreshold);
            curSvData[curSel].sustainedinterruption.maxDuration = changeDouble(TbSustainedInterruptionMaxTime.Text, curSvData[curSel].sustainedinterruption.maxDuration);
            curSvData[curSel].sustainedinterruption.minDuration = changeDouble(TbSustainedInterruptionMinTime.Text, curSvData[curSel].sustainedinterruption.minDuration);

            curSvData[curSel].nominalCurrent = changeInt(TbNomCurrent.Text, curSvData[curSel].nominalCurrent);
            curSvData[curSel].nominalVoltage = changeInt(TbNomVoltage.Text, curSvData[curSel].nominalVoltage);

            // Do the oposite now
            TbSagBottonThreshold.Text = curSvData[curSel].sag.bottomThreshold.ToString();
            TbSagTopThreshold.Text = curSvData[curSel].sag.topThreshold.ToString();
            TbSagMaxTime.Text = curSvData[curSel].sag.maxDuration.ToString();
            TbSagMinTime.Text = curSvData[curSel].sag.minDuration.ToString();

            TbSwellBottonThreshold.Text = curSvData[curSel].swell.bottomThreshold.ToString();
            TbSwellTopThreshold.Text = curSvData[curSel].swell.topThreshold.ToString();
            TbSwellMaxTime.Text = curSvData[curSel].swell.maxDuration.ToString();
            TbSwellMinTime.Text = curSvData[curSel].swell.minDuration.ToString();

            TbUnderVoltageBottomThreshold.Text = curSvData[curSel].underVoltage.bottomThreshold.ToString();
            TbUnderVoltageTopThreshold.Text = curSvData[curSel].underVoltage.topThreshold.ToString();
            TbUnderVoltageMaxTime.Text = curSvData[curSel].underVoltage.maxDuration.ToString();
            TbUnderVoltageMinTime.Text = curSvData[curSel].underVoltage.minDuration.ToString();

            TbOverVoltageBottomThreshold.Text = curSvData[curSel].overVoltage.bottomThreshold.ToString();
            TbOverVoltageTopThreshold.Text = curSvData[curSel].overVoltage.topThreshold.ToString();
            TbOverVoltageMaxTime.Text = curSvData[curSel].overVoltage.maxDuration.ToString();
            TbOverVoltageMinTime.Text = curSvData[curSel].overVoltage.minDuration.ToString();

            TbInterruptionBottonThreshold.Text = curSvData[curSel].interruption.bottomThreshold.ToString();
            TbInterruptionTopThreshold.Text = curSvData[curSel].interruption.topThreshold.ToString();
            TbInterruptionMaxTime.Text = curSvData[curSel].interruption.maxDuration.ToString();
            TbInterruptionMinTime.Text = curSvData[curSel].interruption.minDuration.ToString();

            TbSustainedBottomThreshold.Text = curSvData[curSel].sustainedinterruption.bottomThreshold.ToString();
            TbSustainedTopThreshold.Text = curSvData[curSel].sustainedinterruption.topThreshold.ToString();
            TbSustainedInterruptionMaxTime.Text = curSvData[curSel].sustainedinterruption.maxDuration.ToString();
            TbSustainedInterruptionMinTime.Text = curSvData[curSel].sustainedinterruption.minDuration.ToString();

            TbNomCurrent.Text = curSvData[curSel].nominalCurrent.ToString();
            TbNomVoltage.Text = curSvData[curSel].nominalVoltage.ToString();


        }

    }
}
