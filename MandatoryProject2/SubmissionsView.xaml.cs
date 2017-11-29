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
            uploadSubmissions();



        }

        public async void uploadSubmissions()
        {
            string filename = "table.xml";
            string folder = ApplicationData.Current.LocalFolder.Path;
            string path = folder + "/" + filename;
            if (!File.Exists(folder + "/" + filename))
                return;
            List<string> imageNames = new List<string>();
            using (StreamReader reader = File.OpenText(path))
            {
                XDocument xmlFile = XDocument.Load(reader);
                IEnumerable<XElement> people = xmlFile.Root.Elements();

                foreach (var x in people)
                {

                    imageNames.Add(x.Element("Image").Value.ToString());
                   // Query.Text += x.Element("Image").Value.ToString();
                }
                for (int i = 0; i < 10; i++)
                {
                    if (i >= imageNames.Count)
                        return;
                    Image image = new Image();
                   
                    BitmapImage bm = new BitmapImage();
                    StorageFolder sf = ApplicationData.Current.LocalFolder;

                    StorageFile file = await sf.GetFileAsync(imageNames.ElementAt(i));
                    Windows.Storage.Streams.IRandomAccessStream stream = await file.OpenAsync(FileAccessMode.Read);
                    bm.SetSource(stream);

                    Im.Source = bm;

                }


            }
        }
    }
}
