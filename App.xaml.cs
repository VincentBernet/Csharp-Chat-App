using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ChatRoomProject
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        // Déclaration de variable "global" à toutes les pages de l'application
        public string Session { get; set; }
        public String SessionChatRoom { get; set; }
        public String ip { get; set; }

        public String pathBegining { get; set; }
        public String SessionDestinataire { get; set; }
    }
}
