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
using System.Windows.Navigation;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using ServerData;
using System.Net;
namespace ChatRoomProject
{
    /// <summary>
    /// Interaction logic for ChatRoomChoise.xaml
    /// </summary>
    
    public partial class ChatRoomChoise : Window
    {
        public static string inputChatRoomName;
        public string path = @"C:\Users\Vincent\source\repos\ChatRoomProject\Ressources\ChatRoom.txt";
        
        public ChatRoomChoise()
        {
            InitializeComponent();

            using (StreamWriter Creation = File.AppendText(path))
            {
                Creation.Close();
            }

            int i = 0;
            foreach (string line in File.ReadLines(path))
            {
                Button newBtn = new Button();

                newBtn.Content = line;
                newBtn.Name = line;
                newBtn.Height = 20;
                newBtn.Width = 160;
                newBtn.Click += btnChatRoomChoosed_Click;

                InsertionPlace.Children.Add(newBtn);
                i += 1;
            }
            
        }
        // Page de redirection vers la windows ChatRoom, ici choisir une chatRoom va changer l'adresse IP du serveur, permettant de communiquer seulement 
        //entre personne ayant sélectionner cette même chatRoom
        

        private void btnChatRoomChoosed_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            (App.Current as App).SessionChatRoom = btn.Name;
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
                MessageBox.Show("Nouvelle Chat Room crée, nommée : " + inputChatRoomName, "Création valider", MessageBoxButton.OK, MessageBoxImage.Information);
                sw.Close();
                ChatRoomChoise NewWindow = new ChatRoomChoise();
                NewWindow.Top = this.Top;
                NewWindow.Left = this.Left;
                NewWindow.Show();
                return;
            }

            
        }
    }
}
