using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace PdfSplitter
{
    class PageContainer : PictureBox
    {
        public PageContainer(int page_number, Image preview)
        {
            PageNumber = page_number;
            Image = preview;

            Image = preview;
            SizeMode = PictureBoxSizeMode.Zoom;
            this.AllowDrop = true;
        }

        public int PageNumber
        {
            get;
            private set;
        }
    }
}
