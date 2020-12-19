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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using ServerData;
using System.Net;


namespace ChatRoomProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    
    public partial class MainWindow : Window
    {
        // Déclaration Variable Public
        public static Socket master;
        // Chemin d'accès à mon fichier txt de connexion, pour sa création + modification
        public string path = @"C:\Users\Vincent\source\repos\ChatRoomProject\Ressources\Login.txt"; 
        public static string inputName;
        public static string inputPassword;
        public static string id;
        

        public MainWindow()
        {

            InitializeComponent();

            // On se connecte à notre serveur, si la connexion est impossible (ndlr le server n'est pas lancé) alors l'application est shutdown directement et un message d'erreur apparait
            id = "192.168.56.1";
            master = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint ipe = new IPEndPoint(IPAddress.Parse(id), 4242);
            try
            {
                master.Connect(ipe);
            }
            catch (Exception)
            {
                MessageBox.Show("You are not connected to the server, application gonna shut down", "Warning", MessageBoxButton.OK, MessageBoxImage.Information);
                Application.Current.Shutdown();
            }
           

        }



        // Phase de connection, si nos inputs correspondent à un compte sur notre fichier txt alors la connexion est valide
        //, vérification supplémentaire que l'utilisateur ne mettent pas de champs vide (qui validerait la connexion)
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            
            inputName = txtUsername.Text;
            inputPassword = txtPassword.Password;
            
            if (inputName == "" || inputPassword == "")
            {
                MessageBox.Show("Veillez renseigner à la fois votre indentifiant et votre mot de passe ! " + inputName, "Champs Invalides", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

           

            foreach (string line in File.ReadLines(path))
            {
                
                if (line.Contains(inputName+":"+inputPassword))
                {
                    MessageBox.Show("Authentification réussi cher " + inputName, "Bienvenue", MessageBoxButton.OK, MessageBoxImage.Information);
                    (App.Current as App).Session = inputName;
                    GeneralChatPage NewWindow = new GeneralChatPage();
                    NewWindow.Top = this.Top;
                    NewWindow.Left = this.Left;
                    NewWindow.Show();
                    this.Close();
                    return;
                }
            }
                
                MessageBox.Show("Votre mot de passe ne corresponds pas à votre identifiant " + inputName, "Authentification refusé", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        // Redirection vers la page d'inscription
        private void btnInscription_Click(object sender, RoutedEventArgs e)
        {
            Inscription NewWindow = new Inscription();
            NewWindow.Top = this.Top;
            NewWindow.Left = this.Left;
            NewWindow.Show();
            this.Close();
            return;
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
