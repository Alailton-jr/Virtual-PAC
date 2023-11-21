using System.Data;
using static Server.classes;
using Newtonsoft.Json;

namespace Server
{
    public partial class VmsForm : Form
    {
        public VmsForm()
        {
            InitializeComponent();
        }

        public CommunicationConfig config;

        public List<deviceConfig> devices;

        private ServerCtl main;

        public class deviceConfig
        {
            public string name { get; set; }
            public bool state { get; set; }
            public int tryConnection { get; set; }
            public Label labelStatus { get; set; }
            public Label labelName { get; set; }
            public Button button { get; set; }
            public int[] pos { get; set; }
            public classes.DeviceClass device { get; set; }
        }

        private void General_Load(object sender, EventArgs e)
        {
            main = MainForm.main;
            config = main.communicationConfig;
            devices = new List<deviceConfig>();
            TbPasswd.Validated += ValidateText;
            TbPort.Validated += ValidateText;
            TbIp.Validated += ValidateText;

            TbPasswd.KeyPress += ValidateTextKey;
            TbPort.KeyPress += ValidateTextKey;
            TbIp.KeyPress += ValidateTextKey;

            main.serverCon.ConnectionLost += handleDesconnection;

            UpdateTextBox();
            tableSetup(1, 5);

        }

        private void handleDesconnection(object? sender, EventArgs e)
        {
            serverConnected = false;
            try
            {
                if (detailForm != null) detailForm.BeginInvoke((MethodInvoker)delegate { detailForm.Close(); });
                if (CreateForm != null) CreateForm.BeginInvoke((MethodInvoker)delegate { CreateForm.Close(); });
            }
            catch {; }

            notConnected();
            MessageBox.Show("Connection Was Lost!");
        }

