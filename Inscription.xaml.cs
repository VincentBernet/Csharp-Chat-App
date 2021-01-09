using System.Windows;
using System.Windows.Input;
using System.IO;


namespace ChatRoomProject
{

    public partial class Inscription : Window
    {
        public string path = @"C:\Users\Vincent\source\repos\ChatRoomProject\Ressources\Login.txt";
        public static string inputName;
        public static string inputPassword;
        public Inscription()
        {
            InitializeComponent();
        }

        // Phase d'inscription, tout mot de passe et identifiants sont valides
        // Vérification supplémentaire que l'utilisateur ne mettent pas de champs vide (qui validerait l'inscription)
        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {

            inputName = txtUsername.Text;
            inputPassword = txtPassword.Password;

            if (inputName == "" || inputPassword == "")
            {
                MessageBox.Show("Veillez renseigner à la fois votre indentifiant et votre mot de passe pour valider l'inscription ! " + inputName, "Champs Invalides", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            using (StreamWriter sw = File.AppendText(path))
            {
                sw.WriteLine(inputName+":"+inputPassword);
                MessageBox.Show("Votre Inscription est validé, bienvenue à vous " + inputName, "Bienvenue", MessageBoxButton.OK, MessageBoxImage.Information);
                sw.Close();
                MainWindow NewWindow = new MainWindow();
                NewWindow.Top = this.Top;
                NewWindow.Left = this.Left;
                NewWindow.Show();
                this.Close();
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

    }
}
