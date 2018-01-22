
using System.IO;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.Storage;


// Il modello di elemento Pagina vuota è documentato all'indirizzo https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x410

namespace MandatoryProject2
{
    /// <summary>
    /// Pagina vuota che può essere usata autonomamente oppure per l'esplorazione all'interno di un frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        ClassLibrary.FileSerialNumber fileSerialNumber = null;
        ClassLibrary.FileXML fileXML = null;

        public MainPage()
        {
            this.InitializeComponent();
            fileSerialNumber = ClassLibrary.FileSerialNumber.GetInstance(ApplicationData.Current.LocalFolder.Path + "/SerialNumber.txt");
            fileSerialNumber.generateSerialNumber();
            fileXML = ClassLibrary.FileXML.GetInstance(ApplicationData.Current.LocalFolder.Path+"/"+ "table.xml",ApplicationData.Current.LocalFolder, "table.xml");
            myFrame.Navigate(typeof(BlankPage1));
            ADD_SUBMISSIONS.IsSelected = true;
        }
         private void Hamb_button_Click(object sender, RoutedEventArgs e)
        {
            Hb_menu.IsPaneOpen = true;
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(SEE_SUBMISSIONS.IsSelected)
            {
                if(LogIn.login==false)
                 myFrame.Navigate(typeof(SeeSubmissions));
                else
                   myFrame.Navigate(typeof(SubmissionsView));
            }
            if(ADD_SUBMISSIONS.IsSelected)
            {
                myFrame.Navigate(typeof(BlankPage1));
            }
            if(WINNER.IsSelected)
            {  
                myFrame.Navigate(typeof(Winner));
            }
            if (DELETE_SUBMISSIONS.IsSelected)
            {
                myFrame.Navigate(typeof(Delete));
            }
        }
    }
    
}
