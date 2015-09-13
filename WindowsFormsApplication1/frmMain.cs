namespace WindowsFormsApplication1
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.IO;
    using System.Windows.Forms;

    public class frmMain : Form
    {
        private CheckBox chkDevMode;
        private CheckBox chkNoASCII;
        private CheckBox chkNoKey;
        private CheckBox chkVersion;
        private ComboBox cmbVersion;
        private Button cmdAbout;
        private Button cmdBrowse;
        private Button cmdExit;
        private Button cmdPatch;
        private IContainer components;
        private Halo HaloCE = new Halo();
        internal PictureBox PictureBox1;
        private TextBox txtFilename;
        private TextBox txtStatus;

        public frmMain()
        {
            this.InitializeComponent();
        }

        private void cmdAbout_Click(object sender, EventArgs e)
        {
            new frmAbout().ShowDialog();
        }

        private void cmdBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog {
                Filter = "Applications (*.exe)|*.exe"
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.txtFilename.Text = "";
                this.cmdPatch.Enabled = false;
                this.cmbVersion.Enabled = false;
                this.chkNoKey.Checked = false;
                this.chkNoASCII.Checked = false;
                this.chkDevMode.Checked = false;
                this.chkVersion.Checked = false;
                this.chkNoKey.Enabled = false;
                this.chkNoASCII.Enabled = false;
                this.chkDevMode.Enabled = false;
                this.chkVersion.Enabled = false;
                this.txtStatus.Text = this.txtStatus.Text + "\r\n\r\nOpening...";
                if (this.HaloCE.Open(dialog.FileName))
                {
                    this.txtStatus.Text = this.txtStatus.Text + "Done!";
                    this.txtStatus.Text = this.txtStatus.Text + "\r\nDetermining Version...";
                    int realVersion = this.HaloCE.GetRealVersion();
                    if (realVersion == -1)
                    {
                        this.txtStatus.Text = this.txtStatus.Text + "Failed!\r\nVersion: Unknown";
                    }
                    else
                    {
                        this.txtStatus.Text = this.txtStatus.Text + "Done!\r\nVersion: " + this.HaloCE.Halo_Vers[realVersion].ToString();
                        this.txtStatus.Text = this.txtStatus.Text + "\r\nServerlist Version: " + this.HaloCE.GetVersion().ToString();
                        this.cmbVersion.Enabled = true;
                        this.chkNoKey.Enabled = true;
                        this.chkNoASCII.Enabled = true;
                        this.chkDevMode.Enabled = true;
                        this.chkVersion.Enabled = true;
                        int num2 = this.cmbVersion.FindString(this.HaloCE.GetVersion().ToString());
                        if (num2 == -1)
                        {
                            this.cmbVersion.Items.Add(this.HaloCE.GetVersion().ToString());
                            num2 = this.cmbVersion.FindString(this.HaloCE.GetVersion().ToString());
                        }
                        this.cmbVersion.SelectedIndex = num2;
                        int useAddressSet = this.HaloCE.UseAddressSet;
                        byte[] readData = new byte[0];
                        byte[] buffer2 = new byte[0];
                        byte[] buffer3 = new byte[] { 0x90, 0x90, 0x90, 0x90, 0x90, 0x90 };
                        this.HaloCE.Patch.ReadByte(this.HaloCE.Patch_NoKeyLoc[useAddressSet], 6, ref readData);
                        if (this.HaloCE.Patch.ByteArraySame(readData, buffer3))
                        {
                            this.chkNoKey.Checked = true;
                        }
                        readData = new byte[0];
                        buffer2 = new byte[0];
                        byte[] buffer4 = new byte[] { 0x90, 0x90 };
                        this.HaloCE.Patch.ReadByte(this.HaloCE.Patch_ASCII1Loc[useAddressSet], 2, ref readData);
                        this.HaloCE.Patch.ReadByte(this.HaloCE.Patch_ASCII2Loc[useAddressSet], 2, ref buffer2);
                        if (this.HaloCE.Patch.ByteArraySame(readData, buffer4) && this.HaloCE.Patch.ByteArraySame(buffer2, buffer4))
                        {
                            this.chkNoASCII.Checked = true;
                        }
                        readData = new byte[0];
                        this.HaloCE.Patch.ReadByte(this.HaloCE.Patch_DevModeLoc[useAddressSet], 2, ref readData);
                        if (this.HaloCE.Patch.ByteArraySame(readData, this.HaloCE.Patch_DevModeEnable[useAddressSet]))
                        {
                            this.chkDevMode.Checked = true;
                        }
                        readData = new byte[0];
                        this.HaloCE.Patch.ReadByte(this.HaloCE.Patch_VerCompLoc[useAddressSet], 6, ref readData);
                        byte[] buffer5 = new byte[] { 0x3b, 0xc9, 0x90, 0x90, 0x90, 0x90 };
                        if (this.HaloCE.Patch.ByteArraySame(readData, buffer5))
                        {
                            this.chkVersion.Checked = true;
                        }
                        this.txtFilename.Text = this.HaloCE.Filename;
                        this.cmdPatch.Enabled = true;
                    }
                }
                else
                {
                    this.txtStatus.Text = this.txtStatus.Text + "Failed!";
                }
            }
        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void cmdPatch_Click(object sender, EventArgs e)
        {
            this.txtStatus.Text = this.txtStatus.Text + "\r\n\r\nPatching:";
            if (!File.Exists(this.HaloCE.Filename))
            {
                this.HaloCE.Close();
                MessageBox.Show("File not found!");
            }
            else
            {
                int useAddressSet = this.HaloCE.UseAddressSet;
                int iDFromVersion = this.HaloCE.GetIDFromVersion(this.cmbVersion.Text);
                if (iDFromVersion == -1)
                {
                    this.chkVersion.Checked = false;
                }
                byte[] readData = new byte[0];
                byte[] toWrite = new byte[] { 0x90, 0x90, 0x90, 0x90, 0x90, 0x90 };
                byte[] buffer3 = new byte[] { 0x90, 0x90 };
                this.txtStatus.Text = this.txtStatus.Text + "\r\nCD key check...";
                if (this.chkNoKey.Checked)
                {
                    this.HaloCE.Patch.WriteByte(toWrite, this.HaloCE.Patch_NoKeyLoc[useAddressSet]);
                }
                else
                {
                    this.HaloCE.Patch.WriteByte(this.HaloCE.Patch_NoKeyOrig[useAddressSet], this.HaloCE.Patch_NoKeyLoc[useAddressSet]);
                }
                this.HaloCE.Patch.ReadByte(this.HaloCE.Patch_NoKeyLoc[useAddressSet], 6, ref readData);
                if (this.HaloCE.Patch.ByteArraySame(readData, toWrite))
                {
                    this.txtStatus.Text = this.txtStatus.Text + "Patched!";
                }
                else if (this.HaloCE.Patch.ByteArraySame(readData, this.HaloCE.Patch_NoKeyOrig[useAddressSet]))
                {
                    this.txtStatus.Text = this.txtStatus.Text + "Unpatched!";
                }
                else
                {
                    this.txtStatus.Text = this.txtStatus.Text + "Unknown!";
                }
                this.txtStatus.Text = this.txtStatus.Text + "\r\nASCII Limitation...";
                if (this.chkNoASCII.Checked)
                {
                    this.HaloCE.Patch.WriteByte(buffer3, this.HaloCE.Patch_ASCII1Loc[useAddressSet]);
                    this.HaloCE.Patch.WriteByte(buffer3, this.HaloCE.Patch_ASCII2Loc[useAddressSet]);
                }
                else
                {
                    this.HaloCE.Patch.WriteByte(this.HaloCE.Patch_ASCII1Orig[useAddressSet], this.HaloCE.Patch_ASCII1Loc[useAddressSet]);
                    this.HaloCE.Patch.WriteByte(this.HaloCE.Patch_ASCII2Orig[useAddressSet], this.HaloCE.Patch_ASCII2Loc[useAddressSet]);
                }
                readData = new byte[0];
                this.HaloCE.Patch.ReadByte(this.HaloCE.Patch_ASCII1Loc[useAddressSet], 2, ref readData);
                if (this.HaloCE.Patch.ByteArraySame(readData, buffer3))
                {
                    this.txtStatus.Text = this.txtStatus.Text + "Patched!";
                }
                else if (this.HaloCE.Patch.ByteArraySame(readData, this.HaloCE.Patch_ASCII1Orig[useAddressSet]))
                {
                    this.txtStatus.Text = this.txtStatus.Text + "Unpatched!";
                }
                else
                {
                    this.txtStatus.Text = this.txtStatus.Text + "Unknown!";
                }
                this.txtStatus.Text = this.txtStatus.Text + "\r\nDeveloper mode...";
                if (this.chkDevMode.Checked)
                {
                    this.HaloCE.Patch.WriteByte(this.HaloCE.Patch_DevModeEnable[useAddressSet], this.HaloCE.Patch_DevModeLoc[useAddressSet]);
                }
                else
                {
                    this.HaloCE.Patch.WriteByte(this.HaloCE.Patch_DevModeOrig[useAddressSet], this.HaloCE.Patch_DevModeLoc[useAddressSet]);
                }
                readData = new byte[0];
                this.HaloCE.Patch.ReadByte(this.HaloCE.Patch_DevModeLoc[useAddressSet], 2, ref readData);
                if (this.HaloCE.Patch.ByteArraySame(readData, this.HaloCE.Patch_DevModeEnable[useAddressSet]))
                {
                    this.txtStatus.Text = this.txtStatus.Text + "Patched!";
                }
                else if (this.HaloCE.Patch.ByteArraySame(readData, this.HaloCE.Patch_DevModeOrig[useAddressSet]))
                {
                    this.txtStatus.Text = this.txtStatus.Text + "Unpatched!";
                }
                else
                {
                    this.txtStatus.Text = this.txtStatus.Text + "Unknown!";
                }
                this.txtStatus.Text = this.txtStatus.Text + "\r\nForce version checking...";
                byte[] buffer4 = new byte[] { 0x3b, 0xc9, 0x90, 0x90, 0x90, 0x90 };
                if (this.chkVersion.Checked)
                {
                    this.HaloCE.Patch.WriteByte(buffer4, this.HaloCE.Patch_VerCompLoc[useAddressSet]);
                }
                else
                {
                    this.HaloCE.Patch.WriteByte(this.HaloCE.Patch_VerComp[iDFromVersion], this.HaloCE.Patch_VerCompLoc[useAddressSet]);
                }
                readData = new byte[0];
                this.HaloCE.Patch.ReadByte(this.HaloCE.Patch_VerCompLoc[useAddressSet], 6, ref readData);
                if (this.HaloCE.Patch.ByteArraySame(readData, buffer4))
                {
                    this.txtStatus.Text = this.txtStatus.Text + "Patched!";
                }
                else if (this.HaloCE.Patch.ByteArraySame(readData, this.HaloCE.Patch_VerComp[iDFromVersion]))
                {
                    this.txtStatus.Text = this.txtStatus.Text + "Unpatched!";
                }
                else
                {
                    this.txtStatus.Text = this.txtStatus.Text + "Unknown!";
                }
                if (iDFromVersion > -1)
                {
                    this.txtStatus.Text = this.txtStatus.Text + "\r\nSetting Version...";
                    this.HaloCE.Patch.WriteByte(this.HaloCE.Halo_Vers[iDFromVersion], this.HaloCE.Patch_VerLoc[useAddressSet]);
                    this.txtStatus.Text = this.txtStatus.Text + this.HaloCE.GetVersion();
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ToolTip tip = new ToolTip();
            tip.SetToolTip(this.cmdBrowse, "Browse for file to patch.");
            tip.SetToolTip(this.cmdPatch, "Patch the specified file.");
            tip.SetToolTip(this.cmdAbout, "About");
            tip.SetToolTip(this.cmdExit, "Exit");
            tip.SetToolTip(this.chkNoASCII, "Allow special ASCII in sv_name (only when command used in init.txt).");
            tip.SetToolTip(this.chkNoKey, "Remove CD key check, allowing pirate clients to connect (Crack).");
            tip.SetToolTip(this.chkDevMode, "Enable developer mode commands.");
            tip.SetToolTip(this.cmbVersion, "Server List Version");
            tip.SetToolTip(this.chkVersion, "Allow any version of the HCE client to connect");
            int length = this.HaloCE.Halo_Vers.Length;
            for (int i = 0; i < length; i++)
            {
                this.cmbVersion.Items.Add(this.HaloCE.Halo_Vers[i].ToString());
            }
            this.txtStatus.Text = "Welcome to the Halo: CE dedicated server patching tool by BrandiniMP.\r\n\r\nTo begin patching, first select a file.";
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(frmMain));
            this.PictureBox1 = new PictureBox();
            this.txtStatus = new TextBox();
            this.cmbVersion = new ComboBox();
            this.cmdBrowse = new Button();
            this.txtFilename = new TextBox();
            this.chkNoKey = new CheckBox();
            this.cmdPatch = new Button();
            this.cmdExit = new Button();
            this.cmdAbout = new Button();
            this.chkNoASCII = new CheckBox();
            this.chkDevMode = new CheckBox();
            this.chkVersion = new CheckBox();
            ((ISupportInitialize) this.PictureBox1).BeginInit();
            base.SuspendLayout();
            this.PictureBox1.Image = (Image) manager.GetObject("PictureBox1.Image");
            this.PictureBox1.InitialImage = (Image) manager.GetObject("PictureBox1.InitialImage");
            this.PictureBox1.Location = new Point(1, 0);
            this.PictureBox1.Margin = new Padding(4);
            this.PictureBox1.Name = "PictureBox1";
            this.PictureBox1.Size = new Size(0x218, 0x74);
            this.PictureBox1.TabIndex = 1;
            this.PictureBox1.TabStop = false;
            this.txtStatus.BackColor = SystemColors.ButtonFace;
            this.txtStatus.Enabled = false;
            this.txtStatus.Location = new Point(0x60, 0x92);
            this.txtStatus.Multiline = true;
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.ReadOnly = true;
            this.txtStatus.Size = new Size(0x11f, 0x7f);
            this.txtStatus.TabIndex = 2;
            this.txtStatus.TextChanged += new EventHandler(this.txtStatus_TextChanged);
            this.cmbVersion.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbVersion.Enabled = false;
            this.cmbVersion.FormattingEnabled = true;
            this.cmbVersion.Location = new Point(0x185, 0x92);
            this.cmbVersion.Name = "cmbVersion";
            this.cmbVersion.Size = new Size(140, 0x18);
            this.cmbVersion.TabIndex = 3;
            this.cmdBrowse.Location = new Point(7, 0x77);
            this.cmdBrowse.Name = "cmdBrowse";
            this.cmdBrowse.Size = new Size(0x53, 0x18);
            this.cmdBrowse.TabIndex = 4;
            this.cmdBrowse.Text = "Browse";
            this.cmdBrowse.UseVisualStyleBackColor = true;
            this.cmdBrowse.Click += new EventHandler(this.cmdBrowse_Click);
            this.txtFilename.BackColor = SystemColors.ButtonFace;
            this.txtFilename.Location = new Point(0x60, 120);
            this.txtFilename.Name = "txtFilename";
            this.txtFilename.ReadOnly = true;
            this.txtFilename.Size = new Size(0x1b5, 0x16);
            this.txtFilename.TabIndex = 5;
            this.chkNoKey.AutoSize = true;
            this.chkNoKey.Enabled = false;
            this.chkNoKey.Location = new Point(0x185, 0xb0);
            this.chkNoKey.Name = "chkNoKey";
            this.chkNoKey.Size = new Size(0x8a, 0x15);
            this.chkNoKey.TabIndex = 6;
            this.chkNoKey.Text = "No CD key check";
            this.chkNoKey.UseVisualStyleBackColor = true;
            this.cmdPatch.Enabled = false;
            this.cmdPatch.Location = new Point(7, 0x95);
            this.cmdPatch.Name = "cmdPatch";
            this.cmdPatch.Size = new Size(0x53, 0x18);
            this.cmdPatch.TabIndex = 8;
            this.cmdPatch.Text = "Patch";
            this.cmdPatch.UseVisualStyleBackColor = true;
            this.cmdPatch.Click += new EventHandler(this.cmdPatch_Click);
            this.cmdExit.Location = new Point(7, 0xf9);
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Size = new Size(0x24, 0x18);
            this.cmdExit.TabIndex = 9;
            this.cmdExit.Text = "X";
            this.cmdExit.UseVisualStyleBackColor = true;
            this.cmdExit.Click += new EventHandler(this.cmdExit_Click);
            this.cmdAbout.Location = new Point(0x31, 0xf9);
            this.cmdAbout.Name = "cmdAbout";
            this.cmdAbout.Size = new Size(0x29, 0x18);
            this.cmdAbout.TabIndex = 10;
            this.cmdAbout.Tag = "";
            this.cmdAbout.Text = "?";
            this.cmdAbout.UseVisualStyleBackColor = true;
            this.cmdAbout.Click += new EventHandler(this.cmdAbout_Click);
            this.chkNoASCII.AutoSize = true;
            this.chkNoASCII.Enabled = false;
            this.chkNoASCII.Location = new Point(0x185, 0xca);
            this.chkNoASCII.Name = "chkNoASCII";
            this.chkNoASCII.Size = new Size(0x90, 0x15);
            this.chkNoASCII.TabIndex = 11;
            this.chkNoASCII.Text = "No ASCII limitation";
            this.chkNoASCII.UseVisualStyleBackColor = true;
            this.chkDevMode.AutoSize = true;
            this.chkDevMode.Enabled = false;
            this.chkDevMode.Location = new Point(0x185, 0xe4);
            this.chkDevMode.Name = "chkDevMode";
            this.chkDevMode.Size = new Size(0x86, 0x15);
            this.chkDevMode.TabIndex = 12;
            this.chkDevMode.Text = "Developer mode";
            this.chkDevMode.UseVisualStyleBackColor = true;
            this.chkVersion.AutoSize = true;
            this.chkVersion.Enabled = false;
            this.chkVersion.Location = new Point(0x185, 0xfe);
            this.chkVersion.Name = "chkVersion";
            this.chkVersion.Size = new Size(0x95, 0x15);
            this.chkVersion.TabIndex = 13;
            this.chkVersion.Text = "Don't force version";
            this.chkVersion.UseVisualStyleBackColor = true;
            base.AutoScaleDimensions = new SizeF(8f, 16f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x217, 0x114);
            base.Controls.Add(this.chkVersion);
            base.Controls.Add(this.chkDevMode);
            base.Controls.Add(this.chkNoASCII);
            base.Controls.Add(this.cmdAbout);
            base.Controls.Add(this.cmdExit);
            base.Controls.Add(this.cmdPatch);
            base.Controls.Add(this.chkNoKey);
            base.Controls.Add(this.txtFilename);
            base.Controls.Add(this.cmdBrowse);
            base.Controls.Add(this.cmbVersion);
            base.Controls.Add(this.txtStatus);
            base.Controls.Add(this.PictureBox1);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.MaximizeBox = false;
            base.Name = "frmMain";
            this.Text = "Halo: Custom Edition Patcher";
            base.Load += new EventHandler(this.Form1_Load);
            ((ISupportInitialize) this.PictureBox1).EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.Run(new frmMain());
        }

        private void txtStatus_TextChanged(object sender, EventArgs e)
        {
            this.txtStatus.SelectionStart = this.txtStatus.Text.Length;
            this.txtStatus.ScrollToCaret();
            this.txtStatus.Refresh();
        }
    }
}

