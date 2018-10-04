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

        public static string GetStreetForRegion(string address)
        {
            if (address is null)
            {
                throw new ArgumentNullException(String.Format("{0} is null", nameof(address)));
            }

            return GetWordWithoutSpaces(address.Substring(address.IndexOf(".") + 1));
        }

        public static string GetWordWithoutSpaces(string word)
        {
            if (word is null)
            {
                throw new ArgumentNullException(String.Format("{0} is null", nameof(word)));
            }

            int countOfSpacesInFront = 0;
            int countOfSpacesAfterWord = word.Length - 1;

            while (word[countOfSpacesInFront] == ' ')
            {
                countOfSpacesInFront++;
            }

            while (word[countOfSpacesAfterWord] == ' ')
            {
                countOfSpacesAfterWord--;
            }
            
            return word.Substring(countOfSpacesInFront, countOfSpacesAfterWord - countOfSpacesInFront + 1);
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

        public static bool GetRegion(string region, out string regionName)
        {
            if (region is null)
            {
                throw new ArgumentNullException(String.Format("{0} is null", nameof(region)));
            }

            if (!String.IsNullOrEmpty(region.Substring(4)))
            {
                if (!(Int32.TryParse(region.Substring(1, 2), out int num)))
                {
                    throw new ArgumentException("{0} is not valid", nameof(region));
                }

                region = region.Substring(4);

                int startIndex = 0;
                int lastIndex = region.Length - 1;
                while (region[startIndex] == ' ')
                {
                    startIndex++;
                }
                while (region[lastIndex] == ' ')
                {
                    lastIndex--;
                }
                
                regionName = region.Substring(startIndex, (lastIndex - startIndex + 1));
                return true;
            }
            else
            {
                regionName = region;
                return false;
            }
        }

        public static int GetNum(string text)
        {
            if (text is null)
            {
                throw new ArgumentNullException(String.Format("{0} is null", nameof(text)));
            }

            if (!(Int32.TryParse(text.Substring(1, 2), out int num)))
            {
                return -1;
            }
            else return num;            
        }

        public static bool IsEnteredRegionCorrect(string Region, IEnumerable Regions)
        {
            if (Region is null)
            {
                throw new ArgumentNullException(String.Format("{0} is null", nameof(Region)));
            }
            if (Regions is null)
            {
                throw new ArgumentNullException(String.Format("{0} is null", nameof(Regions)));
            }

            int Num = GetNum(Region);
            GetRegion(Region, out string Name);
            foreach (var r in Regions)
            {
                string regionName = r.ToString();
                if (GetId(regionName) == Num)
                {
                    if ((regionName.Substring(2).ToLower() == Name.ToLower()))
                    {
                        return true;
                    }
                    break;
                }
            }

            return false; 
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
            if (information is null)
            {
                throw new ArgumentNullException(String.Format("{0} is null", nameof(information)));
            }

            return information.Substring(information.Length - 6);
        }
    }
}
