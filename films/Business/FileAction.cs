using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class FileAction
    {
        string path = "C:/Image/";

        public byte[] ReadFile(string fileName)
        {
            if (fileName != string.Empty)
            {
                if (System.IO.Directory.Exists(path))
                {
                    byte[] readBt = System.IO.File.ReadAllBytes(path + fileName);
                    return  readBt;
                }
            }

            return null;
        }
    }
}
