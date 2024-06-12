using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using static Quality.Classes;
using static Quality.MainForm;



namespace Quality
{
    public partial class ProdistForm : Form
    {

        List<CheckBox> cbxList;
        private ProdistFormPanels panels;
        private SocketConnection socket;

        private List<SampledValue> svList;

        private int curSvIdx;

        private bool running;
        private bool _serverConnected;

        private BackgroundWorker serverCheckWorker;

        private int noDownloadedCapt;
        private int noCaptured;
        List<ProdistData> prodistDataList;

        private double maxSmp;

        private double nlp;
        private double nlc;
        private double drp;
        private double drc;


        public bool serverConnected
        {
            get { return _serverConnected; }
            set
            {
                // Use invoke vo updateVarVoltButtons
                if (InvokeRequired)
                {
                    Invoke(new Action(() => serverConnected = value));
                    return;
                }
                else
                {
                    updateVarVoltButtons();
                }
                _serverConnected = value;
                if (panels == null) return;
                if (value)
                {
                    // TODO: Start a thread to check connect here
                    panels.BtnVarVoltConnect.Enabled = false;
                    panels.BtnVarVoltConnect.Text = "Connected";
                    if (!serverCheckWorker.IsBusy)
                    {
                        serverCheckWorker.RunWorkerAsync();
                    }


                }
                else
                {
                    panels.BtnVarVoltConnect.Enabled = true;
                    panels.BtnVarVoltConnect.Text = "Connect";
                    panels.BtnVarVolStart.Enabled = false;
                    panels.BtnVarVoltStop.Enabled = false;
                }
            }
        }

        public ProdistForm()
        {
            InitializeComponent();

            cbxList = new List<CheckBox>()
            {
                CbxVarVolt,
            };

            socket = mainControl.socket;
            serverConnected = false;

            panels = new ProdistFormPanels();
            panels.BtnVarVoltConnect.Click += Connect2Server;
            panels.BtnVarVolStart.Click += BtnVarVoltStart_Click;
            panels.BtnVarVoltStop.Click += BtnVarVoltStop_Click;

            serverCheckWorker = new BackgroundWorker();
            serverCheckWorker.DoWork += serverCheckWorker_DoWork;

            panels.BtnVarVoltSaveCapt.Click += callDebug;

            serverConnected = false;

            fillCheckBoxSV();

            prodistDataList = new List<ProdistData>();

            CbxVarVolt_CheckStateChanged(CbxVarVolt, null);

            panels.TbVarVoltCritcHigh.Validated += VarVolLimsChanged;
            panels.TbVarVoltCritcLow.Validated += VarVolLimsChanged;
            panels.TbVarVoltPrecHigh.Validated += VarVolLimsChanged;
            panels.TbVarVoltPrecLow.Validated += VarVolLimsChanged;

            panels.CbxVarVoltSVID.SelectedIndexChanged += CbxSVIDChanged;

            svList = mainControl.sampledValues;

        }

        void callDebug(object sender, EventArgs e)
        {
            checkNoCaptured();
        }

        public void openExt()
        {
            fillCheckBoxSV();
        }

        private void changeEnables(bool run)
        {
            panels.BtnVarVolStart.Enabled = !run;
            panels.BtnVarVoltStop.Enabled = run;
            panels.TbVarVoltInterval.Enabled = !run;
            panels.TbVarVoltNoSample.Enabled = !run;
        }

        private void fillCheckBoxSV()
        {
            panels.CbxVarVoltSVID.Items.Clear();
            foreach (var sv in mainControl.sampledValues)
            {
                panels.CbxVarVoltSVID.Items.Add(sv.SVID);
            }
            if (panels.CbxVarVoltSVID.Items.Count > 0)
            {
                panels.CbxVarVoltSVID.SelectedIndex = 0;
            }
        }

        private void CbxVarVolt_CheckStateChanged(object sender, EventArgs e)
        {
            var local = sender as CheckBox;

            if (!local.Checked)
            {
                local.CheckStateChanged -= CbxVarVolt_CheckStateChanged;
                local.Checked = true;
                local.CheckStateChanged += CbxVarVolt_CheckStateChanged;
            }

            foreach (var cbx in cbxList)
            {
                if (cbx != local)
                {
                    cbx.CheckStateChanged -= CbxVarVolt_CheckStateChanged;
                    cbx.Checked = false;
                    cbx.CheckStateChanged += CbxVarVolt_CheckStateChanged;
                }
            }

            if (local == CbxVarVolt)
            {
                PnMain.Controls.Add(panels.PnVarVoltage);
                panels.PnVarVoltage.Dock = DockStyle.Fill;
                panels.PnVarVoltage.BringToFront();
                panels.PnVarVoltage.Visible = true;
            }


        }

