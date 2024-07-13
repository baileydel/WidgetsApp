using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace WidgetsApp.src.Util
{
    internal class FileManager
    {
        public static readonly string PATH = AppDomain.CurrentDomain.BaseDirectory;
        public static readonly string SAVEPATH = PATH + "save";
        public static readonly string SCRIPTPATH = PATH + "scripts";
        public static readonly string BROWSERPATH = PATH + "browser";

        public FileManager()
        {
            InitializePaths();
        }

        private void InitializePaths()
        {
            if (!Directory.Exists(SAVEPATH))
            {
                Directory.CreateDirectory(SAVEPATH);
            }

            if (!Directory.Exists(SCRIPTPATH))
            {
                Directory.CreateDirectory(SCRIPTPATH);
            }

            if (!Directory.Exists(BROWSERPATH))
            {
                Directory.CreateDirectory(BROWSERPATH);
            }
        }

        public List<WidgetData> GetShortcutSaves()
        {
            string[] files = Directory.GetFiles(SAVEPATH);

            List<WidgetData> widgetDataList = new List<WidgetData>();

            foreach (string file in files)
            {
                if (file.EndsWith(".json"))
                {
                    string json = File.ReadAllText(file);
                    WidgetData data = JsonConvert.DeserializeObject<WidgetData>(json);
                    data.SavePath = file;
                    widgetDataList.Add(data);
                }
            }
            return widgetDataList;
        }

        public void Save(WidgetData data)
        {
            string json = JsonConvert.SerializeObject(data);

            File.WriteAllText(SAVEPATH + @"\" + ValidateName(data.Name) + ".json", json);
        }

        /**
         * Validate the name of the widget
         * @param name The name of the widget
         * @return The edited name of the widget, if the name is invalid
         */
        public string ValidateName(string name)
        {
            return name.Replace("https://", "").Replace("http://", "").Split('/')[0];
        }

        public void Delete(string name)
        {
            string path = SAVEPATH + $"\\{ValidateName(name)}.json";

            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        public Image GetIcon(string name)
        {
            string path = SAVEPATH + $"\\{name}.png";
            if (File.Exists(path))
            {
                return Image.FromFile(path);
            }
            return null;
        }
    }   
}
