using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Photo
{
    public partial class frMain : Form
    {
        public frMain()
        {
            InitializeComponent();
        }



        private void ProcessPhoto(object sender, ToolStripItemClickedEventArgs e)
        {
            ToolStripItem item = e.ClickedItem;
            string enumVal = item.Tag as string;
            if (enumVal != null)
            {
                pbxPhoto.SizeMode = (PictureBoxSizeMode)
                    Enum.Parse(typeof(PictureBoxSizeMode), enumVal);
            }
        }

        private void mnuImage_DropDownOpening(object sender, EventArgs e)
        {
            ToolStripDropDownItem parent = (ToolStripDropDownItem)sender;
            if (parent != null)
            {
                string enumVal = pbxPhoto.SizeMode.ToString();
                foreach (ToolStripMenuItem item in parent.DropDownItems)
                {
                    item.Enabled = (pbxPhoto.Image != null);
                    item.Checked = item.Tag.Equals(enumVal);
                }
            }
        }
        private void SetstatusStrip(string path)
        {
            if (pbxPhoto.Image != null)
            {
                sttInfo.Text = path;
                sttImageSize.Text = string.Format("{0:#} x {1:#}", pbxPhoto.Image.Width, pbxPhoto.Image.Height);
            }

            else
            {

            }
        }

        private void mnuload_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Title = "Open photo";
            // mở định dạng các file
            dlg.Filter = "jpg files (*.jpg)|*.jpg|All files (*.*)|*.*";

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                try //kiểm tra phần mở rộng
                {
                    pbxPhoto.Image = new Bitmap(dlg.OpenFile());
                    SetstatusStrip(dlg.FileName);
                }
                catch (ArgumentException ex)
                {
                    MessageBox.Show("Unable to load files: " + ex.Message);
                    pbxPhoto.Image = null;
                }
            }
        }

        
       
    }
}
