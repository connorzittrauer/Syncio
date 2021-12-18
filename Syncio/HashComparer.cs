using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Syncio
{
    class HashComparer
    {
        public bool Compare(string FileA, string FileB)
        {
            //string FolderA = @"C:\Users\Connor\3D Objects\Test\doc1.txt";
            //string FolderB = @"C:\Users\Connor\3D Objects\Test2\txt.txt";

            string hash;
            string hash2;

            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(FileA))
                {
                    hash = BitConverter.ToString(md5.ComputeHash(stream)).Replace("-", "").ToLower();
                }
                using (var stream = File.OpenRead(FileB))
                {
                    hash2 = BitConverter.ToString(md5.ComputeHash(stream)).Replace("-", "").ToLower();
                }
            }
            Console.WriteLine(hash);
            Console.WriteLine(hash2);

            FileInfo file = new FileInfo(FileA);
            FileInfo file2 = new FileInfo(FileB);

            if (hash == hash2 && file.Name == file2.Name)
            {
                // Both files are the same, so you can do your stuff here
                Debug.WriteLine("Equal");
                return true;

            }
            else
            {
                return false;
            }
        }
    }
}
