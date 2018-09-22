using System;
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
                throw new  ArgumentException (String.Format("{0} is not valid.", nameof(victim)));
            }
            
            return result;   
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
    }
}
