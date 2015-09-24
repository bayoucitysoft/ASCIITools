using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                            ImageConverter.ConvertColorImage(picture, save + "\\" + name + ".png");
                        if ((c == 'B' || c == 'b'))
                            ImageConverter.ConvertGreyImage(picture, save + "\\" + name + ".png");
                        
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
}
