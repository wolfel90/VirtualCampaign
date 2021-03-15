using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VirtualCampaign.data;
using VirtualCampaign.sys;
using VirtualCampaign.window;

namespace VirtualCampaign.controls {
    public partial class InventoryList : GameItemCollectionControl {
        public event EventHandler ValueChanged;
        public event EventHandler ItemSlotClicked;
        public event EventHandler ItemSlotEquipChanged;
        public Dictionary<int, ItemSlot> items { get; }
        public List<int> indices;
        public bool refreshing { get; set; } = false;

        public InventoryList() {
            InitializeComponent();

            items = new Dictionary<int, ItemSlot>();
            indices = new List<int>();
            itemListPanel.VerticalScroll.Enabled = true;
        }

        public void setInventoryByString(string str) {
            itemListPanel.Controls.Clear();
            items.Clear();
            indices.Clear();

            List<ItemData> toAdd = new List<ItemData>();

            string[] itemStrings = StringFunctions.ReadTagMulti(str, "item");
            foreach(string s in itemStrings) {
                toAdd.Add(new ItemData(s));
                retrieveContents(s, toAdd);
            }
            addItems(toAdd.ToArray());
        }

        private void retrieveContents(string container, List<ItemData> list) {
            string contents = StringFunctions.ReadTag(container, "contents");
            string[] items = StringFunctions.ReadTagMulti(contents, "item");
            for(int i = 0; i < items.Length; ++i) {
                list.Add(new ItemData(items[i]));
                retrieveContents(items[i], list);
            }
        }

        public void addItem(ItemData item) {
            addItems(new ItemData[] { item });
        }

        public int indexOf(ItemSlot slot) {
            foreach(int i in items.Keys) {
                if(items[i] == slot) {
                    return i;
                }
            }
            return -1;
        }

        public void addItems(ItemData[] newItems) {
            foreach(ItemData d in newItems) {
                ItemSlot newSlot = new ItemSlot(d);
                newSlot.refreshing = refreshing;
                int index = getVacantIndex();
                items.Add(index, newSlot);
                newSlot.parentList = this;
                newSlot.ValueChanged += ItemSlot_Changed;
                newSlot.ItemClicked += ItemSlot_Clicked;
                newSlot.ItemEquipChanged += ItemSlotEquipped_Changed;
                
                itemListPanel.Controls.Add(newSlot);
                itemListPanel.Size = new Size(itemListPanel.Width - System.Windows.Forms.SystemInformation.VerticalScrollBarWidth, newSlot.Height);
                indices.Add(index);
                newSlot.refreshing = false;
                if (d.equipped) {
                    ItemSlotEquipChanged?.Invoke(newSlot, EventArgs.Empty);
                }
            }
            sortSlots();
            if (!refreshing) OnValueChanged(this, EventArgs.Empty);
        }

        public void removeItemSlot(ItemSlot slot) {
            if(slot != null) {
                slot.refreshing = refreshing;
                int index = indexOf(slot);
                if (items.Keys.Contains(index)) {
                    indices.Remove(index);
                    items.Remove(index);
                    itemListPanel.Controls.Remove(slot);
                    slot.parentList = null;
                    slot.ValueChanged -= ItemSlot_Changed;
                    slot.ItemClicked -= ItemSlot_Clicked;
                    slot.ItemEquipChanged -= ItemSlotEquipped_Changed;
                    sortSlots();
                    if (!refreshing) OnValueChanged(this, EventArgs.Empty);
                }
                slot.refreshing = false;
            }
        }

        private int getVacantIndex() {
            for(int i = 0; i < 1000000; i++) {
                if(!items.Keys.Contains(i)) {
                    return i;
                }
            }
            return 1000000;
        }

