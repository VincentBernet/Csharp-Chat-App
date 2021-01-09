using System;
using System.Windows;
using System.Windows.Input;
using System.IO;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using ServerData;
using System.Windows.Threading;


namespace ChatRoomProject
{

    public partial class Contact : Window
    {

        public static Socket master;
        public static string ip;
        public static string LastMessage;
        public static string ConditionChatRoomSpecific;
        public static Boolean Connection = false;
        public static string PseudoDemande;
        public static Boolean ContactWindowAlive = true;
        public string path = @"C:\Users\Vincent\source\repos\ChatRoomProject\Ressources\Login.txt";

        public Contact()
        {
            InitializeComponent();

            // Création d'un timer qui permettra plus loin de refresh une fonction toute les 2 secondes
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(2);
            timer.Tick += dispatcherTimerReloadFunction_Tick;
            timer.Start();

            master = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            ip = "192.168.56.1";

            // On se connecte, si la connexion ne fonctionne pas message d'erreur et fermeture de l'application
            try
            {
                IPEndPoint ipe = new IPEndPoint(IPAddress.Parse(ip), 4242);
                master.Connect(ipe);
            }
            catch
            {
                MessageBox.Show("Erreur de connexion au serveur, veillez réessayer en insérant la bonne adresse ip du serveur !", "Echec", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
                return;
            }

            // Thread avec le serveur
            Thread t = new Thread(Data_IN);
            t.Start();
        }

        // Fonction pour envoyer une demande de conversation avec un autre utilisateur
            private void ConnectDesti_Click(object sender, RoutedEventArgs e)
        {
                // On lis chaque ligne de notre Login File
                foreach (string line in File.ReadLines(path))
                {
                    // On verifie si la ligne en question contient le nom de notre potentiel destinataire, si oui on envoi au serveur notre propre nom d'utilisateur puis le nom de notre destinataire
                    if (line.Contains(SelectionDestinataire.Text + ":"))
                    {
                        Packet p = new Packet(PacketType.Chat, ip);
                        p.Gdata.Add((App.Current as App).Session);
                        p.Gdata.Add(SelectionDestinataire.Text); 
                        master.Send(p.ToBytes());  // Envoi au serveur
                        // On enregistre localement notre destinaire, variable "global" / de "Session" accessible depuis toute les pages de l'application 
                        (App.Current as App).SessionDestinataire = SelectionDestinataire.Text;
                        MessageBox.Show("Vous avez envoyé une demande de conversation privé à : " + (App.Current as App).SessionDestinataire, "Invitation envoyé", MessageBoxButton.OK, MessageBoxImage.Information);
                        return;
                    }
                }
                // Message d'erreur si le destinataire choisi n'existe pas sur notre fichier texte
            MessageBox.Show("Ce destinataire n'existe pas dans notre base de donnée, veillez indiqué un destinataire existant et valide", "Erreur", MessageBoxButton.OK, MessageBoxImage.Information);
            return;
        }

        // Fonction que se reload toute les 2 secondes, permettant de d'ouvrir une fenêtre de dialogue afin d'accepter ou non une demande de message privée, fais suite aux valeur récupéré par notre listener
        private void dispatcherTimerReloadFunction_Tick(object sender, EventArgs e)
        {
            if ((ConditionChatRoomSpecific == (App.Current as App).Session))
             {
                MessageBoxResult result = MessageBox.Show("Hey l'utilisateur "+PseudoDemande+" souhaite rentrer en contact avec vous, êtes vous partant ?","Nouveau contact", MessageBoxButton.YesNo);
                switch (result)
                {
                    // Si l'utilisateur accepte, alors envoi au serveur la validation et ouvre la fenêtre de conversation privée avec l'utilisateur ayant demandé cette conversation
                    case MessageBoxResult.Yes:
                        (App.Current as App).SessionDestinataire = PseudoDemande;
                        Packet p = new Packet(PacketType.Chat, ip);
                        p.Gdata.Add(PseudoDemande+"DemandeConversationValide");
                        p.Gdata.Add(ConditionChatRoomSpecific);
                        master.Send(p.ToBytes());//send to server
                        ConditionChatRoomSpecific = "";
                        PseudoDemande = "";
                        ContactWindowAlive = false;
                        ConversationPrivée NewWindow = new ConversationPrivée();
                        NewWindow.Top = this.Top;
                        NewWindow.Left = this.Left;
                        NewWindow.Show();
                        this.Close();
                        break;
                    // Si non envoi au serveur du refus et retour à la fenêtre actuel
                    case MessageBoxResult.No:
                        ConditionChatRoomSpecific = "";
                        PseudoDemande = "";
                        Packet Refus = new Packet(PacketType.Chat, ip); 
                        Refus.Gdata.Add(PseudoDemande+"RefusDemandeConversation");
                        Refus.Gdata.Add(ConditionChatRoomSpecific);
                        master.Send(Refus.ToBytes());//send to server
                        MessageBox.Show("Demande refusée!", "Refus Conversation");
                        break;
                }
                ConditionChatRoomSpecific = "";
                return;
            }

            //  Message de Refus
            if (PseudoDemande == (App.Current as App).Session+"RefusDemandeConversation")
            {
                MessageBox.Show("Mister " + ConditionChatRoomSpecific + " ne souhaite pas communiquer avec vous", "Refus Conversation");
                ConditionChatRoomSpecific = "";
                PseudoDemande = "";
            }

            // Message de validation de la conversation, puis ouverture de la dite fenêtre
            else if (PseudoDemande == (App.Current as App).Session + "DemandeConversationValide")
            {
                MessageBox.Show("Mister "+ConditionChatRoomSpecific+" a accepté votre demande, il est temps de chatter !", "Excellent");
                ConditionChatRoomSpecific = "";
                PseudoDemande = "";
                ContactWindowAlive = false;
                ConversationPrivée NewWindow = new ConversationPrivée();
                NewWindow.Top = this.Top;
                NewWindow.Left = this.Left;
                NewWindow.Show();
                this.Close();
            }
            ConditionChatRoomSpecific = "";
            PseudoDemande = "";

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


        // Listener des données envoyé par le serveur
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
            if (ContactWindowAlive == true)
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
}

