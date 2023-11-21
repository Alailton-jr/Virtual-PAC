using System.Windows.Forms;
using Ied;
using static Ied.IedModel;
using static Ied.Ctl;
using static Ied.Iec61850Config;
using static Ied.Iec61850Config.DataSetConfig;

namespace Ied
{

    public partial class IecDataSetForm : Form
    {

        public IecDataSetForm()
        {
            InitializeComponent();
            if (MainForm.main.iecConf == null) MainForm.main.iecConf = new Iec61850Config();
            if (MainForm.main.iecConf.dataSet == null) MainForm.main.iecConf.dataSet = new List<DataSetConfig>();
            dataList = MainForm.main.iecConf.dataSet;

            foreach (var data in dataList) data.loadData();

            dataGridLoad();
            UpdateTable();

            if (MainForm.main.sclConf == null) MainForm.main.sclConf = new Ied.SCLConfig();
            if (MainForm.main.sclConf.scl == null) MainForm.main.sclConf.defaultConfig();
            scl = MainForm.main.sclConf.scl;

        }


        public List<DataSetConfig> dataList;

        private void dataGridLoad()
        {

            DgvDs.AutoGenerateColumns = false;
            DgvDs.AllowUserToAddRows = false;

            DgvDs.Rows.Clear();

        }
        private void UpdateTable()
        {
            DgvDs.Rows.Clear();
            foreach (var data in dataList)
            {
                DgvDs.Rows.Add("", data.dataSetName, data.desc);
            }
        }

        private int nameCnt = 0;
        private void BtnAddDs_Click(object sender, EventArgs e)
        {
            //dataGridLoad();
            var x = new TreeView();
            x.CheckBoxes = true;
            x.PathSeparator = "$";
            fillTreeview(x);
            string name = "DataSet_";
            int i = 0;
            while (dataList.Any(data => data.dataSetName == name + i.ToString()))
                i++;
            dataList.Add(new DataSetConfig { dataSetName = name + i.ToString(), desc = "", treeview = x });
            saveDatalist();
            UpdateTable();
        }

        private void saveDatalist()
        {
            foreach (var data in dataList)
                data.saveData();
        }

        private void removeRow()
        {
            DgvDs.Select();
        }

