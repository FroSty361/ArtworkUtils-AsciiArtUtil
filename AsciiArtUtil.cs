using System; 
using System.Drawing;
using static System.Console;

namespace AsciiArtUtil
{
  static class AsciiArt
  {
    private static string pixels = " .-+*wGHM#&%";

    public static void ReverseGrayscale()
    {
      char[] chars = pixels.ToCharArray();
      Array.Reverse(chars);
      pixels = new string(chars);
    }

    public static void SetPixelSet(string newSet) => pixels = newSet;
  
    public static string GetAsciiImage(string imageLocation, int width = 0, int height = 0)
    {
      if (!File.Exists(imageLocation))
      {
          throw new FileNotFoundException("Image file not found!", imageLocation);
      }

      string returnString = "";

      var original = new Bitmap(imageLocation);

      if (width == 0 && height == 0)
      {
        width = original.Width / 100;

        height = original.Height / 100;
      }

      var image = new Bitmap(original, new Size(width, height));

      string space = "";

      returnString += space;

      for (var y = 0; y < image.Height; y++)
      {
          for (var x = 0; x < image.Width; x++)
          {
          var color = image.GetPixel(x, y);
          var brightness = Brightness(color);
          var index = brightness / 255 * (pixels.Length - 1);
          var pixel = pixels[pixels.Length - (int)Math.Round(index) - 1];
          returnString += pixel;
        }

        returnString += '\n';

        returnString += space;
      }

      return returnString.TrimEnd();
    }

    public static List<string> GetAsciiImages(List<string> imageLocations, int width = 225, int height = 73)
    {
      List<string> images = new List<string>();

      foreach (var imageLocation in imageLocations)
      {
        if (!File.Exists(imageLocation))
        {
          throw new FileNotFoundException("Image file not found!", imageLocation);
        }

        string asciiImage = "";

        asciiImage = GetAsciiImage(imageLocation, width, height);

        images.Add(asciiImage);
      }

      return images;
    }

    private static double Brightness(Color c)
    {
      return Math.Sqrt(c.R * c.R * .241 + c.G * c.G * .691 + c.B * c.B * .068);
    }
  }
}