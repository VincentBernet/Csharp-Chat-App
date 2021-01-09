using System.Windows;
using System.Windows.Input;


namespace ChatRoomProject
{
    public partial class GeneralChatPage : Window
    {
        // Simple Page de redirection vers soit les ChatRooms soit les messages direct privés d'utilisateur à utilisateurs
        public GeneralChatPage()
        {
            InitializeComponent();
        }

        // Redirection vers Contact Page
        private void btnContact_Click(object sender, RoutedEventArgs e)
        {
            Contact NewWindow = new Contact();
            NewWindow.Top = this.Top;
            NewWindow.Left = this.Left;
            NewWindow.Show();
            this.Close();
        }

        // Redirection vers ChatRoomChoice Page
        private void btnChatRoom_Click(object sender, RoutedEventArgs e)
        {
            ChatRoomChoise NewWindow = new ChatRoomChoise();
            NewWindow.Top = this.Top;
            NewWindow.Left = this.Left;
            NewWindow.Show();
            this.Close();
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
    }
}
