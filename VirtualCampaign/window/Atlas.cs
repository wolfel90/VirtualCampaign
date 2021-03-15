using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VirtualCampaign.controls;
using VirtualCampaign.data;
using VirtualCampaign.net;
using VirtualCampaign.sys;
using VirtualCampaign.window;

namespace VirtualCampaign.window {
    public partial class Atlas : UserControl, SQLListener {
        public const int DEFAULT_MARKER_LENGTH = 24;
        public const int MOUSE_STANDBY = 0, MOUSE_PLACE_MARKER = 1, MOUSE_PLACE_BOUNDARY_POINT = 2, MOUSE_MEASURE = 3;
        private string legalTitleChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789-_!'()+,. ";
        private long _mID;
        public long MapID { get { return _mID;  } }
        private static Font labelFont, smallFont;
        public List<AtlasElement> TabletopElements;
        public int MouseAction;
        private Point LastMousePoint;
        private float _TableX, _TableY;
        private float _Scalar;
        private delegate void RequestRedraw();
        public float TableX { get { return _TableX; } set { SetTableX(value); } }
        public float TableY { get { return _TableY; } set { SetTableY(value); } }
        public float Scalar { get { return _Scalar; } set { SetScalar(value); } }
        private System.Timers.Timer RepaintTimer;
        private bool repaint;
        private Point anchor;
        private AtlasMap map;
        private AtlasElement overElement, clickElement, infoElement, holdElement, boundaryElement;
        private List<PointF> drawBoundaryPoints;
        private Dictionary<Point, string> Labels;
        private Rectangle unitSelectBounds;
        private bool DraggingTabletop;
        private static bool clicked;
        private static Point beginClick;
        private static long clickTime;
        private bool MouseIsOver;
        private AtlasInfoPanel infoPanel;
        private SQLParser parser;
        private Bitmap crosshairImg;
        public Article queuedArticle;
        private bool LeftShiftPressed;
        private bool imperialMeasurements;
        private PointF rulerStart;
        private PointF rulerEnd;

        static Atlas() {
            labelFont = new Font("Verdana", 14f, FontStyle.Bold);
            smallFont = new Font("Verdana", 10f, FontStyle.Bold);
        }

        public Atlas() : this(-1) {}

        public Atlas(long id) {
            InitializeComponent();
            MouseAction = MOUSE_STANDBY;
            LastMousePoint = new Point();
            TabletopElements = new List<AtlasElement>();
            Labels = new Dictionary<Point, string>();
            RepaintTimer = new System.Timers.Timer(30);
            infoPanel = new AtlasInfoPanel();
            infoPanel.Visible = false;
            DraggingTabletop = false;
            TableX = 0;
            TableY = 0;
            Scalar = 1;
            repaint = false;
            _mID = id;
            unitSelectBounds = new Rectangle(5, 5, 18, 18);
            
            RepaintTimer.AutoReset = true;
            RepaintTimer.Elapsed += RepaintTimer_Elapsed;
            RepaintTimer.Enabled = true;
            
            anchor = new Point();
            overElement = null;
            clickElement = null;
            infoElement = null;
            holdElement = null;
            boundaryElement = null;
            clicked = false;
            MouseIsOver = true;
            beginClick = new Point(-1000, -1000);
            crosshairImg = Properties.Resources.crosshairs;
            queuedArticle = null;
            LeftShiftPressed = false;
            imperialMeasurements = false;
            rulerStart = new PointF();
            rulerEnd = new PointF();

            parser = new SQLParser();
            infoPanel.Drag += InfoPanel_Drag;

            Controls.Add(infoPanel);
        }

