﻿namespace WindowsFormsApplication1
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.IO;
    using System.Reflection;
    using System.Windows.Forms;

    internal class frmAbout : Form
    {
        private IContainer components;
        private Label labelCompanyName;
        private Label labelCopyright;
        private Label labelProductName;
        private Label labelVersion;
        private PictureBox logoPictureBox;
        private Button okButton;
        private TableLayoutPanel tableLayoutPanel;
        private TextBox textBoxDescription;

        public frmAbout()
        {
            this.InitializeComponent();
            this.Text = string.Format("About {0}", this.AssemblyTitle);
            this.labelProductName.Text = "HaloCE Dedicated Server Patcher";
            this.labelVersion.Text = string.Format("Version: {0}", this.AssemblyVersion);
            this.labelCopyright.Text = this.AssemblyCopyright;
            this.labelCompanyName.Text = this.AssemblyCompany;
        }

        private void AboutBox1_Load(object sender, EventArgs e)
        {
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(frmAbout));
            this.tableLayoutPanel = new TableLayoutPanel();
            this.logoPictureBox = new PictureBox();
            this.labelProductName = new Label();
            this.labelVersion = new Label();
            this.labelCopyright = new Label();
            this.labelCompanyName = new Label();
            this.textBoxDescription = new TextBox();
            this.okButton = new Button();
            this.tableLayoutPanel.SuspendLayout();
            ((ISupportInitialize) this.logoPictureBox).BeginInit();
            base.SuspendLayout();
            this.tableLayoutPanel.ColumnCount = 2;
            this.tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33f));
            this.tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 67f));
            this.tableLayoutPanel.Controls.Add(this.logoPictureBox, 0, 0);
            this.tableLayoutPanel.Controls.Add(this.labelProductName, 1, 0);
            this.tableLayoutPanel.Controls.Add(this.labelVersion, 1, 1);
            this.tableLayoutPanel.Controls.Add(this.labelCopyright, 1, 2);
            this.tableLayoutPanel.Controls.Add(this.labelCompanyName, 1, 3);
            this.tableLayoutPanel.Controls.Add(this.textBoxDescription, 1, 4);
            this.tableLayoutPanel.Controls.Add(this.okButton, 1, 5);
            this.tableLayoutPanel.Dock = DockStyle.Fill;
            this.tableLayoutPanel.Location = new Point(12, 11);
            this.tableLayoutPanel.Margin = new Padding(4);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 6;
            this.tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 10f));
            this.tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 10f));
            this.tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 10f));
            this.tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 10f));
            this.tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 50f));
            this.tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 10f));
            this.tableLayoutPanel.Size = new Size(0x22c, 0x146);
            this.tableLayoutPanel.TabIndex = 0;
            this.logoPictureBox.Dock = DockStyle.Fill;
            this.logoPictureBox.Image = (Image) manager.GetObject("logoPictureBox.Image");
            this.logoPictureBox.Location = new Point(4, 4);
            this.logoPictureBox.Margin = new Padding(4);
            this.logoPictureBox.Name = "logoPictureBox";
            this.tableLayoutPanel.SetRowSpan(this.logoPictureBox, 6);
            this.logoPictureBox.Size = new Size(0xaf, 0x13e);
            this.logoPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            this.logoPictureBox.TabIndex = 12;
            this.logoPictureBox.TabStop = false;
            this.labelProductName.Dock = DockStyle.Fill;
            this.labelProductName.Location = new Point(0xbf, 0);
            this.labelProductName.Margin = new Padding(8, 0, 4, 0);
            this.labelProductName.MaximumSize = new Size(0, 0x15);
            this.labelProductName.Name = "labelProductName";
            this.labelProductName.Size = new Size(0x169, 0x15);
            this.labelProductName.TabIndex = 0x13;
            this.labelProductName.Text = "Product Name";
            this.labelProductName.TextAlign = ContentAlignment.MiddleLeft;
            this.labelVersion.Dock = DockStyle.Fill;
            this.labelVersion.Location = new Point(0xbf, 0x20);
            this.labelVersion.Margin = new Padding(8, 0, 4, 0);
            this.labelVersion.MaximumSize = new Size(0, 0x15);
            this.labelVersion.Name = "labelVersion";
            this.labelVersion.Size = new Size(0x169, 0x15);
            this.labelVersion.TabIndex = 0;
            this.labelVersion.Text = "Version";
            this.labelVersion.TextAlign = ContentAlignment.MiddleLeft;
            this.labelCopyright.Dock = DockStyle.Fill;
            this.labelCopyright.Location = new Point(0xbf, 0x40);
            this.labelCopyright.Margin = new Padding(8, 0, 4, 0);
            this.labelCopyright.MaximumSize = new Size(0, 0x15);
            this.labelCopyright.Name = "labelCopyright";
            this.labelCopyright.Size = new Size(0x169, 0x15);
            this.labelCopyright.TabIndex = 0x15;
            this.labelCopyright.Text = "Copyright";
            this.labelCopyright.TextAlign = ContentAlignment.MiddleLeft;
            this.labelCompanyName.Dock = DockStyle.Fill;
            this.labelCompanyName.Location = new Point(0xbf, 0x60);
            this.labelCompanyName.Margin = new Padding(8, 0, 4, 0);
            this.labelCompanyName.MaximumSize = new Size(0, 0x15);
            this.labelCompanyName.Name = "labelCompanyName";
            this.labelCompanyName.Size = new Size(0x169, 0x15);
            this.labelCompanyName.TabIndex = 0x16;
            this.labelCompanyName.Text = "Company Name";
            this.labelCompanyName.TextAlign = ContentAlignment.MiddleLeft;
            this.textBoxDescription.Dock = DockStyle.Fill;
            this.textBoxDescription.Location = new Point(0xbf, 0x84);
            this.textBoxDescription.Margin = new Padding(8, 4, 4, 4);
            this.textBoxDescription.Multiline = true;
            this.textBoxDescription.Name = "textBoxDescription";
            this.textBoxDescription.ReadOnly = true;
            this.textBoxDescription.ScrollBars = ScrollBars.Both;
            this.textBoxDescription.Size = new Size(0x169, 0x9b);
            this.textBoxDescription.TabIndex = 0x17;
            this.textBoxDescription.TabStop = false;
            this.textBoxDescription.Text = manager.GetString("textBoxDescription.Text");
            this.okButton.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            this.okButton.DialogResult = DialogResult.Cancel;
            this.okButton.Location = new Point(0x1c4, 0x127);
            this.okButton.Margin = new Padding(4);
            this.okButton.Name = "okButton";
            this.okButton.Size = new Size(100, 0x1b);
            this.okButton.TabIndex = 0x18;
            this.okButton.Text = "&OK";
            base.AcceptButton = this.okButton;
            base.AutoScaleDimensions = new SizeF(8f, 16f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(580, 0x15c);
            base.Controls.Add(this.tableLayoutPanel);
            base.FormBorderStyle = FormBorderStyle.FixedDialog;
            base.Margin = new Padding(4);
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmAbout";
            base.Padding = new Padding(12, 11, 12, 11);
            base.ShowIcon = false;
            base.ShowInTaskbar = false;
            base.StartPosition = FormStartPosition.CenterParent;
            this.Text = "AboutBox1";
            base.Load += new EventHandler(this.AboutBox1_Load);
            this.tableLayoutPanel.ResumeLayout(false);
            this.tableLayoutPanel.PerformLayout();
            ((ISupportInitialize) this.logoPictureBox).EndInit();
            base.ResumeLayout(false);
        }

        public string AssemblyCompany
        {
            get
            {
                object[] customAttributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                if (customAttributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCompanyAttribute) customAttributes[0]).Company;
            }
        }

        public string AssemblyCopyright
        {
            get
            {
                object[] customAttributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                if (customAttributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCopyrightAttribute) customAttributes[0]).Copyright;
            }
        }

        public string AssemblyDescription
        {
            get
            {
                object[] customAttributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                if (customAttributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyDescriptionAttribute) customAttributes[0]).Description;
            }
        }

        public string AssemblyProduct
        {
            get
            {
                object[] customAttributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                if (customAttributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyProductAttribute) customAttributes[0]).Product;
            }
        }

        public string AssemblyTitle
        {
            get
            {
                object[] customAttributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (customAttributes.Length > 0)
                {
                    AssemblyTitleAttribute attribute = (AssemblyTitleAttribute) customAttributes[0];
                    if (attribute.Title != "")
                    {
                        return attribute.Title;
                    }
                }
                return Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }

        public string AssemblyVersion
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }
    }
}

