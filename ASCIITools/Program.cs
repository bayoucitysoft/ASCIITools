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
            var picture = ImageConverter.GetImage(@"C:\Users\paul\Pictures\float.jpg");
            string[] message = ImageConverter.ConvertGreyImage(picture);
            //string[] message2 = ImageConverter.ConvertColorImage(picture);
            ImageConverter.SaveImage(message, "name");

            for(int i = 0; i< message.Count(); i++)
            {
                Console.WriteLine(message[i]);
            }

            Console.ReadLine();
        }
    }
}
