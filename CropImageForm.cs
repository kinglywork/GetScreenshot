using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GetScreenshot
{
    public partial class CropImageForm : Form
    {
        private bool _selecting;
        private Rectangle _selection;

        public Image Image { get; set; }

        public Rectangle CropBorder
        {
            get
            {
                Rectangle imgRec = ImageRectangle;
                float scaleX = (float)Image.Width / (float)imgRec.Width;
                float scaleY = (float)Image.Height / (float)imgRec.Height;
                int x = (int)((_selection.X - imgRec.X) * scaleX);
                int y = (int)((_selection.Y - imgRec.Y) * scaleY);
                int width = (int)(_selection.Width * scaleX);
                int height = (int)(_selection.Height * scaleY);
                return new Rectangle(x, y, width, height);
            }
        }
        private Rectangle ImageRectangle
        {
            get
            {
                PropertyInfo irProperty = pic.GetType().GetProperty("ImageRectangle", BindingFlags.GetProperty | BindingFlags.NonPublic | BindingFlags.Instance);
                return (Rectangle)irProperty.GetValue(pic, null);
            }
        }

        public CropImageForm()
        {
            InitializeComponent();
        }

        private void CropImage_Load(object sender, EventArgs e)
        {
            pic.Image = Image;
        }

        private void pic_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _selecting = true;
                _selection = new Rectangle(new Point(e.X, e.Y), new Size());
            }
        }

        private void pic_MouseMove(object sender, MouseEventArgs e)
        {
            if (_selecting)
            {
                _selection.Width = e.X - _selection.X;
                _selection.Height = e.Y - _selection.Y;

                pic.Refresh();
            }
        }

        private void pic_MouseUp(object sender, MouseEventArgs e)
        {
            _selecting = false;
        }

        private void pic_Paint(object sender, PaintEventArgs e)
        {
            if (_selecting)
            {
                Pen pen = Pens.LightSkyBlue;
                e.Graphics.DrawRectangle(pen, _selection);
            }
        }

        private void CropImageForm_Resize(object sender, EventArgs e)
        {
            this.Text = string.Format("{0},{1}", pic.Width, pic.Height);
        }
    }
}