        protected override void Dispose(bool disposing) {
            RepaintTimer.Stop();
            RepaintTimer.Dispose();
            for (int i = TabletopElements.Count - 1; i >= 0; --i) {
                TabletopElements[i].Dispose();
                TabletopElements.RemoveAt(i);
            }
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        public void InfoPanel_Drag(object source, MouseEventArgs e) {
            infoPanel.Location = new Point(infoPanel.Location.X + infoPanel.DragAmount.X, infoPanel.Location.Y + infoPanel.DragAmount.Y);
            repaint = true;
        }

        public void RepaintTimer_Elapsed(object src, System.Timers.ElapsedEventArgs e) {
            if(!IsDisposed) {
                if (this.InvokeRequired) {
                    this.Invoke(new MethodInvoker(delegate {
                        if(!IsDisposed) {
                            if (repaint) {
                                Refresh();
                                repaint = false;
                            }
                        }
                        
                    }));
                }
            }
        }

        public void SetTableX(float val) {
            SetTableLocation(val, _TableY);
        }

        public void SetTableY(float val) {
            SetTableLocation(_TableX, val);
        }

        public void SetScalar(float val) {
            _Scalar = val;
        }

        public void SetTableLocation(float tX, float tY) {
            float deltaX = tX - _TableX;
            float deltaY = tY - _TableY;

            if(deltaX != 0 || deltaY != 0) {
                repaint = true;
            }

            _TableX = tX;
            _TableY = tY;
        }

        private void Atlas_MouseMove(object sender, MouseEventArgs e) {
            if(e.Button == MouseButtons.Left) {
                if (Math.Abs(e.X - beginClick.X) + Math.Abs(e.Y - beginClick.Y) > 10) beginClick = new Point(-1000, -1000);
            }
            anchor = SpaceToScreenPoint(ScreenToSpacePoint(e.Location));
            repaint = true;
            if (DraggingTabletop) {
                if (e.Location != LastMousePoint) {
                    SetTableLocation(TableX + ((e.Location.X - LastMousePoint.X) / Scalar), TableY + ((e.Location.Y - LastMousePoint.Y) / Scalar));
                }
            }
            LastMousePoint = e.Location;
            PointF mouseWorldPoint = ScreenToSpacePoint(e.Location);
            if(overElement != null) {
                if(overElement is AtlasMarker) {
                    ((AtlasMarker)overElement).DrawLabel = false;
                    ((AtlasMarker)overElement).DrawBoundary = false;
                }
            }
            overElement = null;
            foreach(AtlasElement ae in TabletopElements) {
                if(ae is AtlasMarker) {
                    if(Math.Abs(ae.Location.X - mouseWorldPoint.X) < (ae.Width / 2 / Scalar)) {
                        if(Math.Abs(ae.Location.Y - mouseWorldPoint.Y) < (ae.Height / 2 / Scalar)) {
                            overElement = ae;
                            ((AtlasMarker)ae).DrawLabel = true;
                            ((AtlasMarker)ae).DrawBoundary = true;
                            break;
                        }
                    }
                }
            }

            if(MouseAction == MOUSE_MEASURE) {
                rulerEnd = ScreenToSpacePoint(e.Location);
            }
        }

        private void Atlas_MouseDown(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Left) {
                this.Focus();
                if(LeftShiftPressed) {
                    rulerStart = ScreenToSpacePoint(e.Location);
                    rulerEnd = ScreenToSpacePoint(e.Location);
                    MouseAction = MOUSE_MEASURE;
                } else {
                    DraggingTabletop = true;
                    LastMousePoint = e.Location;
                    beginClick = e.Location;
                    clickTime = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
                }
            } else if(e.Button == MouseButtons.Right) {
                /*AtlasElement test = new AtlasElement();
                PointF p = ScreenToSpacePoint(e.Location);
                test.Bounds = new RectangleF(p.X, p.Y, 20, 20);
                TabletopElements.Add(test);*/
            }
        }

        private void Atlas_MouseUp(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Left) {
                DraggingTabletop = false;
                if(MouseAction == MOUSE_MEASURE) {
                    rulerEnd = ScreenToSpacePoint(e.Location);
                    MouseAction = MOUSE_STANDBY;
                }
            } else if(e.Button == MouseButtons.Right) {
                bool mapAuth = false, markAuth = false, deleteAuth = false;
                if(map != null) {
                    if(UserManager.currentUser.userID > 0) {
                        if(map.useWhitelist) {
                            if(UserManager.currentUser.userID == map.creator || WhitelistHandler.getCurrentUserPermissionLevel(map.whitelistString) >= 2) {
                                mapAuth = true;
                            }
                        } else {
                            if(UserManager.currentUser.userID == map.creator || UserManager.currentUser.userRank >= 3) {
                                mapAuth = true;
                            }
                        }
                    }
                }

                if(overElement != null) {
                    if (UserManager.currentUser.userID > 0) {
                        if (overElement.useWhitelist) {
                            if (UserManager.currentUser.userID == overElement.creator || WhitelistHandler.getCurrentUserPermissionLevel(overElement.whitelistString) >= 2) {
                                markAuth = true;
                            }
                        } else {
                            if(UserManager.currentUser.userID == overElement.creator) {
                                markAuth = true;
                                deleteAuth = true;
                            }
                            if(UserManager.currentUser.userRank >= 3) {
                                markAuth = true;
                            }
                        }
                    }
                }
                newMarkerToolStripMenuItem.Visible = mapAuth;
                moveMarkerToolStripMenuItem.Visible = markAuth;
                deleteMarkerToolStripMenuItem.Visible = deleteAuth;
                defineMarkerBoundsToolStripMenuItem.Visible = markAuth;
                clearMarkerBoundaryToolStripMenuItem.Visible = (markAuth && ((AtlasMarker)overElement).HasProperty("boundary"));

                if (markAuth) clickElement = overElement;
            }
        }

