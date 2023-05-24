using BaseRPG.View.Animation.Utility;
using BaseRPG.View.Image;
using BaseRPG.View.Interfaces;
using BaseRPG.View.Interfaces.Providers;
using MathNet.Spatial.Units;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Effects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.View.Animation.ImageSequence
{
    public class ImageSequenceAnimation:IAnimation<ImageSequenceAnimation>
    {
        private IImageProvider imageProvider;
        private IEnumerator<string> imageNames;
        private AnimationTimer animationTimer;
        private double secondsPassed = 0;
        private string lastValidImage;
        private double timeBetween;
        private int currentIndex = 0;
        public Tuple<double, double> CurrentImageSize
        { 
            get 
            { 
                var result = imageProvider.GetSizeByFilename(imageNames.Current);
                //if (result == null) return imageProvider.GetSizeByFilename(lastValidImage);
                return result;
            } 
        }
        public int CurrentIndex 
        { 
            get => currentIndex; 
            set 
            {
                if (value < currentIndex) throw new ArgumentException("value can not be less then currentIndex");
                moveEnumerator(value - currentIndex);
                currentIndex = value;
            } 
        }

        public event Action<ImageSequenceAnimation> OnAnimationCompleted;
        public ImageSequenceAnimation(
            IImageProvider imageProvider, 
            IEnumerator<string> imageNames,
            Action<ImageSequenceAnimation> actionOnAnimationCompleted = null,
            double timeBetween = 0.1)
        {
            
            this.imageProvider = imageProvider;
            this.imageNames = imageNames;
            OnAnimationCompleted += actionOnAnimationCompleted;
            this.timeBetween = timeBetween;
        }

        public ICanvasImage CalculateImage(double delta)
        {
            secondsPassed += delta;
            CurrentIndex = calculateFrameIndex();
            //animationTimer.Tick(delta);
            if (imageNames.Current == null)
                return imageProvider.GetByFilename(lastValidImage);
            return imageProvider.GetByFilename(imageNames.Current);

            
        }
        private int calculateFrameIndex() {

            var result = (int)Math.Floor(secondsPassed/timeBetween);
            return result;
        }
        private void moveEnumerator(int amount) {
            for (int i = 0; i < amount; i++)
            {
                bool moved = imageNames.MoveNext();
                if (moved)
                    lastValidImage = imageNames.Current;
                else
                {
                    imageNames.Reset();
                    imageNames.MoveNext();
                    OnAnimationCompleted?.Invoke(this);
                }
            }
        }
        public static ImageSequenceAnimation SingleImage(IImageProvider imageProvider, string image, double timeBetween = 0.1) {
            return new ImageSequenceAnimation(imageProvider, new LoopingEnumerator<string>(new List<string> { image }),null, timeBetween);
        }
        public static ImageSequenceAnimation LoopingAnimation(IImageProvider imageProvider, List<string> images, double timeBetween = 0.1)
        {
            return new ImageSequenceAnimation(imageProvider, new LoopingEnumerator<string>(images), null, timeBetween);
        }
        
        public static ImageSequenceAnimation WithDefaultEnumerator(IImageProvider imageProvider, List<string> images, double timeBetween = 0.1)
        {
            List<string>.Enumerator enumerator = images.GetEnumerator();
            enumerator.MoveNext();
            return new ImageSequenceAnimation(imageProvider, enumerator, null, timeBetween);
        }
        public static ImageSequenceAnimation WithTimeFrame(IImageProvider imageProvider, List<string> images, double timeFrame)
        {
            List<string>.Enumerator enumerator = images.GetEnumerator();
            enumerator.MoveNext();
            var timeBetween = timeFrame / (images.Count);
            return new ImageSequenceAnimation(imageProvider, enumerator, null, timeBetween);
        }
        public static ImageSequenceAnimation WithTimeFrameHoldLastItem(IImageProvider imageProvider, List<string> images, double timeFrame)
        {
            var enumerator = new LastItemHoldingEnumerator<string>(images);
            enumerator.MoveNext();
            var timeBetween = timeFrame / (images.Count);
            return new ImageSequenceAnimation(imageProvider, enumerator, null, timeBetween);
        }
        internal static ImageSequenceAnimation SingleImage(DrawingImage image)
        {
            var singleItemList = new List<string> { image.ImageName };
            var enumerator = new LoopingEnumerator<string>(singleItemList);
            return new ImageSequenceAnimation(image.ImageProvider, enumerator, null, 0.1);

        }
    }
}
