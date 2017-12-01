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
        List<String> serialNr;

        public MainPage()
        {
            StorageFolder localFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
            this.InitializeComponent();
            var path = Path.Combine(localFolder.Path, "SerialNumber.txt");

            if (!File.Exists(path))
            {
                serialNr = generateSerialNumbers();
                saveSerialNumbers(serialNr);
            }
           
            string filename = "table.xml";
        //    File.Delete(Path.Combine(localFolder.Path, filename));

            myFrame.Navigate(typeof(BlankPage1));
        }
        private List<String> generateSerialNumbers()
        {
            List<String> serialNumbers = new List<string>();
            Random r = new Random();
            for (int i = 0; i < 100; i++)
            {
                String s = "";
                for (int j = 0; j < 2; j++)
                    s = s + randomLetter(r);
                for (int j = 0; j < 3; j++)
                    s = s + randomNumber(r);
                for (int j = 0; j < 2; j++)
                    s = s + randomLetter(r);
                s = s + randomNumber(r);
                for (int j = 0; j < 3; j++)
                    s = s + randomLetter(r);
                for (int j = 0; j < 2; j++)
                    s = s + randomNumber(r);
                serialNumbers.Add(s);


            }
            return serialNumbers;

        }

        private string randomLetter(Random r)
        {
            string chars = "$%#@!*abcdefghijklmnopqrstuvwxyz1234567890?;:ABCDEFGHIJKLMNOPQRSTUVWXYZ^&";
            int n = r.Next(0, chars.Length - 1);
            char c = chars[n];
            return c.ToString();
        }

        private int randomNumber(Random r)
        {

            int n = r.Next(0, 9);
            return n;
        }

        private async void saveSerialNumbers(List<String> serialNr)
        {
            string filename = "SerialNumber.txt";
            StorageFolder localFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
            StorageFile file = await localFolder.CreateFileAsync(filename, CreationCollisionOption.ReplaceExisting);

            for (int i = 0; i < serialNr.Count; i++)
                await FileIO.AppendTextAsync(file, (serialNr.ElementAt(i) + "\n"));


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