        public void QueueArticle(Article art) {
            queuedArticle = art;
            holdElement = null;
            MouseAction = MOUSE_PLACE_MARKER;
        }

        private async void Atlas_MouseClick(object sender, MouseEventArgs e) {
            if(e.Button == MouseButtons.Left) {
                if(unitSelectBounds.Contains(e.Location)) {
                    imperialMeasurements = !imperialMeasurements;
                    repaint = true;
                    return;
                }
                if (Math.Abs(e.Location.X - beginClick.X) + Math.Abs(e.Location.Y - beginClick.Y) > 10) {
                    return;
                }
                if ((DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond) - clickTime > 1000) return;
                if (clicked) return;
                clicked = true;
                clickElement = overElement;
                if(MouseAction == MOUSE_STANDBY) await Task.Delay(SystemInformation.DoubleClickTime);
                if (!clicked) return;
                clicked = false;
                switch(MouseAction) {
                    case MOUSE_STANDBY:
                        if (clickElement != null) {
                            if (clickElement is AtlasMarker) {
                                string desc = "";
                                List<Dictionary<String, Object>> sqlResult = SQLManager.runImmediateQuery("vc_articles",
                                    new string[] { "description" },
                                    " `id` = " + ((AtlasMarker)clickElement).NetID + " LIMIT 1");
                                if (sqlResult.Count > 0) {
                                    if (sqlResult[0].ContainsKey("description")) {
                                        desc = sqlResult[0]["description"].ToString();
                                    }
                                    if (infoPanel != null) {
                                        infoPanel.SetTargetMarker((AtlasMarker)clickElement, StringFunctions.stripSystemTags(desc));
                                        bool canEdit = false;
                                        if (UserManager.currentUser.userID >= 0) {
                                            if (((AtlasMarker)clickElement).useWhitelist) {
                                                if (clickElement.creator == UserManager.currentUser.userID || WhitelistHandler.getCurrentUserPermissionLevel(clickElement.whitelistString) >= 2) {
                                                    canEdit = true;
                                                }
                                            } else {
                                                canEdit = true;
                                            }
                                        }

                                        infoPanel.SetEditable(canEdit);
                                        if (!infoPanel.Visible) {
                                            Point p = SpaceToScreenPoint(clickElement.Location);
                                            if (p.X > Width / 2) {
                                                infoPanel.Location = new Point(p.X - infoPanel.Width - 25, p.Y - infoPanel.Height / 2 >= 0 ? p.Y - infoPanel.Height / 2 : 0);
                                            } else {
                                                infoPanel.Location = new Point(p.X + 25, p.Y - infoPanel.Height / 2 >= 0 ? p.Y - infoPanel.Height / 2 : 0);
                                            }
                                        }
                                        infoElement = clickElement;
                                        infoPanel.Visible = true;
                                    }
                                } else {
                                    MessageBox.Show("Marker info could not be loaded");
                                    infoPanel.Visible = false;
                                }
                            }
                        }
                        break;
                    case MOUSE_PLACE_MARKER:
                        PointF spacePoint = ScreenToSpacePoint(e.Location);
                        if(holdElement != null) {
                            if(holdElement.NetID < 0) { // New Marker
                                holdElement.Bounds = new RectangleF(spacePoint.X, spacePoint.Y, DEFAULT_MARKER_LENGTH, DEFAULT_MARKER_LENGTH);
                                holdElement.MapPoint = new PointF(spacePoint.X / (float)map.Width, spacePoint.Y / (float)map.Height);
                                holdElement.Z = _Scalar;

                                long newID = -1;

                                if (SQLManager.runInsert("vc_articles",
                                    new string[] { "title", "atlas", "atlas_id", "properties", "creator", "creation_time", "editor", "edit_time", "whitelist_protected", "whitelist" },
                                    new string[] {
                                    ((AtlasMarker)holdElement).Title,
                                    "1",
                                    _mID.ToString(),
                                    ((AtlasMarker)holdElement).GeneratePropertiesString(),
                                    UserManager.currentUser.userID.ToString(),
                                    "NOW()",
                                    UserManager.currentUser.userID.ToString(),
                                    "NOW()",
                                    "0",
                                    "~" + UserManager.currentUser.userID.ToString()
                                    },
                                    out newID
                                )) {
                                    TabletopElements.Add(holdElement);
                                    holdElement.NetID = newID;
                                    holdElement = null;
                                    MouseAction = MOUSE_STANDBY;
                                } else {
                                    MessageBox.Show("An error has occurred. Marker has not been placed");
                                }
                            } else { // Moving existing Marker
                                holdElement.Bounds = new RectangleF(spacePoint.X, spacePoint.Y, holdElement.Width, holdElement.Width);
                                holdElement.MapPoint = new PointF(spacePoint.X / (float)map.Width, spacePoint.Y / (float)map.Height);
                                holdElement.Z = _Scalar;

                                SQLManager.runUpdate(
                                    "vc_articles",
                                    new string[] { "properties" },
                                    new string[] { ((AtlasMarker)holdElement).GeneratePropertiesString() },
                                    "`id` = " + holdElement.NetID + " LIMIT 1"
                                );

                                holdElement = null;
                                MouseAction = MOUSE_STANDBY;
                            }
                        } else if(queuedArticle != null) { // Placing marker from Article Browser
                            AtlasMarker mark = new AtlasMarker();
                            mark.NetID = queuedArticle.netID;
                            mark.Bounds = new RectangleF(spacePoint.X, spacePoint.Y, DEFAULT_MARKER_LENGTH, DEFAULT_MARKER_LENGTH);
                            mark.MapPoint = new PointF(spacePoint.X / (float)map.Width, spacePoint.Y / (float)map.Height);
                            mark.Z = _Scalar;
                            mark.creator = queuedArticle.creator;
                            mark.useWhitelist = queuedArticle.useWhitelist;
                            mark.whitelistString = queuedArticle.whitelistString;
                            mark.Scales = false;
                            mark.Title = queuedArticle.name;
                            mark.bgSrc = "small_marker_blue";

                            queuedArticle.AddProperty("x", mark.MapPoint.X.ToString());
                            queuedArticle.AddProperty("y", mark.MapPoint.Y.ToString());
                            queuedArticle.AddProperty("icon", mark.bgSrc);

                            SQLManager.runUpdate(
                                "vc_articles",
                                new string[] { "properties", "atlas", "atlas_id" },
                                new string[] { queuedArticle.GeneratePropertiesString(), "1", _mID.ToString() },
                                "`id` = " + queuedArticle.netID + " LIMIT 1"
                            );

                            foreach(AtlasElement ae in TabletopElements) {
                                if(ae is AtlasMarker) {
                                    if(ae.NetID == mark.NetID) {
                                        TabletopElements.Remove(ae);
                                        break;
                                    }
                                }
                            }

                            TabletopElements.Add(mark);
                            queuedArticle = null;
                            MouseAction = MOUSE_STANDBY;
                        }
                        break;
                    case MOUSE_PLACE_BOUNDARY_POINT:
                        if(drawBoundaryPoints.Count > 2) {
                            Point originPoint = SpaceToScreenPoint(drawBoundaryPoints[0]);
                            if(Math.Abs(originPoint.X - e.X) + Math.Abs(originPoint.Y - e.Y) < 10) {
                                if (boundaryElement != null) {
                                    if (boundaryElement is AtlasMarker) {
                                        string boundProp = "";
                                        foreach (PointF bp in drawBoundaryPoints) {
                                            boundProp += bp.X + ":" + bp.Y + ";";
                                        }
                                        ((AtlasMarker)boundaryElement).SetProperty("boundary", boundProp);
                                        SQLManager.runUpdate(
                                            "vc_articles",
                                            new string[] { "properties" },
                                            new string[] { ((AtlasMarker)boundaryElement).GeneratePropertiesString() },
                                            "`id` = " + boundaryElement.NetID + " LIMIT 1"
                                        );
                                    }
                                }
                                MouseAction = MOUSE_STANDBY;
                                drawBoundaryPoints = null;
                                repaint = true;
                                break;
                            }
                        }
                        PointF newPoint = ScreenToSpacePoint(e.Location);
                        drawBoundaryPoints.Add(newPoint);
                        repaint = true;
                        break;
                    case MOUSE_MEASURE:
                        rulerStart = new PointF();
                        rulerEnd = new PointF();
                        break;
                }
                
            }
        }

