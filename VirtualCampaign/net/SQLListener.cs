using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualCampaign.net {
    public interface SQLListener {
        void HandleData(int action, Dictionary<String, Object> d);
    }
}
