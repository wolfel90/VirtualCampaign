using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VirtualCampaign.controls {
    public partial class DefenseSlotList : UserControl {
        private List<DefenseSlot> slots;

        public DefenseSlotList() {
            InitializeComponent();

            slots = new List<DefenseSlot>();
        }

        public void AddDefenseSlot() {
            AddDefenseSlot(new DefenseSlot());
        }

        public void AddDefenseSlot(DefenseSlot slot) {
            InsertDefenseSlot(slot, slots.Count);
        }

        public void InsertDefenseSlot(DefenseSlot slot, int index) {
            if (index < 0) index = 0;
            if (index > slots.Count) index = slots.Count;
            slots.Insert(index, slot);
            listPanel.Controls.Add(slot);
            RepositionSlots();
        }

        public void RemoveDefenseSlot(int index) {
            if(index >= 0 && index < slots.Count) {
                DefenseSlot target = slots[index];
                RemoveDefenseSlot(target);
            }
        }

        public void RemoveDefenseSlot(DefenseSlot slot) {
            if(slots.Contains(slot)) {
                slots.Remove(slot);
                listPanel.Controls.Remove(slot);
                RepositionSlots();
            }
        }

        private void RepositionSlots() {
            int yMark = 0;
            int newHeight = 0;
            foreach (DefenseSlot s in slots) {
                newHeight += s.Height;
            }
            foreach (DefenseSlot s in slots) {
                s.Bounds = new Rectangle(0, yMark, listPanel.Width - 2 - (newHeight > listPanel.Height ? System.Windows.Forms.SystemInformation.VerticalScrollBarWidth : 0), s.Height);
                yMark += s.Height;
                Console.Out.WriteLine(listPanel.VerticalScroll.Visible + " " + listPanel.HorizontalScroll.Visible);
            }
            Invalidate(listPanel.Bounds);
        }

        private void DefenseSlotList_SizeChanged(object sender, EventArgs e) {
            listPanel.Size = new Size(Width, Height - addButton.Height - 5);
        }

        private void addButton_Click(object sender, EventArgs e) {
            AddDefenseSlot();
        }
    }
}
