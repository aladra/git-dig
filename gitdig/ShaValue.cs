using System;
using System.Diagnostics;
using System.Security.Cryptography;

namespace gitdig
{

    public class ShaValue
    {
        private const int shaBytes = 20;

        public readonly  byte[] Value = new byte[shaBytes];


        public static ShaValue FromContents(byte[] contents)
        {
            SHA1 sha = new SHA1CryptoServiceProvider();
            return new ShaValue(sha.ComputeHash(contents));
        }


        private ShaValue(byte[] value)
        {
            Debug.Assert(value.Length == shaBytes);
            for (int j = 0; j < shaBytes; j++)
            {
                Value[j] = value[j];
            }
        }

        public ShaValue(string strRep)
        {
            Debug.Assert(strRep.Length == shaBytes*2);
            int j = 0;
            for (int k = 0; k < shaBytes; k++)
            {
                Value[j++] = Convert.ToByte(strRep.Substring(k * 2, 2), 16);
            }
        }

        public byte[] AsAscii()
        {
            byte[] result = new byte[2*shaBytes];
            string work = ToString();
            for (int j = 0; j < work.Length; j++)
            {
                result[j] = (byte) work[j];
            }
            return result;
        }

        public override string ToString()
        {
            string shaTxt = string.Empty;
            int pos = 0;
            for (int k = 0; k < shaBytes; k++)
            {
                shaTxt += $"{Value[pos++]:x2}";
            }
            return shaTxt;
        }
    }
}
