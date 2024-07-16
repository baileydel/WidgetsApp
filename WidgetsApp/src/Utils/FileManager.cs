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

                    if (data == null)
                    {
                        File.Delete(file);
                        continue;
                    }

                    data.SavePath = file;
                    widgetDataList.Add(data);
                }
            }
            return widgetDataList;
        }

        public void Save(WidgetData data)
        {
            string json = JsonConvert.SerializeObject(data);

            if (data.SavePath == null)
            {
                data.SavePath = SAVEPATH + $"\\{data.GetValidName()}.json";
            }

            File.WriteAllText(data.SavePath, json);
        }

        public void Delete(WidgetData widget)
        {   
            if (File.Exists(widget.SavePath))
            {
                File.Delete(widget.SavePath);
            }
        }

        
    }   
}
