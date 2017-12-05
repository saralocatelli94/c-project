using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Xml.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Il modello di elemento Pagina vuota è documentato all'indirizzo https://go.microsoft.com/fwlink/?LinkId=234238

namespace MandatoryProject2
{
    /// <summary>
    /// Pagina vuota che può essere usata autonomamente oppure per l'esplorazione all'interno di un frame.
    /// </summary>
    public sealed partial class SeeSubmissions : Page
    {
        public SeeSubmissions()
        {
            this.InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string username = NameValue.Text;
            string password = PasswordValue.Password.ToString();
            if (password=="password" && username=="admin")
            {
                LogIn.login = true;
                LogIn.user = username;
                LogIn.password = password;
                Frame.Navigate(typeof(SubmissionsView));
                return;
            }


            XDocument doc;
            string filename = "table.xml";
            string path = ApplicationData.Current.LocalFolder.Path;
            StorageFolder localfolder = Windows.Storage.ApplicationData.Current.LocalFolder;
            if (File.Exists(path + "/" + filename))
            {

                var file = Path.Combine(path, filename);
                doc = XDocument.Load(file);
                XElement xelement = doc.Root;
                var person = from nm in xelement.Elements("Person")
                             where (string)nm.Element("SerialNr") == password
                             select nm;         
                if(person.Count()==0)
                {

                    customDialog("Username or password not valid!");
                    return;
                }
                if(username==person.ElementAt(0).Element("name").Value.ToString())
                {
                    LogIn.login = true;
                    LogIn.user = username;
                    LogIn.password = password;
                    Frame.Navigate(typeof(SubmissionsView));
                }
                else
                {
                    customDialog("Username or password not valid!");
                    return;
                }
            }


            else
            {
                customDialog("Username or password not valid!");
            }

            
        }

        private async void customDialog(string s)
        {
            ContentDialog dialog = new ContentDialog()
            {
                Title = "Error",
                MaxWidth = this.ActualWidth,
                PrimaryButtonText = "OK",
                Content = new TextBlock
                {
                    Text = s
                },
            };
            await dialog.ShowAsync();
        }

        private void Query_SelectionChanged(object sender, RoutedEventArgs e)
        {
            
        }
        public void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {

            TextBox tb = (TextBox)sender;
            tb.Background = new SolidColorBrush(Color.FromArgb(100, 228, 222, 222));
            tb.Text = "";
        }
    }
}