        private void Atlas_MouseDoubleClick(object sender, MouseEventArgs e) {
            clicked = false;
            if(e.Button == MouseButtons.Left) {
                if(MouseAction == MOUSE_STANDBY) {
                    if (overElement != null) {
                        if (overElement is AtlasMarker) {
                            PageHandler.RequestArticleBrowser(((AtlasMarker)overElement).NetID);
                        }
                    }
                }
            }
        }

        private void Atlas_MouseWheel(object sender, MouseEventArgs e) {
            if(MouseIsOver) {
                PointF OldWorld = ScreenToSpacePoint(e.Location);

                float factor = 1;
                if (e.Delta > 0) {
                    factor = 1.1f;
                } else {
                    factor = 1f / 1.1f;
                }
                Scalar *= factor;
                if(Scalar < 0.02f) Scalar = 0.02f;
                if(Scalar > 2400) Scalar = 2400;

                Point Translated = SpaceToScreenPoint(OldWorld);
                Point Shift = new Point((int)((Translated.X - e.X)), (int)((Translated.Y - e.Y)));

                SetTableLocation(
                    TableX - (Shift.X / Scalar),
                    TableY - (Shift.Y / Scalar)
                );
                
                repaint = true;
            }
        }

        public PointF ScreenToSpacePoint(Point p) {
            return new PointF(
                ((float)p.X - (float)this.Width / 2f) / Scalar - TableX,
                ((float)p.Y - (float)this.Height / 2f) / Scalar - TableY
            );
        }

