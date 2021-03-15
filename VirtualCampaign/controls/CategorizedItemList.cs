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

namespace VirtualCampaign.controls {
    public partial class CategorizedItemList : UserControl {
        private List<ItemCategory> categories;
        private bool resizing;

        public CategorizedItemList() {
            InitializeComponent();
            categories = new List<ItemCategory>();
            resizing = false;
        }

        public ItemCategory AddCategory(string cat, int type) {
            ItemCategory newCat = new ItemCategory(cat, type);
            int bottom = 0;
            foreach(Control c in Controls) {
                if (c.Bottom > bottom) bottom = c.Bottom;
            }
            categories.Add(newCat);
            Controls.Add(newCat.categoryLabel);
            Controls.Add(newCat.gridPanel);
            newCat.gridPanel.SetBlockSize(48);
            newCat.categoryLabel.Size = new Size(this.Width - System.Windows.Forms.SystemInformation.VerticalScrollBarWidth - 2, 30);
            newCat.categoryLabel.Location = new Point(0, bottom);
            newCat.gridPanel.Size = new Size(newCat.categoryLabel.Width, ItemGrid.DEFAULT_BLOCK_SIZE);
            newCat.gridPanel.Location = new Point(0, bottom + newCat.categoryLabel.Height);
            return newCat;
        }

        public void RemoveCategory(string cat) {
            for(int i = 0; i < categories.Count; ++i) {
                if(categories[i].categoryLabel.Text == cat) {
                    RemoveCategory(categories[i]);
                    break;
                }
            }
        }

        public void RemoveCategory(ItemCategory cat) {
            Controls.Remove(cat.categoryLabel);
            Controls.Remove(cat.gridPanel);
            categories.Remove(cat);
            cat.gridPanel.SizeChanged -= gridSize_Changed;
        }

        public void AddItem(ItemData item) {
            bool found = false;
            for(int i = 0; i < categories.Count; ++i) {
                if(categories[i].typeMatch == item.type) {
                    categories[i].gridPanel.AddItem(item);
                    found = true;
                    break;
                }
            }
            if(!found) {
                ItemCategory newCat = AddCategory(ItemData.GetTypeString(item.type), item.type);
                newCat.gridPanel.GrabMode = GameItemCollectionControl.CLONE;
                newCat.gridPanel.DropMode = GameItemCollectionControl.NONE;
                newCat.gridPanel.SizeChanged += gridSize_Changed;
                newCat.gridPanel.AddItem(item);
            }
        }

        private void ArrangeCategories() {
            int yMark = 0;
            foreach (ItemCategory c in categories) {
                c.categoryLabel.Location = new Point(0, yMark);
                yMark += c.categoryLabel.Height;
                c.gridPanel.Location = new Point(0, yMark);
                yMark += c.gridPanel.Height;
            }
        }

        public void gridSize_Changed(object source, EventArgs e) {
            if(!resizing) ArrangeCategories();
        }

        private void CategorizedItemList_SizeChanged(object sender, EventArgs e) {
            resizing = true;
            int yMark = 0;
            foreach (ItemCategory c in categories) {
                c.categoryLabel.Bounds = new Rectangle(0, yMark, this.Width - System.Windows.Forms.SystemInformation.VerticalScrollBarWidth - 2, 30);
                yMark += c.categoryLabel.Height;
                c.gridPanel.Bounds = new Rectangle(0, yMark, c.categoryLabel.Width, c.gridPanel.CalculateNewHeight(c.categoryLabel.Width));
                yMark += c.gridPanel.Height;
            }
            resizing = false;
        }
    }

    public class ItemCategory {
        private List<KeyValuePair<ItemData, ItemImage>> itemCollection;
        public Label categoryLabel;
        public ItemGrid gridPanel;
        public int typeMatch;

        public ItemCategory(string name) : this(name, ItemData.GENERIC_TYPE) { }

        public ItemCategory(string name, int type) {
            itemCollection = new List<KeyValuePair<ItemData, ItemImage>>();
            categoryLabel = new Label();
            gridPanel = new ItemGrid();
            categoryLabel.Text = name;
            categoryLabel.BorderStyle = BorderStyle.FixedSingle;
            categoryLabel.TextAlign = ContentAlignment.MiddleCenter;
            categoryLabel.AutoSize = false;
            typeMatch = type;
        }
    }
}
