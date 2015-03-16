using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASCIITools
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please select a path to gather an image");
            string path = Console.ReadLine();

            var picture = ImageConverter.GetImage(path);
            string[] message = ImageConverter.ConvertGreyImage(picture);
            //string[] message2 = ImageConverter.ConvertColorImage(picture);
            Console.WriteLine("Saving Image...");
            ImageConverter.SaveImage(message, path);
            Console.WriteLine("Image Saved!");

            Console.ReadLine();
        }
    }
}
