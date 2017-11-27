using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
          /*  using (var stream = new StringReader(xml))
            {
                XDocument xmlFile = XDocument.Load(stream);

                var query = (IEnumerable)xmlFile.XPathEvaluate("/data/item/parameter[@type='id']");

                foreach (var x in query.Cast<XElement>())
                {
                    Console.WriteLine(x.Value);
                }

            }*/
        }
    }
}
