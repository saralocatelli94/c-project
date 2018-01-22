using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ClassLibrary
{
     public class FileClass
    {
        String path;

        public FileClass(String path)
        {
            this.path = path;
        }

        public Boolean checkExistence()
        {
            if (File.Exists(path))
                return true;
            return false;
        }
    }
}
