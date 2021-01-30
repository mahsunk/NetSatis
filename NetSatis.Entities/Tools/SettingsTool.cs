using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IniParser;
using IniParser.Model;

namespace NetSatis.Entities.Tools
{
    public class SettingsTool
    {
        FileIniDataParser parser = new FileIniDataParser();
        private IniData data;
        private string dosyaAdi = "Settings.ini";

        public void AyarDegis(string section, string key, string value)
        {
            if (data != null)
            {
                if (data.Sections.Count(c => c.SectionName == section) == 0)
                {
                    data.Sections.AddSection(section);
                    data[section].AddKey(key);

                }
                else
                {
                    data[section].AddKey(key);


                }

                data[section][key] = value;

            }
        }


        public string AyarOku(string section,string key)
        {
            return data[section][key];
        }

        public void Save()
        {
            parser.WriteFile(dosyaAdi,data);
        }



    }
}