        private void tableSetup(int rowCount, int columnCount)
        {
            TlpIeds.ColumnCount = columnCount;
            TlpIeds.ColumnStyles.Clear();
            TlpIeds.RowStyles.Clear();
            for (int i = 0; i < columnCount; i++)
                TlpIeds.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F / columnCount));
            TlpIeds.RowCount = rowCount;
            for (int i = 0; i < rowCount; i++)
                TlpIeds.RowStyles.Add(new RowStyle(SizeType.Percent, 100F / rowCount));
            TlpIeds.Size = new Size(tabLayout.tableSize[0], tabLayout.tableSize[1] * rowCount);
            TlpIeds.Dock = DockStyle.Top;
            TlpIeds.Location = new Point(20, 102);
            TlpIeds.Name = "TlpIeds";
            TlpIeds.TabIndex = 0;
            DeviceImages.ImageSize = new Size(tabLayout.imageSize[0], tabLayout.imageSize[1]);
        }

        public void ValidateText(object sender, EventArgs e)
        {
            config.setIpAddress(TbIp.Text);
            config.setPortNumber(TbPort.Text);
            config.password = TbPasswd.Text;
            UpdateTextBox();
        }

        public void ValidateTextKey(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                config.setIpAddress(TbIp.Text);
                config.setPortNumber(TbPort.Text);
                config.password = TbPasswd.Text;
                UpdateTextBox();
            }
        }
        public void UpdateTextBox()
        {
            TbIp.Text = config.ip.ToString();
            TbPasswd.Text = config.password;
            TbPort.Text = config.port.ToString();
        }

        private class tableLayout
        {
            public int tableCol = 0;
            public int tableRow = 0;
            public int maxCol = 5;
            public int[] panelSize = new int[] { 184, 234 };
            public int[] label1Size = new int[] { 182, 38 };
            public int[] label2Size = new int[] { 182, 38 };
            public int[] buttonSize = new int[] { 182, 161 };
            public int[] tableSize = new int[] { 918, 240 };
            public int[] imageSize = new int[] { 143, 161 };
            public void changeMaxCol(int col)
            {
                panelSize = new int[] { 184 * 5 / col, 234 * 5 / col };
                label1Size = new int[] { 182 * 5 / col, 38 * 5 / col };
                label2Size = new int[] { 182 * 5 / col, 38 * 5 / col };
                buttonSize = new int[] { 182 * 5 / col, 161 * 5 / col };
                tableSize = new int[] { 918, 240 * 5 / col };
                imageSize = new int[] { 143 * 5 / col, 161 * 5 / col };
                this.maxCol = col;
            }
        }

        private tableLayout tabLayout = new tableLayout();

        private void addIed(DeviceClass x)
        {

            Panel panel = new Panel();
            panel.Dock = DockStyle.Fill;

            panel.BorderStyle = BorderStyle.FixedSingle;
            panel.Dock = DockStyle.Fill;
            panel.Location = new Point(3, 3);
            panel.Size = new Size(tabLayout.panelSize[0], tabLayout.panelSize[1]);
            panel.TabIndex = 0;

            Label textLabel = new Label();
            textLabel.Dock = DockStyle.Top;
            textLabel.Font = new Font("Segoe UI", 13F, FontStyle.Regular, GraphicsUnit.Point);
            textLabel.Location = new Point(0, 0);
            textLabel.Size = new Size(tabLayout.label1Size[0], tabLayout.label1Size[1]);
            textLabel.TabIndex = 0;
            textLabel.Text = x.name;
            textLabel.TextAlign = ContentAlignment.MiddleCenter;

            Button button = new Button();
            button.BackgroundImageLayout = ImageLayout.Zoom;
            button.Dock = DockStyle.Top;
            button.FlatAppearance.BorderSize = 0;
            button.FlatStyle = FlatStyle.Flat;
            if (x.type == DeviceType.ied)
                button.Image = Properties.Resources.Ied;

            else if (x.type == DeviceType.mu)
                button.Image = Properties.Resources.Mu;

            button.Location = new Point(0, 38);
            button.Name = x.name;
            button.Size = new Size(tabLayout.buttonSize[0], tabLayout.buttonSize[1]);
            button.TabIndex = 1;
            button.TextImageRelation = TextImageRelation.ImageAboveText;
            button.UseVisualStyleBackColor = true;
            button.Click += deviceClick;

            Label textLabel2 = new Label();
            textLabel2.Dock = DockStyle.Top;
            textLabel2.Font = new Font("Segoe UI", 13F, FontStyle.Regular, GraphicsUnit.Point);
            textLabel2.Location = new Point(0, 0);
            textLabel2.Size = new Size(tabLayout.label2Size[0], tabLayout.label2Size[1]);
            textLabel2.TabIndex = 0;
            textLabel2.Text = x.name;
            textLabel2.TextAlign = ContentAlignment.MiddleCenter;

            panel.Controls.Add(textLabel);
            panel.Controls.Add(button);
            panel.Controls.Add(textLabel2);

            TlpIeds.Controls.Add(panel, tabLayout.tableCol, tabLayout.tableRow);

            devices.Add(new deviceConfig
            {
                button = button,
                labelName = textLabel2,
                labelStatus = textLabel,
                name = x.name,
                pos = new int[] { tabLayout.tableCol, tabLayout.tableRow },
                state = false,
                tryConnection = 0,
                device = x
            });

            if (tabLayout.tableCol == tabLayout.maxCol)
            {
                tabLayout.tableCol = 0;
                tabLayout.tableRow++;
                tableSetup(tabLayout.tableRow + 1, 5);
            }
            else
                tabLayout.tableCol++;
        }


        private DeviceDetailForm detailForm = null;
        private void deviceClick(object sender, EventArgs e)
        {

            if (sender is Button button)
            {
                deviceConfig device = devices.Find(x => x.name == button.Name);

                if (device != null)
                {
                    detailForm = new DeviceDetailForm();
                    if (!detailForm.callForm(device.device))
                    {
                        MessageBox.Show("Error Loading");
                    }
                    detailForm = null;
                }
            }

        }

        private bool serverConnected = false;
        private int connectionTry = 0;

        private void iedList()
        {
            Thread.Sleep(1000);
            while (main.devices.Any(x => x.type == DeviceType.None) && serverConnected)
            {
                foreach (DeviceClass device in main.devices)
                {
                    if (device.type == DeviceType.None)
                    {
                        var res = main.serverCon.SendData("getVmInfo", device.name);
                        if (res != null && res != "error")
                        {
                            Dictionary<string, string> results = null;
                            try
                            {
                                results = JsonConvert.DeserializeObject<Dictionary<string, string>>(res);
                            }
                            catch
                            {
                                ;
                            }
                            if (results != null)
                            {
                                if (results["type"] == "IED")
                                {
                                    device.type = DeviceType.ied;
                                    device.port = 8080;
                                }
                                else
                                {
                                    device.type = DeviceType.mu;
                                    device.port = 8081;
                                }
                                device.ip = results["ip"].Split("/")[0];
                            }
                        }
                    }
                }
                Thread.Sleep(2000);
            }

            if (serverConnected)
            {
                foreach (DeviceClass device in main.devices)
                {
                    TlpIeds.Invoke((Action)(() =>
                    {
                        addIed(device);
                    }));
                }
            }

        }
        private Thread timerConThread;
        private void BtnConnect_Click(object sender, EventArgs e)
        {
            Console.WriteLine("hi");
            if (serverConnected)
            {
                notConnected();
            }
            else
            {
                main.serverCon.changeConProperties(config.ip, config.port);
                if (!getDeviceList()) notConnected();
                else
                {
                    Connected();
                    var t = new Thread(iedList);
                    t.Start();
                    if (timerConThread == null)
                    {
                        timerConThread = new Thread(TestConThread);
                        timerConThread.Start();
                    }
                }
            }
        }

        private void notConnected()
        {
            if (BtnConnect.InvokeRequired == true)
            {
                BtnConnect.BeginInvoke((Action)(() =>
                {
                    BtnConnect.Text = "Conectar";
                }));
            }
            else BtnConnect.Text = "Conectar";


            //TimerConnection.Stop();
            serverConnected = false;
            if (LbConnection.InvokeRequired)
            {
                LbConnection.BeginInvoke((Action)(() =>
                {
                    LbConnection.Text = "Desconectado";
                    LbConnection.Image = Properties.Resources.stoped;
                }));
            }
            else
            {
                LbConnection.Text = "Desconectado";
                LbConnection.Image = Properties.Resources.stoped;
            }

            if (TlpIeds.InvokeRequired)
            {
                TlpIeds.BeginInvoke((Action)(() =>
                {
                    TlpIeds.Enabled = false;
                }));
            }
            else TlpIeds.Enabled = false;

            if (BtnCreateVm.InvokeRequired)
            {
                BtnCreateVm.BeginInvoke((Action)(() =>
                {
                    BtnCreateVm.Enabled = false;
                }));
            }
            else BtnCreateVm.Enabled = false;

            if (BtnDelVm.InvokeRequired)
            {
                BtnDelVm.BeginInvoke((Action)(() =>
                {
                    BtnDelVm.Enabled = false;
                }));
            }
            else BtnDelVm.Enabled = false;

        }

        private void Connected()
        {
            tabLayout.tableCol = 0;
            tabLayout.tableRow = 0;
            LbConnection.Text = "Conectando...";
            LbConnection.Image = Properties.Resources.connecting;
            connectionTry = 0;
            BtnConnect.Text = "Parar";
            serverConnected = true;
            TlpIeds.Controls.Clear();
            TlpIeds.Enabled = true;
            BtnCreateVm.Enabled = true;
            BtnDelVm.Enabled = true;
        }

        private bool getDeviceList()
        {
            main.serverCon.changeConProperties(config.ip, config.port);
            string res = main.serverCon.SendData("getDeviceList", "all");
            if (res != null && res != "error")
            {
                Dictionary<string, List<string>> results = null;
                try
                {
                    results = JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(res);
                }
                catch {; }
                if (results == null) return false;
                main.devices.Clear();
                devices.Clear();
                foreach (KeyValuePair<string, List<string>> kvp in results)
                {
                    switch (kvp.Key)
                    {
                        case "shut":
                            foreach (string vm in kvp.Value)
                            {
                                main.devices.Add(new DeviceClass()
                                {
                                    name = vm,
                                    VmState = DeviceState.down,
                                    type = DeviceType.None
                                });
                            }
                            break;

                        case "running":
                            foreach (string vm in kvp.Value)
                            {
                                main.devices.Add(new DeviceClass()
                                {
                                    name = vm,
                                    VmState = DeviceState.up,
                                    type = DeviceType.None
                                });
                            }
                            break;
                    }
                }
                return true;
            }
            return false;
        }

        private void updateDevices()
        {
            try
            {
                foreach (var device in devices)
                {
                    var res = main.serverCon.SendData("getVmState", device.name);
                    if (res == null) continue;
                    try
                    {
                        Dictionary<string, int> results = JsonConvert.DeserializeObject<Dictionary<string, int>>(res);
                        device.device.VmState = results["state"] == 0 ? DeviceState.down : DeviceState.up;
                    }
                    catch { continue; }

                    if (device.device.VmState == DeviceState.up)
                    {
                        if (device.labelStatus.InvokeRequired)
                        {
                            device.labelStatus.BeginInvoke((Action)(() =>
                            {
                                device.labelStatus.Text = "Running";
                            }));
                        }
                        else
                            device.labelStatus.Text = "Running";
                    }
                    else if (device.device.VmState == DeviceState.down)
                    {
                        if (device.labelStatus.InvokeRequired)
                        {
                            device.labelStatus.BeginInvoke((Action)(() =>
                            {
                                device.labelStatus.Text = "Stoped";
                            }));
                        }
                        else
                            device.labelStatus.Text = "Stoped";
                    }
                }
            }
            catch {; }
        }

        private void TestConThread()
        {
            while (serverConnected)
            {
                try
                {
                    main.serverCon.SendData("testConnection", "all");
                    connectionTry++;
                    updateDevices();

                    if (connectionTry > 2 && serverConnected)
                    {
                        LbConnection.Invoke((Action)(() =>
                        {
                            LbConnection.Text = "Conectado";
                            LbConnection.Image = Properties.Resources.running;
                        }));
                    }
                    Thread.Sleep(2000);
                }
                catch { break; }

            }
        }

        private void
            TimerConnection_Tick(object sender, EventArgs e)
        {
            if (serverConnected)
            {
                main.serverCon.SendData("testConnection", "all");
                connectionTry++;
                //getDeviceList();
                updateDevices();

                if (connectionTry > 2 && serverConnected)
                {
                    LbConnection.Text = "Conectado";
                    LbConnection.Image = Properties.Resources.running;
                }
            }
            else
            {
                TimerConnection.Stop();
            }
        }

        private DeviceCreatingForm CreateForm = null;
        private void BtnCreateVm_Click(object sender, EventArgs e)
        {
            CreateForm = new DeviceCreatingForm();
            if (CreateForm.openExt())
            {
                getDeviceList();
                updateDevices();
            }

            CreateForm = null;
        }

        private DeviceRemoveForm DeleteForm = null;
        private void BtnDelVm_Click(object sender, EventArgs e)
        {
            DeleteForm = new DeviceRemoveForm();
            var list = devices.Select(x => new string[] { x.name, x.device.type == DeviceType.mu ? "Merging Unit" : "IED" }).ToList();
            if (DeleteForm.openExt(list))
            {
                getDeviceList();
                updateDevices();
            }
            DeleteForm = null;
        }
    }
}
