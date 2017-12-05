using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Xml.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
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
    public sealed partial class SubmissionsView : Page
    {
        public SubmissionsView()
        {
            this.InitializeComponent();
            UserValue.Text = LogIn.user;
            PasswordValue_.Text = LogIn.password;
            uploadSubmissions();
            



        }

        public async void uploadSubmissions()
        {
            string filename = "table.xml";
            string folder = ApplicationData.Current.LocalFolder.Path;
            string path = folder + "/" + filename;
            if (!File.Exists(folder + "/" + filename))
            {
                textb.Text = "There aren't any submissions yet";
                return;
            }
            List<string> imageNames = new List<string>();
            List<string> names = new List<string>();
            List<string> surnames = new List<string>();
            List<string> emails = new List<string>();
            List<string> phNumbers = new List<string>();
            List<string> dates = new List<string>();
            List<string> serialNumbers = new List<string>();
            Grid g = new Grid();
            using (StreamReader reader = File.OpenText(path))
            {
                XDocument xmlFile = XDocument.Load(reader);
                IEnumerable<XElement> people = xmlFile.Root.Elements();
                int count = people.Count();
                if(count==0)
                {
                    textb.Text = "There aren't any submissions yet";
                    return;
                }
                foreach (var x in people)
                {

                    imageNames.Add(x.Element("Image").Value.ToString());
                    names.Add(x.Element("name").Value.ToString());
                    surnames.Add(x.Element("surname").Value.ToString());
                    emails.Add(x.Element("Email").Value.ToString());
                    phNumbers.Add(x.Element("PhoneNr").Value.ToString());
                    dates.Add(x.Element("Date").Value.ToString());
                    serialNumbers.Add(x.Element("SerialNr").Value.ToString());
                    // Query.Text += x.Element("Image").Value.ToString();
                }
                int number = imageNames.Count;
                int pagesNumber = number / 10;
                if (number % 10 != 0)
                    pagesNumber++;

                for (int j = 0; j < pagesNumber; j++)
                {
                    ListView l = new ListView();
                    l.HorizontalAlignment = HorizontalAlignment.Left;
                    l.Height = 540;
                    l.Margin = new Thickness(245, 160, 0, 0);
                    l.VerticalAlignment = VerticalAlignment.Top;
                    l.Width = 800;
                    flip.Items.Add(l);

                    for (int i = j * 10; i < (j * 10) + 10; i++)
                    {
                        if (i >= imageNames.Count)
                            return;
                        Grid grid = new Grid();
                        grid.Height = 250;

                        Image image = new Image();
                        image.HorizontalAlignment = HorizontalAlignment.Left;
                        image.Height = 240;
                        image.Width = 240;
                        image.Margin = new Thickness(50, 20, 20, 20);
                        image.VerticalAlignment = VerticalAlignment.Top;
                        TextBlock name = new TextBlock();
                        name.Margin = new Thickness(350, 50, 0, 0);
                        TextBlock nameValue = new TextBlock();
                        nameValue.Margin = new Thickness(420, 50, 0, 0);
                        TextBlock surname = new TextBlock();
                        surname.Margin = new Thickness(350, 80, 0, 0);
                        TextBlock surnameValue = new TextBlock();
                        surnameValue.Margin = new Thickness(420, 80, 0, 0);
                        TextBlock email = new TextBlock();
                        email.Margin = new Thickness(350, 110, 0, 0);
                        TextBlock emailValue = new TextBlock();
                        emailValue.Margin = new Thickness(420, 110, 0, 0);
                        TextBlock phone = new TextBlock();
                        phone.Margin = new Thickness(350, 140, 0, 0);
                        TextBlock phoneValue = new TextBlock();
                        phoneValue.Margin = new Thickness(470, 140, 0, 0);
                        TextBlock date = new TextBlock();
                        date.Margin = new Thickness(350, 170, 0, 0);
                        TextBlock dateValue = new TextBlock();
                        dateValue.Margin = new Thickness(470, 170, 0, 0);
                        TextBlock serial = new TextBlock();
                        serial.Margin = new Thickness(350, 200, 0, 0);
                        TextBlock serialValue = new TextBlock();
                        serialValue.Margin = new Thickness(470, 200, 0, 0);
                        grid.Children.Add(image);
                        grid.Children.Add(name);
                        grid.Children.Add(surname);
                        grid.Children.Add(nameValue);
                        grid.Children.Add(surnameValue);
                        grid.Children.Add(email);
                        grid.Children.Add(emailValue);
                        grid.Children.Add(phone);
                        grid.Children.Add(phoneValue);
                        grid.Children.Add(date);
                        grid.Children.Add(dateValue);
                        grid.Children.Add(serial);
                        grid.Children.Add(serialValue);


                        BitmapImage bm = new BitmapImage();
                        StorageFolder sf = ApplicationData.Current.LocalFolder;

                        StorageFile file = await sf.GetFileAsync(imageNames.ElementAt(i));
                        Windows.Storage.Streams.IRandomAccessStream stream = await file.OpenAsync(FileAccessMode.Read);
                        bm.SetSource(stream);

                        image.Source = bm;
                        name.Text = "Name: ";
                        surname.Text = "Surname: ";
                        email.Text = "Email: ";
                        phone.Text = "Phone number: ";
                        date.Text = "Date of birth: ";
                        serial.Text = "Serial number:";
                        nameValue.Text = names.ElementAt(i);
                        surnameValue.Text = surnames.ElementAt(i);
                        emailValue.Text = emails.ElementAt(i);
                        phoneValue.Text = phNumbers.ElementAt(i);
                        dateValue.Text = dates.ElementAt(i);
                        serialValue.Text = serialNumbers.ElementAt(i);
                        image.Visibility = Visibility.Visible;
                        image.Stretch = Stretch.Uniform;
                        image.VerticalAlignment = VerticalAlignment.Center;
                        l.Items.Add(grid);


                    }
                }
                

            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            LogIn.login = false;
            LogIn.password = "";
            LogIn.user = "";
            Frame.Navigate(typeof(SeeSubmissions));
        }
    }
}
