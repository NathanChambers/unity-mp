using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Collections.Generic;

namespace Server {

    public enum eMessageType {
        UNKNOWN = 0,
        CONNECT = 1,
        DISCONNECT = 2,
    }

    public static class EntryPoint {
        private static int port = 11000;
        private static Socket server = null;
        private static List<IPEndPoint> connections = new List<IPEndPoint>();

        private static void Main(string[] args) {
            UdpClient listener = new UdpClient(port);
            IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, port);

            try {
                while(true) {
                    byte[] packet = listener.Receive(ref remoteEndPoint);
                    eMessageType type = (eMessageType)BitConverter.ToInt32(packet, 0);
                    HandleMessage(remoteEndPoint, type, packet);
                    Broadcast(remoteEndPoint);
                }
            } catch(SocketException e) {
                Console.WriteLine(e);
            } finally {
                listener.Close();
            }
        }

        private static void HandleMessage(IPEndPoint remoteEndPoint, eMessageType type, byte[] packet) {
            switch(type) {
                case eMessageType.CONNECT: {
                    Console.WriteLine($"MSG [{remoteEndPoint.Address}:{remoteEndPoint.Port}] Connected");
                    connections.Add(remoteEndPoint);
                    break;
                }

                case eMessageType.DISCONNECT: {
                    Console.WriteLine($"MSG [{remoteEndPoint.Address}:{remoteEndPoint.Port}] Disconnected");
                    connections.Remove(remoteEndPoint);
                    break;
                }

                default: {
                    Console.WriteLine($"MSG [{remoteEndPoint.Address}:{remoteEndPoint.Port}] {Encoding.ASCII.GetString(packet)}");
                    break;
                }
            }
        }

        private static void Broadcast(IPEndPoint remoteEndPoint) {
            var connection = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            byte[] buff = Encoding.ASCII.GetBytes($"Message From: {remoteEndPoint.Address}:{remoteEndPoint.Port}");
            connection.SendTo(buff, remoteEndPoint);
        }
    }
}
