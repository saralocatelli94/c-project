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
using Windows.UI.Xaml.Navigation;

// Il modello di elemento Pagina vuota è documentato all'indirizzo https://go.microsoft.com/fwlink/?LinkId=234238

namespace MandatoryProject2
{
    /// <summary>
    /// Pagina vuota che può essere usata autonomamente oppure per l'esplorazione all'interno di un frame.
    /// </summary>
    public sealed partial class Delete : Page
    {
        public Delete()
        {
            this.InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            string serialNr = SerialValue.Text;
            string filename = "table.xml";
            StorageFolder sf = ApplicationData.Current.LocalFolder;
            string folder = sf.Path;
            

            var file = Path.Combine(folder, filename);
            var doc = XDocument.Load(file);
            XElement xelement = doc.Root;
            var person = from nm in xelement.Elements("Person")
                         where (string)nm.Element("SerialNr") == serialNr
                         select nm;

            if (person.Count() == 0)
                customDialog("Submission not found");
            else
            {
                person.ElementAt(0).Remove();
                customDialog("Submission removed correctly");
                // StorageFolder localfolder = Windows.Storage.ApplicationData.Current.LocalFolder;
                StorageFile f = await sf.CreateFileAsync(filename, CreationCollisionOption.ReplaceExisting);
                using (var s = await f.OpenStreamForWriteAsync())
                {
                    doc.Save(s);
                }


            }
        }

            private async void customDialog(string s)
            {
                ContentDialog dialog = new ContentDialog()
                {
                    Title = "Custom dialog",
                    MaxWidth = this.ActualWidth,
                    PrimaryButtonText = "OK",
                    Content = new TextBlock
                    {
                        Text = s
                    },
                };
                await dialog.ShowAsync();
            }
        }
}
