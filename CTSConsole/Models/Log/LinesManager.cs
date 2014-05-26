using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CTSConsole.Models.Log
{
    class LinesManager
    {
        public Lines Lines { get; set; }
        public string FilePath { get; set; }

        public LinesManager()
        {
            Lines = new Lines();
            FilePath = null;
        }

        public LinesManager(string filePath)
        {
            FilePath = filePath;
            Lines = new Lines();
        }
        public string GetNext(string s, ref int index)
        {
            string result = "";
            bool digit = char.IsDigit(s[index]);

            for (; index < s.Length; index++)
            {
                if (digit && char.IsDigit(s[index]))
                {
                    result += s[index];
                }
                else
                {
                    if (!digit && char.IsLetter(s[index]))
                    {
                        result += s[index];
                    }
                    else
                    {
                        return result;

                    }
                }
            }
            return result;
        }

        public void ProcessFileAndStore()
        {
            StreamReader reader = File.OpenText(FilePath);
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                string[] items = line.Split(';');
                if (items[14] != "" && items[3].Equals("CY"))
                {
                    int i = 0, etiquette1, etiquette2;
                    string sense = "", sNumeroBus = "", sNumeroLine = "";
                    double latitude, longitude;
                    DateTime dateOperation = Convert.ToDateTime(items[0] + " " + items[1]);
                    items[5] = items[5].Replace('.', ',');
                    items[6] = items[6].Replace('.', ',');
                    int rssi, ber, la;
                    rssi = int.Parse(items[16]);
                    ber = int.Parse(items[17]);
                    la = int.Parse(items[18]);

                    if (items[5] != "")
                        latitude = double.Parse((items[5]));
                    else
                        latitude = 0;
                    if (items[6] != "")
                        longitude = double.Parse((items[6]));
                    else
                        longitude = 0;
                    if (items[9] != "")
                        etiquette1 = int.Parse((items[9]));
                    else
                        etiquette1 = 0;
                    if (items[10] != "")
                        etiquette2 = int.Parse((items[10]));
                    else
                        etiquette2 = 0;

                    sNumeroLine = GetNext(items[14], ref i);
                    sense = GetNext(items[14], ref i);
                    sNumeroBus = items[2];
                    Lines.AddLine(int.Parse(sNumeroLine), int.Parse(sNumeroBus), items[3], latitude, longitude, sense[0], dateOperation, etiquette1, etiquette2, rssi, ber, la);
                }

            }

        }
        public LinesManager(Lines lines, string filePath)
        {
            Lines = lines;
            FilePath = filePath;
        }
    }
}
