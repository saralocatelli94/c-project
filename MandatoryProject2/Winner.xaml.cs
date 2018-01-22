using System;
using System.IO;
using System.Xml.Linq;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;


// Il modello di elemento Pagina vuota è documentato all'indirizzo https://go.microsoft.com/fwlink/?LinkId=234238

namespace MandatoryProject2
{
    /// <summary>
    /// Pagina vuota che può essere usata autonomamente oppure per l'esplorazione all'interno di un frame.
    /// </summary>
    /// 
     
    public sealed partial class Winner : Page
    {
        bool visible = false;
        Random r = new Random();
        ClassLibrary.FileXML fileXML;

        public Winner()
        {
            this.InitializeComponent();
            fileXML = ClassLibrary.FileXML.GetInstance(ApplicationData.Current.LocalFolder.Path + "/" + "table.xml", ApplicationData.Current.LocalFolder, "table.xml");
            chooseAWinner(r);
        }

        private async void chooseAWinner(Random r)
        {
            
            if (!fileXML.checkExistence())
                textb.Text = "There aren't any submissions yet";
            else
            {
                XElement winnerPerson = await fileXML.chooseWinnerAsync(r);
                if (winnerPerson != null)
                {

                    if (!visible)
                    {
                        visible = true;
                        Name.Visibility = Visibility.Visible;
                        Surname.Visibility = Visibility.Visible;
                        Email.Visibility = Visibility.Visible;
                        Date.Visibility = Visibility.Visible;
                        Phone.Visibility = Visibility.Visible;
                        Serial.Visibility = Visibility.Visible;
                        NameValue.Visibility = Visibility.Visible;
                        SurnameValue.Visibility = Visibility.Visible;
                        EmailValue.Visibility = Visibility.Visible;
                        PhoneValue.Visibility = Visibility.Visible;
                        DateValue.Visibility = Visibility.Visible;
                        SerialValue.Visibility = Visibility.Visible;
                        changeWinner.Visibility = Visibility.Visible;
                    }
                    NameValue.Text = winnerPerson.Element("name").Value.ToString();
                    SurnameValue.Text = winnerPerson.Element("surname").Value.ToString();
                    EmailValue.Text = winnerPerson.Element("Email").Value.ToString();
                    EmailValue.Width = 400;
                    PhoneValue.Text = winnerPerson.Element("PhoneNr").Value.ToString();
                    DateValue.Text = winnerPerson.Element("Date").Value.ToString();
                    SerialValue.Text = winnerPerson.Element("SerialNr").Value.ToString();
                    BitmapImage bm = new BitmapImage();
                    StorageFolder sf = ApplicationData.Current.LocalFolder;
                    StorageFile sfImage = await sf.GetFileAsync(winnerPerson.Element("Image").Value.ToString());
                    Windows.Storage.Streams.IRandomAccessStream stream = await sfImage.OpenAsync(FileAccessMode.Read);
                    bm.SetSource(stream);
                    ImageValue.Source = bm;
                }
                else
                {
                    textb.Text = "There aren't any submissions yet";
                }
            }

        }

        private void TextBlock_SelectionChanged(object sender, RoutedEventArgs e)
        {
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            if (fileXML.checkExistence())
            {
                await fileXML.changeWinnerAsync(r);        
                chooseAWinner(r);
            }
        }

    }
}
