using BaseRPG.Controller.Interfaces;
using BaseRPG.View.Animation.Utility;
using BaseRPG.View.Interfaces;
using MathNet.Spatial.Euclidean;
using Microsoft.UI.Input;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Controller.Input
{
    public class InputHandler
    {
        private IProcessedInputActionMapper processedInputActionMapper;
        private IRawInputProcessedInputMapper rawInputProcessedInputMapper;
        private List<string> pressedButNotReleasedInput = new List<string>();
        private PositionTracker mousePositionTracker = new PositionTracker();
        public Vector2D MousePosition {
            get { return mousePositionTracker.Position; }
            set {
                mousePositionTracker.Position = new(value.X,value.Y);
            } 
        }
        public PositionTracker MousePositionTracker => mousePositionTracker;
        public InputHandler(IRawInputProcessedInputMapper rawInputProcessedInputMapper)
        {
            this.rawInputProcessedInputMapper = rawInputProcessedInputMapper;
        }
        internal void OnTick(double delta)
        {
            lock (pressedButNotReleasedInput) {
                foreach (string input in pressedButNotReleasedInput)
                {
                    reactToPressedInput(input, delta);
                }
            }
            
        }
        public void MouseDown(object sender, PointerRoutedEventArgs e)
        {
            PointerPoint point = e.GetCurrentPoint((Canvas)sender);
            string input = "";
            if (point.Properties.IsLeftButtonPressed )
                input = "MouseLeft";
            if (point.Properties.IsRightButtonPressed)
                input = "MouseRight";
            reactToInputDown(input);
        }

        

        public void MouseUp(object sender, PointerRoutedEventArgs e)
        {
            PointerPoint point = e.GetCurrentPoint((Canvas)sender);
            string input = "";
            if (pressedButNotReleasedInput.Contains("MouseLeft") &&!point.Properties.IsLeftButtonPressed)
                input = "MouseLeft";
            if (pressedButNotReleasedInput.Contains("MouseRight") && !point.Properties.IsRightButtonPressed)
                input = "MouseRight";
            reactToInputUp(input);
        }

        public void KeyDown(object sender,KeyRoutedEventArgs e)
        {
            string key = e.Key.ToString();
            reactToInputDown(key);
        }

        internal IProcessedInputActionMapper ProcessedInputActionMapper 
        {
            set 
            {
                this.processedInputActionMapper = value;
            } 
        }

        public void KeyUp(object sender, KeyRoutedEventArgs e)
        {
            string key = e.Key.ToString();
            reactToInputUp(key);
        }
        public void MouseMoved(object sender, PointerRoutedEventArgs args)
        {
            Windows.Foundation.Point position = args.GetCurrentPoint((Canvas)sender).Position;
            MousePosition = new(position.X,position.Y);
        }

        private void reactToInputDown(string rawInput)
        {
            
            lock (pressedButNotReleasedInput) {

                if (pressedButNotReleasedInput.Contains(rawInput)) return;
                foreach (var action in toAction(rawInput))
                {
                    action.OnPressed();
                }
                pressedButNotReleasedInput.Add(rawInput);
            }
            
        }

        

        private void reactToInputUp(string rawInput)
        {
            foreach (var action in toAction(rawInput))
            {
                action.OnReleased();
            }
            lock (pressedButNotReleasedInput)
            {
                pressedButNotReleasedInput.RemoveAll((s) => s == rawInput);
            }
            
        }
        private void reactToPressedInput(string rawInput,double delta) {
            foreach (var action in toAction(rawInput)) {
                action.OnHold(delta);
            }
        }

        private IEnumerable<IInputAction> toAction(string rawInput) {
            return processedInputActionMapper.ToAction(rawInputProcessedInputMapper.toProcessedInput(rawInput));
        }
    }
}
