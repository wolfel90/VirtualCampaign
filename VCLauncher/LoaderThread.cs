using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Windows.Forms;

namespace VCLauncher {
    class LoaderThread {
        
        public Label callbackLabel { get; set; }

        public void doVersionCheck() {
            int webVersion = 0;
            int localVersion = 0;

            sendStatus("Loading...");

            List<string> properties = new List<string>();
            using (var wc = new WebClient()) {
                wc.CachePolicy = new System.Net.Cache.RequestCachePolicy(System.Net.Cache.RequestCacheLevel.BypassCache);
                wc.Headers.Add("Cache-Control", "no-cache");
                Stream stream = wc.OpenRead("http://ayaseye.com/vc/VersionCheck.txt");
                StreamReader reader = new StreamReader(stream);
                String str;
                while((str = reader.ReadLine()) != null) {
                    properties.Add(str);
                }
                reader.Dispose();
                stream.Dispose();
            }
            string baseline = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase).Replace("file:" + System.IO.Path.DirectorySeparatorChar, "");

            if(File.Exists(baseline + Path.DirectorySeparatorChar + "VersionCheck.txt")) {
                FileStream fs = new FileStream(baseline + Path.DirectorySeparatorChar + "VersionCheck.txt", FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                StreamReader reader = new StreamReader(fs, System.Text.Encoding.UTF8, true, 128);
                string line;
                if((line = reader.ReadLine()) != null) {
                    string[] vS = line.Split('.');
                    if(vS.Length == 3) {
                        for (int n = 0; n < vS.Length; ++n) {
                            int v;
                            if (Int32.TryParse(vS[n], out v)) {
                                localVersion += v * (int)Math.Pow(10, (double)(2 - n) * 3);
                            }
                        }
                    }
                }
                reader.Dispose();
                fs.Dispose();
            }
            Console.Out.WriteLine("Local Version: " + localVersion);

            string[] split = properties.ToArray();
            sendStatus("Checking dependencies...");
            for (int i = 0; i < split.Length; ++i) {
                if(i == 0) { // Parse web version
                    
                    string[] split2 = split[i].Split('.');
                    if (split2.Length == 3) {
                        
                        for(int i2 = 0; i2 < split2.Length; ++i2) {
                            int v;
                            if(Int32.TryParse(split2[i2], out v)) {
                                webVersion += v * (int)Math.Pow(10, (double)(2 - i2) * 3);
                                
                            }
                        }
                    }
                    Console.Out.WriteLine("Web Version: " + webVersion);
                } else { // Recover unfound dependencies
                    if(!File.Exists(baseline + Path.DirectorySeparatorChar + split[i])) {
                        using(var wc = new WebClient()) {
                            wc.DownloadFile("http://ayaseye.com/vc/" + split[i], baseline + Path.DirectorySeparatorChar + split[i]);
                            wc.Dispose();
                        }
                    }
                }
            }
            
            if(webVersion > localVersion) {
                sendStatus("New version found. Downloading new version...");
                using(var wc = new WebClient()) {
                    wc.CachePolicy = new System.Net.Cache.RequestCachePolicy(System.Net.Cache.RequestCacheLevel.BypassCache);
                    wc.Headers.Add("Cache-Control", "no-cache");
                    wc.DownloadFile("http://ayaseye.com/vc/VirtualCampaign.exe", baseline + Path.DirectorySeparatorChar + "VirtualCampaign.exe");
                    wc.DownloadFile("http://ayaseye.com/vc/VersionCheck.txt", baseline + Path.DirectorySeparatorChar + "VersionCheck.txt");
                }
            }

            sendStatus("Launching...");
            System.Diagnostics.Process.Start(baseline + Path.DirectorySeparatorChar + "VirtualCampaign.exe");
            System.Windows.Forms.Application.Exit();
        }

        private void sendStatus(string text) {
            if(callbackLabel != null) {
                if(callbackLabel.InvokeRequired) {
                    var d = new SplashPage.CallDelegate(sendStatus);
                    callbackLabel.BeginInvoke(d, new object[] { text });
                } else {
                    callbackLabel.Text = text;
                    callbackLabel.Update();
                }
            }
        }
    }
}
