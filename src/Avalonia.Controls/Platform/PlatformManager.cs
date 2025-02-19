using System;
using System.Reactive.Disposables;
using Avalonia.Metadata;
using Avalonia.Platform;

namespace Avalonia.Controls.Platform
{
    [Unstable]
    public static partial class PlatformManager
    {
        static bool s_designerMode;

        public static IDisposable DesignerMode()
        {
            s_designerMode = true;
            return Disposable.Create(() => s_designerMode = false);
        }

        public static void SetDesignerScalingFactor(double factor)
        {
        }

        public static ITrayIconImpl? CreateTrayIcon()
        {
            var platform = AvaloniaLocator.Current.GetService<IWindowingPlatform>();

            if (platform == null)
            {
                throw new Exception("Could not CreateTrayIcon(): IWindowingPlatform is not registered.");
            }

            return s_designerMode ? null : platform.CreateTrayIcon();
        }


        public static IWindowImpl CreateWindow()
        {
            var platform = AvaloniaLocator.Current.GetService<IWindowingPlatform>();
            
            if (platform == null)
            {
                throw new Exception("Could not CreateWindow(): IWindowingPlatform is not registered.");
            }

            return s_designerMode ? platform.CreateEmbeddableWindow() : platform.CreateWindow();
        }

        public static IWindowImpl CreateEmbeddableWindow()
        {
            var platform = AvaloniaLocator.Current.GetService<IWindowingPlatform>();
            if (platform == null)
                throw new Exception("Could not CreateEmbeddableWindow(): IWindowingPlatform is not registered.");
            return platform.CreateEmbeddableWindow();
        }
    }
}
