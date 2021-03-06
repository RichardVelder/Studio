using System.Collections.Generic;

namespace FamiStudio
{
    public class IniFile
    {
        Dictionary<string, Dictionary<string, string>> iniContent = new Dictionary<string, Dictionary<string, string>>();

        public void Load(string filename)
        {
            try
            {
                var lines = System.IO.File.ReadAllLines(filename);
                var sectionName = "";
                var sectionValues = new Dictionary<string, string>();

                foreach (string line in lines)
                {
                    if (line.StartsWith("["))
                    {
                        if (sectionName != "")
                        {
                            iniContent.Add(sectionName, sectionValues);
                            sectionName = "";
                            sectionValues = new Dictionary<string, string>();
                        }

                        sectionName = line.TrimStart('[').TrimEnd(']');
                    }
                    else
                    {
                        int eq = line.IndexOf('=');
                        if (eq >= 0)
                        {
                            sectionValues.Add(line.Substring(0, eq), line.Substring(eq + 1));
                        }
                    }
                }

                if (sectionName != "")
                {
                    iniContent.Add(sectionName, sectionValues);
                }
            }
            catch
            {
            }
        }

        public int GetInt(string section, string key, int defaultValue)
        {
            try
            {
                return int.Parse(iniContent[section][key]);
            }
            catch
            {
                return defaultValue;
            }
        }

        public bool GetBool(string section, string key, bool defaultValue)
        {
            try
            {
                return bool.Parse(iniContent[section][key]);
            }
            catch
            {
                return defaultValue;
            }
        }

        public string GetString(string section, string key, string defaultValue)
        {
            try
            {
                return iniContent[section][key];
            }
            catch
            {
                return defaultValue;
            }
        }

        public void SetInt(string section, string key, int value)
        {
            if (!iniContent.ContainsKey(section))
                iniContent[section] = new Dictionary<string, string>();
            iniContent[section][key] = value.ToString();
        }

        public void SetBool(string section, string key, bool value)
        {
            if (!iniContent.ContainsKey(section))
                iniContent[section] = new Dictionary<string, string>();
            iniContent[section][key] = value.ToString();
        }

        public void SetString(string section, string key, string value)
        {
            if (!iniContent.ContainsKey(section))
                iniContent[section] = new Dictionary<string, string>();
            iniContent[section][key] = value == null ? "" : value;
        }

        public void Save(string filename)
        {
            var lines = new List<string>();

            foreach (var itSection in iniContent)
            {
                var sectionName = itSection.Key;
                var sectionValues = itSection.Value;

                lines.Add("[" + sectionName + "]");

                foreach (var itValues in sectionValues)
                {
                    lines.Add(itValues.Key + "=" + itValues.Value);
                }

                lines.Add("");
            }

            System.IO.File.WriteAllLines(filename, lines);
        }
    }
}