        private void serverCheckWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (_serverConnected)
            {
                if (serverConnected)
                {
                    checkNoCaptured();
                    Connect2Server(null, null);
                }
                System.Threading.Thread.Sleep(1000);
            }
        }

        private void Connect2Server(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => Connect2Server(null, null)));
                return;
            }

            var res = socket.SendData(SocketConnection.entryType.PRODIST_STATUS, "");

            if (res != null)
            {
                string[] resLines = res.Split("|");
                if (resLines[0] == "1")
                {
                    serverConnected = true;
                    running = true;
                    changeEnables(true);
                }
                else if (resLines[0] == "0")
                {
                    serverConnected = true;
                    running = false;
                    changeEnables(false);
                    panels.BtnVarVolStart.Enabled = true;
                    panels.BtnVarVoltStop.Enabled = false;
                }
                else
                {
                    serverConnected = false;
                    changeEnables(false);
                    panels.BtnVarVolStart.Enabled = false;
                }
            }
            else
            {
                serverConnected = false;
                changeEnables(false);
                panels.BtnVarVolStart.Enabled = false;
            }
        }

        private void BtnVarVoltStart_Click(object sender, EventArgs e)
        {
            if (serverConnected)
            {
                byte[] bytesArr = new byte[mainControl.sampledValues.Count * (128 + 4 + 4) + 4];
                Array.Copy(BitConverter.GetBytes(mainControl.sampledValues.Count), 0, bytesArr, 0, 4);
                for (int i = 0; i < mainControl.sampledValues.Count; i++)
                {
                    int len = mainControl.sampledValues[i].SVID.Length;
                    Array.Copy(Encoding.ASCII.GetBytes(mainControl.sampledValues[i].SVID), 0, bytesArr, i * (128 + 4 + 4) + 4, len);
                    Array.Copy(BitConverter.GetBytes(mainControl.sampledValues[i].noChannels), 0, bytesArr, i * (128 + 4 + 4) + 128 + 4, 4);
                    Array.Copy(BitConverter.GetBytes(mainControl.sampledValues[i].smpRate), 0, bytesArr, i * (128 + 4 + 4) + 128 + 4 + 4, 4);
                }

                var res = socket.SendByte(SocketConnection.entryType.PRODIST_SETUP, bytesArr);
                if (res == "success")
                {
                    var sendString = mainControl.sampledValues.Count.ToString() + "|" + panels.TbVarVoltInterval.Text + "|" + panels.TbVarVoltNoSample.Text;
                    res = socket.SendData(SocketConnection.entryType.PRODIST_START, sendString);
                    if (res == "success")
                    {
                        running = true;
                        panels.BtnVarVolStart.Enabled = false;
                        panels.BtnVarVoltStop.Enabled = true;
                        maxSmp = double.Parse(panels.TbVarVoltNoSample.Text);
                        clearCapt();
                    }
                }
            }
        }

        private void updateTextBoxLims()
        {
            if (svList[curSvIdx].prodistInfo.critLims[0] == 0)
            {
                svList[curSvIdx].prodistInfo.setNormalLimist(svList[curSvIdx].nominalVoltage);
            }

            panels.TbVarVoltCritcLow.Text = svList[curSvIdx].prodistInfo.critLims[0].ToString();
            panels.TbVarVoltCritcHigh.Text = svList[curSvIdx].prodistInfo.critLims[1].ToString();
            panels.TbVarVoltPrecLow.Text = svList[curSvIdx].prodistInfo.precLims[0].ToString();
            panels.TbVarVoltPrecHigh.Text = svList[curSvIdx].prodistInfo.precLims[1].ToString();

        }

        private void VarVolLimsChanged(object sender, EventArgs e)
        {
            svList[curSvIdx].prodistInfo.critLims[0] = double.Parse(panels.TbVarVoltCritcLow.Text);
            svList[curSvIdx].prodistInfo.critLims[1] = double.Parse(panels.TbVarVoltCritcHigh.Text);
            svList[curSvIdx].prodistInfo.precLims[0] = double.Parse(panels.TbVarVoltPrecLow.Text);
            svList[curSvIdx].prodistInfo.precLims[1] = double.Parse(panels.TbVarVoltPrecHigh.Text);

            recreateTable();
        }

        private void BtnVarVoltStop_Click(object sender, EventArgs e)
        {
            if (serverConnected)
            {
                if (running)
                {
                    var res = socket.SendData(SocketConnection.entryType.PRODIST_STOP, "");
                    running = false;
                    panels.BtnVarVolStart.Enabled = true;
                    panels.BtnVarVoltStop.Enabled = false;
                }
            }
        }

        private void updateVarVoltButtons()
        {
            if (panels == null) return;
            if (running)
            {
                panels.BtnVarVolStart.Enabled = false;
                panels.BtnVarVolStart.Enabled = true;
            }
            else
            {
                panels.BtnVarVolStart.Enabled = true;
                panels.BtnVarVoltStop.Enabled = false;
            }
            panels.BtnVarVolStart.Update();
            panels.BtnVarVoltStop.Update();
        }

        private void CbxSVIDChanged(object sender, EventArgs e)
        {
            ComboBox local = sender as ComboBox;
            curSvIdx = local.SelectedIndex;
            clearCapt();
            updateTextBoxLims();
            checkNoCaptured();
        }

        private void checkNoCaptured()
        {
            if (serverConnected)
            {
                var res = socket.SendData(SocketConnection.entryType.PRODIST_INFO_DATA, "");
                if (res != null)
                {
                    var resSplit = res.Split("|");
                    bool running = resSplit[0] == "1";
                    noCaptured = int.Parse(resSplit[1]);

                    if (running)
                    {
                        int noSmp = int.Parse(resSplit[2]);
                        double time = double.Parse(resSplit[3]);
                        maxSmp = noSmp;

                        if (InvokeRequired)
                        {
                            Invoke(new Action(() =>
                            {
                                if (panels.TbVarVoltNoSample.Text != noSmp.ToString())
                                {
                                    panels.TbVarVoltNoSample.Text = noSmp.ToString();
                                }
                                if (panels.TbVarVoltInterval.Text != time.ToString("0"))
                                {
                                    panels.TbVarVoltInterval.Text = time.ToString("0");
                                }
                            }));
                        }
                        else
                        {
                            if (panels.TbVarVoltNoSample.Text != noSmp.ToString())
                            {
                                panels.TbVarVoltNoSample.Text = noSmp.ToString();
                            }
                            if (panels.TbVarVoltInterval.Text != time.ToString("0"))
                            {
                                panels.TbVarVoltInterval.Text = time.ToString("0");
                            }
                        }
                    }


                    getDataFromServer();
                }
            }
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        struct Complex
        {
            public double Real;
            public double Imaginary;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        struct ProdistData
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public ulong[] Timestamp;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public Complex[] Phasor;
            public Complex AparentPower;
            public double ActivePower;
            public double ReactivePower;
            public double Fp;
            public double DitH, Dtt, DttP, DttI, Dtt3, Dtt95, DttP99, DttI95, Dtt395;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public Complex[] CompSymI;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public Complex[] CompSymV;
            public double Fd;
        }

        static T ByteArrayToStructure<T>(byte[] bytes) where T : struct
        {
            GCHandle handle = GCHandle.Alloc(bytes, GCHandleType.Pinned);
            T obj = Marshal.PtrToStructure<T>(handle.AddrOfPinnedObject());
            handle.Free();
            return obj;
        }

        private void clearCapt()
        {
            noCaptured = 0;
            noDownloadedCapt = 0;
            drc = 0;
            drp = 0;
            nlc = 0;
            nlp = 0;
            prodistDataList.Clear();
            panels.DgvVarVolt.Rows.Clear();

        }


        int curPgb = 0;
        private void tmUpt_Tick(object sender, EventArgs e)
        {
            PbProg.Value = curPgb;
            PbProg.Update();
        }

        private void getDataFromServer()
        {
            //if (InvokeRequired)
            //{
            //    Invoke(new Action(() => getDataFromServer()));
            //    return;
            //}
            if (noCaptured != noDownloadedCapt)
            {
                bool needLoader = (noCaptured - noDownloadedCapt) > 10;
                if (needLoader)
                {
                    if (InvokeRequired)
                    {
                        Invoke(new Action(() =>
                        {
                            PbProg.Value = 0;
                            PbProg.Style = ProgressBarStyle.Continuous;
                            PbProg.MarqueeAnimationSpeed = 30;
                            PbProg.Visible = true;
                            PbProg.BringToFront();
                            PbProg.Update();
                            tmUpt.Start();
                        }));
                    }
                    else
                    {
                        PbProg.Value = 0;
                        PbProg.Style = ProgressBarStyle.Continuous;
                        PbProg.MarqueeAnimationSpeed = 30;
                        PbProg.Visible = true;
                        PbProg.BringToFront();
                        PbProg.Update();
                        tmUpt.Start();
                    }

                }
                double initial = noDownloadedCapt;
                for (double i = noDownloadedCapt; i < noCaptured; i++)
                {
                    if (needLoader)
                    {
                        //PbProg.Value = (int)((i - initial) / (noCaptured - initial) * 100);
                        //PbProg.Update();
                        curPgb = (int)((i - initial) / (noCaptured - initial) * 100);
                    }

                    var sendText = i.ToString() + "|" + svList[curSvIdx].SVID;//panels.CbxVarVoltSVID.Text;
                    var res = socket.ReceiveFile(SocketConnection.entryType.PRODIST_DATA, sendText);
                    if (res != null && res.Length > 0)
                    {
                        ProdistData data = ByteArrayToStructure<ProdistData>(res);
                        noDownloadedCapt++;
                        prodistDataList.Add(data);
                    }
                }
                if (needLoader)
                {
                    if (InvokeRequired)
                    {
                        Invoke(new Action(() =>
                        {
                            PbProg.Visible = false;
                            tmUpt.Stop();
                        }));
                    }
                    else
                    {
                        PbProg.Visible = false;
                        tmUpt.Stop();
                    }
                }
                updateTable();
            }
        }

        private void recreateTable()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => recreateTable()));
            }
            else
            {
                nlc = 0;
                nlp = 0;
                panels.DgvVarVolt.Rows.Clear();
                updateTable();
            }
        }

        private void updateTable()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => updateTable()));
                return;
            }

            var curDgv = panels.DgvVarVolt;
            if (curDgv.Rows.Count < noCaptured)
            {
                var idx = curDgv.Rows.Count;
                for (int i = idx; i < prodistDataList.Count; i++)
                {
                    // Time | RMS | Classification
                    curDgv.Rows.Add();
                    // Convert to time to date
                    var date = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                    date = date.AddSeconds(prodistDataList[i].Timestamp[0]);
                    date = date.AddMilliseconds(prodistDataList[i].Timestamp[1] / 1e6);
                    curDgv.Rows[i].Cells[0].Value = (i + 1).ToString();
                    curDgv.Rows[i].Cells[1].Value = date.ToString("HH:mm:ss.fff");

                    string rmsString = "";
                    double rmsMax = 0;
                    double rmsMin = -1;
                    for (int j = 4; j < 7; j++)
                    {
                        var val = Math.Sqrt(prodistDataList[i].Phasor[j].Real * prodistDataList[i].Phasor[j].Real + prodistDataList[i].Phasor[j].Imaginary * prodistDataList[i].Phasor[j].Imaginary);
                        if (j == 6)
                            rmsString += val.ToString("0.0");
                        else
                            rmsString += val.ToString("0.0") + " | ";
                        if (val > rmsMax)
                        {
                            rmsMax = val;
                        }
                        if (rmsMin == -1 || val < rmsMin)
                        {
                            rmsMin = val;
                        }
                    }
                    curDgv.Rows[i].Cells[2].Value = rmsString;
                    string classString = "";
                    if (rmsMin < svList[curSvIdx].prodistInfo.critLims[0] || rmsMax > svList[curSvIdx].prodistInfo.critLims[1])
                    {
                        classString = "Critico";
                        nlc++;
                    }
                    else if (rmsMin < svList[curSvIdx].prodistInfo.precLims[0] || rmsMax > svList[curSvIdx].prodistInfo.precLims[1])
                    {
                        classString = "Precária";
                        nlp++;
                    }
                    else
                    {
                        classString = "Normal";
                    }
                    curDgv.Rows[i].Cells[3].Value = classString;

                    panels.TbVarVoltCaptNoCapt.Text = noCaptured.ToString();
                    panels.TbVarVoltCaptNoCaptPerc.Text = $"{100} %";

                    panels.VarVoltCaptNLP.Text = nlp.ToString();
                    panels.VarVoltCaptNLPPerc.Text = ((nlp / noCaptured) * 100).ToString("0.00") + " %";

                    panels.VarVoltCaptNLC.Text = nlc.ToString();
                    panels.VarVoltCaptNLCPerc.Text = ((nlc / noCaptured) * 100).ToString("0.00") + " %";

                    panels.VarVoltCaptDRCPerc.Text = ((nlc / maxSmp) * 100).ToString("0.00") + " %";

                    panels.VarVoltCaptDPRPerc.Text = ((nlp / maxSmp) * 100).ToString("0.00") + " %";


                }
            }
        }

        
    }
}
