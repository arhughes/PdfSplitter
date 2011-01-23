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

            // render each page and stuff them into the preview panel
            for (int i = 0; i < pages; i++)
            {
                Image image = RenderPage(m_doc, i);
                PictureBox box = new PictureBox();
                box.Top = 0;
                box.Left = i * 105;
                box.Height = 200;
                box.Width = 100;
                box.SizeMode = PictureBoxSizeMode.Zoom;
                box.Image = image;
                preview_panel.Controls.Add(box);
            }
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
