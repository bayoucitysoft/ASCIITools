using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Text;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;


namespace ASCIITools
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Would you like to make a [C]olor or [B]&W image?");
            char c = ' '; 
            bool isvalid = Char.TryParse(Console.ReadLine(), out c);
            if (isvalid && (c == 'C' || c == 'c') | (c == 'B' || c== 'b'))
            {
                Console.WriteLine("Please select a path to gather an image");
                string path = Console.ReadLine();

                if (File.Exists(path))
                {
                    Console.WriteLine("Choose a name for new image");
                    string name = Console.ReadLine();

                    Console.WriteLine("Please select a directory to save the image to");
                    string save = Console.ReadLine();
                    Console.WriteLine("Saving Image...");
                    try
                    {
                        var picture = ImageConverter.GetImage(path);
                        if ((c == 'C' || c == 'c'))
                            ImageConverter.ConvertImage(picture, save + "\\" + name + ".png", true);
                        if ((c == 'B' || c == 'b'))
                            ImageConverter.ConvertImage(picture, save + "\\" + name + ".png", false);
                        
                        Console.WriteLine("Image Saved!");
                        Main(new string[0]);
                    }
                    catch (Exception ex)
                    {
                        Error(new Exception(ex.Message + System.Environment.NewLine + "Generally invalid post path specification. i.e. C: instead of C:\\"));
                    }
                }
                else
                    Error(new Exception("Invalid path."));
            }
            else
                Error(new Exception("Invalid decision."));
        }

        static void Error(Exception ex)
        {
            Console.WriteLine(String.Format("Error: {0}", ex.Message));
            Main(new string[0]);
        }
    }

    public static class ImageConverter
    {
        public static void ConvertImage(Image img, string path, bool isColor)
        {
            int ratio = img.Width / img.Height;
            Image toDraw = new Bitmap(img.Width * 10, img.Height * 10);
            Graphics draw = Graphics.FromImage(toDraw);
            draw.Clear(Color.White);
            draw.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
            Font font = new Font(FontFamily.GenericSansSerif, 10);

            Bitmap bmp = new Bitmap(img);
            for (int y = 0; y < bmp.Height; y++)
            {
                for (int x = 0; x < bmp.Width; x++)
                {
                    Color color = bmp.GetPixel(x, y);
                    int grey = ConvertToGrayScale(color);
                    Color colorGreyScale = Color.FromArgb(grey, grey, grey);
                    string toPaint = ASCIIPixel((int)colorGreyScale.R).ToString();
                    
                    Brush text;
                    if (isColor)
                        text = new SolidBrush(color);
                    else
                        text = new SolidBrush(Color.Black);

                    draw.DrawString(toPaint, font, text, new PointF((float)x * 10, (float)y * 10));
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
