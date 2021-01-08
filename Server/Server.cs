using System;
using System.Collections.Generic;
using ServerData;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using System.Net;
using System.Threading.Tasks;


namespace Server
{
    class Server
    {
        static Socket listenerSocket;
        static List<CliData> cli;
        static void Main(string[] args)
        {
            Console.WriteLine("Starting server on " + Packet.GetIpForAddress());
            listenerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            cli = new List<CliData>();

            IPEndPoint ip = new IPEndPoint(IPAddress.Parse(Packet.GetIpForAddress()), 4242);
            // On lie le listener socket a l'ip adress
            listenerSocket.Bind(ip);

            Thread listenThread = new Thread(ListenThread);

            // Démarage du serveur :
            listenThread.Start();

           
        }

        //listener - ecouter les client qui se connectent
        static void ListenThread()
        {
            for(; ;)// loop jusqu'a la fermeture du program
            { 
            listenerSocket.Listen(0);// 0 pour le backlog
            cli.Add(new CliData(listenerSocket.Accept()));//retourner la socket
        
            }
        }
        //clientdata thread - recevoir les data de tt les clients de maniere indépendante
        public static void Data_IN(object cSocket)// plus simple a passer en thread en tant qu'object
        {
            Socket clientSocket = (Socket)cSocket;
            byte[] Buffer;
            int readBytes;
            for(; ;)
            {
                try 
                { 
                Buffer = new byte[clientSocket.SendBufferSize];
                readBytes = clientSocket.Receive(Buffer);

                if(readBytes>0)
                {
                    //tenir les data
                    Packet packet = new Packet(Buffer);
                    DataManager(packet);


                }
                }
                catch(SocketException)
                {
                    Console.WriteLine("Un client s'est déconnecté");
                    Console.WriteLine("Le serveur ferme aussi");
                    Thread.Sleep(1000);
                    Environment.Exit(0);
                    
                }
            }


        }
        //data manager
        public static void DataManager(Packet p)
        {
            switch (p.packetType)
            {
                case PacketType.Chat:
                    foreach(CliData c in cli)
                    { c.clientSocket.Send(p.ToBytes()); }
                    break;

                case PacketType.Login:
                    foreach (CliData c in cli)
                    { c.clientSocket.Send(p.ToBytes()); }
                    break; 
            }
        }
    }
    class CliData
    {
        public Socket clientSocket;
        public Thread clientThread;
        public string id;

        public CliData()
        {
            id = Guid.NewGuid().ToString();
            clientThread = new Thread(Server.Data_IN);
            clientThread.Start(clientSocket);
            SendRegistrationPacket();
        }
        public CliData(Socket clientSocket)
        {
            this.clientSocket = clientSocket;
            id = Guid.NewGuid().ToString();
            clientThread = new Thread(Server.Data_IN);
            clientThread.Start(clientSocket);
            SendRegistrationPacket();
        }

        public void SendRegistrationPacket()
        {
            Packet p = new Packet(PacketType.Registration, "server");
            p.Gdata.Add(id);
            clientSocket.Send(p.ToBytes());

        }
    }
}
