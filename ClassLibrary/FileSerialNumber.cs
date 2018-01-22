using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Storage;

namespace ClassLibrary
{
    public sealed class FileSerialNumber: FileClass
    {
        String path;
        List<String> serialNr;
        private static FileSerialNumber instance = null;

        private FileSerialNumber(String path) : base(path)
        {
            this.path = path;
        }
        public static FileSerialNumber GetInstance(String path)
        {
            if (instance == null)
            {
                if (instance == null)
                {
                    instance = new FileSerialNumber(path);
                }
            }

            return instance;
        }


        public void generateSerialNumber()
        {
            if (!checkExistence())
            {
                serialNr = generateSerialNumbers();
                saveSerialNumbers();
            }
        }

        private List<String> generateSerialNumbers()
        {
            List<String> serialNumbers = new List<string>();
            Random r = new Random();
            for (int i = 0; i < 100; i++)
            {
                String s = "";
                for (int j = 0; j < 2; j++)
                    s = s + randomLetter(r);
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
            string chars = "$%#@!*abcdefghijklmnopqrstuvwxyz?;:ABCDEFGHIJKLMNOPQRSTUVWXYZ^&";
            int n = r.Next(0, chars.Length - 1);
            char c = chars[n];
            return c.ToString();
        }

        private int randomNumber(Random r)
        {
            int n = r.Next(0, 9);
            return n;
        }


        private async void saveSerialNumbers()
        {
            string filename = "SerialNumber.txt";
            StorageFolder localFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
            StorageFile file = await localFolder.CreateFileAsync(filename, CreationCollisionOption.ReplaceExisting);
            for (int i = 0; i < serialNr.Count; i++)
                await FileIO.AppendTextAsync(file, (serialNr.ElementAt(i) + "\n"));
        }

        public List<string> readSerialNumbers()
        {
            List<String> serialNr = new List<String>();
            string[] lines;
            try
            {
                using (StreamReader reader = File.OpenText(path))
                {

                    lines = reader.ReadToEnd().Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                    foreach (var line in lines)
                        serialNr.Add(line);
                }
            }catch(FileNotFoundException e)
            {
                throw new Exception("file not found", e);
            }
            catch(Exception e)
            {
                throw new Exception("another exception occurred",e);
            }
            return serialNr;
        }

    }
  


}
