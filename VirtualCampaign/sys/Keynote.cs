using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualCampaign.sys {
    public class Keynote {
        private object _data;
        private Point _coordinate;
        public object Data { get { return _data; } set { setData(value); } }
        public Point Coordinate { get { return _coordinate; } set { setCoordinate(value); } }

        public Keynote() {

        }

        public Keynote(Point p) : base() {
            setCoordinate(p);
        }

        private void setCoordinate(Point p) {
            _coordinate = p;
        }

        private void setData(object o) {
            _data = o;
        }

        public virtual string GenerateKeynoteString() {
            return "";
        }
    }
}
