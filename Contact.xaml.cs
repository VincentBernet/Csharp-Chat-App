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
    /// Interaction logic for Contact.xaml
    /// </summary>
    public partial class Contact : Window
    {

        public static Socket master;
        public static string ip;
        public static string LastMessage;
        public static string ConditionChatRoomSpecific;
        public static Boolean Connection = false;
        public static string PseudoDemande;
        public string path = @"C:\Users\Vincent\source\repos\ChatRoomProject\Ressources\Login.txt";

        public Contact()
        {
            InitializeComponent();

            // Création d'un timer qui permettra plus loin de refresh une fonction toute les 0.5 secondes
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(0.5);
            timer.Tick += dispatcherTimerReloadFunction_Tick;
            timer.Start();

            master = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            ip = "192.168.56.1";

            // On se connecte, si la connexion ne fonctionne pas message d'erreur, l'utilisateur peut réessayer en rentrant une adresse ip valide / en activant son serveur

            try
            {
                IPEndPoint ipe = new IPEndPoint(IPAddress.Parse(ip), 4242);
                master.Connect(ipe);
                MessageBox.Show("Connexion au serveur!", "Réussite", MessageBoxButton.OK, MessageBoxImage.Information);

            }
            catch // Si la connexion échoue, message d'erreur puis retour à notre window
            {
                MessageBox.Show("Erreur de connexion au serveur, veillez réessayer en insérant la bonne adresse ip du serveur !", "Echec", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            // Thread avec le serveur
            Thread t = new Thread(Data_IN);
            t.Start();
        }
            private void ConnectDesti_Click(object sender, RoutedEventArgs e)
        {
                foreach (string line in File.ReadLines(path))
                {
                    if (line.Contains(SelectionDestinataire.Text + ":"))
                    {
                        Packet p = new Packet(PacketType.Chat, ip);
                        p.Gdata.Add((App.Current as App).Session);
                        p.Gdata.Add(SelectionDestinataire.Text);
                        master.Send(p.ToBytes());//send to server
                        (App.Current as App).SessionDestinataire = SelectionDestinataire.Text;
                        MessageBox.Show("Vous avez envoyé une demande de conversation privé à : " + (App.Current as App).SessionDestinataire, "Invation envoyé", MessageBoxButton.OK, MessageBoxImage.Information);
                        return;
                    }
                }
            MessageBox.Show("Ce destinataire n'existe pas dans notre base de donnée, veillez indiqué un destinataire existant et valide", "Erreur", MessageBoxButton.OK, MessageBoxImage.Information);
            return;
        }


        private void dispatcherTimerReloadFunction_Tick(object sender, EventArgs e)
        {
            if ((ConditionChatRoomSpecific == (App.Current as App).Session))
             {
                MessageBoxResult result = MessageBox.Show("Hey l'utilisateur "+PseudoDemande+" souhaite rentrer en contact avec vous, êtes vous partant ?","Nouveau contact", MessageBoxButton.YesNo);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        (App.Current as App).SessionDestinataire = PseudoDemande;
                        Packet p = new Packet(PacketType.Chat, ip);
                        p.Gdata.Add(ConditionChatRoomSpecific);
                        p.Gdata.Add(PseudoDemande);
                        master.Send(p.ToBytes());//send to server

                        ConversationPrivée NewWindow = new ConversationPrivée();
                        NewWindow.Top = this.Top;
                        NewWindow.Left = this.Left;
                        NewWindow.Show();
                        this.Close();
                        break;
                    case MessageBoxResult.No:
                        MessageBox.Show("Demande refusée!", "Refus Conversation");
                        break;
                }
                ConditionChatRoomSpecific = "";
                return;
            }
        }
        

        // Boutton de Fermeture de l'application
        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        // Event : Sélectionner l'application pour la déplacer
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

                    ip = p.Gdata[0];
                    break;

                case PacketType.Chat:
                    PseudoDemande = p.Gdata[0];
                    ConditionChatRoomSpecific = p.Gdata[1];
                    
                    LastMessage = (p.Gdata[0] + " : " + p.Gdata[1]);
                    break;

            }

        }
    }
}

