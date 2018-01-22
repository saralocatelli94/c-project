using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Windows.Storage;

namespace ClassLibrary
{
    public sealed class FileXML:FileClass
    {
        XDocument doc;
        private String path;
        private static FileXML instance=null;

       
        public String Path
        {
            get { return path; }
        }

        private StorageFolder sf;
        //private StorageFolder SF;
        private String filename;
        private FileXML(string path) : base(path)
        {
        }
        private FileXML(String path,StorageFolder sf,String filename) :base(path)
        {
            this.path = path;
            this.sf = sf;
            this.filename = filename;
            
        }

        public static FileXML GetInstance(String path, StorageFolder sf, String filename)
        {
            if (instance == null)
            {
                    if (instance == null)
                    {
                        instance = new FileXML(path,sf,filename);
                    } 
            }

            return instance;
        }


        public void savePerson(Person p)
        {
            try
            {
                doc = XDocument.Load(path);
                doc.Root.Add(new XElement("Person",
                new XElement("name", p.Name),
                new XElement("surname", p.Surname),
                new XElement("Date", p.Date),
                new XElement("PhoneNr", p.PhoneNr),
                new XElement("Email", p.Email),
                new XElement("Image", p.ImagePath),
                new XElement("SerialNr", p.SerialNr),
                new XElement("Winner", "N")
                 ));
            }
            catch (FileNotFoundException)
            {
                throw new Exception("File not found");
            }

            try
            {
                using (var stream = new FileStream(path, FileMode.Open))
                {
                    doc.Save(stream);
                    stream.Flush();
                }
            }catch(FileNotFoundException)
            {
                throw new Exception("file not found");
            }
        }

        public async Task createAndSaveAsync(Person p, string filename, StorageFolder localfolder)
        {
            StorageFile file = await localfolder.CreateFileAsync(filename, CreationCollisionOption.ReplaceExisting);
            try
            {
                using (var stream = await file.OpenStreamForWriteAsync())
                {
                    doc =
                     new XDocument(
                     new XElement("Subbmissions",
                     new XElement("Person",
                     new XElement("name", p.Name),
                     new XElement("surname", p.Surname),
                     new XElement("Date", p.Date),
                     new XElement("PhoneNr", p.PhoneNr),
                     new XElement("Email", p.Email),
                     new XElement("Image", p.ImagePath),
                     new XElement("SerialNr", p.SerialNr),
                     new XElement("Winner", "N")
                      )));
                    doc.Save(stream);
                    stream.Flush();

                }
            }catch(Exception e)
            {
                throw new Exception(" An exception occourred ", e);
            }
        }

        public IEnumerable<XElement> findBySerialnNumber(string password)
        {
            try
            {
                doc = XDocument.Load(path);
                XElement xelement = doc.Root;
                var person = from nm in xelement.Elements("Person")
                             where (string)nm.Element("SerialNr") == password
                             select nm;
                return person;
            }
            catch(FileNotFoundException)
            {
                throw new Exception("file not found");
            }
            catch (Exception)
            {
                throw new Exception(" another exception occurred");
            }

        }

        public List<Person> getSubmissions()
        {
            List<Person> peopleSubmissions = new List<Person>();
            try
            {
                using (StreamReader reader = File.OpenText(path))
                {
                    XDocument xmlFile = XDocument.Load(reader);
                    IEnumerable<XElement> people = xmlFile.Root.Elements();
                    int count = people.Count();

                    foreach (var x in people)
                    {
                        Person p = new Person();
                        p.ImagePath = (x.Element("Image").Value.ToString());
                        p.Name = (x.Element("name").Value.ToString());
                        p.Surname = (x.Element("surname").Value.ToString());
                        p.Email = (x.Element("Email").Value.ToString());
                        p.PhoneNr = (x.Element("PhoneNr").Value.ToString());
                        p.Date = (x.Element("Date").Value.ToString());
                        p.SerialNr = (x.Element("SerialNr").Value.ToString());
                        peopleSubmissions.Add(p);
                    }
                }
            }catch(FileNotFoundException)
            {
                throw new Exception("file not found");
            }
            return peopleSubmissions;
        }

        public async Task<bool> deletePersonAsync(string serialNr)
        {
            try
            {
                var doc = XDocument.Load(path);
            }catch(FileNotFoundException)
            { throw new Exception("File not found"); }
            XElement xelement = doc.Root;
            var person = from nm in xelement.Elements("Person")
                         where (string)nm.Element("SerialNr") == serialNr
                         select nm;
            if (person.Count() == 0)
                return false;
            else
            {
                string image=person.ElementAt(0).Element("Image").Value;
                string imagePath = sf.Path + @"\" + image;
                File.Delete(imagePath);
                person.ElementAt(0).Remove();             
                StorageFile f = await sf.CreateFileAsync(filename, CreationCollisionOption.ReplaceExisting);
                try
                {
                    using (var s = await f.OpenStreamForWriteAsync())
                    {
                        doc.Save(s);
                    }
                }
                catch(Exception )
                { throw new Exception("some exceptions occour"); }
                return true;
            }
        }

        public int numberSubmission()
        {
            if (File.Exists(path + "/" + filename))
            {
                try
                {
                    doc = XDocument.Load(path);
                }catch(FileNotFoundException)
                {
                    throw new Exception("File not found");
                }
                XElement xelement = doc.Root;
                int number = doc.Root.Elements().Count();
                return number;
            }
            return -1;
        }


        public async Task<XElement> chooseWinnerAsync(Random r)
        {
                XElement winnerPerson;
                try
                {
                    doc = XDocument.Load(path);
                }catch(FileNotFoundException)
                {
                    throw new Exception("File not found");
                }
                XElement xelement = doc.Root;
                int number = doc.Root.Elements().Count();
                if (number == 0)
                     return null;
                var person = from nm in xelement.Elements("Person")
                             where (string)nm.Element("Winner") == "Y"
                             select nm;
                if (person.Count() > 0)
                    winnerPerson = person.ElementAt(0);
                else
                {
                    int winner = r.Next(0, number - 1);
                    winnerPerson = doc.Descendants("Person").ElementAt(winner);
                    winnerPerson.Element("Winner").Value = "Y";
                    StorageFile f = await sf.CreateFileAsync(filename, CreationCollisionOption.ReplaceExisting);
                    try
                    {
                        using (var s = await f.OpenStreamForWriteAsync())
                        {
                            doc.Save(s);
                        }
                    }catch(Exception)
                    {
                        throw new Exception
                            ("File not found");
                    }
                }
                return winnerPerson;
        }

        public async Task changeWinnerAsync(Random r)
        {
            XDocument doc;
            try
            {
                doc = XDocument.Load(path);
            }catch(FileNotFoundException)
            { throw new Exception("File not found"); }
            XElement xelement = doc.Root;
            var person = from nm in xelement.Elements("Person")
                            where (string)nm.Element("Winner") == "Y"
                            select nm;
            person.ElementAt(0).Element("Winner").Value = "N";
            StorageFile f = await sf.CreateFileAsync(filename, CreationCollisionOption.ReplaceExisting);
            try
            {
                using (var s = await f.OpenStreamForWriteAsync())
                {
                    doc.Save(s);
                }
            }catch(Exception e)
            {
                throw new Exception("File not found");
            }
            await chooseWinnerAsync(r);
        }
            

      

    }
}
