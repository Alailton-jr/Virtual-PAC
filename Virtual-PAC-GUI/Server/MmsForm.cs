using Newtonsoft.Json.Linq;
using System.Diagnostics;

namespace Server
{
    public partial class MmsForm : Form
    {
        public MmsForm()
        {
            InitializeComponent();
        }

        private void MmsForm_Load(object sender, EventArgs e)
        {

            fillTreeViewer();


        }

        private void fillTreeViewer()
        {
            //string jsonFilePath = "C:\\Users\\ALAILTON\\My Drive\\Faculdade\\TCC\\0.0.8\\mms\\output.json";
            //string jsonData = File.ReadAllText(jsonFilePath);
            //JObject jsonObject = JObject.Parse(jsonData);

            //using (var reader = new StreamReader(jsonFilePath))
            //using (var jsonReader = new JsonTextReader(reader))
            //{
            //    var root = JToken.Load(jsonReader);
            //    DisplayTreeView(root, Path.GetFileNameWithoutExtension(jsonFilePath));
            //}
        }

        private void DisplayTreeView(JToken root, string rootName)
        {
            TvData.BeginUpdate();
            try
            {
                TvData.Nodes.Clear();
                var tNode = TvData.Nodes[TvData.Nodes.Add(new TreeNode(rootName))];
                tNode.Tag = root;

                AddNode(root, tNode);

                //TvData.ExpandAll();
            }
            finally
            {
                TvData.EndUpdate();
            }
        }

        private void AddNode(JToken token, TreeNode inTreeNode)
        {
            if (token == null)
                return;
            if (token is JValue)
            {
                var childNode = inTreeNode.Nodes[inTreeNode.Nodes.Add(new TreeNode(token.ToString()))];
                childNode.Tag = token;
            }
            else if (token is JObject)
            {
                var obj = (JObject)token;
                foreach (var property in obj.Properties())
                {
                    var childNode = inTreeNode.Nodes[inTreeNode.Nodes.Add(new TreeNode(property.Name))];
                    childNode.Tag = property;
                    AddNode(property.Value, childNode);
                }
            }
            else if (token is JArray)
            {
                var array = (JArray)token;
                for (int i = 0; i < array.Count; i++)
                {
                    var childNode = inTreeNode.Nodes[inTreeNode.Nodes.Add(new TreeNode(i.ToString()))];
                    childNode.Tag = array[i];
                    AddNode(array[i], childNode);
                }
            }
            else
            {
                Debug.WriteLine(string.Format("{0} not implemented", token.Type)); // JConstructor, JRaw
            }
        }
    }
}
