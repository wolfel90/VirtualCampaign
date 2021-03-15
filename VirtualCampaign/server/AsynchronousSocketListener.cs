using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace VirtualCampaign.server {
    public class ServerStateObject {
        public Socket workSocket = null;
        public const int BufferSize = 1024;
        public byte[] buffer = new byte[BufferSize];
        public StringBuilder sb = new StringBuilder();
    }

    public class AsynchronousSocketListener {
        private const int port = 21025;
        public static ManualResetEvent allDone = new ManualResetEvent(false);
        private static List<Thread> activeListeners;

        static AsynchronousSocketListener() {
            activeListeners = new List<Thread>();
        }

        public AsynchronousSocketListener() {}

        public static void ShutdownActiveClients() {
            foreach (Thread t in activeListeners) {
                //if (t.IsAlive) {
                    t.Abort();
                //}
            }
        }

        public void StartServerThread() {
            Thread t = new Thread(new ThreadStart(StartListening));
            t.Start();
        }

        public void StartListening() {
            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[0];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, port);

            Socket listener = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            try {
                listener.Bind(localEndPoint);
                listener.Listen(100);

                while(true) {
                    allDone.Reset();

                    Console.Out.WriteLine("Waiting for a connection...");
                    listener.BeginAccept(new AsyncCallback(AcceptCallback), listener);
                    allDone.WaitOne();
                }
            } catch(Exception x) {
                Console.Out.WriteLine(x.ToString());
            }
        }

        public void AcceptCallback(IAsyncResult ar) {
            Console.Out.WriteLine("Server accepting callback...");
            allDone.Set();

            Socket listener = (Socket)ar.AsyncState;
            Socket handler = listener.EndAccept(ar);

            ServerStateObject state = new ServerStateObject();
            state.workSocket = handler;
            handler.BeginReceive(state.buffer, 0, ServerStateObject.BufferSize, 0, new AsyncCallback(ReadCallback), state);
        }

        public void ReadCallback(IAsyncResult ar) {
            Console.Out.WriteLine("Server reading callback...");
            String content = String.Empty;

            ServerStateObject state = (ServerStateObject)ar.AsyncState;
            Socket handler = state.workSocket;

            int bytesRead = handler.EndReceive(ar);

            if(bytesRead > 0) {
                state.sb.Append(Encoding.ASCII.GetString(state.buffer, 0, bytesRead));
                content = state.sb.ToString();

                if(content.IndexOf("<EOF>") > -1) {
                    Console.Out.WriteLine("Read {0} bytes from socket. \n Data : {1}", content.Length, content);
                    Send(handler, content);
                } else {
                    handler.BeginReceive(state.buffer, 0, ServerStateObject.BufferSize, 0, new AsyncCallback(ReadCallback), state);
                }
            }
        }

        private void Send(Socket handler, String data) {
            Console.Out.WriteLine("Server sending...");
            byte[] byteData = Encoding.ASCII.GetBytes(data);
            handler.BeginSend(byteData, 0, byteData.Length, 0, new AsyncCallback(SendCallback), handler);
        }

        private void SendCallback(IAsyncResult ar) {
            Console.Out.WriteLine("Server sending callback...");
            try {
                Socket handler = (Socket)ar.AsyncState;
                int bytesSent = handler.EndSend(ar);
                Console.Out.WriteLine("Sent {0} bytes to client", bytesSent);

                handler.Shutdown(SocketShutdown.Both);
                handler.Close();
            } catch(Exception x) {
                Console.Out.WriteLine(x.ToString());
            }
        }
    }
}
