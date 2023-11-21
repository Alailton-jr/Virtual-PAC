using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Ied.Ctl;
using static Ied.Iec61850Config;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using TreeView = System.Windows.Forms.TreeView;
using static Ied.IecDataSetForm;
using static Ied.Iec61850Config.DataSetConfig;

namespace Ied
{
    public partial class IecDataSetConfForm : Form
    {
        private DataSetConfig returnedClass;
        public DataSetConfig openExt(DataSetConfig settings)
        {
            if (settings != null)
            {
                BtnSave.Text = "Atualizar";
                TbName.Text = settings.dataSetName;
                TbDesc.Text = settings.desc;
                if (settings.data != null) foreach (string[] data in settings.data) DgvDs.Rows.Add(data[0], data[1]);
            
            }
            ShowDialog();
            return returnedClass;
        }

        public IecDataSetConfForm()
        {
            InitializeComponent();

            fillTreeview();
        }

        //Drag Form
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void panelTitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }


        private Size formSize;
        private void btnMinimize_Click(object sender, EventArgs e)
        {
            formSize = this.ClientSize;
            this.WindowState = FormWindowState.Minimized;
            BtnResize.Image = null;
        }

        private void btnMaximize_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                formSize = this.ClientSize;
                this.WindowState = FormWindowState.Maximized;
                BtnResize.Image = Image.FromFile("C:\\Users\\ALAILTON\\My Drive\\Faculdade\\TCC\\Código\\Visual Studio\\Images\\inside.png");
            }
            else
            {
                this.WindowState = FormWindowState.Normal;
                this.Size = formSize;
                BtnResize.Image = Image.FromFile("C:\\Users\\ALAILTON\\My Drive\\Faculdade\\TCC\\Código\\Visual Studio\\Images\\outside.png");
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            //Application.Exit();
            this.Close();
        }


        private void findDaType(TreeNode node, string id, FcdaClass fcda)
        {
            var x = scl.DataTypeTemplates.DAType.FirstOrDefault(daType => daType.id == id);
            if (x != null)
            {
                foreach (var bda in x.BDA)
                {
                    node.Nodes.Add(bda.name);
                    //node.Nodes[node.Nodes.Count - 1].Tag = tag + "$" + bda.name;
                    node.Nodes[node.Nodes.Count - 1].Tag = fcda.clone();
                    ((FcdaClass)node.Nodes[node.Nodes.Count - 1].Tag).daName.Add(bda.name);

                    if (bda.type != null || bda.bType == tBasicTypeEnum.Enum) findDaType(node.Nodes[node.Nodes.Count - 1], bda.type, ((FcdaClass)node.Nodes[node.Nodes.Count - 1].Tag));
                    else node.Nodes[node.Nodes.Count - 1].Text += $" [{bda.bType.ToString()}]";
                }
            }
            else
            {
                var y = scl.DataTypeTemplates.DOType.FirstOrDefault(doType => doType.id == id);
                if (y != null)
                {
                    foreach (var item in y.Items)
                    {
                        if (item is tDA)
                        {
                            var DA = (tDA)item;
                            var debug = DA.fc.ToString();
                            if (DA.fc.ToString() != CbFilter.Text && CbFilter.Text != "All") continue;

                            node.Nodes.Add(DA.name);
                            //node.Nodes[node.Nodes.Count - 1].Tag = tag + "$" + DA.name;
                            node.Nodes[node.Nodes.Count - 1].Tag = fcda.clone();
                            ((FcdaClass)node.Nodes[node.Nodes.Count - 1].Tag).daName.Add(DA.name);

                            if (DA.type != null && DA.bType != tBasicTypeEnum.Enum) findDaType(node.Nodes[node.Nodes.Count - 1], DA.type, ((FcdaClass)node.Nodes[node.Nodes.Count - 1].Tag));
                            else node.Nodes[node.Nodes.Count - 1].Text += $" [{DA.bType.ToString()}]";
                        }
                        else
                        {
                            var SDO = (tSDO)item;
                            node.Nodes.Add(SDO.name);
                            //node.Nodes[node.Nodes.Count - 1].Tag = tag + "$" + SDO.name;
                            node.Nodes[node.Nodes.Count - 1].Tag = fcda.clone();
                            ((FcdaClass)node.Nodes[node.Nodes.Count - 1].Tag).sdoName = SDO.name;
                            if (SDO.type != null) findDaType(node.Nodes[node.Nodes.Count - 1], SDO.type, ((FcdaClass)node.Nodes[node.Nodes.Count - 1].Tag));

                        }
                    }
                }
                else
                    Console.WriteLine("hi");
            }


        }

        private Dictionary<string, object> dataSet;
        private SCL scl = MainForm.main.sclConf.scl;

        //public class FcdaClass
        //{
        //    public string fc { get; set; }
        //    public string lnClass { get; set; }
        //    public string lnInst { get; set; }
        //    public string doName { get; set; }
        //    public List<string> daName { get; set; }
        //    public string sdoName { get; set; }
        //    public string ldInst { get; set; }
        //    public string prefix { get; set; }

        //    FcdaClass()
        //    {
        //        daName = new List<string>();
        //    }

        //    public FcdaClass clone()
        //    {
        //        var x = new FcdaClass()
        //        {
        //            fc = this.fc,
        //            lnClass = this.lnClass,
        //            lnInst = this.lnInst,
        //            doName = this.doName,
        //            ldInst = this.ldInst,
        //            prefix = this.prefix,
        //            sdoName = this.sdoName
        //        };
        //        foreach (var da in this.daName) x.daName.Add(da);
        //        return x;
        //    }
        //}
        
        private void fillTreeview()
        {
            tServer server = (tServer)scl.IED[0].AccessPoint[0].Items[0];
            var lnTypeList = new List<string[]>();

            var lnList = new Dictionary<string, List<string[]>>();

            foreach (tLDevice _ld in server.LDevice)
            {
                lnList.Add(_ld.inst, new List<string[]>());

                lnList[_ld.inst].Add(new string[] { _ld.LN0.lnType, _ld.LN0.lnClass });
                foreach (var ln in _ld.LN)
                {
                    lnList[_ld.inst].Add(new string[] { ln.lnType, ln.lnClass, ln.prefix });
                    lnTypeList.Add(new string[] { ln.lnType, ln.lnClass, ln.prefix });
                }
            }
            var tvList = new Dictionary<string, string>();

            TvIed.Nodes.Clear();
            foreach (var kvp in lnList)
            {
                TvIed.Nodes.Add(kvp.Key);
                var node_0 = TvIed.Nodes[TvIed.Nodes.Count - 1];

                //node_0.Tag = "CFGLLN0." + kvp.Key;
                node_0.Tag = new FcdaClass() { ldInst = kvp.Key};

                foreach (var ln in kvp.Value)
                {
                    if (ln.Length == 3)
                        node_0.Nodes.Add(ln[2] + ln[0]);
                    else
                        node_0.Nodes.Add(ln[0]);
                    var node_1 = node_0.Nodes[node_0.Nodes.Count - 1];

                    //node_1.Tag = node_0.Tag + $"${node_1.Text}";
                    node_1.Tag = ((FcdaClass)node_0.Tag).clone();
                    ((FcdaClass)node_1.Tag).prefix = ln[2];
                    ((FcdaClass)node_1.Tag).lnClass = ln[1];
                    ((FcdaClass)node_1.Tag).lnInst = ln[0];

                    var x = scl.DataTypeTemplates.LNodeType.FirstOrDefault(lnType => lnType.id == ln[0]);
                    if (x == null) continue;
                    foreach (var DO in x.DO)
                    {
                        node_1.Nodes.Add(DO.name);
                        node_1.Nodes[node_1.Nodes.Count - 1].Tag = node_1.Tag + $"${DO.name}";
                        var _fcda = (FcdaClass)node_1.Nodes[node_1.Nodes.Count - 1].Tag;
                        _fcda.doName = DO.name;
                        if (DO.type != null) findDaType(node_1.Nodes[node_1.Nodes.Count - 1], DO.type, _fcda);

                    }
                }

            }


            removeList = new List<TreeNode>();
            foreach (TreeNode node in TvIed.Nodes)
            {
                if (node == null) continue;
                if (!checkTreeView(node)) removeList.Add(node);
            }
            foreach (TreeNode node in removeList) node.Remove();
        }

        private List<TreeNode> removeList;
        private bool checkTreeView(TreeNode parent)
        {
            bool flag = false;
            foreach (TreeNode node in parent.Nodes)
            {
                if (node == null) continue;
                if (node.Text.Contains("[")) return true;
                else if (node.Nodes.Count > 0) flag = checkTreeView(node);
                else removeList.Add(node);
            }
            if (!flag) removeList.Add(parent);
            return flag;
        }

        private void TvIed_ItemDrag(object sender, ItemDragEventArgs e)
        {
            TreeNode draggedNode = (TreeNode)e.Item;
            TvIed.DoDragDrop(e.Item, DragDropEffects.Copy);
        }


        private void TvDs_DragDrop(object sender, DragEventArgs e)
        {
            TreeNode draggedNode = (TreeNode)e.Data.GetData(typeof(TreeNode));
            if (draggedNode == null) return;
            TreeNode newNode = (TreeNode)draggedNode.Clone();
            DgvDs.Rows.Add(CbFilter.Text, draggedNode.Tag);
            DgvDs.Rows[DgvDs.Rows.Count - 1].Tag = newNode;
        }

        private void TvIed_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete && TvIed.SelectedNode != null)
            {
                // Remove the selected node
                TvIed.Nodes.Remove(TvIed.SelectedNode);
            }
        }

        private void TvDs_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void CbFilter_TextChanged(object sender, EventArgs e)
        {
            fillTreeview();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            returnedClass = new DataSetConfig();
            returnedClass.nodes = new List<TreeNode>();
            if (String.IsNullOrEmpty(TbName.Text))
            {
                MessageBox.Show("Digite um nome para o DataSet");
                return;
            }

            returnedClass.data = new List<string[]>();
            foreach (DataGridViewRow row in DgvDs.Rows)
            {
                returnedClass.data.Add(new string[] { row.Cells[0].Value.ToString(), row.Cells[1].Value.ToString() });
                TreeNode localNode = (TreeNode)row.Tag;
                returnedClass.nodes.Add(localNode);
            }
            returnedClass.dataSetName = TbName.Text;
            returnedClass.desc = TbDesc.Text;
            this.Close();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            returnedClass = null;
            this.Close();
        }
    }

}
