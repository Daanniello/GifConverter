using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AnimatedGif;

namespace GifConverter
{
    class Converter
    {
        private Color colorPicked;
        private bool isGif;
        private Image bitMap;
        private byte margin = 20;
        private byte mainMargin = 30;

        public Converter(Image image, Color color)
        {
            colorPicked = color;
            bitMap = image;

            var frames = getFrames(image);
            
            for(var x = 0; x < frames.Length; x++)
            {
                bitMap = frames[x];
                frames[x] = RemoveGreenColor();
                
            }
            if (frames.Length == 1)
            {
                frames[0].Save("image.png", ImageFormat.Png);
            }
            FramesToImage(frames);
            //var result = RemoveGreenColor();

        }

        public Bitmap RemoveGreenColor()
        {
            Bitmap scrBitmap = (Bitmap)bitMap;
            Color newColor = Color.Transparent;
            Color actualColor;

            Bitmap newBitmap = new Bitmap(scrBitmap.Width, scrBitmap.Height);

            for (int i = 0; i < scrBitmap.Width; i++)
            {
                for (int j = 0; j < scrBitmap.Height; j++)
                {
                    actualColor = scrBitmap.GetPixel(i, j);
                    if (actualColor.A > 150)
                    {
                        if (actualColor.R > (colorPicked.R - margin) && actualColor.R < (colorPicked.R + margin))
                        {
                            if (actualColor.G > (colorPicked.G - margin - mainMargin) && actualColor.G < (colorPicked.G + margin + mainMargin))
                            {
                                if (actualColor.B > (colorPicked.B - margin) && actualColor.B < (colorPicked.B + margin))
                                {
                                    newBitmap.SetPixel(i, j, newColor);
                                }
                            }
                            else
                            {
                                newBitmap.SetPixel(i, j, actualColor);
                            }
                        }
                        else
                        {
                            newBitmap.SetPixel(i, j, actualColor);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Under 150");
                    }
                }
            }
            SaveFileDialog dialog = new SaveFileDialog();
            
            Console.WriteLine("Done");

            return newBitmap;
        }

        private Image[] getFrames(Image originalImg)
        {

            try
            {
                int numberOfFrames = originalImg.GetFrameCount(FrameDimension.Time);
                Image[] frames = new Image[numberOfFrames];
               

                for (int i = 0; i < numberOfFrames; i++)
                {
                    originalImg.SelectActiveFrame(FrameDimension.Time, i);
                    frames[i] = ((Image)originalImg.Clone());
                }
                isGif = true;
                return frames;
            }
            catch
            {
                
                Image[] frames = new Image[1];
                frames[0] = originalImg;
                isGif = false;
                return frames;
            }
        }

        private void FramesToImage(Image[] frames)
        {
            AnimatedGifCreator s = new AnimatedGifCreator("gif.gif", 33);
            using (var gif = s)
            {
                foreach (var frame in frames)
                {
                    gif.AddFrame(frame, delay: -1, quality: GifQuality.Bit8);
                 
                }
            }
        }

        public Image GetDisplayImage()
        {
            if (isGif)
            {
                return Image.FromFile(@"gif.gif");
            }
            else
            {
                return Image.FromFile(@"image.png");
            }
            
        }

    }
}
