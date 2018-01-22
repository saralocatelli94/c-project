using System;
using System.Linq;
using Windows.Storage;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;


// Il modello di elemento Pagina vuota è documentato all'indirizzo https://go.microsoft.com/fwlink/?LinkId=234238

namespace MandatoryProject2
{
    /// <summary>
    /// Pagina vuota che può essere usata autonomamente oppure per l'esplorazione all'interno di un frame.
    /// </summary>
    public sealed partial class SeeSubmissions : Page
    {
        ClassLibrary.FileXML fileXML;
        public SeeSubmissions()
        {
            fileXML = ClassLibrary.FileXML.GetInstance(ApplicationData.Current.LocalFolder.Path + "/" + "table.xml", ApplicationData.Current.LocalFolder, "table.xml");
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

            if (fileXML.checkExistence())
            {
                var person=fileXML.findBySerialnNumber(password);
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
