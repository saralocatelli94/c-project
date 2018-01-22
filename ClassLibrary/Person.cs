using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Windows.Storage;

namespace ClassLibrary
{
    public class Person
    {
        private String name;
        public string Name 
        {
            get { return name; }
            set { name = value; }
        }
        private String surname;
        public String Surname 
        {
            get { return surname; }
            set { surname = value; }
        }
        private String phoneNumber;
        public String PhoneNr 
        {
            get { return phoneNumber; }
            set { phoneNumber = value; }
        }
        private String email;
        public String Email 
        {
            get { return email; }
            set { email = value; }
        }
        private String serialNr;
        public String SerialNr 
        {
            get { return serialNr; }
            set { serialNr = value; }
        }
        private string date;
        public String Date
        {
            get { return date; }
            set { date = value; }
        }

        private string imagePath;
        public String ImagePath
        {
            get { return imagePath; }
            set { imagePath = value; }
        }
        public Person()
        {

        }

        public Boolean validateName()
        {
            Regex regex = new Regex(@"^[a-zA-Z_]\w*(\.[a-zA-Z_]\w*)*$");
            Match match = regex.Match(name);
            if (!match.Success)
            {
                return true;
            }
            return false;
        }
        public Boolean validateSurname()
        {
            Regex regex = new Regex(@"^[a-zA-Z_]\w*(\.[a-zA-Z_]\w*)*$");
            Match match = regex.Match(surname);
            if (!match.Success)
            {
                return true;
            }
            return false;
        }

        public Boolean phoneValidation()
        {
            Regex regex = new Regex(@"\(?\d{3}\)?-? *\d{3}-? *-?\d{4}");
            Match match = regex.Match(phoneNumber);
            if (!match.Success)
            {
                return true;
            }
            return false;
        }

        public Boolean emailValidation()
        {
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(email);
            if (!match.Success)
            {
                return true;
            }
            return false;
        }

        public int validateSerialNumber(List<string> serialNr)
        {
            if (!serialNr.Contains(SerialNr))
            {
                //not a valid serial nr
                return 1;
            }
            else
            {

                string filename = "table.xml";
                string path = ApplicationData.Current.LocalFolder.Path;
                StorageFolder localfolder = Windows.Storage.ApplicationData.Current.LocalFolder;
                ClassLibrary.FileXML fileXML = ClassLibrary.FileXML.GetInstance(path + "/" + filename, localfolder, filename);
                if (fileXML.checkExistence())
                {
                    var n = fileXML.findBySerialnNumber(SerialNr);
                    if (n.Count() > 0)
                    {
                        //serial nr already used
                        return 2;
                    }
                }
            }
            return 0;
        }

        public Boolean imageValidation()
        {
            if (imagePath == "")
            {
                return true;
            }
            return false;
        }

        public Boolean DateValidation(int year)
        {
            int currentYear = DateTime.Now.Year;
            //date validation
            if (year > (currentYear - 10))
            {
                return true;
            }
            return false;
        }

        public Boolean ValidateData()
        {
            bool error = false;
            //name validation
            
            //surname validation
            
           
            //phone nr validation
            
           
            //email validation
           

            //serial number validation
           
            //image validation
        
           
            return error;
        }



    }
}
