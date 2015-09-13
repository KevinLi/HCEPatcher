namespace WindowsFormsApplication1
{
    using System;

    internal class Halo
    {
        public long[] Dedi_Sizes = new long[] { 0x1c0000L, 0x1c2438L, 0x1c0000L, 0x1c0000L };
        public string Filename;
        public string[] Halo_Vers = new string[] {
            "01.00.09.0620",
            "01.00.08.0616",
            "01.00.07.0613",
            "01.00.00.0609",
        };
        public string MarkedVersion = "";
        public Patcher Patch = new Patcher();

        public int[] Patch_ASCII1Loc = new int[] {
            0xce0b9,
            0xce089,
            0xcdf79,
            0xcdf08,
        };

        public byte[][] Patch_ASCII1Orig = new byte[][] {
            new byte[] { 0x74, 0x5d },
            new byte[] { 0x74, 0x5d },
            new byte[] { 0x74, 0x5d },
            new byte[] { 0x74, 0x60 },
        };
        public int[] Patch_ASCII2Loc = new int[] {
            0xce0cb,
            0xce09b,
            0xcdf8b,
            0xcdf1a,
        };
        public byte[][] Patch_ASCII2Orig = new byte[][] {
            new byte[] { 0x74, 0x4b },
            new byte[] { 0x74, 0x4b },
            new byte[] { 0x74, 0x4b },
            new byte[] { 0x74, 0x4e },
        };

        public byte[][] Patch_DevModeEnable = new byte[][] {
            new byte[] { 0x74, 0x77 },
            new byte[] { 0x74, 0x77 },
            new byte[] { 0x74, 0x77 },
            new byte[] { 0x74, 0x77 },
        };
        public int[] Patch_DevModeLoc = new int[] {
            0x7dc4d,
            0x7dc3d,
            0x7dc5d,
            0x7dc4d,
        };
        public byte[][] Patch_DevModeOrig = new byte[][] {
            new byte[] { 0x74, 0x18 },
            new byte[] { 0x74, 0x18 },
            new byte[] { 0x74, 0x18 },
            new byte[] { 0x74, 0x18 },
        };

        public int[] Patch_NoKeyLoc = new int[] {
            0x12fa87,
            0x12f9d7,
            0x12f907,
            0x12f857,
        };
        public byte[][] Patch_NoKeyOrig = new byte[][] {
            new byte[] { 0x81, 0xef, 0xf0, 0xf8, 0x5a, 0 },
            new byte[] { 0x81, 0xef, 0x68, 0xf8, 0x5a, 0 },
            new byte[] { 0x81, 0xef, 0x68, 0xf8, 0x5a, 0 },
            new byte[] { 0x81, 0xef, 0x68, 0xf8, 0x5a, 0 },
        };

        public byte[][] Patch_VerComp = new byte[][] {
            new byte[] { 0x81, 0xf9, 0x27, 0x6a, 0x09, 0 },
            new byte[] { 0x81, 0xf9, 0xe7, 0xcf, 0x5b, 0 },
            new byte[] { 0x81, 0xf9, 0x2f, 0xc4, 0x5b, 0 },
            new byte[] { 0x81, 0xf9, 0xcf, 0x4e, 0x09, 0 },
        };
        public int[] Patch_VerCompLoc = new int[] {
            0xcb581,
            0xcb551,
            0xcb441,
            0xcb3e1,
        };

        public int[] Patch_VerLoc = new int[] {
            0x164b34,
            0x164b74,
            0x164b74,
            0x164b74,
        };

        public int UseAddressSet = -1;

        public void Close()
        {
            this.Filename = "";
            this.MarkedVersion = "";
            this.UseAddressSet = -1;
        }

        public int GetIDFromVersion(string Ver)
        {
            int length = this.Halo_Vers.Length;
            for (int i = 0; i < length; i++)
            {
                if (this.Halo_Vers[i] == Ver)
                {
                    return i;
                }
            }
            return -1;
        }

        public int GetRealVersion()
        {
            int length = this.Halo_Vers.Length;
            string readData = "";
            if (length <= this.Patch_VerLoc.Length)
            {
                for (int i = 0; i < length; i++)
                {
                    this.Patch.ReadString(this.Patch_NoKeyLoc[i] - 2, 2, ref readData);
                    if (readData == "+\x00fa")
                    {
                        return i;
                    }
                }
            }
            return -1;
        }

        public string GetVersion()
        {
            int realVersion = this.GetRealVersion();
            if (realVersion == -1)
            {
                return "00.00.00.0000";
            }
            string readData = "";
            this.Patch.ReadString(this.Patch_VerLoc[realVersion], 13, ref readData);
            return readData;
        }

        public bool Open(string Filename)
        {
            if (!this.Patch.OpenFile(Filename))
            {
                this.Filename = "";
                return false;
            }
            this.Filename = Filename;
            this.MarkedVersion = this.GetVersion();
            this.UseAddressSet = this.GetRealVersion();
            return true;
        }
    }
}

