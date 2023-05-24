using BaseRPG.View.Animation;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BaseRPG.Controller.Utility
{
    public class DeltaLoopHandler
    {
        private bool isFirstTick = true;
        private Stopwatch stopWatch;
        public double Fps { get { return (int)fps; } }
        public DeltaLoopHandler()
        {
            this.stopWatch = new();
            stopWatch.Start();
        }

        private double fps;
        private double lastTickTime = 0.0;
        public event Action FirsTickEvent;

        public double Tick() {
            if (isFirstTick)
            {
                FirsTickEvent?.Invoke();
                isFirstTick = false;
            }
            var currentTime = stopWatch.Elapsed.TotalMilliseconds;
            double delta = (currentTime - lastTickTime) / 1000.0;
            fps = 1/delta;
            lastTickTime = currentTime;
            if (System.Diagnostics.Debugger.IsAttached) { 
                return Math.Min(delta, 0.1);
            }
            return delta;
        }
    }
}