        private void Atlas_MouseEnter(object sender, EventArgs e) {
            MouseIsOver = true;
        }

        private void Atlas_Load(object sender, EventArgs e) {

        }

        private void Atlas_MouseLeave(object sender, EventArgs e) {
            MouseIsOver = false;
        }

        private void newMarkerToolStripMenuItem_Click(object sender, EventArgs e) {
            bool auth = false;
            if (UserManager.currentUser.userID > 0) {
                if(map.useWhitelist) {
                    if (UserManager.currentUser.userID == map.creator || WhitelistHandler.getCurrentUserPermissionLevel(map.whitelistString) >= 2) {
                        auth = true;
                    }
                } else {
                    if(UserManager.currentUser.userID == map.creator || UserManager.currentUser.userRank >= 3) {
                        auth = true;
                    }
                }
            }
            if(auth) {
                MouseAction = MOUSE_PLACE_MARKER;
                holdElement = new AtlasMarker();
                holdElement.NetID = -1;
                holdElement.Scales = false;
                ((AtlasMarker)holdElement).bgSrc = "small_marker_blue";

                using (var nameQueue = new SimpleMessageBox()) {
                    bool containsIllegal = false;
                    do {
                        nameQueue.Text = "New Marker Name";
                        var result = nameQueue.ShowDialog();
                        if (result == DialogResult.OK) {
                            containsIllegal = false;
                            foreach (char c in nameQueue.result) {
                                if (!legalTitleChars.Contains(c)) {
                                    containsIllegal = true;
                                    MessageBox.Show("Title contains illegal characters that must be removed. Legal characters include:\n\n" + legalTitleChars);
                                    break;
                                }
                            }
                            if (!containsIllegal) ((AtlasMarker)holdElement).Title = nameQueue.result;
                        } else {
                            holdElement?.Dispose();
                            holdElement = null;
                            MouseAction = MOUSE_STANDBY;
                            containsIllegal = false;
                        }
                    } while (containsIllegal);
                }
            }
        }

