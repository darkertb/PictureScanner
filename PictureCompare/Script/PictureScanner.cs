using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

namespace PictureCompare.Script {
    class PictureScanner {
        public static PictureScanner ins = new PictureScanner();

        public List<string> Scan (string root) {
            Dictionary<string, string> picDict = new Dictionary<string, string>();
            List<string> dupPicPath = new List<string>();
            string[] picsPath = Directory.GetFiles(root, "*", SearchOption.AllDirectories);

            for (int i = 0; i < picsPath.Length; i++) {
                string picPath = picsPath[i];
                string result = "";

                using (StreamReader sr = new StreamReader(picPath)) {
                    result = sr.ReadToEnd();
                }
                HashAlgorithm ha = HashAlgorithm.Create("MD5");

                byte[] picByte = Encoding.Default.GetBytes(result);
                byte[] hashByte = ha.ComputeHash(picByte);
                
                string hash = BitConverter.ToString(hashByte);

                if (picDict.ContainsKey(hash))
                    dupPicPath.Add(picPath);
                else
                    picDict.Add(hash, "");
            }

            return dupPicPath;
        }

        public int Remove (List<string> pathList) {
            int count = 0;
            for (int i = 0; i < pathList.Count; i++) {
                File.Delete(pathList[i]);
                count++;
            }
            return count;
        }

        public int Remove (string path) {
            File.Delete(path);
            return 1;
        }
    }
}
