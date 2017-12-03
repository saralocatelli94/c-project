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

// Il modello di elemento Pagina vuota è documentato all'indirizzo https://go.microsoft.com/fwlink/?LinkId=234238

namespace MandatoryProject2
{
    /// <summary>
    /// Pagina vuota che può essere usata autonomamente oppure per l'esplorazione all'interno di un frame.
    /// </summary>
    public sealed partial class BlankPage1 : Page
    {
        List<String> serialNr;
        string imagePath = "";
        public BlankPage1()
        {

            this.InitializeComponent();
             
            
        }
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            if(serialNr==null)
                serialNr = readSerialNumbers();
            if (ValidateData())
            {
                ShowDialog_Click();
                return;
            }
            String s = Date.Date.Day.ToString() + "-" + Date.Date.Month.ToString() + "-" + Date.Date.Year.ToString();
            XDocument doc;
            string filename = "table.xml";
            string path = ApplicationData.Current.LocalFolder.Path;
            StorageFolder localfolder = Windows.Storage.ApplicationData.Current.LocalFolder;
            if (File.Exists(path + "/" + filename))
            {
                var file = Path.Combine(path, filename);
                doc = XDocument.Load(file);
                doc.Root.Add(new XElement("Person",
                    new XElement("name", Name.Text),
                   new XElement("surname", Surname.Text),
                   new XElement("Date", s),
                   new XElement("PhoneNr", PhoneNr.Text),
                      new XElement("Email", Email.Text),
                       new XElement("Image", imagePath),
                      new XElement("SerialNr", SeriaNr.Text),
                       new XElement("Winner", "N")
                      ));


                using (var stream = new FileStream(path + "/" + filename, FileMode.Open))
                {
                    doc.Save(stream);
                    stream.Flush();
                }
            }

            if (!File.Exists(path + "/" + filename))
            {


                StorageFile file = await localfolder.CreateFileAsync(filename, CreationCollisionOption.ReplaceExisting);
                using (var stream = await file.OpenStreamForWriteAsync())
                {
                    doc =
                    new XDocument(
                     new XElement("Subbmissions",
                      new XElement("Person",
                    new XElement("name", Name.Text),
                   new XElement("surname", Surname.Text),
                   new XElement("Date", s),
                   new XElement("PhoneNr", PhoneNr.Text),
                      new XElement("Email", Email.Text),
                      new XElement("Image", imagePath),
                      new XElement("SerialNr", SeriaNr.Text),
                      new XElement("Winner", "N")
                      )));
                    doc.Save(stream);
                    stream.Flush();
                }
            }

        }

        private void save_Click(object sender, RoutedEventArgs e)
        {



            XDocument doc;
            string filename = "table.xml";
            string folder = ApplicationData.Current.LocalFolder.Path;
            string path = folder + "/" + filename;
            if (!File.Exists(folder + "/" + filename))
                return;
            using (StreamReader reader = File.OpenText(path))
            {
                doc = XDocument.Load(reader);
                XElement rootElement = doc.Root;
               // xmlResult.Text = rootElement.ToString();
            }

        }

        public void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {

            TextBox tb = (TextBox)sender;
            tb.Background=new SolidColorBrush(Color.FromArgb(100, 228, 222, 222));
          //  tb.BorderBrush = new SolidColorBrush(Color.FromArgb(40, 0, 0, 0));
            tb.Text = "";
        }