        public void sortSlots() {
            int mY = 0;
            foreach(int i in indices) {
                if(items.Keys.Contains(i)) {
                    ItemSlot slot = items[i];
                    slot.SetBounds(slot.Location.X, mY + itemListPanel.AutoScrollPosition.Y, itemListPanel.Width - System.Windows.Forms.SystemInformation.VerticalScrollBarWidth, slot.Height);
                    slot.Visible = true;
                    slot.Enabled = true;
                    mY += slot.Height;
                }
            }

            foreach(int i in items.Keys) {
                if(!indices.Contains(i)) {
                    items[i].Visible = false;
                    items[i].Enabled = false;
                }
            }
            itemListPanel.Update();
        }

        public String generateString() {
            string result = "";
            foreach(int i in items.Keys) {
                result += items[i].itemData.generateString();
            }
            return result;
        }

        private void addButton_Click(object sender, EventArgs e) {
            if (DragControlHandler.isCarrying(typeof(ItemData))) {
                addItem((ItemData)DragControlHandler.getCarriedData());
                if ((ModifierKeys & Keys.Control) != Keys.Control) {
                    DragControlHandler.clearCarriedData();
                }
            } else {
                using (var itemDesigner = new ItemDesigner()) {
                    var result = itemDesigner.ShowDialog();
                    if (result == DialogResult.OK) {
                        addItem(itemDesigner.resultItem);
                    }
                }
            }
        }

        public void ItemSlot_Changed(object sender, EventArgs e) {
            if(sender is ItemSlot) {
                if(!((ItemSlot)sender).refreshing) {
                    OnValueChanged(this, EventArgs.Empty);
                }
            }
        }

        public void ItemSlotEquipped_Changed(object sender, EventArgs e) {
            ItemSlotEquipChanged?.Invoke(sender, e);
        }

        protected virtual void OnValueChanged(object source, EventArgs e) {
            ValueChanged?.Invoke(source, e);
        }

        protected virtual void OnItemSlotClicked(object source, EventArgs e) {
            // Attempt to drop or grab item. If this cannot be done, pass the event down the chain
            if(((MouseEventArgs)e).Button == MouseButtons.Left) {
                if(((MouseEventArgs)e).Clicks == 1) {
                    if(DragControlHandler.isCarrying(typeof(ItemData))) {
                        switch(DropMode) {
                            case NONE:
                                break;
                            case MOVE:
                                addItem((ItemData)DragControlHandler.getCarriedData());
                                if ((ModifierKeys & Keys.Control) != Keys.Control) {
                                    DragControlHandler.clearCarriedData();
                                }
                                break;
                            case CLONE:
                                addItem((ItemData)DragControlHandler.getCarriedData());
                                break;
                        }
                    } else {
                        ItemSlotClicked?.Invoke(source, e);
                    }
                } else if(((MouseEventArgs)e).Clicks == 2) {
                    if(!DragControlHandler.isCarrying(typeof(ItemData))) {
                        switch (GrabMode) {
                            case NONE:
                                break;
                            case MOVE:
                                if(source is ItemSlot) {
                                    DragControlHandler.setCarriedData(((ItemSlot)source).itemData);
                                    if ((ModifierKeys & Keys.Control) != Keys.Control) {
                                        removeItemSlot((ItemSlot)source);
                                    }
                                }
                                break;
                            case CLONE:
                                if (source is ItemSlot) {
                                    DragControlHandler.setCarriedData(((ItemSlot)source).itemData);
                                }
                                break;
                        }
                    }
                } else {
                    ItemSlotClicked?.Invoke(source, e);
                }
            } else {
                ItemSlotClicked?.Invoke(source, e);
            }
        }

        public void ItemSlot_Clicked(object sender, EventArgs e) {
            OnItemSlotClicked(sender, e);
        }

        private void itemListPanel_MouseClick(object sender, MouseEventArgs e) {
            if(DragControlHandler.isCarrying(typeof(ItemData))) {
                addItem((ItemData)DragControlHandler.getCarriedData());
                if ((ModifierKeys & Keys.Control) != Keys.Control) {
                    DragControlHandler.clearCarriedData();
                }
            }
        }
    }
}
