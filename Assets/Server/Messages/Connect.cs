using System;
using System.IO;
using System.Text;
using Server;

namespace Server.Messages {
    public struct Connect {
        public const Int32 MESSAGE_ID = (Int32)eMessageType.CONNECT;
        public Int32 Port;
        public byte[] Address;

        public static Connect Parse(byte[] packet) {
            return new Connect {
                Port = BitConverter.ToInt32(packet, 4),
                Address = new ArraySegment<byte>(packet, 12, BitConverter.ToInt32(packet, 8)).Array,
            };
        }

        public byte[] Serialise() {
            MemoryStream w = new MemoryStream();
            w.Write(BitConverter.GetBytes(MESSAGE_ID), 0, 4);
            w.Write(BitConverter.GetBytes(Port), 0, 4);
            w.Write(BitConverter.GetBytes(Address.Length), 0, 4);
            w.Write(Address, 0, Address.Length);
            return w.ToArray();
        }
    }
}