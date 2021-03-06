using System;
using System.IO;
using System.Text;
using Server;

namespace Server.Messages {
    public struct Connect {
        public const Int32 MESSAGE_ID = (Int32)eMessageType.CONNECT;
        public byte[] Serialise() {
            MemoryStream w = new MemoryStream();
            w.Write(BitConverter.GetBytes(MESSAGE_ID), 0, 4);
            return w.ToArray();
        }
    }
}