using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Model.Attribute
{
    public class Health
    {
        
        public event Action HealthReachedZeroEvent;
        public event Action<double> HealthReachedMaxEvent;
        public event Action HealthCanged;
        private double currentValue;
        private double maxValue;

        public Health(int maxValue)
        {
            currentValue = maxValue;
            this.maxValue = maxValue;
        }
        public bool BelowZero {
            get {
                return currentValue <= 0;
            }
        }
        public double CurrentValue{
            get { return currentValue; }
            set {
                currentValue = value;
                HealthCanged?.Invoke();
                if (currentValue <= 0) {
                    currentValue = 0;
                    HealthReachedZeroEvent?.Invoke();
                }
                if (currentValue > maxValue){
                    currentValue = maxValue;
                    HealthReachedMaxEvent?.Invoke(maxValue);
                }
            }
        }
        public double MaxValue {
            get { return maxValue; } 
            set {
                var delta = (value - maxValue);
                maxValue = value;
                CurrentValue += delta;
            }
        }

        public double HealthPercentage => (currentValue / maxValue)*100;

    }
}
