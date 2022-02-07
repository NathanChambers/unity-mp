using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Server {

    public enum eMessageType {
        UNKNOWN = 0,
        CONNECT = 1,
    }

    public class SocketConnection : SocketAsyncEventArgs {
        protected override void OnCompleted(SocketAsyncEventArgs e) {
            base.OnCompleted(e);

            Console.WriteLine(e.RemoteEndPoint);
        }
    }

    public class SocketEvent : SocketAsyncEventArgs {

    }

    public static class EntryPoint {
        private static int port = 11000;
        private static Socket server = null;
        private static SocketConnection acceptConnection = new SocketConnection();
        private static void Main(string[] args) {
            UdpClient listener = new UdpClient(port);
            IPEndPoint endpoint = new IPEndPoint(IPAddress.Any, port);

            try {
                while(true) {
                    byte[] packet = listener.Receive(ref endpoint);
                    Console.WriteLine(endpoint.Address.ToString());
                    
                    eMessageType type = (eMessageType)BitConverter.ToInt32(packet, 0);
                    HandleMessage(type, packet);
                    Console.WriteLine($"Raw: {Encoding.ASCII.GetString(packet, 0, packet.Length)}");
                }
            } catch(SocketException e) {
                Console.WriteLine(e);
            } finally {
                listener.Close();
            }
        }

        private static void HandleMessage(eMessageType type, byte[] packet) {
            switch(type) {
                case eMessageType.CONNECT: {
                    var msg = Messages.Connect.Parse(packet);
                    Console.WriteLine(msg.Port);
                    break;
                }
            }
        }
    }
}
