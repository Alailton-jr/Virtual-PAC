using Newtonsoft.Json;
using OxyPlot.Series;
using OxyPlot;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using YamlDotNet.Serialization;
using static Quality.Classes;
using static Quality.Classes.SampledValue;
using static Quality.MainForm;
using static Quality.MonitorForm;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Quality
{
    public partial class AnalyseForm : Form
    {

        private List<CheckBox> checkBoxes;
        private List<Panel> panelList;

        private List<CheckBox> checkBoxFenQuality;
        private List<Panel> panelFenQualityList;

        private List<SampledValue> svList;
        private int curSvIdx;

        private List<CheckBox> GraphCheckBoxes;

        private List<RegisteredEvents> curEventsList;

        private SocketConnection socket;

        public AnalyseForm()
        {
            InitializeComponent();


            checkBoxes = new List<CheckBox>();
            checkBoxes.Add(CbxConfig);
            checkBoxes.Add(CbxInfo);
            checkBoxes.Add(CbxEvents);
            checkBoxes.Add(CbxCapt);

            panelList = new List<Panel>();
            panelList.Add(PnConfig);
            panelList.Add(PnInfo);
            panelList.Add(PnEvents);
            panelList.Add(PnCapt);


            checkBoxFenQuality = new List<CheckBox>();
            checkBoxFenQuality.Add(CbxVtcd);
            checkBoxFenQuality.Add(CbxVtld);
            checkBoxFenQuality.Add(CbxUnbalance);
            checkBoxFenQuality.Add(Cbxfluctuation);
            checkBoxFenQuality.Add(CbxHarm);
            checkBoxFenQuality.Add(CbxTransient);

            panelFenQualityList = new List<Panel>();
            panelFenQualityList.Add(PnVtcd);
            panelFenQualityList.Add(PnVtld);


            CbxConfig_CheckedChanged(CbxConfig, EventArgs.Empty);
            CbxVtcd_CheckedChanged(CbxVtcd, EventArgs.Empty);

            socket = mainControl.socket;

            CbxHarmo.Items.Clear();
            CbxHarmo.Items.Add("Fundamental");
            for (int i = 1; i < 40; i++)
            {
                CbxHarmo.Items.Add($"Harmônico {i + 1}");
            }
            CbxHarmo.SelectedIndex = 0;
            CbxInfoElectric_CheckedChanged(CbxInfoElectric, EventArgs.Empty);

            curEventsList = new List<RegisteredEvents>();

            duration = "1 s";

            GraphCheckBoxes = new List<CheckBox>()
            {
                CbxCn1,
                CbxCn2,
                CbxCn3,
                CbxCn4,
                CbxCn5,
                CbxCn6,
                CbxCn7,
                CbxCn8,
            };

            socket.ConnectionLost += desconnected;

            if (mainControl.sampledValues == null) mainControl.sampledValues = new List<SampledValue>();
            svList = mainControl.sampledValues;

        }

        public void extLoad()
        {
            if (mainControl.sampledValues == null) mainControl.sampledValues = new List<SampledValue>();
            svList = mainControl.sampledValues;
            CbxSv.Items.Clear();
            foreach (var sv in svList)
            {
                CbxSv.Items.Add(sv.SVID);
            }
            stopRunning();
            if (CbxSv.Items.Count > 0) CbxSv.SelectedIndex = 0;
            else CbxSv.SelectedIndex = -1;

        }

        private void CbxConfig_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox local = (CheckBox)sender;
            foreach (CheckBox cb in checkBoxes)
            {
                cb.CheckStateChanged -= new EventHandler(CbxConfig_CheckedChanged);
                cb.Checked = false;
            }
            local.Checked = true;
            foreach (CheckBox cb in checkBoxes)
            {
                cb.CheckStateChanged += new EventHandler(CbxConfig_CheckedChanged);
            }

            for (int i = 0; i < checkBoxes.Count; i++)
            {
                checkBoxes[i].CheckStateChanged -= new EventHandler(CbxConfig_CheckedChanged);
                if (checkBoxes[i] != local) checkBoxes[i].Checked = false;
                checkBoxes[i].CheckStateChanged += new EventHandler(CbxConfig_CheckedChanged);
            }
            for (int i = 0; i < checkBoxes.Count; i++)
            {
                if (checkBoxes[i] == local)
                {
                    if (!(i < panelList.Count)) return;
                    PnMenus.Controls.Add(panelList[i]);
                    panelList[i].Visible = true;
                    panelList[i].Dock = DockStyle.Fill;
                    panelList[i].BringToFront();
                }
            }
        }

        private void CbxVtcd_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox local = (CheckBox)sender;
            for (int i = 0; i < checkBoxFenQuality.Count; i++)
            {
                if (checkBoxFenQuality[i] == local)
                {
                    if (!(i < panelFenQualityList.Count)) return;
                    PnFenQuality.Controls.Add(panelFenQualityList[i]);
                    panelFenQualityList[i].Visible = true;
                    panelFenQualityList[i].Dock = DockStyle.Fill;
                    panelFenQualityList[i].BringToFront();
                }
            }
        }

        private void CbxInfoElectric_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox local = (CheckBox)sender;
            CbxInfoElectric.CheckStateChanged -= new EventHandler(CbxInfoElectric_CheckedChanged);
            CbxInfoSampledValue.CheckStateChanged -= new EventHandler(CbxInfoElectric_CheckedChanged);

            CbxInfoElectric.Checked = false;
            CbxInfoSampledValue.Checked = false;

            local.Checked = true;

            CbxInfoElectric.CheckStateChanged += new EventHandler(CbxInfoElectric_CheckedChanged);
            CbxInfoSampledValue.CheckStateChanged += new EventHandler(CbxInfoElectric_CheckedChanged);

            if (CbxInfoElectric.Checked)
            {
                PnInfo.Controls.Add(PnInfoElectric);
                PnInfoElectric.Visible = true;
                PnInfoElectric.Dock = DockStyle.Fill;
                PnInfoSv.Visible = false;
            }
            else
            {
                PnInfo.Controls.Add(PnInfoSv);
                PnInfoSv.Visible = true;
                PnInfoSv.Dock = DockStyle.Fill;
                PnInfoElectric.Visible = false;
            }
            PnInfo.Controls.SetChildIndex(PnInfoMenu, PnInfo.Controls.Count - 1);
        }

        private bool running;

        private void BtnAnalyse_Click(object sender, EventArgs e)
        {
            if (running)
            {
                stopRunning();
            }
            else
            {
                startRunning();
            }
        }

        private void startRunning()
        {
            if (!(CbxSv.SelectedIndex >= 0)) return;
            if (!(CbxSv.SelectedIndex < svList.Count)) return;

            string yamlFile = mainControl.CreateYamlSampledValue();
            //File.WriteAllText("temp.yaml", yamlFile);
            //var res = socket.SendData("ReceiveMonitorSetup", yamlFile);
            var res = socket.SendData(SocketConnection.entryType.ANALYSER_SETUP, yamlFile);
            if (res == "success")
            {
                //res = socket.SendData("netMonitorStart", "");
                res = socket.SendData(SocketConnection.entryType.ANALYSER_START, "");

                if (res == "success")
                {
                    running = true;
                    BtnAnalyse.Text = "Parar Análise";
                    updateEventsFields(true);
                    TimerAnalyse.Start();
                }
            }
        }

        private void stopRunning()
        {
            if (running)
            {
               //socket.SendData("netMonitorStop", "");
               socket.SendData(SocketConnection.entryType.ANALYSER_STOP, "");

            }
            running = false;
            recording = false;
            BtnAnalyse.Text = "Iniciar Análise";
            TimerAnalyse.Stop();
            updateConfigFields();
            updateInfoFields();
            updateEventsFields();
        }

        private void desconnected(object? sender, EventArgs e)
        {
            running = false;
            BtnAnalyse.Text = "Iniciar Análise";
            TimerAnalyse.Stop();

            recording = false;
            BtnRecord.Text = "Iniciar Gravação";
            TimerRecording.Stop();
        }

        private void serverDataRoutine()
        {
            try
            {
                // Electric Info
                //var res = socket.ReceiveFile("getMonitorSvInfo", svList[curSvIdx].SVID);
                var res = socket.ReceiveFile(SocketConnection.entryType.ANALYSER_DATA, svList[curSvIdx].SVID);
                if (res != null && res.Length > 0)
                {
                    string resStr = System.Text.Encoding.UTF8.GetString(res);

                    // Make a sumary of the file:
                    // - SV Found -> Line 0
                    // - Flags -> Line 1
                    // - RMS [8] -> Line 2
                    // - Power Values Fundamental Only [3] -> Line 3
                    // - Harmonics [8][40][2] -> Line 4
                    // - Symetrical Component [2][3][2] -> Line 5
                    // - Unbalance [2] -> Line 6

                    // - SV Found -> Line 0
                    var lines = resStr.Trim().Split('\n');
                    svList[curSvIdx].running = lines[0].Trim() == "1";

                    // - Flags -> Line 1
                    var flagLines = lines[1].Trim().Split('|');
                    svList[curSvIdx].sag.flag = flagLines[0] == "1";
                    svList[curSvIdx].swell.flag = flagLines[1] == "1";
                    svList[curSvIdx].interruption.flag = flagLines[2] == "1";
                    svList[curSvIdx].overVoltage.flag = flagLines[3] == "1";
                    svList[curSvIdx].underVoltage.flag = flagLines[4] == "1";
                    svList[curSvIdx].sustainedinterruption.flag = flagLines[4] == "1";

                    // - RMS [8] -> Line 2
                    var rmsLines = lines[2].Trim().Split('|');
                    svList[curSvIdx].generalInfo.Rms = new double[8];
                    for (int i = 0; i < svList[curSvIdx].generalInfo.Rms.Length; i++)
                    {
                        svList[curSvIdx].generalInfo.Rms[i] = double.Parse(rmsLines[i]);
                    }

                    // - Power Values Fundamental Only [3] -> Line 3
                    var powerLines = lines[3].Trim().Split('|');
                    svList[curSvIdx].generalInfo.power = new double[3];
                    for (int i = 0; i < svList[curSvIdx].generalInfo.power.Length; i++)
                    {
                        svList[curSvIdx].generalInfo.power[i] = double.Parse(powerLines[i]);
                    }

                    // - Harmonics [8][40][2] -> Line 4
                    var phasorLines = lines[4].Trim().Split('|');
                    svList[curSvIdx].generalInfo.PhasorPolar = new double[8][][];
                    for (int i = 0; i < 8; i++)
                    {
                        svList[curSvIdx].generalInfo.PhasorPolar[i] = new double[40][];
                        for (int j = 0; j < 40; j++)
                        {
                            svList[curSvIdx].generalInfo.PhasorPolar[i][j] = new double[2];
                            var xdebug = phasorLines[j + i * 40];
                            var phasor = phasorLines[j + i*40].Trim().Split(';');
                            svList[curSvIdx].generalInfo.PhasorPolar[i][j][0] = double.Parse(phasor[0]);
                            svList[curSvIdx].generalInfo.PhasorPolar[i][j][1] = double.Parse(phasor[1]);
                        }
                    }

                    // - Symetrical Component [2][3][2] -> Line 5
                    svList[curSvIdx].generalInfo.Symmetrical = new double[2][][];
                    var symLines = lines[5].Trim().Split('|');
                    svList[curSvIdx].generalInfo.Symmetrical[0] = new double[3][];
                    svList[curSvIdx].generalInfo.Symmetrical[1] = new double[3][];
                    for (int j = 0; j < 3; j++)
                    {
                        svList[curSvIdx].generalInfo.Symmetrical[0][j] = new double[2];
                        var symI = symLines[j].Trim().Split(';');
                        svList[curSvIdx].generalInfo.Symmetrical[0][j][0] = double.Parse(symI[0]);
                        svList[curSvIdx].generalInfo.Symmetrical[0][j][1] = double.Parse(symI[1]);

                        svList[curSvIdx].generalInfo.Symmetrical[1][j] = new double[2];
                        var symV = symLines[j + 3].Trim().Split(';');
                        svList[curSvIdx].generalInfo.Symmetrical[1][j][0] = double.Parse(symV[0]);
                        svList[curSvIdx].generalInfo.Symmetrical[1][j][1] = double.Parse(symV[1]);
                    }

                    // - Unbalance [2] -> Line 6
                    svList[curSvIdx].generalInfo.Unbalance = new double[2];
                    var unbalanceLines = lines[6].Trim().Split('|');
                    svList[curSvIdx].generalInfo.Unbalance[0] = double.Parse(unbalanceLines[0]);
                    svList[curSvIdx].generalInfo.Unbalance[1] = double.Parse(unbalanceLines[1]);

                    

                    //var events = JsonConvert.DeserializeObject<SampledValue.JsonSvServerInfo>(resStr);
                    //if (events == null) return;

                    //svList[curSvIdx].running = events.Found;
                    //svList[curSvIdx].sag.flag = events.flags.Sag > 0;
                    //svList[curSvIdx].swell.flag = events.flags.Swell > 0;
                    //svList[curSvIdx].interruption.flag = events.flags.Interruption > 0;
                    //svList[curSvIdx].overVoltage.flag = events.flags.OverVoltage > 0;
                    //svList[curSvIdx].underVoltage.flag = events.flags.UnderVoltage > 0;

                    //svList[curSvIdx].generalInfo.Rms = events.Rms;
                    //svList[curSvIdx].generalInfo.PhasorPolar = events.Phasor;
                    //svList[curSvIdx].generalInfo.Symmetrical = events.Symmetrical;
                    //svList[curSvIdx].generalInfo.Unbalance = events.Unbalance;
                    //updateConfigFields();
                    updateInfoFields();
                }
                else
                {
                    stopRunning();
                    return;
                }

                //Events
                //res = socket.ReceiveFile("getRegistedEvents", svList[curSvIdx].SVID);
                res = socket.ReceiveFile(SocketConnection.entryType.ANALYSER_EVENT_INFO, svList[curSvIdx].SVID);
                if (res != null)
                {
                    if (res.Length == 0) return;
                    string resStr = System.Text.Encoding.UTF8.GetString(res);

                    var events = resStr.Split('\n');
                    //var events = JsonConvert.DeserializeObject<List<SampledValue.JsonServerRegistedEvents>>(resStr);

                    svList[curSvIdx].sag.RegisteredEvents.Clear();
                    svList[curSvIdx].swell.RegisteredEvents.Clear();
                    svList[curSvIdx].interruption.RegisteredEvents.Clear();


                    curEventsList = new List<RegisteredEvents>();
                    for (int i = 0; i < events.Length; i++)
                    {
                        var ev = events[i].Trim().Split('|');
                        if (ev.Length < 3) continue;
                        var revent = new RegisteredEvents();
                        revent.date = DateTime.Parse(ev[1]);
                        revent.duration = double.Parse(ev[2]);
                        //revent.name = ev[3];
                        revent.eventType = ev[0].Trim() == "sag" ? SampledValue.EventTypes.sag :
                            ev[0] == "swell" ? SampledValue.EventTypes.swell :
                            SampledValue.EventTypes.interruption;

                        if (revent.eventType == EventTypes.sag)
                        {
                            revent.magnitude = double.Parse(ev[3]) / svList[curSvIdx].nominalVoltage;
                            svList[curSvIdx].sag.RegisteredEvents.Add(revent);
                        }
                        else if (revent.eventType == EventTypes.swell)
                        {
                            revent.magnitude = double.Parse(ev[4]) / svList[curSvIdx].nominalVoltage;
                            svList[curSvIdx].swell.RegisteredEvents.Add(revent);
                        }
                        else if (revent.eventType == EventTypes.interruption)
                        {
                            svList[curSvIdx].interruption.RegisteredEvents.Add(revent);
                        }
                        curEventsList.Add(revent);
                    }
                    updateEventsFields();
                }
                else
                {
                    stopRunning();
                    return;
                }
            }
            catch { }
        }

        private void TimerAnalyse_Tick(object sender, EventArgs e)
        {
            if (running)
            {
                serverDataRoutine();
            }
            else
            {
                TimerAnalyse.Stop();
            }
        }

        private void updateConfigFields()
        {
            if (svList == null) return;
            if (!(curSvIdx < 0 || curSvIdx >= svList.Count))
            {
                #region Fields
                TbFreq.Text = svList[curSvIdx].frequency.ToString();
                TbMacDst.Text = svList[curSvIdx].macDst;
                TbSvID.Text = svList[curSvIdx].SVID;
                TbNAsdu.Text = svList[curSvIdx].noAsdu.ToString();
                TbNChannels.Text = svList[curSvIdx].noChannels.ToString();
                TbSmpRate.Text = svList[curSvIdx].smpRate.ToString();
                TbVLANID.Text = svList[curSvIdx].vLANID.ToString();
                TbVLANPriority.Text = svList[curSvIdx].vLANPriority.ToString();

                TbUnderVoltageBottomThreshold.Text = svList[curSvIdx].underVoltage.bottomThreshold.ToString();
                TbUnderVoltageTopThreshold.Text = svList[curSvIdx].underVoltage.topThreshold.ToString();
                TbUnderVoltageMaxTime.Text = svList[curSvIdx].underVoltage.maxDuration.ToString();
                TbUnderVoltageMinTime.Text = svList[curSvIdx].underVoltage.minDuration.ToString();

                TbOverVoltageBottomThreshold.Text = svList[curSvIdx].overVoltage.bottomThreshold.ToString();
                TbOverVoltageTopThreshold.Text = svList[curSvIdx].overVoltage.topThreshold.ToString();
                TbOverVoltageMaxTime.Text = svList[curSvIdx].overVoltage.maxDuration.ToString();
                TbOverVoltageMinTime.Text = svList[curSvIdx].overVoltage.minDuration.ToString();

                TbSagBottonThreshold.Text = svList[curSvIdx].sag.bottomThreshold.ToString();
                TbSagTopThreshold.Text = svList[curSvIdx].sag.topThreshold.ToString();
                TbSagMaxTime.Text = svList[curSvIdx].sag.maxDuration.ToString();
                TbSagMinTime.Text = svList[curSvIdx].sag.minDuration.ToString();

                TbSwellBottonThreshold.Text = svList[curSvIdx].swell.bottomThreshold.ToString();
                TbSwellTopThreshold.Text = svList[curSvIdx].swell.topThreshold.ToString();
                TbSwellMaxTime.Text = svList[curSvIdx].swell.maxDuration.ToString();
                TbSwellMinTime.Text = svList[curSvIdx].swell.minDuration.ToString();

                TbInterruptionBottonThreshold.Text = svList[curSvIdx].interruption.bottomThreshold.ToString();
                TbInterruptionTopThreshold.Text = svList[curSvIdx].interruption.topThreshold.ToString();
                TbInterruptionMaxTime.Text = svList[curSvIdx].interruption.maxDuration.ToString();
                TbInterruptionMinTime.Text = svList[curSvIdx].interruption.minDuration.ToString();

                TbSustainedBottomThreshold.Text = svList[curSvIdx].sustainedinterruption.bottomThreshold.ToString();
                TbSustainedTopThreshold.Text = svList[curSvIdx].sustainedinterruption.topThreshold.ToString();
                TbSustainedInterruptionMaxTime.Text = svList[curSvIdx].sustainedinterruption.maxDuration.ToString();
                TbSustainedInterruptionMinTime.Text = svList[curSvIdx].sustainedinterruption.minDuration.ToString();

                TbNomCurrent.Text = svList[curSvIdx].nominalCurrent.ToString();
                TbNomVoltage.Text = svList[curSvIdx].nominalVoltage.ToString();
                TbNomFreq.Text = svList[curSvIdx].frequency.ToString();
                #endregion
            }
            else
            {
                // Empy all fiels
                TbFreq.Text = "";
                TbMacDst.Text = "";
                TbSvID.Text = "";
                TbNAsdu.Text = "";
                TbNChannels.Text = "";
                TbSmpRate.Text = "";
                TbVLANID.Text = "";
                TbVLANPriority.Text = "";

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

                TbNomCurrent.Text = "";
                TbNomVoltage.Text = "";
                TbNomFreq.Text = "";

            }



        }

        private void updateInfoFields()
        {
            try
            {
                if (running)
                {
                    #region Fundamental
                    if (svList[curSvIdx].generalInfo == null) return;
                    if (svList[curSvIdx].generalInfo.PhasorPolar == null) return;

                    TbPotP.Text = svList[curSvIdx].generalInfo.power[0].ToString("0.00");
                    TbPotQ.Text = svList[curSvIdx].generalInfo.power[1].ToString("0.00");
                    TbPotS.Text = svList[curSvIdx].generalInfo.power[2].ToString("0.00");

                    
                    TbGenIaMod.Text = svList[curSvIdx].generalInfo.PhasorPolar[0][1][0].ToString("0.00");
                    TbGenIaAng.Text = svList[curSvIdx].generalInfo.PhasorPolar[0][1][1].ToString("0.00");
                    TbGenIbMod.Text = svList[curSvIdx].generalInfo.PhasorPolar[1][1][0].ToString("0.00");
                    TbGenIbAng.Text = svList[curSvIdx].generalInfo.PhasorPolar[1][1][1].ToString("0.00");
                    TbGenIcMod.Text = svList[curSvIdx].generalInfo.PhasorPolar[2][1][0].ToString("0.00");
                    TbGenIcAng.Text = svList[curSvIdx].generalInfo.PhasorPolar[2][1][1].ToString("0.00");
                    TbGenInMod.Text = svList[curSvIdx].generalInfo.PhasorPolar[3][1][0].ToString("0.00");
                    TbGenInAng.Text = svList[curSvIdx].generalInfo.PhasorPolar[3][1][1].ToString("0.00");
                    TbGenVaMod.Text = svList[curSvIdx].generalInfo.PhasorPolar[4][1][0].ToString("0.00");
                    TbGenVaAng.Text = svList[curSvIdx].generalInfo.PhasorPolar[4][1][1].ToString("0.00");
                    TbGenVbMod.Text = svList[curSvIdx].generalInfo.PhasorPolar[5][1][0].ToString("0.00");
                    TbGenVbAng.Text = svList[curSvIdx].generalInfo.PhasorPolar[5][1][1].ToString("0.00");
                    TbGenVcMod.Text = svList[curSvIdx].generalInfo.PhasorPolar[6][1][0].ToString("0.00");
                    TbGenVcAng.Text = svList[curSvIdx].generalInfo.PhasorPolar[6][1][1].ToString("0.00");
                    TbGenVnMod.Text = svList[curSvIdx].generalInfo.PhasorPolar[7][1][0].ToString("0.00");
                    TbGenVnAng.Text = svList[curSvIdx].generalInfo.PhasorPolar[7][1][1].ToString("0.00");

                    #endregion

                    #region Harmonics
                    int harmIdx = CbxHarmo.SelectedIndex;
                    if (harmIdx < 0 || harmIdx >= 39) harmIdx = 0;

                    TbHarmIa.Text = svList[curSvIdx].generalInfo.PhasorPolar[0][harmIdx + 1][0].ToString("0.00");
                    TbHarmIaAng.Text = svList[curSvIdx].generalInfo.PhasorPolar[0][harmIdx + 1][1].ToString("0.00");
                    TbHarmIb.Text = svList[curSvIdx].generalInfo.PhasorPolar[1][harmIdx + 1][0].ToString("0.00");
                    TbHarmIbAng.Text = svList[curSvIdx].generalInfo.PhasorPolar[1][harmIdx + 1][1].ToString("0.00");
                    TbHarmIc.Text = svList[curSvIdx].generalInfo.PhasorPolar[2][harmIdx + 1][0].ToString("0.00");
                    TbHarmIcAng.Text = svList[curSvIdx].generalInfo.PhasorPolar[2][harmIdx + 1][1].ToString("0.00");
                    TbHarmIn.Text = svList[curSvIdx].generalInfo.PhasorPolar[3][harmIdx + 1][0].ToString("0.00");
                    TbHarmInAng.Text = svList[curSvIdx].generalInfo.PhasorPolar[3][harmIdx + 1][1].ToString("0.00");
                    TbHarmVa.Text = svList[curSvIdx].generalInfo.PhasorPolar[4][harmIdx + 1][0].ToString("0.00");
                    TbHarmVaAng.Text = svList[curSvIdx].generalInfo.PhasorPolar[4][harmIdx + 1][1].ToString("0.00");
                    TbHarmVb.Text = svList[curSvIdx].generalInfo.PhasorPolar[5][harmIdx + 1][0].ToString("0.00");
                    TbHarmVbAng.Text = svList[curSvIdx].generalInfo.PhasorPolar[5][harmIdx + 1][1].ToString("0.00");
                    TbHarmVc.Text = svList[curSvIdx].generalInfo.PhasorPolar[6][harmIdx + 1][0].ToString("0.00");
                    TbHarmVcAng.Text = svList[curSvIdx].generalInfo.PhasorPolar[6][harmIdx + 1][1].ToString("0.00");
                    TbHarmVn.Text = svList[curSvIdx].generalInfo.PhasorPolar[7][harmIdx + 1][0].ToString("0.00");
                    TbHarmVnAng.Text = svList[curSvIdx].generalInfo.PhasorPolar[7][harmIdx + 1][1].ToString("0.00");
                    #endregion

                    #region CompSym
                    TbSymI0.Text = svList[curSvIdx].generalInfo.Symmetrical[0][0][0].ToString("0.00");
                    TbSymI0Ang.Text = svList[curSvIdx].generalInfo.Symmetrical[0][0][1].ToString("0.00");
                    TbSymI1.Text = svList[curSvIdx].generalInfo.Symmetrical[0][1][0].ToString("0.00");
                    TbSymI1Ang.Text = svList[curSvIdx].generalInfo.Symmetrical[0][1][1].ToString("0.00");
                    TbSymI2.Text = svList[curSvIdx].generalInfo.Symmetrical[0][2][0].ToString("0.00");
                    TbSymI2Ang.Text = svList[curSvIdx].generalInfo.Symmetrical[0][2][1].ToString("0.00");
                    TbSymV0.Text = svList[curSvIdx].generalInfo.Symmetrical[1][0][0].ToString("0.00");
                    TbSymV0Ang.Text = svList[curSvIdx].generalInfo.Symmetrical[1][0][1].ToString("0.00");
                    TbSymV1.Text = svList[curSvIdx].generalInfo.Symmetrical[1][1][0].ToString("0.00");
                    TbSymV1Ang.Text = svList[curSvIdx].generalInfo.Symmetrical[1][1][1].ToString("0.00");
                    TbSymV2.Text = svList[curSvIdx].generalInfo.Symmetrical[1][2][0].ToString("0.00");
                    TbSymV2Ang.Text = svList[curSvIdx].generalInfo.Symmetrical[1][2][1].ToString("0.00");
                    
                    TbSymIUnbalance.Text = svList[curSvIdx].generalInfo.Unbalance[0].ToString("0.00") + "%";
                    TbSymVUnbalance.Text = svList[curSvIdx].generalInfo.Unbalance[1].ToString("0.00") + "%";

                    #endregion
                }
                else
                {
                    // Every Fields receives ""
                    #region Fundamental
                    TbGenIaMod.Text = "";
                    TbGenIaMod.Text = "";
                    TbGenIbMod.Text = "";
                    TbGenIbMod.Text = "";
                    TbGenIcMod.Text = "";
                    TbGenIcMod.Text = "";
                    TbGenInMod.Text = "";
                    TbGenInMod.Text = "";
                    TbGenVaMod.Text = "";
                    TbGenVaMod.Text = "";
                    TbGenVbMod.Text = "";
                    TbGenVbMod.Text = "";
                    TbGenVcMod.Text = "";
                    TbGenVcMod.Text = "";
                    TbGenVnMod.Text = "";
                    TbGenVnMod.Text = "";
                    #endregion
                    #region Harmonics
                    TbHarmIa.Text = "";
                    TbHarmIaAng.Text = "";
                    TbHarmIb.Text = "";
                    TbHarmIbAng.Text = "";
                    TbHarmIc.Text = "";
                    TbHarmIcAng.Text = "";
                    TbHarmIn.Text = "";
                    TbHarmInAng.Text = "";
                    TbHarmVa.Text = "";
                    TbHarmVaAng.Text = "";
                    TbHarmVb.Text = "";
                    TbHarmVbAng.Text = "";
                    TbHarmVc.Text = "";
                    TbHarmVcAng.Text = "";
                    TbHarmVn.Text = "";
                    TbHarmVnAng.Text = "";
                    #endregion
                    #region CompSym
                    TbSymI0.Text = "";
                    TbSymI0Ang.Text = "";
                    TbSymI1.Text = "";
                    TbSymI1Ang.Text = "";
                    TbSymI2.Text = "";
                    TbSymI2Ang.Text = "";
                    TbSymV0.Text = "";
                    TbSymV0Ang.Text = "";
                    TbSymV1.Text = "";
                    TbSymV1Ang.Text = "";
                    TbSymV2.Text = "";
                    TbSymV2Ang.Text = "";
                    #endregion
                }
            }
            catch { }
        }

        private void BtnDownloadClicked(object sender, EventArgs e)
        {
            Button local = (Button)sender;
            for (int i_bnt = 0; i_bnt < tlpButtons.Count; i_bnt++)
            {
                if (tlpButtons[i_bnt][0] == local)
                {
                    //byte[] waveFormBytes = socket.ReceiveFile("getMonitorWaveForm", TlpLabels[i_bnt][0].Text);
                    byte[] waveFormBytes = socket.ReceiveFile(SocketConnection.entryType.NONE, TlpLabels[i_bnt][0].Text);
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
                            showData.OpenExt(waveForm, TlpLabels[i_bnt][0].Text, double.Parse(TlpLabels[i_bnt][2].Text));
                        }
                        return;
                    }
                    catch { }
                }
            }
        }

        List<List<Label>> TlpLabels = new List<List<Label>>();
        List<List<Button>> tlpButtons = new List<List<Button>>();
        private void updateEventsFields(bool clear = false)
        {

            if (clear)
            {
                TlpEvents.Controls.Clear();
                TlpEvents.Controls.Add(LbEventName);
                TlpEvents.Controls.Add(LbEventType);
                TlpEvents.Controls.Add(LbEventDuration);
                TlpEvents.Controls.Add(LbEventMagnitude);
                TlpEvents.Controls.Add(LbEventBaixar);
                TlpEvents.Controls.Add(LbEventConfirm);
                TlpLabels = new List<List<Label>>();
                tlpButtons = new List<List<Button>>();
                TlpEvents.RowCount = 1;
                return;
            }

            TlpEvents.GrowStyle = TableLayoutPanelGrowStyle.AddRows;
            TlpEvents.ColumnCount = 6;

            while (TlpEvents.RowCount < curEventsList.Count + 1)
            {
                int i = 0;
                Label LbEventName = new Label() { Text = curEventsList[i].name };
                Label LbEventType = new Label() { Text = curEventsList[i].eventType.ToString() };
                Label LbEventDuration = new Label() { Text = curEventsList[i].duration.ToString() };
                Label LbEventMagnitude = new Label() { Text = curEventsList[i].magnitude.ToString() };

                Button LbEventBaixar = new Button() { Text = "" };
                LbEventBaixar.Click += new EventHandler(BtnDownloadClicked);
                Button LbEventConfirm = new Button() { Text = "" };


                TlpEvents.Controls.Add(LbEventName);
                TlpEvents.Controls.Add(LbEventType);
                TlpEvents.Controls.Add(LbEventDuration);
                TlpEvents.Controls.Add(LbEventMagnitude);
                TlpEvents.Controls.Add(LbEventBaixar);
                TlpEvents.Controls.Add(LbEventConfirm);
                TlpLabels.Add(new List<Label>() { LbEventName, LbEventType, LbEventDuration, LbEventMagnitude });
                tlpButtons.Add(new List<Button>() { LbEventBaixar, LbEventConfirm });

                foreach (Label lb in TlpLabels.Last())
                {
                    lb.Dock = DockStyle.Fill;
                    lb.TextAlign = ContentAlignment.MiddleCenter;
                    lb.BackColor = Color.FromArgb(31, 45, 56);
                }
                foreach (Button bt in tlpButtons.Last())
                {
                    bt.Dock = DockStyle.Fill;
                    bt.TextAlign = ContentAlignment.MiddleCenter;
                    bt.BackColor = Color.FromArgb(31, 45, 56);
                }

                TlpEvents.RowCount += 1;
            }
            for (int i = 0; i < curEventsList.Count - 1; i++)
            {
                TlpLabels[i][0].Text = curEventsList[i].name;
                TlpLabels[i][1].Text = curEventsList[i].eventType.ToString();
                TlpLabels[i][2].Text = curEventsList[i].duration.ToString();
                TlpLabels[i][3].Text = curEventsList[i].magnitude.ToString();
            }
            foreach (RowStyle rowStyle in TlpEvents.RowStyles)
            {
                rowStyle.SizeType = SizeType.AutoSize;
            }
        }

        private void CbxSv_SelectedIndexChanged(object sender, EventArgs e)
        {
            curSvIdx = CbxSv.SelectedIndex;
            updateConfigFields();
            updateInfoFields();
            updateEventsFields();
        }

        private void CbxHarmo_SelectedIndexChanged(object sender, EventArgs e)
        {
            updateConfigFields();
            updateInfoFields();
        }

        private bool recording;
        private void BtnRecord_Click(object sender, EventArgs e)
        {
            if (running)
            {
                stopRunning();
            }

            if (recording)
            {
                StopRecord();
            }
            else
            {
                StartRecord();
            }

        }

        private double _duration;
        private string duration
        {
            get
            {
                return _duration.ToString("0.000") + " s";
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

        private void StartRecord()
        {

            string res = socket.SendData(SocketConnection.entryType.CAPT_ESPECIFC_SV, _duration.ToString() + "|" + svList[curSvIdx].SVID);
            if (res != null && res == "success")
            {
                BtnRecord.Text = "Parar Gravação";
                recording = true;
                BtnAnalyse.Enabled = false;
                TimerRecording.Start();
                LbLoading.Visible = true;
            }
        }

        private void StopRecord()
        {
            recording = false;
            BtnRecord.Text = "Iniciar Gravação";
            TimerRecording.Stop();
            BtnAnalyse.Enabled = true;
            LbLoading.Visible = false;
            socket.SendData(SocketConnection.entryType.CAPT_SV_STOP, "");
            getRecordingResults();
        }

        List<List<double>> CurCaptData;
        private void updateGraph()
        {
            var model = new PlotModel
            {
                Title = "Captura SV",
                TitleColor = OxyColor.FromRgb(255, 255, 255),
            };

            for (int i = 0; i < CurCaptData.Count; i++)
            {
                if (!GraphCheckBoxes[i].Checked) continue;
                var lineSeries = new LineSeries();
                lineSeries.Title = $"Canal {i + 1}";
                for (int j = 0; j < CurCaptData[i].Count; j++)
                {
                    lineSeries.Points.Add(new DataPoint(j, CurCaptData[i][j]));
                }
                model.Series.Add(lineSeries);
            }
            // Customize plot appearance
            model.DefaultColors = OxyPalettes.Jet(CurCaptData.Count).Colors;

            // Customize plot axes
            model.Axes.Add(new OxyPlot.Axes.LinearAxis
            {
                Position = OxyPlot.Axes.AxisPosition.Bottom,
                Title = "N Sample",
                TitleColor = OxyColor.FromRgb(255, 255, 255),
                TextColor = OxyColor.FromRgb(255, 255, 255),
                MajorGridlineStyle = LineStyle.Solid,
            });
            model.Axes.Add(new OxyPlot.Axes.LinearAxis
            {
                Position = OxyPlot.Axes.AxisPosition.Left,
                Title = "Valor",
                MajorGridlineStyle = LineStyle.Solid,
                TitleColor = OxyColor.FromRgb(255, 255, 255),
                TextColor = OxyColor.FromRgb(255, 255, 255),
            });

            PltData.Model = model;
        }

        private void getRecordingResults()
        {
            //byte[] waveFormBytes = socket.ReceiveFile("netCaptureWaveForm", svList[curSvIdx].SVID);
            byte[] waveFormBytes = socket.ReceiveFile(SocketConnection.entryType.CAPT_SV_WAVEFORM, svList[curSvIdx].SVID);
            if(waveFormBytes != null && waveFormBytes.Length <= 0) return;
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
                    CurCaptData = waveForm;
                    updateGraph();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading file from Server, try capturing again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TimerRecording_Tick(object sender, EventArgs e)
        {
            if (recording)
            {
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
                            pBarSearch.Value = (int)(time / _duration * 100);
                        }
                        catch { }

                    }
                    else if (resLines[0] == "0")
                    {
                        pBarSearch.Value = 100;
                        StopRecord();
                    }
                }
            }
            else
            {
                TimerRecording.Stop();
            }
        }

        private void TbDuration_Validated(object sender, EventArgs e)
        {
            duration = TbDuration.Text;
        }

        private void CbxCn1_CheckedChanged(object sender, EventArgs e)
        {
            updateGraph();
        }
    }
}
