using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualCampaign.sys;

namespace VirtualCampaign.controls {
    public class AtlasMarker : AtlasElement {
        private string _bgSrc, _iconSrc;
        public string bgSrc { get { return _bgSrc; } set { SetBGSrc(value); } }
        public string iconSrc { get { return _iconSrc; } set { SetIconSrc(value); } }
        public string Title { get; set; }
        private static Bitmap imgHolder;
        private static Dictionary<string, Bitmap> BMPLibrary;
        private Dictionary<string, List<string>> Props;
        public bool DrawLabel, DrawBoundary;

        static AtlasMarker() {
            imgHolder = null;
            BMPLibrary = new Dictionary<string, Bitmap>();
        }

        public AtlasMarker() : base() {
            Title = "";
            _bgSrc = "";
            _iconSrc = "";
            DrawLabel = false;
            DrawBoundary = false;
            Props = new Dictionary<string, List<string>>();
        }

        private void SetBGSrc(string val) {
            if(!BMPLibrary.ContainsKey(val.ToLower())) {
                object img = Properties.Resources.ResourceManager.GetObject(val.ToLower());
                if (img != null) {
                    BMPLibrary.Add(val.ToLower(), (Bitmap)img);
                }
            }
            _bgSrc = val.ToLower();
        }

        private void SetIconSrc(string val) {
            if (!BMPLibrary.ContainsKey(val.ToLower())) {
                object img = Properties.Resources.ResourceManager.GetObject(val.ToLower());
                if (img != null) {
                    BMPLibrary.Add(val.ToLower(), (Bitmap)img);
                }
            }
            _iconSrc = val.ToLower();
        }

        public void ParsePropertyString(string str) {
            ParsePropertyString(str, true);
        }

        public void ParsePropertyString(string str, bool overwrite) {
            if(overwrite) Props.Clear();
            if(str != null) {
                string[] newProps = StringFunctions.TagFinder(str);
                foreach(string s in newProps) {
                    string[] adds = StringFunctions.ReadTagMulti(str, s);
                    if(s != "property") {
                        foreach (string a in adds) {
                            AddProperty(s, a);
                        }
                    } else {
                        // Legacy support
                        foreach(string a in adds) {
                            string propName = StringFunctions.ReadTag(a, "name");
                            string propValue = StringFunctions.ReadTag(a, "value");
                            AddProperty(propName, propValue);
                        }
                        // End Legacy support
                    }
                }
            }
        }

        public void AddProperty(string key, string val) {
            if (Props.ContainsKey(key)) {
                SetProperty(key, val, Props[key].Count);
            } else {
                SetProperty(key, val, 0);
            }
        }

        public void SetProperty(string key, string val) {
            SetProperty(key, val, 0);
        }

        public void SetProperty(string key, string val, int index) {
            if(key == "x") {
                this.MapPoint = new PointF(float.TryParse(val, out float f) ? f : 0, MapPoint.Y);
            } else if(key == "y") {
                this.MapPoint = new PointF(MapPoint.X, float.TryParse(val, out float f) ? f : 0);
            } else if(key == "z") {
                this.Z = float.TryParse(val, out float f) ? f : 0;
            } else if(key == "mark") {
                this.iconSrc = val;
            } else if(key == "icon") {
                this.bgSrc = val;
            } else {
                if (!Props.ContainsKey(key)) Props.Add(key, new List<string>());
                if (index >= 0 && index < Props[key].Count) {
                    Props[key][index] = val;
                } else if (index == Props[key].Count) {
                    Props[key].Add(val);
                }
            }
        }

        public bool HasProperty(string key) {
            return Props.ContainsKey(key);
        }

        public string GetProperty(string key) {
            return GetProperty(key, 0);
        }

        public string GetProperty(string key, int index) {
            if(key == "x") {
                return this.MapPoint.X.ToString();
            } else if(key == "y") {
                return this.MapPoint.Y.ToString();
            } else if(key == "z") {
                return this.Z.ToString();
            } else if(key == "mark") {
                return this.iconSrc;
            } else if(key == "icon") {
                return this.bgSrc;
            } else {
                if (Props.ContainsKey(key)) {
                    if (index >= 0 && index < Props[key].Count) {
                        return Props[key][index];
                    }
                }
            }
            return "";
        }

        public void RemoveProperty(string key) {
            if(Props.ContainsKey(key)) {
                Props.Remove(key);
            }
        }

        public string GeneratePropertiesString() {
            string result = "";
            foreach(string key in Props.Keys) {
                if(Props[key] != null) {
                    foreach(string val in Props[key]) {
                        result += "[" + key + "]" + val + "[/" + key + "]";
                    }
                }
            }
            result += "[x]" + this.MapPoint.X + "[/x]";
            result += "[y]" + this.MapPoint.Y + "[/y]";
            result += "[z]" + this.Z + "[/z]";
            result += "[mark]" + this.iconSrc + "[/mark]";
            result += "[icon]" + this.bgSrc + "[/icon]";
            return result;
        }

        public override void PaintElement(Graphics g, Rectangle r) {
            if(BMPLibrary.ContainsKey(_bgSrc)) {
                imgHolder = BMPLibrary[_bgSrc];
                if(imgHolder != null) {
                    g.DrawImage((Bitmap)imgHolder, r, 0, 0, ((Bitmap)imgHolder).Width, ((Bitmap)imgHolder).Height, GraphicsUnit.Pixel);
                }
            }

            if (BMPLibrary.ContainsKey(_iconSrc)) {
                imgHolder = BMPLibrary[_iconSrc];
                if (imgHolder != null) {
                    g.DrawImage((Bitmap)imgHolder, r, 0, 0, ((Bitmap)imgHolder).Width, ((Bitmap)imgHolder).Height, GraphicsUnit.Pixel);
                }
            }
        }
    }
}
