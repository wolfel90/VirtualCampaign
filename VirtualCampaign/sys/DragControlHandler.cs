using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ComponentModel;
using VirtualCampaign.controls;
using VirtualCampaign.data;
using VirtualCampaign.window;

namespace VirtualCampaign.sys {
    static class DragControlHandler {
        private static DragObject TargetControl = null;
        private static object carried = null;
        private delegate void dragSafeCall(Point p);
        private static System.Timers.Timer dragTimer;
        public static event EventHandler DragControlChanged;

        static DragControlHandler() {
            dragTimer = new System.Timers.Timer(30);
            dragTimer.Elapsed += OnDragTimerElapsed;
            dragTimer.AutoReset = true;
        }

        public static void setTargetComponent(DragObject c) {
            TargetControl = c;
        }

        public static DragObject getTargetComponent() {
            return TargetControl;
        }

        private static void OnDragTimerElapsed(object source, System.Timers.ElapsedEventArgs e) {
            var p = new Point(Control.MousePosition.X + 5, Control.MousePosition.Y + 5);
            if (TargetControl != null) {
                updateDragObjectLocation(p);
            }
        }

        private static void updateDragObjectLocation(Point p) {
            if (TargetControl.InvokeRequired) {
                var d = new dragSafeCall(updateDragObjectLocation);
                TargetControl.Invoke(d, new object[] { p });
            } else {
                TargetControl.Location = p;
            }
        }

        public static void setCarriedData(object o) {
            if(o == null) {
                TargetControl.setVisualControl(null);
                if(carried != null) {
                    carried = o;
                    dragTimer.Enabled = false;
                    TargetControl.Hide();
                    TargetControl.Location = new Point(0, 0);
                    dispatchDragItemChangedEvent(EventArgs.Empty);
                }
            } else {
                AdaptableTooltipHandler.ClearTooltip();
                if (o != carried) {
                    if (o is ItemData) {
                        ItemImage newImage = new ItemImage();
                        newImage.bgSrc = ((ItemData)o).bgSrc;
                        newImage.iconSrc = ((ItemData)o).iconSrc;
                        newImage.setBGColor(((ItemData)o).bgColor);
                        newImage.setIconColor(((ItemData)o).iconColor);
                        if(((ItemData)o).stackable && ((ItemData)o).count > 1) {
                            newImage.footStr = ((ItemData)o).count.ToString();
                        } else {
                            newImage.footStr = "";
                        }
                        newImage.Size = new Size(48, 48);
                        TargetControl.setVisualControl(newImage);
                    }
                    carried = o;
                    dragTimer.Enabled = true;
                    TargetControl.Show();
                    dispatchDragItemChangedEvent(EventArgs.Empty);
                }
            }
        }

        public static void clearCarriedData() {
            setCarriedData(null);
        }

        public static object getCarriedData() {
            return carried;
        }

        public static bool isCarrying() {
            return carried != null;
        }

        public static bool isCarrying(Type check) {
            if(carried != null) {
                if(check == carried.GetType()) {
                    return true;
                }
            }
            return false;
        }

        public static void dispatchDragItemChangedEvent(EventArgs e) {
            DragControlChanged?.Invoke(TargetControl, e);
        }
    }
}
