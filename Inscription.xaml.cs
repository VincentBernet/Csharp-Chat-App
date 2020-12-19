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
    /// Interaction logic for Inscription.xaml
    /// </summary>
    public partial class Inscription : Window
    {
        public string path = @"C:\Users\Vincent\source\repos\ChatRoomProject\Ressources\Login.txt";
        public static string inputName;
        public static string inputPassword;
        public static string id;
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
                MainWindow NewWindow = new MainWindow();
                NewWindow.Top = this.Top;
                NewWindow.Left = this.Left;
                NewWindow.Show();
                this.Close();
                return;
            }



        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                this.DragMove();
        }
    }
}