        public  Boolean ValidateData()
        {
            bool error = false;
            //name validation


            string name = Name.Text;
            Regex regex = new Regex(@"^[a-zA-Z_]\w*(\.[a-zA-Z_]\w*)*$");
            Match match = regex.Match(name);
            if (!match.Success)
            {
                textBoxError(Name);
                Name.Text = "Insert only letters.";
                error = true;
            }
            //surname validation

            string surname = Surname.Text;
            regex = new Regex(@"^[a-zA-Z_]\w*(\.[a-zA-Z_]\w*)*$");
            match = regex.Match(surname);
            if (!match.Success)
            {
                textBoxError(Surname);
                Surname.Text = "Insert only letters.";
                error = true;
            }
            //phone nr validation
            string phoneNr = PhoneNr.Text;
            regex = new Regex(@"\(?\d{3}\)?-? *\d{3}-? *-?\d{4}");
            match = regex.Match(phoneNr);
            if (!match.Success)
            {
                textBoxError(PhoneNr);
                PhoneNr.Text = "Insert at least 10 numbers.";
                error = true;
            }


            //email validation
            string email = Email.Text;
            regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            match = regex.Match(email);
            if (!match.Success)
            {
                textBoxError(Email);
                Email.Text = "Insert a valid email.";
                error = true;
            }

            //serial number validation
            if (!serialNr.Contains(SeriaNr.Text))
            {
                textBoxError(SeriaNr);
                SeriaNr.Text = "Not valid serial number.";
                error = true;
            }
           else
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
                    var n = from nm in xelement.Elements("Person")
                               where (string)nm.Element("SerialNr") == SeriaNr.Text
                               select nm;
                    if(n.Count()>0)
                    {
                        SeriaNr.Text = "Serial number already used.";
                        textBoxError(SeriaNr);
                        error = true;
                    }
                    
                }
            }

            //image validation
            if (imagePath == "")
            {
                Upload.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));
                error = true;
            }

            int currentYear = DateTime.Now.Year;
            //date validation
            if (Date.Date.Year > (currentYear - 10))
            {
                Date.Background = new SolidColorBrush(Color.FromArgb(255, 129, 43, 43));
                //    Date.BorderBrush=new SolidColorBrush(Color.FromArgb(255, 129,43, 43));
                error = true;
            }
            return error;
        }

        public void textBoxError(TextBox t)
        {
            t.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));
        }


       
        private List<string> readSerialNumbers()
        {
            List<String> serialNr = new List<String>();
            string filename = "SerialNumber.txt";
            StorageFolder localFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
            var path = Path.Combine(localFolder.Path, filename);
            string[] lines;
            using (StreamReader reader = File.OpenText(path))
            {

                lines = reader.ReadToEnd().Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                foreach (var line in lines)
                    serialNr.Add(line);
            }
            //for (int i = 0; i < serialNr.Count; i++)
                //xmlResult.Text += serialNr.ElementAt(i) + "\n";
            return serialNr;
        }

        private async void Upload_Click(object sender, RoutedEventArgs e)
        {
            ((Button)sender).BorderBrush = new SolidColorBrush(Color.FromArgb(0, 255, 255, 255));
            FileOpenPicker picker = new FileOpenPicker();
            picker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            picker.ViewMode = PickerViewMode.Thumbnail;
            picker.FileTypeFilter.Clear();
            picker.FileTypeFilter.Add(".jpeg");
            picker.FileTypeFilter.Add(".jpg");
            picker.FileTypeFilter.Add(".png");
            StorageFile sf = await picker.PickSingleFileAsync();
           
            if (sf != null)
            {
                try
                {

                    await sf.CopyAsync(ApplicationData.Current.LocalFolder);
                }catch(Exception)
                {
                    
                }
                imagePath = Path.GetFileName(sf.Path); 
                Windows.Storage.Streams.IRandomAccessStream stream = await sf.OpenAsync(FileAccessMode.Read);
                BitmapImage bm = new BitmapImage();
                bm.SetSource(stream);
                Image.Source = bm;
            }


        }

        private void DateChanged(object sender, DatePickerValueChangedEventArgs e)
        {
            Date.Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
        }
        private async void ShowDialog_Click()
        {
            // Show the custom dialog
            ContentDialog dialog = new ContentDialog()
            {
                Title = "Custom dialog",
                MaxWidth = this.ActualWidth,
                PrimaryButtonText = "OK",
                Content=new TextBlock
                {
                    Text="Some values aren't valid!"
                },
            };
            await dialog.ShowAsync();

        }


    }
}
