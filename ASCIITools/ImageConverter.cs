using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Drawing.Imaging;

namespace ASCIITools
{
    public static class ImageConverter
    {
        public static Image GetImage(string path)
        {
            return Image.FromFile(path);
        }

        public static void ConvertGreyImage(Image img, string path) 
        {
            Bitmap bmp = new Bitmap(img);
            string[] message = new string[bmp.Height];
            string row = string.Empty;
            for (int y = 0; y < bmp.Height; y++)
            {
                for (int x = 0; x < bmp.Width; x++)
                {
                    Color color = bmp.GetPixel(x, y);
                    int grey = ConvertToGrayScale(color);
                    color = Color.FromArgb(grey, grey, grey);
                    row += ASCIIPixel((int)color.R).ToString();
                    if (x == bmp.Width - 1)
                    {
                        message[y] = row;
                        row = string.Empty;
                    }
                }
            }
            SaveGreyImage(message, path);
        }


        internal static void SaveGreyImage(string[] image, string path)
        {
            Image img = new Bitmap((image.Count() + 1) * 10, image.Length * 10);
            Graphics draw = Graphics.FromImage(img);
            SizeF textSize = draw.MeasureString(image[0], new Font(FontFamily.GenericMonospace, 2F));
            draw.Clear(Color.White);
            draw.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
            Brush text = new SolidBrush(Color.Black);
            float y = 0;
            Font font = new Font(FontFamily.GenericMonospace, 10.0F, FontStyle.Bold);
            for (int i = 0; i < image.Count(); i++)
                draw.DrawString(image[i], font, text, new PointF(0, y += font.SizeInPoints));

            draw.Save();
            text.Dispose();
            draw.Dispose();

            img.Save(path, ImageFormat.Png);
        }

        public static void ConvertColorImage(Image img, string path)
        {
            int ratio = img.Width / img.Height;
            Image toDraw = new Bitmap(img.Width * 10, img.Height * 10);
            Graphics draw = Graphics.FromImage(toDraw);
            draw.Clear(Color.White);
            draw.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
            Font font = new Font(FontFamily.GenericSansSerif, 10);

            Bitmap bmp = new Bitmap(img);
            string[] message = new string[bmp.Height];
            string row = string.Empty;
            for (int y = 0; y < bmp.Height; y++)
            {
                for (int x = 0; x < bmp.Width; x++)
                {
                    Color color = bmp.GetPixel(x, y);
                    int grey = ConvertToGrayScale(color);
                    Color colorGreyScale = Color.FromArgb(grey, grey, grey);
                    string toPaint = ASCIIPixel((int)colorGreyScale.R).ToString();
                    Brush text = new SolidBrush(color);
                    draw.DrawString(toPaint, font, text, new PointF((float)x * 10 , (float)y * 10));
                    text.Dispose();
                }
            }

            draw.Save();
            draw.Dispose();
            toDraw.Save(path, ImageFormat.Png);
            toDraw.Dispose();
        }

        /// <summary>
        /// Can be adjusted as needed.
        /// The idea is to start with the smallest character 
        /// as a representation closest to white and vice versa
        /// </summary>
        /// <param name="shade"></param>
        /// <returns></returns>
        private static char ASCIIPixel(int shade)
        {
            char dot = '.';
            if (shade >= 250)
                return (char)32;    //' '
            else if (shade >= 245)
                return (char)39;    //.
            else if (shade >= 235)
                return (char)46;    //'
            else if (shade >= 230)
                return (char)42;    //"
            else if (shade >= 210)
                return (char)34;    //*
            else if (shade >= 185)
                return (char)58;    //:
            else if (shade >= 175)
                return (char)59;    //;
            else if (shade >= 155)
                return (char)33;   //?
            else if (shade >= 120)
                return (char)63;    //!
            else if (shade >= 85)
                return (char)37;    //$
            else if (shade >= 10)
                return (char)56;    //@
            else if (shade >= 0)
                return (char)35;    //#
            return dot; 
        }

        public static int ConvertToGrayScale(Color color)
        {
            return (color.R / 3) + (color.G / 3) + (color.B / 3);
        }
    }
}
