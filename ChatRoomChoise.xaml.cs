using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.IO;
using System.Windows.Threading;

namespace ChatRoomProject
{

    public partial class ChatRoomChoise : Window
    {
        public static string inputChatRoomName;
        public string OldLastChat;
        public string NewLastChat;
        public string path = (App.Current as App).pathBegining + @"ChatRoom.txt";

        public ChatRoomChoise()
        {
            InitializeComponent();

            // Création de notre fichier text si n'existe pas
            using (StreamWriter Creation = File.AppendText(path))
            {
                Creation.Close();
            }

            // Création d'un timer qui permettra plus loin de refresh une fonction toute les 0.5 secondes
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(3);
            timer.Tick += dispatcherTimerReloadFunction_Tick;
            timer.Start();

            // Création initial de nos boutons de ChatRooms en lisant notre fichier text
            foreach (string line in File.ReadAllLines(path))
            {
                Button newBtn = new Button();

                newBtn.Content = line;
                newBtn.FontSize = 13;
                newBtn.Height = 30;
                newBtn.Width = 180;
                newBtn.Margin = new Thickness(2);
                newBtn.Click += btnChatRoomChoosed_Click;

                InsertionPlace.Children.Add(newBtn);
                OldLastChat = line;
            }
            
        }

        // Reload fontion qui permet d'actualisé les ChatRooms Disponible, en créant un bouton correspondant pour chaque nouvel ligne de notre fichier text
        private void dispatcherTimerReloadFunction_Tick(object sender, EventArgs e)
        {
            foreach (string line in File.ReadAllLines(path))
            {
                NewLastChat = line;
            }
                if (OldLastChat != NewLastChat)
                {
                    Button newBtn = new Button();

                    newBtn.Content = NewLastChat;
                    newBtn.FontSize = 13;
                    newBtn.Height = 30;
                    newBtn.Width = 180;
                    newBtn.Margin = new Thickness(2);
                    newBtn.Click += btnChatRoomChoosed_Click;

                    InsertionPlace.Children.Add(newBtn);
                    OldLastChat = NewLastChat;
            }
            
        }

        // Fonction de redirection vers la fenêtre ChatRoom, en passant le nom de la ChatRoom sélectionné en variable global / de session
        private void btnChatRoomChoosed_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            (App.Current as App).SessionChatRoom = btn.Content.ToString();
            ChatRoom NewWindow = new ChatRoom();
            NewWindow.Top = this.Top;
            NewWindow.Left = this.Left;
            NewWindow.Show();
            this.Close();
        }

        // Fonction qui simplement écris dans le fichier text la nouvelle ChatRoom Créée, en vérifiant quelques conditions
        private void btnCreationbutton_Click(object sender, RoutedEventArgs e)
        {
            inputChatRoomName = NewChatRoomName.Text;
            if (inputChatRoomName == "")
            {
                MessageBox.Show("Veillez renseigner un nom valide pour votre nouvelle Chat Room ", "Champs Invalides", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            using (StreamWriter sw = File.AppendText(path))
            {
                sw.WriteLine(inputChatRoomName);
                sw.Close();
                MessageBox.Show("Nouvelle Chat Room créée, nommée : " + inputChatRoomName, "Création valider", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
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

    }
}
