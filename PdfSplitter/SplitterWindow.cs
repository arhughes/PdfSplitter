using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using iTextSharp.text.pdf;
using System.IO;

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

            file_panel.DragEnter += new DragEventHandler(file_panel_DragEnter);
            file_panel.DragDrop += new DragEventHandler(file_panel_DragDrop);
        }

        void file_panel_DragDrop(object sender, DragEventArgs e)
        {
            var box = e.Data.GetData(typeof(PictureBox)) as PictureBox;
            file_panel.Controls.Add(box);
            preview_panel.Controls.Remove(box);
        }

        void file_panel_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(PictureBox)))
            {
                e.Effect = DragDropEffects.Move;
            }
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
            preview_panel.Controls.Clear();

            status_progress.Value = 0;
            status_progress.Visible = true;
            status_progress_label.Visible = true;

            BackgroundWorker worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.DoWork += new DoWorkEventHandler(worker_DoWork);
            worker.ProgressChanged += new ProgressChangedEventHandler(worker_ProgressChanged);
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);

            status_label.Text = string.Format("Loading {0}", m_path);
            worker.RunWorkerAsync(worker);
        }

        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = (BackgroundWorker)e.Argument;
            int pages = m_doc.PageCount;
            List<Image> images = new List<Image>();

            // render each page and stuff them into the list
            for (int i = 0; i < pages; i++)
            {
                worker.ReportProgress(i, pages);
                Image image = RenderPage(m_doc, i);
                images.Add(image);
            }

            worker.ReportProgress(pages, pages);
            e.Result = images;
        }

        void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            int pages = (int)e.UserState;
            
            status_progress.Value = 100 * e.ProgressPercentage / pages;
            if (e.ProgressPercentage < pages)
                status_progress_label.Text = string.Format("Page {0} of {1}", e.ProgressPercentage + 1, pages);
        }

        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            List<Image> images = (List<Image>)e.Result;

            status_progress_label.Text = "";

            foreach (Image image in images)
            {
                PictureBox box = new PictureBox();
                box.Size = GetPreviewSize(image, preview_size_combo.Text);
                box.SizeMode = PictureBoxSizeMode.Zoom;
                box.Image = image;
                box.MouseDown += new MouseEventHandler(box_MouseDown);
                preview_panel.Controls.Add(box);
            }

            status_progress_label.Visible = false;
            status_progress.Visible = false;
            status_label.Text = string.Format("Loaded {0}", m_path);
        }

        void box_MouseDown(object sender, MouseEventArgs e)
        {
            var box = sender as PictureBox;
            box.DoDragDrop(box, DragDropEffects.Move);
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

        private void save_button_Click(object sender, EventArgs e)
        {
            int[] pages = new int[] { 1, 2 };
            CreatePdf(m_path, pages, @"c:\temp\test.pdf");
        }

        void CreatePdf(string input_path, IList<int> pages, string output_path)
        {
            // open a reader for the source file
            PdfReader reader = new PdfReader(input_path);

            try
            {
                // get output file
                using (var fs = File.OpenWrite(output_path))
                {
                    // create input document
                    var input_doc = new iTextSharp.text.Document(reader.GetPageSizeWithRotation(pages[0]));
                    // create the writer
                    PdfWriter writer = PdfWriter.GetInstance(input_doc, fs);
                    try
                    {
                        input_doc.Open();
                        try
                        {
                            // for each page copy the page directly from the reader into the writer
                            PdfContentByte cb = writer.DirectContent;
                            foreach (int page_number in pages)
                            {
                                input_doc.SetPageSize(reader.GetPageSizeWithRotation(page_number));
                                input_doc.NewPage();

                                PdfImportedPage page = writer.GetImportedPage(reader, page_number);

                                int rotation = reader.GetPageRotation(page_number);
                                if (rotation == 90 || rotation == 270)
                                    cb.AddTemplate(page, 0, -1f, 1f, 0, 0, reader.GetPageSizeWithRotation(page_number).Height);
                                else
                                    cb.AddTemplate(page, 1f, 0, 0, 1f, 0, 0);
                            }
                        }
                        finally
                        {
                            input_doc.Close();
                        }
                    }
                    finally
                    {
                        writer.Close();
                    }
                }
            }
            finally
            {
                reader.Close();
            }
        }
    }
}
