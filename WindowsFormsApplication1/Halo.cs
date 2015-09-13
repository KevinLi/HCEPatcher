namespace WindowsFormsApplication1
{
    using System;

    internal class Halo
    {
        public long[] Dedi_Sizes = new long[] { 0x1c0000L, 0x1c2438L, 0x1c0000L, 0x1c0000L };
        public string Filename;
        public string[] Halo_Vers = new string[] {
            "01.00.10.0621",
            "01.00.09.0620",
            "01.00.08.0616",
            "01.00.07.0613",
            "01.00.00.0609",
        };
        public string MarkedVersion = "";
        public Patcher Patch = new Patcher();

        public int[] Patch_ASCII1Loc = new int[] {
            0xCDCB9,
            0xCE0B9,
            0xCE089,
            0xCDF79,
            0xCDF08,
        };

        public byte[][] Patch_ASCII1Orig = new byte[][] {
            new byte[] { 0x74, 0x5D },
            new byte[] { 0x74, 0x5D },
            new byte[] { 0x74, 0x5D },
            new byte[] { 0x74, 0x5D },
            new byte[] { 0x74, 0x60 },
        };
        public int[] Patch_ASCII2Loc = new int[] {
            0xCDCCB,
            0xCE0CB,
            0xCE09B,
            0xCDF8B,
            0xCDF1A,
        };
        public byte[][] Patch_ASCII2Orig = new byte[][] {
            new byte[] { 0x74, 0x4B },
            new byte[] { 0x74, 0x4B },
            new byte[] { 0x74, 0x4B },
            new byte[] { 0x74, 0x4B },
            new byte[] { 0x74, 0x4E },
        };

        public byte[][] Patch_DevModeEnable = new byte[][] {
            new byte[] { 0x74, 0x77 },
            new byte[] { 0x74, 0x77 },
            new byte[] { 0x74, 0x77 },
            new byte[] { 0x74, 0x77 },
            new byte[] { 0x74, 0x77 },
        };
        public int[] Patch_DevModeLoc = new int[] {
            0x7DAED,
            0x7DC4D,
            0x7DC3D,
            0x7DC5D,
            0x7DC4D,
        };
        public byte[][] Patch_DevModeOrig = new byte[][] {
            new byte[] { 0x74, 0x18 },
            new byte[] { 0x74, 0x18 },
            new byte[] { 0x74, 0x18 },
            new byte[] { 0x74, 0x18 },
            new byte[] { 0x74, 0x18 },
        };

        public int[] Patch_NoKeyLoc = new int[] {
            0x12F507,
            0x12FA87,
            0x12F9D7,
            0x12F907,
            0x12F857,
        };
        public byte[][] Patch_NoKeyOrig = new byte[][] {
            new byte[] { 0x81, 0xEF, 0xD0, 0xF8, 0x5A, 0x00 },
            new byte[] { 0x81, 0xEF, 0xF0, 0xF8, 0x5A, 0x00 },
            new byte[] { 0x81, 0xEF, 0x68, 0xF8, 0x5A, 0x00 },
            new byte[] { 0x81, 0xEF, 0x68, 0xF8, 0x5A, 0x00 },
            new byte[] { 0x81, 0xEF, 0x68, 0xF8, 0x5A, 0x00 },
        };

        public byte[][] Patch_VerComp = new byte[][] {
            new byte[] { 0x81, 0xF9, 0x27, 0x6A, 0x09, 0x00 },
            new byte[] { 0x81, 0xF9, 0x27, 0x6A, 0x09, 0x00 },
            new byte[] { 0x81, 0xF9, 0xE7, 0xCF, 0x5B, 0x00 },
            new byte[] { 0x81, 0xF9, 0x2F, 0xC4, 0x5B, 0x00 },
            new byte[] { 0x81, 0xF9, 0xCF, 0x4E, 0x09, 0x00 },
        };
        public int[] Patch_VerCompLoc = new int[] {
            0xCB1A1,
            0xCB581,
            0xCB551,
            0xCB441,
            0xCB3E1,
        };

        public int[] Patch_VerLoc = new int[] {
            0x164ADC,
            0x164B34,
            0x164B74,
            0x164B74,
            0x164B74,
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
                    if (readData == "\x2B\xFA")
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

