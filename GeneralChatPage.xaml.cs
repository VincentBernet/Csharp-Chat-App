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
    /// Interaction logic for GeneralChatPage.xaml
    /// </summary>
    public partial class GeneralChatPage : Window
    {
        // Simple Page de redirection vers soit les ChatRooms soit les Message direct entre utilisateur (DM qui ne sont pas encore fonctionnels)
        public GeneralChatPage()
        {
            InitializeComponent();
        }
        private void btnContact_Click(object sender, RoutedEventArgs e)
        {
            Contact NewWindow = new Contact();
            NewWindow.Top = this.Top;
            NewWindow.Left = this.Left;
            NewWindow.Show();
            this.Close();
        }
        private void btnChatRoom_Click(object sender, RoutedEventArgs e)
        {
            ChatRoomChoise NewWindow = new ChatRoomChoise();
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
