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
            else
                serialNr=readSerialNumbers();
            string filename = "table.xml";
            File.Delete(Path.Combine(localFolder.Path, filename));
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateData())
            {
                return;
            }

            XDocument doc;
            string filename = "table.xml";
            string path = ApplicationData.Current.LocalFolder.Path;
            StorageFolder localfolder = Windows.Storage.ApplicationData.Current.LocalFolder;
              if ( File.Exists(path + "/" + filename))
               {
                var file = Path.Combine(path, filename);
                doc = XDocument.Load(file);
                doc.Root.Add(new XElement("Person",
                    new XElement("name", new XAttribute("name", Name.Text)),
                   new XElement("surname", new XAttribute("surname", Surname.Text)),
                
                   new XElement("PhoneNr", PhoneNr.Text),
                      new XElement("Email", Email.Text),
                      new XElement("SerialNr", SeriaNr.Text)
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
                    new XElement("name", new XAttribute("name", Name.Text)),
                   new XElement("surname", new XAttribute("surname", Surname.Text)),
                  // new XElement("Date of birth", Date.ToString()),
                   new XElement("PhoneNr", PhoneNr.Text),
                      new XElement("Email", Email.Text),
                      new XElement("SerialNr", SeriaNr.Text)
                      )));
                    doc.Save(stream);
                    stream.Flush();
                }
            }
   
        }

        private  void save_Click(object sender, RoutedEventArgs e)
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
                xmlResult.Text = rootElement.ToString();
            }

        }

        public void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {

            TextBox tb = (TextBox)sender;
            tb.BorderBrush = new SolidColorBrush(Color.FromArgb(40, 0, 0, 0));
            tb.Text = "";
        }

        public Boolean ValidateData()
        {
            bool error = false;
            //name validation
           

            string name = Name.Text;
            Regex regex = new Regex(@"^[a-zA-Z_]\w*(\.[a-zA-Z_]\w*)*$");
            Match match = regex.Match(name);
            if (!match.Success)
            {
                textBoxError(Name);
                error = true;
            }
            //surname validation

            string surname = Surname.Text;
             regex = new Regex(@"^[a-zA-Z_]\w*(\.[a-zA-Z_]\w*)*$");
             match = regex.Match(surname);
            if (!match.Success)
            {
                textBoxError(Surname);
                error = true;
            }
            //phone nr validation
            string phoneNr = PhoneNr.Text;
             regex = new Regex(@"\(?\d{3}\)?-? *\d{3}-? *-?\d{4}");
             match = regex.Match(phoneNr);
            if (!match.Success)
            {
                textBoxError(PhoneNr);
                error = true;
            }


             //email validation
            string email = Email.Text;
            regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            match = regex.Match(email);
            if (!match.Success)
            {
                textBoxError(Email);
                error = true;
            }

            //serial number validation
            if (!serialNr.Contains(SeriaNr.Text))
            {
                textBoxError(SeriaNr);
                error = true;
            }
            return error;
        }

        public void textBoxError(TextBox t)
        {
            t.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));
        }


        private List<String>  generateSerialNumbers()
        {
            List<String> serialNumbers = new List<string>();
            Random r = new Random();
            for (int i = 0; i < 100; i++)
            {
                String s = "";
                for (int j = 0; j < 2; j++)
                    s =s+ randomLetter(r);
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

        private async  void saveSerialNumbers(List<String> serialNr)
        {
            string filename = "SerialNumber.txt";
            StorageFolder localFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
            StorageFile file = await localFolder.CreateFileAsync(filename, CreationCollisionOption.ReplaceExisting);
         
            for (int i = 0; i < serialNr.Count; i++)
            await FileIO.AppendTextAsync(file, (serialNr.ElementAt(i) + "\n"));
            

        }

        private  List<string> readSerialNumbers()
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
            for (int i = 0; i < serialNr.Count; i++)
                xmlResult.Text += serialNr.ElementAt(i)+"\n";
            return serialNr;
        }

    }
    
}