        private void moveMarkerToolStripMenuItem_Click(object sender, EventArgs e) {
            if(clickElement != null) {
                holdElement = clickElement;
                clickElement = null;

                MouseAction = MOUSE_PLACE_MARKER;
            }
        }

        private void deleteMarkerToolStripMenuItem_Click(object sender, EventArgs e) {
            if(clickElement != null) {
                if(clickElement is AtlasMarker) {
                    AtlasMarker target = (AtlasMarker)clickElement; // Click element may change during dialog, so store in another variable
                    target.bgSrc = "";
                    target.iconSrc = "";
                    using (DeleteMarkerDialog dmd = new DeleteMarkerDialog()) {
                        DialogResult result = dmd.ShowDialog();
                        if (result == (DialogResult)DeleteMarkerDialog.DELETE_MARK) {
                            SQLManager.runUpdate("vc_articles", 
                                new string[] { "atlas", "atlas_id", "properties" }, 
                                new string[] { "0", "0", target.GeneratePropertiesString() }, 
                                "`id` = " + target.NetID + " LIMIT 1"
                            );
                            TabletopElements.Remove(target);
                        } else if (result == (DialogResult)DeleteMarkerDialog.DELETE_MARK_AND_ARTICLE) {
                            SQLManager.runDelete("vc_articles", "`id` = " + target.NetID);
                            TabletopElements.Remove(target);
                        }
                    }
                }
            }
        }
        
        private void defineMarkerBoundsToolStripMenuItem_Click(object sender, EventArgs e) {
            if(clickElement != null) {
                drawBoundaryPoints = new List<PointF>();
                boundaryElement = clickElement;
                clickElement = null;

                MouseAction = MOUSE_PLACE_BOUNDARY_POINT;
            }
        }

        private void clearMarkerBoundaryToolStripMenuItem_Click(object sender, EventArgs e) {
            if(clickElement != null) {
                if(clickElement is AtlasMarker) {
                    ((AtlasMarker)clickElement).RemoveProperty("boundary");
                    SQLManager.runUpdate(
                        "vc_articles",
                        new string[] { "properties" },
                        new string[] { ((AtlasMarker)clickElement).GeneratePropertiesString() },
                        "`id` = " + clickElement.NetID + " LIMIT 1"
                    );
                }
            }
        }

        private void Atlas_KeyDown(object sender, KeyEventArgs e) {
            if(e.KeyCode == Keys.Escape) {
                if(MouseAction == MOUSE_PLACE_MARKER) {
                    holdElement?.Dispose();
                    holdElement = null;
                    queuedArticle = null;
                    MouseAction = MOUSE_STANDBY;
                    repaint = true;
                } else if(MouseAction == MOUSE_PLACE_BOUNDARY_POINT) {
                    if(drawBoundaryPoints.Count > 1) {
                        drawBoundaryPoints.RemoveAt(drawBoundaryPoints.Count - 1);
                        repaint = true;
                    } else {
                        drawBoundaryPoints = null;
                        MouseAction = MOUSE_STANDBY;
                    }
                }
            } else if(e.KeyCode == Keys.ShiftKey) {
                LeftShiftPressed = true;
            }
        }

        private void Atlas_KeyUp(object sender, KeyEventArgs e) {
            if(e.KeyCode == Keys.ShiftKey) {
                LeftShiftPressed = false;
                if(MouseAction == MOUSE_MEASURE) {
                    MouseAction = MOUSE_STANDBY;
                }
            }
        }

        public Point SpaceToScreenPoint(PointF p) {
            return new Point(
                (int)(Scalar * (p.X + TableX) + (this.Width/2)),
                (int)(Scalar * (p.Y + TableY) + (this.Height/2))
            );
        }

        public void AddMapFromURL(string url) {
            AtlasMap test = new AtlasMap();
            test.SetMapImgSrc(url);
            TabletopElements.Add(test);
            map = test;
            RefreshMarkers();
        }

        public void SetMapScale(float val) {
            map.scale = val;
        }
        
        public void RefreshMarkers() {
            SQLManager.runQuery("vc_articles",
                new string[] { "id", "title", "creator", "properties", "whitelist_protected", "whitelist" },
                " `atlas` = 1 AND `atlas_id` = " + _mID,
                this,
                SQLManager.LOAD_ARTICLE);
        }
        
