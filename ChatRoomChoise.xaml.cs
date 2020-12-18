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

namespace ChatRoomProject
{
    /// <summary>
    /// Interaction logic for ChatRoomChoise.xaml
    /// </summary>
    public partial class ChatRoomChoise : Window
    {
        public static string RoomChoice;
        public ChatRoomChoise()
        {
            InitializeComponent();
        }
        // Page de redirection vers la windows ChatRoom, ici choisir une chatRoom va changer l'adresse IP du serveur, permettant de communiquer seulement 
        //entre personne ayant sélectionner cette même chatRoom

        private void btnPolitique_Click(object sender, RoutedEventArgs e)
        {
            RoomChoice = "192.168.56.1";
            (App.Current as App).SessionChatRoom = RoomChoice;
            ChatRoom NewWindow = new ChatRoom();
            NewWindow.Show();
            this.Close();
        }

        private void btnVacance_Click(object sender, RoutedEventArgs e)
        {
            RoomChoice = "192.168.56.2";
            (App.Current as App).SessionChatRoom = RoomChoice;
            ChatRoom NewWindow = new ChatRoom();
            NewWindow.Show();
            this.Close();
        }

        private void btnScienceFiction_Click(object sender, RoutedEventArgs e)
        {
            RoomChoice = "192.168.56.3";
            (App.Current as App).SessionChatRoom = RoomChoice;
            ChatRoom NewWindow = new ChatRoom();
            NewWindow.Show();
            this.Close();
        }
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                this.DragMove();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
