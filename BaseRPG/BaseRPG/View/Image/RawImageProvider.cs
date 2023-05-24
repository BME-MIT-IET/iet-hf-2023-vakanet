using BaseRPG.View.Exceptions;
using Microsoft.Graphics.Canvas;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using Windows.Foundation;
using System.Threading;
using BaseRPG.View.Interfaces.Providers;

namespace BaseRPG.View.Image
{
    public class RawImageProvider : IImageProvider
    {
        private static Dictionary<string, CanvasVirtualBitmap> images = new Dictionary<string, CanvasVirtualBitmap>();
        private static bool initialized = false;
        public static async Task LoadImages(ICanvasResourceCreator canvasResourceCreator)
        {
            string projectPath =  AppDomain.CurrentDomain.BaseDirectory;
            List<string> pics = 
                File.ReadAllLines(Path.Combine(projectPath, @"Assets\config\images-to-load.txt"))
                .Select(line=>line.Trim()).ToList();
            foreach (string p in pics)
                images.Add(p, await CanvasVirtualBitmap.LoadAsync(canvasResourceCreator, Path.Combine(projectPath, p)));
            initialized = true;
        }
        public ICanvasImage GetByFilename(string fileName)
        {
            if (fileName == null) return null;
            if (!initialized)
            {
                //This exception is also thrown if an image couldn't be loaded
                throw new ImageProviderUninitializedException();
            }
            if (images.ContainsKey(fileName))
            {
                return images[fileName];
            }
            throw new NoSuchFileException("image doesn't exist: " + fileName);
        }
        public CanvasVirtualBitmap GetByFilenameAsBitmap(string fileName)
        {
            if (fileName == null) return null;
            if (!initialized)
            {
                //This exception is also thrown if an image couldn't be loaded
                throw new ImageProviderUninitializedException();
            }
            if (images.ContainsKey(fileName))
            {
                return images[fileName];
            }
            throw new NoSuchFileException("image doesn't exist: " + fileName);
        }

        public Tuple<double, double> GetSizeByFilename(string fileName)
        {
            if (fileName == null) return null;
            var bitmap = GetByFilenameAsBitmap(fileName);
            return new(bitmap.Size.Width, bitmap.Size.Height);
        }
    }
}
