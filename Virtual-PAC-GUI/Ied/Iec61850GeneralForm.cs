using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Xml.Serialization;
using YamlDotNet.Core;
using YamlDotNet.Serialization;
using static Ied.MainForm;


namespace Ied
{
    public partial class Iec61850GeneralForm : Form
    {
        public Iec61850GeneralForm()
        {
            InitializeComponent();
        }

        private List<Iec61850Config.GoSend> goList;

        private void Iec61850GeneralForm_Load(object sender, EventArgs e)
        {
            goList = main.iecConf.goSendList;
        }

        private void BtnImportScl_Click(object sender, EventArgs e)
        {

        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            var x = new SCLConfig();
            x.defaultConfig();

            var ldCfg = x.ldDevice["cfg"];
            ldCfg.LN0.GSEControl = new tGSEControl[goList.Count];
            ldCfg.LN0.DataSet = new tDataSet[goList.Count];

            x.scl.Communication.SubNetwork[0].ConnectedAP[0].GSE = new tGSE[goList.Count];
            var gse = x.scl.Communication.SubNetwork[0].ConnectedAP[0].GSE;

            for (int i = 0; i < goList.Count; i++)
            {
                // Data Set
                ldCfg.LN0.GSEControl[i] = new tGSEControl();
                ldCfg.LN0.GSEControl[i].appID = goList[i].goId;
                ldCfg.LN0.GSEControl[i].type = tGSEControlTypeEnum.GOOSE;
                if (goList[i].dataSet!= null)
                ldCfg.LN0.GSEControl[i].datSet = goList[i].dataSet.dataSetName;
                ldCfg.LN0.GSEControl[i].confRev = goList[i].confRef;
                ldCfg.LN0.GSEControl[i].name = goList[i].cbRef;
                var data = goList[i].getSendList();
                if (data == null) continue;

                ldCfg.LN0.DataSet[i] = new tDataSet()
                {
                    Items = new tFCDA[data.Count],
                    desc = goList[i].dataSet.desc,
                    name = goList[i].dataSet.dataSetName,
                };
                for (int j = 0; j < data.Count; j++)
                {
                    ldCfg.LN0.DataSet[i].Items[j] = new tFCDA()
                    {
                        ldInst = data[j].ldInst,
                        prefix = data[j].prefix,
                        lnClass = data[j].lnClass,
                        lnInst = data[j].lnInst,
                    };
                    if (data[j].sdoName != null) ldCfg.LN0.DataSet[i].Items[j].doName = $"{data[j].doName}.{data[j].sdoName}";
                    else ldCfg.LN0.DataSet[i].Items[j].doName = data[j].doName;
                    ldCfg.LN0.DataSet[i].Items[j].daName = string.Join(".", data[j].daName);
                }

                // Control Block
                gse[i] = new tGSE
                {
                    ldInst = "CFG",
                    cbName = goList[i].cbRef,
                    MinTime = new tDurationInMilliSec() { multiplier = tUnitMultiplierEnum.m, unit = "s", Value = goList[i].minTime, multiplierSpecified=true},
                    MaxTime = new tDurationInMilliSec() { multiplier = tUnitMultiplierEnum.m, unit = "s", Value = goList[i].maxTime, multiplierSpecified = true },
                    Address = new tP[]
                    {
                        new tP{type =  "MAC-Address", Value = goList[i].macDst},
                        new tP{type =  "APPID", Value = goList[i].appId.ToString("X")},
                        new tP{type =  "VLAN-PRIORITY", Value = goList[i].vLanPriority.ToString()},
                        new tP{type =  "VLAN-ID", Value = goList[i].vLanId.ToString("X")},
                    }
                };

            }

            XmlSerializer serializer = new XmlSerializer(typeof(SCL));
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "SCL Files|*.icd;*.scd;*.scl;*.cid|ICD Files (*.icd)|*.icd|SCD Files (*.scd)|*.scd|SCL Files (*.scl)|*.scl|CID Files (*.cid)|*.cid";
                saveFileDialog.Title = "vIED";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    using (FileStream stream = new FileStream(saveFileDialog.FileName, FileMode.Create))
                    {
                        serializer.Serialize(stream, x.scl);
                    }
                }
            }

        }

        
    }
}
