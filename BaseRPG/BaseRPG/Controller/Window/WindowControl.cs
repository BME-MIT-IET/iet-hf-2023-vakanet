using BaseRPG.View.UIElements;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;

namespace BaseRPG.Controller.Window
{
    public class WindowControl
    {
        private class WindowData {

            private CustomWindow window;
            private bool opened;
            public CustomWindow Window { get => window; set => window = value; }
            public bool Opened { get => opened; set => opened = value; }
            public WindowData(CustomWindow window, bool opened)
            {
                this.window = window;
                this.opened = opened;
            }

            internal void Open(Canvas mainCanvas)
            {
                Opened = true;
                mainCanvas.Children.Add(Window);
                Window.OnOpened();
            }
            internal void Close(Canvas mainCanvas)
            {
                Opened = false;
                mainCanvas.Children.Remove(Window);
                Window.OnClosed();
            }
        }
        private Canvas mainCanvas;
        private Dictionary<string, WindowData> windows = new Dictionary<string, WindowData>();

   
        private WindowControl(Canvas mainCanvas, Dictionary<string, WindowData> windows)
        {
            this.mainCanvas = mainCanvas;
            this.windows = windows;
        }
        //public void AddWindow(string name, CustomWindow customWindow) {
        //    WindowData windowData = new(customWindow, false);
        //    windows.Add(name, windowData);
        //}
        //public void RemoveWindow(string name)
        //{
        //    windows.Remove(name);
        //}
        public void Open(string windowName) {
            windows[windowName].Open(mainCanvas);
        }
        public bool IsOpen(CustomWindow window) {
            var windowName = FindByValue(window);
            return windows[windowName].Opened;
        }
        public bool IsOpen(string window)
        {
            return windows[window].Opened;
        }
        public void Close(string windowName) {
            windows[windowName].Close(mainCanvas);
            
        }
        public void Close(CustomWindow window)
        {
            var windowName = FindByValue(window);
            Close(windowName);
        }
        public string FindByValue(CustomWindow window) {
            string windowName = null;
            foreach (var elem in windows)
            {
                if (elem.Value.Window == window)
                {
                    windowName = elem.Key;
                }
            }
            if (windowName == null)
            {
                throw new NoSuchWindowException(window.GetType().ToString());
            }
            return windowName;
        }

        public void OpenOrClose(string name) {
            var open = IsOpen(name);

            if  (open) Close(name);
            else       Open(name);
        }

        public class Builder {
            private Dictionary<string, WindowData> windows = new Dictionary<string, WindowData>();
            private Canvas mainCanvas;
            private SettingsWindow settingsWindow;
            public Builder(Canvas mainCanvas)
            {
                this.mainCanvas = mainCanvas;
            }

            public Builder SettingsWindowAs(SettingsWindow window)
            {
                settingsWindow = window;
                WindowData windowData = new(window, false);
                windows.Add(SettingsWindow.WindowName, windowData);
                return this;
            }
            public Builder Window(string name, CustomWindow window) {
                WindowData windowData = new(window, false);
                windows.Add(name, windowData);
                return this; 
            }

            public WindowControl Build() {

                WindowControl windowControl = new WindowControl(mainCanvas, windows);
                foreach (var windowData in windows.Values)
                {
                    windowData.Window.XButtonClicked += windowControl.Close;
                }
                settingsWindow.WindowControl = windowControl;
                return windowControl;
            }
        }

        internal CustomWindow FindByName(string windowName)
        {
            return windows[windowName].Window;
        }
    }
}
