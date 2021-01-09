using System;
using System.Windows;
using System.Windows.Input;
using System.Net.Sockets;
using System.Net;

using System.Threading;
using ServerData;
using System.Windows.Threading;

namespace ChatRoomProject
{

   // Page : Conversation Privée
    public partial class ConversationPrivée : Window
    {
        public static Socket master;
        public static string name;
        public static string id;
        public static string LastMessage;
        public static string LastMessageDoNotRepeat;
        public static string ConditionDmSpecific;
        public static Boolean Connection = false;
        public ConversationPrivée()
        {
            InitializeComponent();

            master = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            string ip = "192.168.56.1";
            Thread.Sleep(1500);
            // On se connecte, si la connexion ne fonctionne pas message d'erreur, l'utilisateur peut réessayer en rentrant une adresse ip valide / en activant son serveur
            try
            {
                IPEndPoint ipe = new IPEndPoint(IPAddress.Parse(ip), 4242);
                master.Connect(ipe);
                DestinatiretextBox.Text = Convert.ToString((App.Current as App).SessionDestinataire);
                Connection = true;

            }
            catch // Si la connexion échoue, message d'erreur puis retour à notre window
            {
                MessageBox.Show("Erreur de connexion a l'host, veillez réessayer en insérant la bonne adresse ip du serveur !", "Echec", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            // Thread avec le serveur
            Thread t = new Thread(Data_IN);
            t.Start();


            // Création d'un timer qui permettra plus loin de refresh une fonction toute les 0.5 secondes
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(0.5);
            timer.Tick += Reload;
            timer.Start();
        }

        // Fonction pour afficher les messages dans la chatbox, se reload toutes les 0.5 secondes
        private void Reload(object sender, EventArgs e)
        {
            if (ConditionDmSpecific == (App.Current as App).SessionDestinataire + (App.Current as App).Session)
            {
                if (LastMessageDoNotRepeat != LastMessage)
                {
                    ChatScreentextBox.Text = ChatScreentextBox.Text + DateTime.Now.ToLongTimeString() + " | " + (App.Current as App).SessionDestinataire + " : " + LastMessage + "\n";
                    LastMessageDoNotRepeat = LastMessage;
                }
            }
            else if (ConditionDmSpecific == (App.Current as App).Session + (App.Current as App).SessionDestinataire)
            { 
                if (LastMessageDoNotRepeat != LastMessage)
                {
                    ChatScreentextBox.Text = ChatScreentextBox.Text + DateTime.Now.ToLongTimeString() + " | " + (App.Current as App).Session + " : " + LastMessage + "\n";
                    LastMessageDoNotRepeat = LastMessage;
                }
            }
        }

        // Fonction d'envoi du message au serveur
        private void Sendbutton_Click(object sender, RoutedEventArgs e)
        {
            string input = MessagetextBox.Text;
            // Vérification que nous sommes bien connecté au serveur
            if (Connection != true)
            {
                MessageBox.Show("Pas de connexion détecté, veillez vous connecter au serveur d'abord !", "Impossibilité d'envoyer un message", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            Packet p = new Packet(PacketType.Chat, id);
            p.Gdata.Add((App.Current as App).SessionDestinataire + (App.Current as App).Session);
            p.Gdata.Add(MessagetextBox.Text);
            master.Send(p.ToBytes());  // Envoi du nom des interlocuteurs concaténés puis du message au serveur
            MessagetextBox.Text = "";  // On libère notre champ d'envoi après l'envoie du dit message
        }

        // Boutton de Fermeture de l'application
        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        // Event permettant de déplacer (DragMove) l'application
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                this.DragMove();
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
                    ConditionDmSpecific = p.Gdata[0];
                    LastMessage = p.Gdata[1];
                    break;

            }

        }
    }
}
