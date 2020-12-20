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
        public static string IdRoomChoice;
        public ChatRoomChoise()
        {
            InitializeComponent();
        }
        // Page de redirection vers la windows ChatRoom, ici choisir une chatRoom va changer l'adresse IP du serveur, permettant de communiquer seulement 
        //entre personne ayant sélectionner cette même chatRoom
        private void btnVacance_Click(object sender, RoutedEventArgs e)
        {
            IdRoomChoice = "1";
            (App.Current as App).SessionChatRoom = IdRoomChoice;
            ChatRoom NewWindow = new ChatRoom();
            NewWindow.Top = this.Top;
            NewWindow.Left = this.Left;
            NewWindow.Show();
            this.Close();
        }
        private void btnPolitique_Click(object sender, RoutedEventArgs e)
        {
            IdRoomChoice = "2";
            (App.Current as App).SessionChatRoom = IdRoomChoice;
            ChatRoom NewWindow = new ChatRoom();
            NewWindow.Top = this.Top;
            NewWindow.Left = this.Left;
            NewWindow.Show();
            this.Close();
        }

        

        private void btnScienceFiction_Click(object sender, RoutedEventArgs e)
        {
            IdRoomChoice = "3";
            (App.Current as App).SessionChatRoom = IdRoomChoice;
            ChatRoom NewWindow = new ChatRoom();
            NewWindow.Top = this.Top;
            NewWindow.Left = this.Left;
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
