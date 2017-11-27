using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Xml;

using System.Xml.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System;
using System.IO;
using System.Xml;
using System.Text;
using Windows.Storage;
using System.Reflection;
using Windows.UI;
using System.Text.RegularExpressions;
using Windows.Storage.Pickers;
using Windows.UI.Xaml.Media.Imaging;

// Il modello di elemento Pagina vuota è documentato all'indirizzo https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x410

namespace MandatoryProject2
{
    /// <summary>
    /// Pagina vuota che può essere usata autonomamente oppure per l'esplorazione all'interno di un frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        public MainPage()
        {


            this.InitializeComponent();
            myFrame.Navigate(typeof(BlankPage1));
        }

        private void Hamb_button_Click(object sender, RoutedEventArgs e)
        {
            Hb_menu.IsPaneOpen = true;
        }

        private void see_submissions_Click(object sender, RoutedEventArgs e)
        {
            myFrame.Navigate(typeof(SeeSubmissions));
        }

        private void winner_Click(object sender, RoutedEventArgs e)
        {
            myFrame.Navigate(typeof(Winner));
        }

        private void new_submission_Click(object sender, RoutedEventArgs e)
        {
            myFrame.Navigate(typeof(BlankPage1));
        }
    }
    
}
