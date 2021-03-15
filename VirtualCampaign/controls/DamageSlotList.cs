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
    public partial class DamageSlotList : UserControl {
        private List<DamageSlot> slots;

        public DamageSlotList() {
            InitializeComponent();

            slots = new List<DamageSlot>();
        }

        public void AddDamageSlot() {
            AddDamageSlot(new DamageSlot());
        }

        public void AddDamageSlot(DamageSlot slot) {
            InsertDamageSlot(slot, slots.Count);
        }

        public void InsertDamageSlot(DamageSlot slot, int index) {
            if (index < 0) index = 0;
            if (index > slots.Count) index = slots.Count;
            slots.Insert(index, slot);
            listPanel.Controls.Add(slot);
            RepositionSlots();
        }

        public void RemoveDamageSlot(int index) {
            if(index >= 0 && index < slots.Count) {
                DamageSlot target = slots[index];
                RemoveDamageSlot(target);
            }
        }

        public void RemoveDamageSlot(DamageSlot slot) {
            if(slots.Contains(slot)) {
                slots.Remove(slot);
                listPanel.Controls.Remove(slot);
                RepositionSlots();
            }
        }

        private void RepositionSlots() {
            int yMark = 0;
            int newHeight = 0;
            foreach (DamageSlot s in slots) {
                newHeight += s.Height;
            }
            foreach (DamageSlot s in slots) {
                s.Bounds = new Rectangle(0, yMark, listPanel.Width - 2 - (newHeight > listPanel.Height ? System.Windows.Forms.SystemInformation.VerticalScrollBarWidth : 0), s.Height);
                yMark += s.Height;
            }
            Invalidate(listPanel.Bounds);
        }

        private void DamageSlotList_SizeChanged(object sender, EventArgs e) {
            listPanel.Size = new Size(Width, Height - addButton.Height - 5);
        }

        private void addButton_Click(object sender, EventArgs e) {
            AddDamageSlot();
        }
    }
}
