using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Text;


public class NetworkController {

    public System.Action<string> Receive;

    private IPEndPoint endpoint = null;
    private Socket connection = null;
    private int port = 0;
    private string address = "";

    public NetworkController(string address, int port) {
        endpoint = new IPEndPoint(IPAddress.Parse(address), port);
        connection = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
    }

    public void Send(string message) {
        //byte[] buff = Encoding.ASCII.GetBytes(message);
        var msg = new Server.Messages.Connect {
            Address = IPAddress.Parse("101.190.197.138").GetAddressBytes(),
            Port = 11000,
        };

        var buff = msg.Serialise();

        connection.SendTo(buff, endpoint);
    }

    public async void Listen() {
        UdpClient listener = new UdpClient(port);
        IPEndPoint group = new IPEndPoint(IPAddress.Any, port);

        try {
            while(true) {
                Console.WriteLine("Waiting for broadcast");
                byte[] bytes = listener.Receive(ref group);

                Console.WriteLine($"Received broadcast from {group} :");
                Console.WriteLine($" {Encoding.ASCII.GetString(bytes, 0, bytes.Length)}");
                await Task.Yield();
            }
        } catch(SocketException e) {
            Console.WriteLine(e);
        } finally {
            listener.Close();
        }
    }
}
