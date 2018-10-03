using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PolyclinicBL
{
    public static class Editor
    {
        public static string GetStreet(string address)
        {
            if (address is null)
            {
                throw new ArgumentNullException(String.Format("{0} is null", nameof(address)));
            }

            return GetWordWithoutSpaces(address.Substring(address.IndexOf(".") + 1, address.IndexOf(',') - 3));
        }

        public static string GetWordWithoutSpaces(string word)
        {
            if (word is null)
            {
                throw new ArgumentNullException(String.Format("{0} is null", nameof(word)));
            }

            int countOfSpacesInFront = 0;
            int countOfSpacesAfterWord = 0;

            while (word[countOfSpacesInFront] == ' ')
            {
                countOfSpacesInFront++;
            }

            while (word[word.Length - countOfSpacesAfterWord - 1] == ' ')
            {
                countOfSpacesAfterWord++;
            }

            return word.Substring(countOfSpacesInFront, word.Length - countOfSpacesAfterWord - 1);
        }

        public static int GetId(string victim)
        {
            if (victim is null)
            {
                throw new ArgumentNullException(String.Format("{0} is null", nameof(victim)));
            }

            if (!Int32.TryParse(victim.Substring(0, victim.IndexOf(".")), out int result))
            {
                throw new ArgumentException(String.Format("{0} is not valid.", nameof(victim)));
            }

            return result;
        }

        public static string GetSpecialization(string victim)
        {
            if (victim is null)
            {
                throw new ArgumentNullException(String.Format("{0} is null", nameof(victim)));
            }

            return victim.Substring(victim.IndexOf(".") + 1);
        }

        public static DateTime ParseToDateTime(string date, string time)
        {
            if (date is null)
            {
                throw new ArgumentNullException(String.Format("{0} is null", nameof(date)));
            }

            if (time is null)
            {
                throw new ArgumentNullException(String.Format("{0} is null", nameof(time)));
            }

            DateTime Date = DateTime.Parse(date);
            DateTime Time = DateTime.Parse(time);

            DateTime newDate = new DateTime(Date.Year, Date.Month, Date.Day, Time.Hour, Time.Minute, Time.Second);
            return newDate;
        }

        public static string GetRegion(string region)
        {
            if (!String.IsNullOrEmpty(region.Substring(4)))
            {
                int num = Convert.ToInt32(region.Substring(1, 2));
                region = region.Substring(4);
                while (region[0] == ' ')
                {
                    region = region.Substring(1, region.Length - 1);
                }
                while (region[region.Length - 1] == ' ')
                {
                    region = region.Substring(0, region.Length - 1);
                }
                return (region);
            }
            else return "666";
        }

        public static int GetNum(string s)
        {
            try
            {
                s = s.Substring(1, 2);
                if (String.IsNullOrEmpty(s)) return -1;

                return Convert.ToInt32(s);
            }
            catch (Exception)
            {
                return -1;
            };
        }

        public static int IsEnteredRegionCorrect(string Region, IEnumerable Regions)
        {
            int Num = GetNum(Region);
            string Name = GetRegion(Region);
            foreach (var r in Regions)
            {
                string regionName = r.ToString();
                if (GetId(regionName) == Num)
                {
                    if ((regionName.Substring(regionName.IndexOf("." + 1)).ToLower() == Name.ToLower()))
                    {
                        return 1;
                    }
                    else return -1;
                }
            }
            return 0;
        }


        public static byte[] GetByteRepresentation(string[] strings, int LastLineIndex)
        {
            if (strings is null)
            {
                throw new ArgumentNullException(String.Format("{0} is null", nameof(strings)));
            }

            int countOfStrings = strings.Length - LastLineIndex;

            if (countOfStrings > 0)
            {
                string records = String.Join("\n", strings, LastLineIndex, countOfStrings);

                return Encoding.Default.GetBytes(records);
            }

            else return Encoding.Default.GetBytes(new char[0]);
        }

        public static string GetText(List<string> data)
        {
            if (data is null)
            {
                throw new ArgumentNullException(String.Format("{0} is null", nameof(data)));
            }

            return String.Join(Environment.NewLine, data);
        }

        public static string GetTime(string information)
        {
            return information.Substring(information.Length - 6);
        }
    }
}
