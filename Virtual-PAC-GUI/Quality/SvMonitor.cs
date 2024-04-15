using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quality
{
    public partial class SvMonitor : UserControl
    {

        private bool _wasFound;
        public bool wasFound
        {
            get { return _wasFound; }
            set { _wasFound = value; }
        }

        private bool _vtcd;
        public bool vtcd
        {
            get { return _vtcd; }
            set
            {
                _vtcd = value;
                changeImage(LbVtcd, value);
            }
        }

        private bool _vtld;
        public bool vtld
        {
            get { return _vtld; }
            set
            {
                _vtld = value;
                changeImage(LbVtld, value);
            }
        }

        private bool _harm;
        public bool harm
        {
            get { return _harm; }
            set
            {
                _harm = value;
                changeImage(LbHarm, value);
            }
        }

        private bool _unbalance;
        public bool unbalance
        {
            get { return _unbalance; }
            set
            {
                _unbalance = value;
                changeImage(LbUnbalance, value);
            }
        }

        private bool _transient;
        public bool transient
        {
            get { return _transient; }
            set
            {
                _transient = value;
                changeImage(LbTransient, value);
            }
        }

        private bool _fluctuation;
        public bool fluctuation
        {
            get { return _fluctuation; }
            set
            {
                _fluctuation = value;
                changeImage(LbFluctiations, value);
            }
        }

        private string _svID;
        public string svID
        {
            get { return _svID; }
            set
            {
                _svID = value;
                LbSVID.Text = value;
            }
        }

        private bool mouseInside = false;

        private bool _selected = false;
        public bool selected
        {
            get { return _selected; }
            set 
            {
                _selected = value;
                if (value)
                {
                    PnMain.BackColor = SelectedColor;
                }
                else
                {
                    if (mouseInside) PnMain.BackColor = HoverColor;
                    else PnMain.BackColor = UnselectedColor;
                }
            }
        }

        private bool _isRunning = false;
        public bool isRunning
        {
            get { return _isRunning; }
            set
            {
                _isRunning = value;
                RunningUpdate();
            }
        }

        private Color SelectedColor = Color.FromArgb(53, 88, 118);
        private Color UnselectedColor = Color.FromArgb(44, 65, 84);
        private Color HoverColor = Color.FromArgb(73, 55, 40);
        private Color NotRunningColor = Color.FromArgb(78, 79, 80);

        public event EventHandler myOnClick;

        public SvMonitor()
        {
            InitializeComponent();
            wasFound = false;
            vtcd = false;
            vtld = false;
            harm = false;
            unbalance = false;
            transient = false;
            fluctuation = false;
            selected = false;
            isRunning = false;

            MouseEnter += SvMonitor_MouseEnter;
            MouseLeave += SvMonitor_MouseLeave;

            PnMain.MouseEnter += SvMonitor_MouseEnter;
            PnMain.MouseLeave += SvMonitor_MouseLeave;
            PnMain.MouseClick += (sender, e) => MyOnClick(e);
            foreach (Control c in PnMain.Controls)
            {
                c.MouseEnter += SvMonitor_MouseEnter;
                c.MouseLeave += SvMonitor_MouseLeave;
                c.MouseClick += (sender, e) => MyOnClick(e);
            }

        }

        private void RunningUpdate()
        {
            ForeColor = _isRunning ? Color.Lavender : NotRunningColor;
            LbSVID.ForeColor = Color.Lavender;
        }
        
        private void SvMonitor_MouseLeave(object sender, EventArgs e)
        {
            mouseInside = false;
            if (selected) PnMain.BackColor = SelectedColor;
            else PnMain.BackColor = UnselectedColor;
        }

        private void SvMonitor_MouseEnter(object sender, EventArgs e)
        {
            mouseInside = true;
            if (!selected) PnMain.BackColor = HoverColor;
        }

        private void changeImage(Label lb, bool status)
        {
            if (status)
            {
                lb.Image = Properties.Resources.running;
            }
            else
            {
                lb.Image = Properties.Resources.stoped;
            }
        }

        protected virtual void MyOnClick(EventArgs e)
        {
            selected = !selected;
            myOnClick?.Invoke(this, e);
        }

    }
}
