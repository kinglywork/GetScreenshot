using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace TestScreenshot
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private Bitmap _image;

        private string Path_Screenshot
        {
            get
            {
                return System.IO.Path.Combine(Application.StartupPath, "screenshot.png");
            }
        }
        private string Path_CroppedScreenshot
        {
            get
            {
                return System.IO.Path.Combine(Application.StartupPath, "cropped.png");
            }
        }

        private void btnCrop_Click(object sender, EventArgs e)
        {
            if (_image == null)
            {
                MessageBox.Show("Get screenshot first.");
                return;
            }

            CropImageForm frm = new CropImageForm();
            frm.Image = _image;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    var image = _image.Clone(frm.CropBorder, PixelFormat.DontCare);
                    SaveImage(image, Path_CroppedScreenshot);

                    MessageBox.Show(string.Format("Done.\r\nSaved to:{0}", Path_CroppedScreenshot));
                }
                catch (Exception exp)
                {
                    MessageBox.Show(exp.Message);
                }
            }
        }

        private void btnScreenshot_Click(object sender, EventArgs e)
        {
            try
            {
                _image = CaptureScreen();
                SaveImage(_image, Path_Screenshot);

                MessageBox.Show(string.Format("Done.\r\nSaved to:{0}", Path_Screenshot));
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }

        public Bitmap CaptureScreen()
        {
            Rectangle rc = GetScreenRectangle();
            var image = new Bitmap(rc.Width, rc.Height, PixelFormat.Format32bppArgb);
            using (Graphics memoryGrahics = Graphics.FromImage(image))
            {
                memoryGrahics.CopyFromScreen(rc.X, rc.Y, 0, 0, rc.Size, CopyPixelOperation.SourceCopy);
            }
            return image;
        }

        private Rectangle GetScreenRectangle()
        {
            List<Screen> screens = Screen.AllScreens.ToList().OrderBy(s => s.Bounds.X).ToList();
            List<DisplaySettings> displaySettings = GetDisplaySettings();

            if (displaySettings != null && displaySettings.Count != screens.Count)
            {
                throw new Exception("screen count error.");
            }

            Point location = GetScreenStartPoint(screens, displaySettings);
            Size size = GetScreenSize(screens, displaySettings);

            return new Rectangle(location, size);
        }

        private List<DisplaySettings> GetDisplaySettings()
        {
            try
            {
                var devices = DeviceManager.GetDisplayDevices();
                var displaySettings = devices
                    .Select(device =>
                    {
                        return DisplayManager.GetCurrentSettings(device.DeviceName);
                    })
                    .Where(setting => setting != null)
                    .Select(setting => setting.Value)
                    .ToList();

                return displaySettings;
            }
            catch
            {
                return null;
            }
        }

        private Point GetScreenStartPoint(List<Screen> screens, List<DisplaySettings> displaySettings = null)
        {
            Func<int, int> getScreenWidth;
            if (displaySettings != null)
            {
                getScreenWidth = (index) => displaySettings[index].Width;
            }
            else
            {
                getScreenWidth = (index) => screens[index].Bounds.Width;
            }

            int primaryScreenIndex = screens.FindIndex(screen => screen.Primary);

            int pointX = 0;
            int i = 0;
            while (i < primaryScreenIndex)
            {
                pointX -= getScreenWidth(i);
                i++;
            }

            return new Point(pointX, 0);
        }

        private Size GetScreenSize(List<Screen> screens, List<DisplaySettings> displaySettings = null)
        {
            int sumWidth;
            int maxHeight;
            if (displaySettings != null)
            {
                sumWidth = displaySettings.Sum(s => s.Width);
                maxHeight = displaySettings.Max(s => s.Height);
            }
            else
            {
                sumWidth = screens.Sum(s => s.Bounds.Width);
                maxHeight = screens.Max(s => s.Bounds.Height);
            }
            return new Size(sumWidth, maxHeight);
        }

        private void SaveImage(Image image, string path)
        {
            image.Save(path, ImageFormat.Png);
        }

        #region GetScreenshotWay2(Obsolete)
        //private void btnScreenshot2_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        CaptureScreenWithPrtScKey();

        //        if (_image == null)
        //        {
        //            CaptureScreenWithLib();
        //        }

        //        if (_image == null)
        //        {
        //            MessageBox.Show("Can not capture screen.");
        //            return;
        //        }

        //        SaveScreenshot();

        //        MessageBox.Show(string.Format("Done.\r\nSaved to:{0}", Path_Screenshot));
        //    }
        //    catch (Exception exp)
        //    {
        //        MessageBox.Show(exp.Message);
        //    }
        //}

        //private void CaptureScreenWithPrtScKey()
        //{
        //    SendKeys.Send("{PRTSC}");
        //    _image = Clipboard.GetImage();
        //}

        //private void CaptureScreenWithLib()
        //{
        //    _image = Pranas.ScreenshotCapture.TakeScreenshot();
        //}
        #endregion

    }
}
