namespace WindowsFormsApplication1
{
    using System;
    using System.IO;
    using System.Text;

    internal class Patcher
    {
        private string Filename;
        private long prvFileSize;

        public bool ByteArraySame(byte[] arr1, byte[] arr2)
        {
            int length = arr1.Length;
            int num2 = arr2.Length;
            if (length != num2)
            {
                return false;
            }
            for (int i = 0; i < length; i++)
            {
                if (arr1[i] != arr2[i])
                {
                    return false;
                }
            }
            return true;
        }

        public bool CanAccessFile(string path)
        {
            try
            {
                using (FileStream stream = new FileStream(path, FileMode.Open, FileAccess.ReadWrite))
                {
                    if ((!stream.CanRead || !stream.CanWrite) || !stream.CanSeek)
                    {
                        stream.Close();
                        return false;
                    }
                }
                return true;
            }
            catch (IOException)
            {
                return false;
            }
        }

        public void CloseFile()
        {
            this.Filename = null;
            this.prvFileSize = 0L;
        }

        public string ConvertToHex(string asciiString)
        {
            string str = "";
            foreach (char ch in asciiString)
            {
                str = str + string.Format("{0:x2}", Convert.ToUInt32(ch));
            }
            return str;
        }

        public bool OpenFile(string Filename)
        {
            this.Filename = null;
            if (!this.CanAccessFile(Filename))
            {
                return false;
            }
            if (File.Exists(Filename))
            {
                this.Filename = Filename;
            }
            FileStream stream = new FileStream(Filename, FileMode.Open, FileAccess.ReadWrite);
            this.prvFileSize = stream.Length;
            stream.Close();
            return (this.Filename != null);
        }

        public int ReadByte(int Address, int Length, ref byte[] ReadData)
        {
            if (this.Filename == null)
            {
                return 0;
            }
            if (Length < 1)
            {
                return 0;
            }
            FileStream stream = new FileStream(this.Filename, FileMode.Open, FileAccess.Read);
            byte[] buffer = new byte[Length];
            stream.Seek((long) Address, SeekOrigin.Begin);
            for (int i = 0; i < Length; i++)
            {
                buffer[i] = Convert.ToByte(stream.ReadByte());
                if (((i + 1) < Length) && (Convert.ToInt32(buffer[i + 1]) == -1))
                {
                    break;
                }
            }
            stream.Close();
            ReadData = buffer;
            return ReadData.Length;
        }

        public long ReadString(int Address, int Length, ref string ReadData)
        {
            if (this.Filename == null)
            {
                return 0L;
            }
            if (Length < 1)
            {
                return 0L;
            }
            StreamReader reader = new StreamReader(new FileStream(this.Filename, FileMode.Open, FileAccess.Read));
            char[] chArray = new char[Length];
            reader.BaseStream.Seek((long) Address, SeekOrigin.Begin);
            for (int i = 0; i < Length; i++)
            {
                chArray[i] = (char) Convert.ToUInt32(reader.BaseStream.ReadByte());
                if (((i + 1) < Length) && (Convert.ToInt32(chArray[i + 1]) == -1))
                {
                    break;
                }
            }
            reader.Close();
            ReadData = new string(chArray);
            return (long) ReadData.Length;
        }

        public byte[] StrToByteArray(string str)
        {
            ASCIIEncoding encoding = new ASCIIEncoding();
            return encoding.GetBytes(str);
        }

        public bool WriteByte(byte[] ToWrite, int Address)
        {
            if (this.Filename == null)
            {
                return false;
            }
            FileStream stream = new FileStream(this.Filename, FileMode.Open, FileAccess.Write);
            stream.Seek((long) Address, SeekOrigin.Begin);
            stream.Write(ToWrite, 0, ToWrite.Length);
            stream.Flush();
            stream.Close();
            return true;
        }

        public bool WriteByte(byte ToWrite, int Address)
        {
            if (this.Filename == null)
            {
                return false;
            }
            FileStream stream = new FileStream(this.Filename, FileMode.Open, FileAccess.Write);
            stream.Seek((long) Address, SeekOrigin.Begin);
            stream.WriteByte(ToWrite);
            stream.Flush();
            stream.Close();
            return true;
        }

        public bool WriteByte(string ToWrite, int Address)
        {
            if (this.Filename == null)
            {
                return false;
            }
            FileStream stream = new FileStream(this.Filename, FileMode.Open, FileAccess.Write);
            stream.Seek((long) Address, SeekOrigin.Begin);
            stream.Write(this.StrToByteArray(ToWrite), 0, ToWrite.Length);
            stream.Flush();
            stream.Close();
            return true;
        }

        public long FileSize
        {
            get
            {
                return this.prvFileSize;
            }
        }
    }
}

