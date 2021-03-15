using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VirtualCampaign.window;

namespace VirtualCampaign.controls {
    public partial class VocationList : UserControl {
        private List<VocationPanel> panels;
        public event EventHandler ValueChanged;
        public bool refreshing { get; set; }
        private bool _editable;
        public bool editable { get { return _editable; } set { setEditable(value); } }

        public VocationList() {
            InitializeComponent();
            panels = new List<VocationPanel>();
        }

        public void setEditable(bool val) {
            _editable = val;
            for (int i = 0; i < panels.Count; ++i) {
                panels[i].setEditable(val);
            }
        }

        private void addButton_Click(object sender, EventArgs e) {
            using(var nameQueue = new SimpleMessageBox()) {
                nameQueue.Text = "Vocation Name";
                var result = nameQueue.ShowDialog();
                if(result == DialogResult.OK) {
                    addVocation(nameQueue.result);
                }
            }
        }

        public void addVocation(String name) {
            VocationPanel newPanel = new VocationPanel();
            newPanel.Title = name;
            addVocation(newPanel);
        }

        public void addVocation(VocationPanel panel) {
            addVocations(new VocationPanel[] { panel });
        }

        public void addVocations(String[] names) {
            VocationPanel[] panels = new VocationPanel[names.Length];
            for(int i = 0; i < names.Length; ++i) {
                panels[i].Title = names[i];
            }
            addVocations(panels);
        }

        public void addVocations(VocationPanel[] p) {
            if(p.Length > 0) {
                VocationPanel bottom = null;
                foreach(VocationPanel v in p) {
                    panels.Add(v);
                    Controls.Add(v);
                    v.Location = new Point(5, panels.IndexOf(v) * v.Height);
                    bottom = v;
                }
                if(bottom != null) this.Height = bottom.Location.Y + bottom.Height + 30;
                positionVocationPanels();
                if(!refreshing) OnChanged(EventArgs.Empty);
            }
        }

        protected virtual void OnChanged(EventArgs e) {
            ValueChanged?.Invoke(this, e);
        }

        public int GetVocationCount() {
            return panels.Count;
        }

        public void RemoveVocationAt(int index) {
            if (index >= 0 && index < panels.Count) {
                VocationPanel p = panels[index];
                panels.RemoveAt(index);
                p.Dispose();
                Controls.Remove(p);
                positionVocationPanels();
                if (!refreshing) OnChanged(EventArgs.Empty);
            }
        }

        public void SetVocationAt(int index, String s) {
            if (index < panels.Count) {
                panels[index].Title = s;
                if (!refreshing) OnChanged(EventArgs.Empty);
            }
        }

        public void SwapVocations(int index1, int index2) {
            if (index1 >= 0 && index1 < panels.Count && index2 >= 0 && index2 < panels.Count) {
                String temp = panels[index2].Title;
                SetVocationAt(index2, panels[index1].Title);
                SetVocationAt(index1, temp);
                if (!refreshing) OnChanged(EventArgs.Empty);
            }
        }

        public int IndexOf(VocationPanel panel) {
            return panels.IndexOf(panel);
        }

        private void positionVocationPanels() {
            if (panels.Count > 0) {
                int yMark = 0;
                for (int i = 0; i < panels.Count; ++i) {
                    panels[i].Location = new Point(5, yMark);
                    yMark += panels[i].Height;
                }
                this.Height = yMark + 30;
            } else {
                this.Height = 25;
            }
        }

        public string generateString() {
            string result = "";
            foreach(VocationPanel v in panels) {
                result += "[vocation]" + v.Title + "[/vocation]";
            }

            return result;
        }
    }
}
