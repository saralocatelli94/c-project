using System;
using System.Collections.Generic;
using System.IO;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.Storage;
using Windows.UI;
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
        ClassLibrary.FileXML fileXML;
        ClassLibrary.FileSerialNumber fileSerialNumber;
        ClassLibrary.Person p = new ClassLibrary.Person();
        string imagePath = "";
        public BlankPage1()
        {
            fileXML = ClassLibrary.FileXML.GetInstance(ApplicationData.Current.LocalFolder.Path + "/" + "table.xml", ApplicationData.Current.LocalFolder, "table.xml");
            fileSerialNumber =  ClassLibrary.FileSerialNumber.GetInstance(ApplicationData.Current.LocalFolder.Path + "/SerialNumber.txt");
            this.InitializeComponent();
        }
        private async void Button_Click(object sender, RoutedEventArgs e)
        {         
            if (serialNr==null)
            {
                serialNr=fileSerialNumber.readSerialNumbers();
            }
               
            p.Name = Name.Text;
            p.Surname = Surname.Text;
            p.Email = Email.Text;
            p.PhoneNr = PhoneNr.Text;
            p.SerialNr = SeriaNr.Text;
            p.Date = Date.Date.Day.ToString() + "-" + Date.Date.Month.ToString() + "-" + Date.Date.Year.ToString();
            p.ImagePath = imagePath;

            if (ValidateData(p))
            {
                ShowDialog_Click("Some parameters are not valid!");
                return;
            }


            if(fileXML.checkExistence())
            {
                fileXML.savePerson(p);
            }

            if(!fileXML.checkExistence())
            {
                await fileXML.createAndSaveAsync(p, "table.xml", Windows.Storage.ApplicationData.Current.LocalFolder); 
            }
            ShowDialog_Click("Done!");

        }

  

        public void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {

            TextBox tb = (TextBox)sender;
            tb.Background=new SolidColorBrush(Color.FromArgb(100, 228, 222, 222));
            tb.BorderBrush = new SolidColorBrush(Color.FromArgb(40, 0, 0, 0));
            tb.Text = "";
        }

        public  Boolean ValidateData(ClassLibrary.Person p)
        {
            bool error = false;
            if(p.validateName())
            {
                textBoxError(Name);
                Name.Text = "Insert only letters.";
                error = true;
            }
            if(p.validateSurname())
            {
                textBoxError(Surname);
                Surname.Text = "Insert only letters.";
                error = true;
            }
            if(p.phoneValidation())
            {
                textBoxError(PhoneNr);
                PhoneNr.Text = "Insert at least 10 numbers.";
                error = true;
            }
            if(p.emailValidation())
            {
                textBoxError(Email);
                Email.Text = "Insert a valid email.";
                error = true;
            }
            int errortype = p.validateSerialNumber(serialNr);
            if (errortype == 1)
            {
                textBoxError(SeriaNr);
                SeriaNr.Text = "Not valid serial number.";
                error = true;
            }
            if(errortype==2)
            {
                SeriaNr.Text = "Serial number already used.";
                textBoxError(SeriaNr);
                error = true;
            }

            if(p.imageValidation())
            {
                Upload.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));
                error = true;
            }
            int year=Int32.Parse( Date.Date.Year.ToString());
            if (p.DateValidation(year))
            {
                Date.Background = new SolidColorBrush(Color.FromArgb(255, 129, 43, 43));
                error = true;
            }
            return error;
        }

        public void textBoxError(TextBox t)
        {
            t.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));
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
                }catch(System.Exception ex )
                {
                    ShowDialog_Click(" the file already exists");
                    return;
                }
                imagePath = Path.GetFileName(sf.Path);
                p.ImagePath =imagePath;
                Windows.Storage.Streams.IRandomAccessStream stream = await sf.OpenAsync(FileAccessMode.Read);
                using (stream)
                {
                    BitmapImage bm = new BitmapImage();
                    bm.SetSource(stream);
                    Image.Source = bm;
                }
            }
        }

        private void DateChanged(object sender, DatePickerValueChangedEventArgs e)
        {
            Date.Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
        }
        private async void ShowDialog_Click(string s)
        {
            // Show the custom dialog
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
