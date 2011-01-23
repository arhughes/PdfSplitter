using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PdfSplitter
{
    public partial class SplitterWindow : Form
    {
        string m_path;
        PDFLibNet.PDFWrapper m_doc;

        public SplitterWindow()
        {
            InitializeComponent();

            preview_size_combo.SelectedIndex = 1;
            preview_size_combo.SelectedIndexChanged += new EventHandler(preview_size_combo_SelectedIndexChanged);
        }

        void preview_size_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            // preview size changed, reload the images
            string size = preview_size_combo.Text;

            foreach (Control control in preview_panel.Controls)
            {
                PictureBox box = control as PictureBox;
                if (box != null)
                {
                    box.Size = GetPreviewSize(box.Image, size);
                }
            }
        }

        private void open_button_Click(object sender, EventArgs e)
        {
            using (var o = new OpenFileDialog())
            {
                o.Filter = "PDF Files (*.pdf)|*.pdf";

                if (o.ShowDialog() == DialogResult.OK)
                {
                    OpenFile(o.FileName);
                }
            }
        }

        void OpenFile(string path)
        {
            var doc = new PDFLibNet.PDFWrapper();
            doc.UseMuPDF = true;
            doc.LoadPDF(path);

            m_path = path;
            m_doc = doc;

            LoadImages();
        }

        void LoadImages()
        {
            int pages = m_doc.PageCount;

            preview_panel.Controls.Clear();

            // render each page and stuff them into the preview panel
            for (int i = 0; i < pages; i++)
            {
                Image image = RenderPage(m_doc, i);
                PictureBox box = new PictureBox();
                box.Size = GetPreviewSize(image, preview_size_combo.Text);
                box.SizeMode = PictureBoxSizeMode.Zoom;
                box.Image = image;
                preview_panel.Controls.Add(box);
            }
        }

        Size GetPreviewSize(Image image, string preview_size)
        {
            int max_width = 150, max_height = 350;
            switch (preview_size)
            {
                case "Small":
                    max_width = 100;
                    max_height = 200;
                    break;
                case "Medium":
                    max_width = 150;
                    max_height = 300;
                    break;
                case "Large":
                    max_width = 250;
                    max_height = 500;
                    break;
            }

            decimal width_ratio = max_width / ((decimal)image.Width);
            decimal height_ratio = max_height / ((decimal)image.Height);

            if (width_ratio < height_ratio)
                return new Size((int)(image.Width * width_ratio), (int)(image.Height * width_ratio));
            return new Size((int)(image.Width * height_ratio), (int)(image.Height * height_ratio));
        }

        Image RenderPage(PDFLibNet.PDFWrapper doc, int page)
        {
            doc.CurrentPage = page + 1;
            doc.CurrentX = 0;
            doc.CurrentY = 0;

            using (var box = new PictureBox())
            {
                // have to give the document a handle to render into
                doc.RenderPage(box.Handle);

                // create an image to draw the page into
                var buffer = new Bitmap(doc.PageWidth, doc.PageHeight);
                doc.ClientBounds = new Rectangle(0, 0, doc.PageWidth, doc.PageHeight);
                using (var g = Graphics.FromImage(buffer))
                {
                    var hdc = g.GetHdc();
                    try
                    {
                        doc.DrawPageHDC(hdc);
                    }
                    finally
                    {
                        g.ReleaseHdc();
                    }
                }

                return buffer;
            }
        }
    }
}
