﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Drawing.Imaging;

using System.Threading.Tasks;

namespace ASCIITools
{
    public static class ImageConverter
    {
        public static Image GetImage(string path)
        {
            return Image.FromFile(path);
        }

        public static string[] ConvertGreyImage(Image img) 
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
            return message;
        }


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
            else if (shade >= 45)
                return (char)64;    //8
            else if (shade >= 35)
                return (char)56;    //@
            else if (shade >= 25)
                return (char)38;
            else if (shade >= 0)
                return (char)35;    //#

          

            return dot; 
        }

        private static int ConvertToGrayScale(Color color)
        {
            return (color.R / 3) + (color.G / 3) + (color.B / 3);
        }

        

        internal static void SaveImage(string[] image, string path)
        {
            File.WriteAllLines(path + ".txt", image);
        }

        internal static void SaveColorImage(string[] message, Image img)
        {
            
            
        }
    }
}
