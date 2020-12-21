using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;  
using System.Windows.Shapes;
using System.IO;
using System.Net.Sockets;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using ServerData;
using System.Windows.Threading;





namespace ChatRoomProject
{
    /// <summary>
    /// Interaction logic for ChatRoom.xaml
    /// </summary>
   
    public partial class ChatRoom : Window
    {
        public static Socket master;
        public static string name;
        public static string id;
        public static string LastMessage;
        public static string LastMessageDoNotRepeat;
        public static string ConditionChatRoomSpecific;
        public static Boolean Connection = false;
        public ChatRoom()
        {
            InitializeComponent();

            IpAdresstextBox.Text = Convert.ToString((App.Current as App).SessionChatRoom);

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += dispatcherTimerReloadFunction_Tick;
            timer.Start();

        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void dispatcherTimerReloadFunction_Tick(object sender, EventArgs e)
        {
            if (ConditionChatRoomSpecific == (App.Current as App).SessionChatRoom)
            {
                if (LastMessageDoNotRepeat != LastMessage)
                {
                    ChatScreentextBox.Text = ChatScreentextBox.Text + DateTime.Now.ToLongTimeString() + " | " + LastMessage + "\n";
                    LastMessageDoNotRepeat = LastMessage;
                }
            }
        }

        // Event permettant de DragMove l'application
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                this.DragMove();
        }

        // Connection au serveur avec l'adresse ip sélectionner précédemment grace au simili de variable global SessionChatRoom
        private void Connectbutton_Click(object sender, RoutedEventArgs e)
        {
            master = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            string ip = "192.168.56.1";

            // On se connecte, si la connexion ne fonctionne pas message d'erreur, l'utilisateur peut réessayer en rentrant une adresse ip valide / en activant son serveur

            try
            {
                IPEndPoint ipe = new IPEndPoint(IPAddress.Parse(ip), 4242);
                master.Connect(ipe);
                MessageBox.Show("Connection à la ChatRoom \""+ (App.Current as App).SessionChatRoom+"\"", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);
                Connection = true;

                Packet p = new Packet(PacketType.Chat, id);
                string SessionName = (App.Current as App).Session;
                p.Gdata.Add("");
                p.Gdata.Add("Un nouveau membre a rejoint la chatroom: Bienvenue "+SessionName);
                p.Gdata.Add(Convert.ToString((App.Current as App).SessionChatRoom));
                master.Send(p.ToBytes());//send to server
                LastMessage = (p.Gdata[0]+ p.Gdata[1]);
            }
            catch//check si on ne peux pas se co et reboucle
            {
                MessageBox.Show("Erreur de connexion a l'host, cette ChatRoom n'existe pas !", "Echec", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            // Thread avec le serveur
            Thread t = new Thread(Data_IN);
            t.Start();
        }

        // "Réfraichissement" de notre page pour afficher les messages des autres utilisateurs, il faut que je trouve un event automatique qui s'active tout les X milisecondes.  
        // Envoie de notre message au serveur, modification adéquate de notre propre ChatRoom
        private void Sendbutton_Click(object sender, RoutedEventArgs e)
        { 
            string input = MessagetextBox.Text;
            
            if (Connection != true)
            {
                MessageBox.Show("Pas de connexion détecté, veillez vous connecter à l'host d'abord !", "Impossibilité d'envoyer un message", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            Packet p = new Packet(PacketType.Chat, id);
            string SessionName = (App.Current as App).Session;
            p.Gdata.Add(SessionName);// get name
            p.Gdata.Add(input);//get input
            p.Gdata.Add(Convert.ToString((App.Current as App).SessionChatRoom));
            master.Send(p.ToBytes());//send to server
           
        }

       




        void Data_IN()
        {
            byte[] Buffer;
            int readByte;

            for (; ; )
            {
                Buffer = new byte[master.SendBufferSize];
                readByte = master.Receive(Buffer);

                if (readByte > 0)
                {
                    DataManager(new Packet(Buffer));
                }
            }

        }

        void DataManager(Packet p)
        {
            
            switch (p.packetType)
            {
                case PacketType.Registration:
                    
                    id = p.Gdata[0];
                    break;

                case PacketType.Chat:
                    ConditionChatRoomSpecific = (p.Gdata[2]);
                    LastMessage = (p.Gdata[0] + " : " + p.Gdata[1]);
                    break;

            }

        }






    }
    }












