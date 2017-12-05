using System;
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
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

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
        public Winner()
        {
            this.InitializeComponent();
           
            chooseAWinner(r);
        }

        private async void chooseAWinner(Random r)
        {
            XDocument doc;
            string filename = "table.xml";
            string path = ApplicationData.Current.LocalFolder.Path;
            StorageFolder localfolder = Windows.Storage.ApplicationData.Current.LocalFolder;
            if (File.Exists(path + "/" + filename))
            {

                var file = Path.Combine(path, filename);
                doc = XDocument.Load(file);
                XElement xelement = doc.Root;
                int number = doc.Root.Elements().Count();
                var person = from nm in xelement.Elements("Person")
                           where (string)nm.Element("Winner") == "Y"
                           select nm;
                XElement winnerPerson;
                if(number == 0)
                {
                    /* if (visible)
                     {
                         visible = false;
                         Name.Visibility = Visibility.Collapsed;
                         Surname.Visibility = Visibility.Collapsed;
                         Email.Visibility = Visibility.Collapsed;
                         Date.Visibility = Visibility.Collapsed;
                         Phone.Visibility = Visibility.Collapsed;
                         Serial.Visibility = Visibility.Collapsed;
                         NameValue.Visibility = Visibility.Collapsed;
                         SurnameValue.Visibility = Visibility.Collapsed;
                         EmailValue.Visibility = Visibility.Collapsed;
                         PhoneValue.Visibility = Visibility.Collapsed;
                         DateValue.Visibility = Visibility.Collapsed;
                         SerialValue.Visibility = Visibility.Collapsed;
                         changeWinner.Visibility = Visibility.Collapsed;

                     }*/
                    textb.Text = "There aren't any submissions yet";
                    return;
                }
                 if (person.Count() == 0)
                {
                   
                    
                    int winner = r.Next(0, number - 1);
                    winnerPerson = doc.Descendants("Person").ElementAt(winner);
                    winnerPerson.Element("Winner").Value = "Y";
                    StorageFile f = await localfolder.CreateFileAsync(filename, CreationCollisionOption.ReplaceExisting);
                    using (var s = await f.OpenStreamForWriteAsync())
                    {
                        doc.Save(s);
                    }
                   
                }
                else
                {

                   
                    winnerPerson = person.ElementAt(0);
                }
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

        private void TextBlock_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
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
                             where (string)nm.Element("Winner") == "Y"
                             select nm;
               
                
                person.ElementAt(0).Element("Winner").Value = "N";
                StorageFile f = await localfolder.CreateFileAsync(filename, CreationCollisionOption.ReplaceExisting);
                using (var s = await f.OpenStreamForWriteAsync())
                {
                    doc.Save(s);
                }
                
            }
            
            chooseAWinner(r);
        }

    }
}
