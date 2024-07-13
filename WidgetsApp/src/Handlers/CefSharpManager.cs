
using CefSharp;
using CefSharp.WinForms;
using WidgetsApp.src.Util;


namespace WidgetsApp.src.Handlers
{
    public class CefSharpManager
    {
        public CefSharpManager()
        {
            
        }

        public void Initialize()
        {
            CefSettingsBase settings = new CefSettings
            {
                CachePath = FileManager.BROWSERPATH
            };

            settings.CefCommandLineArgs.Add("enable-persistent-cookies", "1");

            CefSharpSettings.ConcurrentTaskExecution = true;
            CefSharpSettings.ShutdownOnExit = true;
            Cef.Initialize(settings);
        }

        public bool IsInitialized()
        {
            return Cef.IsInitialized;
        }
    }
}