using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Text;

namespace ZplLabels.ZPL
{
    public class StringGraphicGenerator : FieldGenerator
    {
        private readonly string _name = Guid.NewGuid().ToString();
        private string _content;
        private Font _font = new Font("宋体", 9);
        private int _totalDots;

        private int Width { get; set; }

        private int Height { get; set; }

        public StringGraphicGenerator At(int x, int y)
        {
            _position = new LabelPosition(x, y);
            return this;
        }

        public StringGraphicGenerator WithData(string value)
        {
            _content = value;
            return this;
        }

        public StringGraphicGenerator SetFont(Font font)
        {
            _font = font;
            return this;
        }

        public StringGraphicGenerator Centered(int width)
        {
            _totalDots = width;
            return this;
        }

        public override string ToString()
        {
            return string.Format("{0}\r\n{1}\r\n{2}\r\n{3}", DGCommand(), FOCommand(), XGCommand(), IDCommand());
        }

        private string DGCommand()
        {
            return CreateGRF(ChangeToPic(_content, _font), _name);
        }

        private string FOCommand()
        {
            var t = _totalDots - Width;
            if (t > 0)
            {
                t /= 2;
            }
            return string.Format("^FO{0},{1}", _position.X + t, _position.Y);
        }

        private string XGCommand()
        {
            return string.Format("^XGR:{0}.GRF,1,1^FS", _name);
        }

        private string IDCommand()
        {
            return string.Format("^IDR:{0}.GRF", _name);
        }

        private Bitmap ChangeToPic(string content, Font font)
        {
            var g = Graphics.FromImage(new Bitmap(1, 1));
            var sizeF = g.MeasureString(content, font); //测量出字体的高度和宽度  

            Width = Convert.ToInt32(Math.Ceiling(sizeF.Width));
            Height = Convert.ToInt32(Math.Ceiling(sizeF.Height));

            Brush brush; //笔刷，颜色  
            brush = Brushes.Black;
            var pf = new PointF(0, 0);
            var img = new Bitmap(Width, Height);
            using (g = Graphics.FromImage(img))
            {
                var imageSize = new Rectangle(0, 0, Width, Height);
                g.FillRectangle(Brushes.White, imageSize);
                g.DrawString(content, font, brush, pf);
            }

            return img;
        }

        private string CreateGRF(Bitmap bmp, string name)
        {
            BitmapData imgData = null;
            byte[] pixels;
            int x, y, width;
            StringBuilder sb;
            IntPtr ptr;

            try
            {
                imgData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadOnly,
                    PixelFormat.Format1bppIndexed);
                width = (bmp.Width + 7)/8;
                pixels = new byte[width];
                sb = new StringBuilder(width*bmp.Height*2);
                ptr = imgData.Scan0;
                for (y = 0; y < bmp.Height; y++)
                {
                    Marshal.Copy(ptr, pixels, 0, width);
                    for (x = 0; x < width - 1; x++)
                    {
                        sb.AppendFormat("{0:X2}", (byte) ~pixels[x]);
                    }
                    sb.AppendFormat("{0:X2}", 0);
                    ptr = (IntPtr) (ptr.ToInt64() + imgData.Stride);
                }
            }
            finally
            {
                if (bmp != null)
                {
                    if (imgData != null) bmp.UnlockBits(imgData);
                    bmp.Dispose();
                }
            }
            return string.Format("~DG{0}.GRF,{1},{2},", name, width*y, width) + sb;
        }
    }
}