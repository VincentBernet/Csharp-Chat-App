
using System;
using System.Collections.Generic;
using System.Net;
using ServerData;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace ServerData
{
    [Serializable]//on supprime l'exception car c'est pas evident
    public class Packet
    {
        public List<string> Gdata;
        public List<string> Ldata;
        public int packetInt;
        public bool packetBool;
        public string senderID;
        public PacketType packetType;

        public Packet(PacketType type, string senderID)
        {
            Gdata = new List<string>();
            Ldata = new List<String>();
            this.senderID = senderID;// pour la securité 
            this.packetType = type;
        }

        public Packet(byte[] packetbytes)
        {
            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream(packetbytes);

            
            Packet p = (Packet)bf.Deserialize(ms);
            ms.Close();// fermer le stream
            this.Gdata = p.Gdata;
            this.Ldata = p.Ldata;
            this.packetInt = p.packetInt;
            this.packetBool = p.packetBool;
            this.senderID = p.senderID;
            this.packetType = p.packetType;
            

        }

        public byte[] ToBytes()
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (var ms = new MemoryStream())
            {
                bf.Serialize(ms, this);
                return ms.ToArray();
            }
            /*
            BinaryFormatter bs = new BinaryFormatter();// mieux que le jason 
            MemoryStream ms = new MemoryStream();
            

            bs.Serialize(ms, this);// on renvois aux objets contenus

            byte[] bytes = ms.ToArray();
            
            ms.Close();// toujours couper les streams 
            return bytes;*/
        }

        public static string GetIpForAddress()
        {
            IPAddress[] IPA = Dns.GetHostAddresses(Dns.GetHostName());
            
            foreach (IPAddress a in IPA)
            {
                if (a.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    return a.ToString();

                 
            }
            return "127.0.0.1";
        }

    }
    public enum PacketType 
    {

    Registration,
    Chat,
    Login

    }
}