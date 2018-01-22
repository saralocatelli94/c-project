using System;
using System.IO;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;


// Il modello di elemento Pagina vuota è documentato all'indirizzo https://go.microsoft.com/fwlink/?LinkId=234238

namespace MandatoryProject2
{
    /// <summary>
    /// Pagina vuota che può essere usata autonomamente oppure per l'esplorazione all'interno di un frame.
    /// </summary>
    public sealed partial class Delete : Page
    {
        ClassLibrary.FileXML fileXML;
        public Delete()
        {
            fileXML = ClassLibrary.FileXML.GetInstance(ApplicationData.Current.LocalFolder.Path + "/" + "table.xml", 
                ApplicationData.Current.LocalFolder, "table.xml");
            this.InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            Boolean res=await fileXML.deletePersonAsync(SerialValue.Text);
            if(res)
                customDialog("Submission removed correctly");
            else
                customDialog("Submission not found");
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