        public void HandleData(int action, Dictionary<String, object> d) {
            if(action == SQLManager.LOAD_ARTICLE) {
                AtlasMarker mark = parser.parseAtlasMarker(d);
                mark.Bounds = new RectangleF(mark.MapPoint.X * map.Width, mark.MapPoint.Y * map.Height, DEFAULT_MARKER_LENGTH, DEFAULT_MARKER_LENGTH);
                if(mark.useWhitelist) {
                    if(mark.creator == UserManager.currentUser.userID || WhitelistHandler.getCurrentUserPermissionLevel(mark.whitelistString) > 0) {
                        TabletopElements.Add(mark);
                    } else {
                        mark.Dispose();
                    }
                } else {
                    TabletopElements.Add(mark);
                }
            }
        }

        protected override void OnPaint(PaintEventArgs e) {
            Graphics g = e.Graphics; // Shorthand reference
            Point p;
            Rectangle r = new Rectangle(0, 0, 1, 1);
            Brush boundaryBrush = new SolidBrush(Color.FromArgb(120, 200, 200, 200));
            Size s = new Size();
            Labels.Clear();

            if(map != null) {
                p = SpaceToScreenPoint(map.Location);
                r = new Rectangle(
                    p.X,
                    p.Y,
                    (int)(map.Width * (map.Scales ? Scalar : 1)),
                    (int)(map.Height * (map.Scales ? Scalar : 1))
                );
                map.PaintElement(g, r);
            }

            foreach(AtlasElement ae in TabletopElements) {
                if(ae is AtlasMarker) {
                    if(((AtlasMarker)ae).DrawBoundary) {
                        string pointsString = ((AtlasMarker)ae).GetProperty("boundary");
                        string[] splitPoints = pointsString.Split(new char[] {';'}, StringSplitOptions.RemoveEmptyEntries);
                        if(splitPoints.Length >= 3) {
                            PointF[] boundPoints = new PointF[splitPoints.Length];
                            for(int n = 0; n < splitPoints.Length; ++n) {
                                string[] values = splitPoints[n].Split(':');
                                if(values.Length == 2) {
                                    boundPoints[n] = SpaceToScreenPoint( new PointF((float.TryParse(values[0], out float fx) ? fx : 0), (float.TryParse(values[1], out float fy) ? fy : 0)));
                                }
                            }
                            g.FillPolygon(boundaryBrush, boundPoints);
                        }
                    }
                }
            }
            foreach (AtlasElement ae in TabletopElements) {
                if (ae == map) continue;
                if(ae.Visible) {
                    p = SpaceToScreenPoint(ae.Location);
                    switch (ae.TrackMode) {
                        case AtlasElement.TRACK_CENTER:
                            s = new Size((int)(ae.Size.Width * (ae.Scales ? Scalar : 1)), (int)(ae.Size.Height * (ae.Scales ? Scalar : 1)));
                            r = new Rectangle(
                                p.X - (s.Width / 2),
                                p.Y - (s.Height / 2),
                                s.Width,
                                s.Height
                            );

                            break;
                        case AtlasElement.TRACK_CORNER:
                            r = new Rectangle(
                                p.X,
                                p.Y,
                                (int)(ae.Width * (ae.Scales ? Scalar : 1)),
                                (int)(ae.Height * (ae.Scales ? Scalar : 1))
                            );
                            break;
                    }

                    if (g.ClipBounds.IntersectsWith(r)) {
                        ae.PaintElement(g, r);
                        if(ae is AtlasMarker) {
                            if(((AtlasMarker)ae).DrawLabel) {
                                Labels.Add(new Point(r.Right, r.Top), ((AtlasMarker)ae).Title);
                            }
                        }
                    }
                }
            }
            if(Labels.Count > 0) {
                foreach(Point lp in Labels.Keys) {
                    if (!String.IsNullOrWhiteSpace(Labels[lp])) {
                        GraphicsPath gp = new GraphicsPath(FillMode.Alternate);
                        gp.AddString(Labels[lp], labelFont.FontFamily, (int)FontStyle.Bold, g.DpiY * labelFont.Size / 72, new PointF(lp.X, lp.Y), new StringFormat());
                        g.SmoothingMode = SmoothingMode.AntiAlias;
                        using(Pen pen = new Pen(Color.Black, 5)) {
                            g.DrawPath(pen, gp);
                        }
                        g.FillPath(Brushes.White, gp);
                    }
                }
            }
            if(infoPanel.Visible) {
                if(infoElement != null) {
                    g.DrawLine(Pens.White, new Point(infoPanel.Location.X + infoPanel.Width / 2, infoPanel.Location.Y + infoPanel.Height / 2), 
                        SpaceToScreenPoint(infoElement.Location));
                }
            }
            
            if(rulerStart != rulerEnd) {
                Point screenRulerStart = SpaceToScreenPoint(rulerStart);
                Point screenRulerEnd = SpaceToScreenPoint(rulerEnd);
                Point screenRulerCenter = new Point((screenRulerStart.X + screenRulerEnd.X) / 2, (screenRulerStart.Y + screenRulerEnd.Y) / 2);
                g.DrawLine(Pens.White, screenRulerStart, screenRulerEnd);
                string rulerText = "";
                float rulerLength = (float)Math.Sqrt((
                    ((rulerStart.X - rulerEnd.X) * (rulerStart.X - rulerEnd.X)) +
                    ((rulerStart.Y - rulerEnd.Y) * (rulerStart.Y - rulerEnd.Y))));
                rulerLength *= map.scale;

                if(imperialMeasurements) {
                    rulerLength *= 3.28084f;
                    if (rulerLength >= 5280000) {
                        rulerText += (rulerLength / 5280).ToString("n0") + " mi.";
                    } else if (rulerLength >= 5280) {
                        rulerText += (rulerLength / 5280).ToString("n2") + " mi.";
                    } else {
                        rulerText += rulerLength.ToString("n0") + " ft.";
                    }
                } else {
                    if (rulerLength >= 1000000) {
                        rulerText += (rulerLength / 1000).ToString("n0") + "km";
                    } else if (rulerLength >= 1000) {
                        rulerText += (rulerLength / 1000).ToString("n2") + "km";
                    } else {
                        rulerText += rulerLength.ToString("n0") + "m";
                    }
                }
                
                if(!String.IsNullOrWhiteSpace(rulerText)) {
                    GraphicsPath gp = new GraphicsPath(FillMode.Alternate);
                    gp.AddString(rulerText, labelFont.FontFamily, (int)FontStyle.Bold, g.DpiY * labelFont.Size / 72, screenRulerCenter, new StringFormat());
                    g.SmoothingMode = SmoothingMode.AntiAlias;
                    using (Pen pen = new Pen(Color.Black, 5)) {
                        g.DrawPath(pen, gp);
                    }
                    g.FillPath(Brushes.White, gp);
                }
            }
            
            if(MouseAction == MOUSE_PLACE_MARKER) {
                g.DrawImage(crosshairImg, LastMousePoint.X - 12, LastMousePoint.Y - 12, 25, 25);
            }
            if(MouseAction == MOUSE_PLACE_BOUNDARY_POINT) {
                g.DrawImage(crosshairImg, LastMousePoint.X - 12, LastMousePoint.Y - 12, 25, 25);
                if (drawBoundaryPoints != null) {
                    if(drawBoundaryPoints.Count > 0) {
                        foreach(PointF bp in drawBoundaryPoints) {
                            Rectangle br = new Rectangle(SpaceToScreenPoint(bp), new Size(11, 11));
                            br.Offset(-6, -6);
                            g.FillEllipse(Brushes.White, br);
                        }
                    }
                    if(drawBoundaryPoints.Count > 1) {
                        for(int bi = 0; bi < drawBoundaryPoints.Count - 1; ++bi) {
                            g.DrawLine(Pens.White, SpaceToScreenPoint(drawBoundaryPoints[bi]), SpaceToScreenPoint(drawBoundaryPoints[bi + 1]));
                        }
                    }
                }
            }

            // Unit selection box
            g.DrawRectangle(Pens.White, unitSelectBounds);
            if(imperialMeasurements) {
                g.DrawString("ft", smallFont, Brushes.White, unitSelectBounds.Location);
            } else {
                g.DrawString("m", smallFont, Brushes.White, unitSelectBounds.Location);
            }
            
            base.OnPaint(e);
        }
    }
}
