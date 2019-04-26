using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using WinFormUtilityLibrary.Properties;

namespace WinFormUtilityLibrary.FormUtilities
{
    public partial class ComplexMessageBox : Form
    {
        private ComplexMessageBoxButtons buttonType = ComplexMessageBoxButtons.OKCancel;
        private CultureInfo culture = Thread.CurrentThread.CurrentUICulture;
        private static ComplexMessageBox instance;

        public ComplexMessageBox()
        {
            InitializeComponent();
        }

        public ComplexMessageBox(string header, string message, string messagedetails) : this(header, message)
        {
            lnkDetails.Show();
            txtDetails.Text = messagedetails;
        }
        public ComplexMessageBox(string header, string message):this()
        {
            this.Text = header;
            this.lblMsg.Text = message;
            Reduce();
        }
        public ComplexMessageBox(string header, string message, string messagedetails, ComplexMessageBoxIcon icons, ComplexMessageBoxButtons buttons)
            :this(header, message, icons, buttons)
        {
            lnkDetails.Visible = false;
            txtDetails.Text = messagedetails;
        }

        public ComplexMessageBox(string header, string message, ComplexMessageBoxIcon icons, ComplexMessageBoxButtons buttons):this()
        {
            this.Text = header;
            this.lblMsg.Text = message;

            switch (icons)
            {
                case ComplexMessageBoxIcon.OK:
                    pbIcon.Image = Resources.icons8_Ok_48px;
                    break;
                case ComplexMessageBoxIcon.Info:
                    pbIcon.Image = Resources.icons8_Info_48px;
                    break;
                case ComplexMessageBoxIcon.Question:
                    pbIcon.Image = Resources.icons8_Help_48px;
                    break;
                case ComplexMessageBoxIcon.Warning:
                    pbIcon.Image = Resources.icons8_Error_48px;
                    break;
                case ComplexMessageBoxIcon.Error:
                    pbIcon.Image = Resources.icons8_Cancel_48px_1;
                    break;
                case ComplexMessageBoxIcon.Forbidden:
                    pbIcon.Image = Resources.icons8_No_Entry_48px;
                    break;
                default:
                    break;
            }

            this.buttonType = buttons;

            switch (buttons)
            {
                case ComplexMessageBoxButtons.OKCancel:
                    break;
                case ComplexMessageBoxButtons.YesNo:
                    btOk.Text = culture.TwoLetterISOLanguageName.Contains("en") ? "Yes" : "Oui";
                    btCancel.Text = culture.TwoLetterISOLanguageName.Contains("en") ? "No" : "Non";

                    break;
                case ComplexMessageBoxButtons.OK:
                    btOk.Location = new Point(btOk.Location.X + btCancel.Width + 5, btOk.Location.Y);
                    btCancel.Visible = false;
                    break;
                default:
                    break;
            }
            Reduce();
        }



        public static void Show(string header, string message, string messagedetails)
        {
            Show(header, message);
        }
        public static void Show(string header, string message)
        {
            instance = new ComplexMessageBox(header, message);
            instance.ShowDialog();

        }

        public static void Show(string header, string message, ComplexMessageBoxIcon icons, ComplexMessageBoxButtons buttons)
        {
            instance = new ComplexMessageBox(header, message, icons, buttons);
            instance.ShowDialog();

        }

        public static void Show(string header, string message, string messagedetails, ComplexMessageBoxIcon icons, ComplexMessageBoxButtons buttons)
        {
            instance = new ComplexMessageBox(header, message, icons, buttons);
            instance.lnkDetails.Visible = true;
            instance.txtDetails.Text = messagedetails;
            instance.ShowDialog();
            
        }



        private void Reduce()
        {
            this.Height -= txtDetails.Height;
            txtDetails.Visible = false ;
            lnkDetails.Visible = false;
        }
        private void lnkDetails_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (txtDetails.Visible)
            {
                this.Height -= (txtDetails.Height);
                txtDetails.Visible = false;
            }
            else
            {
                this.Height += (txtDetails.Height);
                txtDetails.Visible = true;
            }
            

        }

        private void btOk_Click(object sender, EventArgs e)
        {
            switch (buttonType)
            {
                case ComplexMessageBoxButtons.OKCancel:
                    this.DialogResult = DialogResult.OK;
                    break;
                case ComplexMessageBoxButtons.YesNo:
                    this.DialogResult = DialogResult.Yes;
                    break;
                case ComplexMessageBoxButtons.OK:
                    this.DialogResult = DialogResult.OK;
                    break;
                default:
                    break;
            }
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            switch (buttonType)
            {
                case ComplexMessageBoxButtons.OKCancel:
                    this.DialogResult = DialogResult.Cancel;
                    break;
                case ComplexMessageBoxButtons.YesNo:
                    this.DialogResult = DialogResult.No;
                    break;
                default:
                    break;
            }
        }

        private void ComplexMessageBox_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult == DialogResult.None)
                this.DialogResult = DialogResult.Abort;
        }
    }
    public enum ComplexMessageBoxIcon 
    {
        OK=0,
        Info = 1,
        Question = 2,
        Warning = 3,
        Error = 4,
        Forbidden = 5
    }

    public enum ComplexMessageBoxButtons 
    {
        OKCancel = 0,
        YesNo = 1,
        OK = 2
    }
}
