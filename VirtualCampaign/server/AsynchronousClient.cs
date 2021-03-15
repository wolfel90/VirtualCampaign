using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace VirtualCampaign.server {
    public class ClientStateObject {
        public Socket workSocket = null;
        public const int BufferSize = 1024;
        public byte[] buffer = new byte[BufferSize];
        public StringBuilder sb = new StringBuilder();
    }
    

    public class AsynchronousClient {
        private const int port = 21025;
        public string message = "This is a test";
        private static ManualResetEvent connectDone = new ManualResetEvent(false);
        private static ManualResetEvent sendDone = new ManualResetEvent(false);
        private static ManualResetEvent receiveDone = new ManualResetEvent(false);
        //private IPAddress hostIP;
        private static List<Thread> activeClients;

        private static String response = String.Empty;

        static AsynchronousClient() {
            activeClients = new List<Thread>();
        }

        public AsynchronousClient() {}

        // This is a test method. Dunno if this shit should work
        public void RunClient() {
            Thread t = new Thread(new ThreadStart(StartClient));
            activeClients.Add(t);
            t.Start();
        }

        public static void ShutdownActiveClients() {
            foreach(Thread t in activeClients) {
                //if(t.IsAlive) {
                    t.Abort();
                //}
            }
        }

        public void StartClient() {
            try {
                Console.Out.WriteLine("Starting Client...");
                IPHostEntry ipHostInfo = Dns.GetHostEntry("72.208.128.11");
                IPAddress ipAddress = ipHostInfo.AddressList[0];
                //ipAddress = IPAddress.Parse("72.208.128.11");
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, port);

                Socket client = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                
                client.BeginConnect(remoteEP, new AsyncCallback(ConnectCallback), client);
                connectDone.WaitOne();

                Send(client, message + "<EOF>");
                sendDone.WaitOne();

                Receive(client);
                receiveDone.WaitOne();

                Console.Out.WriteLine("Response received : {0}", response);

                client.Shutdown(SocketShutdown.Both);
                client.Close();
            } catch (Exception x) {
                Console.Out.WriteLine(x.ToString());
            }
        }

        private void ConnectCallback(IAsyncResult ar) {
            Console.Out.WriteLine("Client connecting callback...");
            try {
                Socket client = (Socket)ar.AsyncState;

                client.EndConnect(ar);

                Console.Out.WriteLine("Socket connected to {0}", client.RemoteEndPoint.ToString());

                connectDone.Set();
            } catch(Exception x) {
                Console.Out.WriteLine(x.ToString());
            }
        }

        private void Receive(Socket client) {
            Console.Out.WriteLine("Client receiving...");
            try {
                ClientStateObject state = new ClientStateObject();
                state.workSocket = client;

                client.BeginReceive(state.buffer, 0, ClientStateObject.BufferSize, 0, new AsyncCallback(ReceiveCallback), state);
                
            } catch(Exception x) {;
                Console.Out.WriteLine(x.ToString());
            }
        }

        private void ReceiveCallback(IAsyncResult ar) {
            Console.Out.WriteLine("Client receiving callback...");
            try {
                ClientStateObject state = (ClientStateObject)ar.AsyncState;
                Socket client = state.workSocket;

                int bytesRead = client.EndReceive(ar);
                if(bytesRead > 0) {
                    state.sb.Append(Encoding.ASCII.GetString(state.buffer, 0, bytesRead));
                    client.BeginReceive(state.buffer, 0, ClientStateObject.BufferSize, 0, new AsyncCallback(ReceiveCallback), state);
                } else {
                    if(state.sb.Length > 1) {
                        response = state.sb.ToString();
                    }
                    receiveDone.Set();
                }
            } catch(Exception x) {
                Console.Out.WriteLine(x.ToString());
            }
        }

        private void Send(Socket client, String data) {
            byte[] byteData = Encoding.ASCII.GetBytes(data);
            client.BeginSend(byteData, 0, byteData.Length, 0, new AsyncCallback(SendCallback), client);
            Console.Out.WriteLine("Client Sending...");
        }

        private void SendCallback(IAsyncResult ar) {
            Console.Out.WriteLine("Client Sending callback...");
            try {
                Socket client = (Socket)ar.AsyncState;
                int bytesSent = client.EndSend(ar);
                Console.Out.WriteLine("Sent {0} bytes to server", bytesSent);
                sendDone.Set();
            } catch(Exception x) {
                Console.Out.WriteLine(x.ToString());
            }
        }
    }
}