        private TreeView currentTv;
        private int currentIndex;
        private void DgvDs_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                currentIndex = e.RowIndex;
                currentTv = dataList[e.RowIndex].treeview;
                updateTreeNode();
            }
            updateTreeNode();
            updateTreeNode();
        }

        private void updateTreeNode()
        {
            if (currentTv == null)
                return;
            TvDs.Nodes.Clear();
            foreach (TreeNode node in currentTv.Nodes)
            {
                if (node == null) continue;
                TvDs.Nodes.Add(node);
            }
            TvDs.Refresh();
        }

        private void BtnRemoveDs_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in DgvDs.SelectedRows)
            {
                dataList.RemoveAt(row.Index);
                DgvDs.Rows.Remove(row);
            }
        }

        private void findDaType(TreeNode node, string id, FcdaClass fcda)
        {
            var x = scl.DataTypeTemplates.DAType.FirstOrDefault(daType => daType.id == id);
            if (x != null)
            {
                foreach (var bda in x.BDA)
                {
                    node.Nodes.Add(bda.name);
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

                            node.Nodes.Add(DA.name);
                            node.Nodes[node.Nodes.Count - 1].Tag = fcda.clone();
                            ((FcdaClass)node.Nodes[node.Nodes.Count - 1].Tag).daName.Add(DA.name);

                            if (DA.type != null && DA.bType != tBasicTypeEnum.Enum) findDaType(node.Nodes[node.Nodes.Count - 1], DA.type, ((FcdaClass)node.Nodes[node.Nodes.Count - 1].Tag));
                            else node.Nodes[node.Nodes.Count - 1].Text += $" [{DA.bType.ToString()}]";
                        }
                        else
                        {
                            var SDO = (tSDO)item;
                            node.Nodes.Add(SDO.name);
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
        private SCL scl;



        private void fillTreeview(TreeView TvIed)
        {
            tServer server = (tServer)scl.IED[0].AccessPoint[0].Items[0];
            var lnTypeList = new List<string[]>();

            var lnList = new Dictionary<string, List<string[]>>();

            foreach (tLDevice _ld in server.LDevice)
            {
                lnList.Add(_ld.inst, new List<string[]>());

                lnList[_ld.inst].Add(new string[] { _ld.LN0.lnType, _ld.LN0.lnClass, "" });
                foreach (var ln in _ld.LN)
                {
                    lnList[_ld.inst].Add(new string[] { ln.lnType, ln.lnClass, ln.prefix, ln.inst });
                    lnTypeList.Add(new string[] { ln.lnType, ln.lnClass, ln.prefix, ln.inst });
                }
            }
            var tvList = new Dictionary<string, string>();

            TvIed.Nodes.Clear();
            foreach (var kvp in lnList)
            {
                TvIed.Nodes.Add(kvp.Key);
                var node_0 = TvIed.Nodes[TvIed.Nodes.Count - 1];

                //node_0.Tag = "CFGLLN0." + kvp.Key;
                node_0.Tag = new FcdaClass() { ldInst = kvp.Key };

                foreach (var ln in kvp.Value)
                {
                    if (ln.Length == 4)
                        node_0.Nodes.Add(ln[2] + ln[0]);
                    else
                        node_0.Nodes.Add(ln[0]);
                    var node_1 = node_0.Nodes[node_0.Nodes.Count - 1];

                    //node_1.Tag = node_0.Tag + $"${node_1.Text}";
                    node_1.Tag = ((FcdaClass)node_0.Tag).clone();
                    if (ln.Length == 4)
                    {
                        ((FcdaClass)node_1.Tag).prefix = ln[2];
                        ((FcdaClass)node_1.Tag).lnClass = ln[1];
                        ((FcdaClass)node_1.Tag).lnInst = ln[3];
                    }
                    else
                    {
                        ((FcdaClass)node_1.Tag).lnClass = ln[1];
                        ((FcdaClass)node_1.Tag).lnInst = ln[2];
                    }

                    var x = scl.DataTypeTemplates.LNodeType.FirstOrDefault(lnType => lnType.id == ln[0]);
                    if (x == null) continue;
                    foreach (var DO in x.DO)
                    {
                        node_1.Nodes.Add(DO.name);
                        //node_1.Nodes[node_1.Nodes.Count - 1].Tag = node_1.Tag + $"${DO.name}";
                        //var _fcda = ((FcdaClass)node_1.Nodes[node_1.Nodes.Count - 1].Tag).clone();
                        //_fcda.doName = DO.name;
                        node_1.Nodes[node_1.Nodes.Count - 1].Tag = ((FcdaClass)node_1.Tag).clone();
                        ((FcdaClass)node_1.Nodes[node_1.Nodes.Count - 1].Tag).doName = DO.name;
                        if (DO.type != null) findDaType(node_1.Nodes[node_1.Nodes.Count - 1], DO.type, ((FcdaClass)node_1.Nodes[node_1.Nodes.Count - 1].Tag));

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

        private void SetParent(TreeNode node, bool state)
        {
            if (node.Parent == null) return;
            if (state)
            {
                node.Parent.Checked = state;
                SetParent(node.Parent, state);
            }
            else
            {
                bool flag = false;
                foreach (TreeNode child in node.Nodes) flag |= child.Checked;
                if (flag) return;
                else
                {
                    node.Parent.Checked = state;
                    SetParent(node.Parent, state);
                }
            }
        }
        private void setChiled(TreeNode node, bool state)
        {
            if (!(node.Nodes.Count > 0)) return;
            foreach (TreeNode chield in node.Nodes)
            {
                chield.Checked = state;
                setChiled(chield, state);
            }
        }

        private void IecDataSetForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            saveDatalist();
        }
        private bool checkChanging = false;

        private void TvDs_AfterCheck(object sender, TreeViewEventArgs e)
        {
            TreeNode local = (TreeNode)e.Node;
            if (checkChanging) return;
            checkChanging = true;
            SetParent(local, local.Checked);
            setChiled(local, local.Checked);
            checkChanging = false;
            saveDatalist();
        }
    }
}
