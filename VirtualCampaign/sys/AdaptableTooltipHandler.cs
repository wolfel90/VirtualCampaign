using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VirtualCampaign.window;

namespace VirtualCampaign.sys {
    public class AdaptableTooltipHandler {
        private static AdaptableTooltip _Tooltip;
        public static AdaptableTooltip Tooltip { get { return _Tooltip; } }
        public static Control CurrentControl;
        
        static AdaptableTooltipHandler() {
            _Tooltip = new AdaptableTooltip();
        }

        public static void ClearTooltip() {
            CurrentControl = null;
            _Tooltip.Hide();
        }
    }
}
