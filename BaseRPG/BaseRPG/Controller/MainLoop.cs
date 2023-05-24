using BaseRPG.Controller.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BaseRPG.Controller
{
    internal class MainLoop
    {
        private Controller controller;
        private readonly int msBetweenTicks;
        private bool running = true;
        private Thread logicThread;

        public MainLoop(Controller controller, int msBetweenTicks = 0)
        {
            this.controller = controller;
            this.msBetweenTicks = msBetweenTicks;
        }

        public void Start() {
            logicThread = new Thread(o => Run());
            logicThread.IsBackground = true;
            logicThread.Start();

        }
        private void Run() {
            lock (controller.Game)
            {
                DeltaLoopHandler loopHandler = new();
                var timer = new System.Timers.Timer(1000);
                timer.Elapsed += (a, b) => Console.WriteLine("Tick fps: " + loopHandler.Fps);
                timer.Start();

                while (running)
                {
                    controller.Tick(loopHandler.Tick());
                    Thread.Sleep(msBetweenTicks);
                }
            }

        }
        public void Stop() {
        
        }
    }
}
