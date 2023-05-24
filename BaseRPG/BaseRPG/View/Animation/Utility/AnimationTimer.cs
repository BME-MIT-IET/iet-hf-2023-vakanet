using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.View.Animation.Utility
{
    public class AnimationTimer
    {
        private double secondsSinceStarted;
        private double maxSeconds;
        private readonly bool reset;

        public double SecondsSinceStarted { get => secondsSinceStarted; }
        public double MaxSeconds { get => maxSeconds; }

        public event Action Elapsed;
        public AnimationTimer(double maxSeconds, bool reset = false)
        {
            secondsSinceStarted = 0;
            this.maxSeconds = maxSeconds;
            this.reset = reset;
        }
        public void Tick(double secondsPassed)
        {
            secondsSinceStarted += secondsPassed;
            if (SecondsSinceStarted >= MaxSeconds)
            {
                Elapsed?.Invoke();
                if (reset) secondsSinceStarted = 0.0;
            }
        }

        /**
         * When the time passed since the last reset is larger in proportions than the first parameter,
         * the callback function is called in the second parameter
         */
        //public void WhenAt(double percent, Action action) {

        //}

    }
}
