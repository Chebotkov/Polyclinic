using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolyclinicBL
{
    public static class Editor
    {
        public static string GetStreet(string address)
        {
            return GetWordWithoutSpaces(address.Substring(address.IndexOf(".") + 1, address.IndexOf(',') - 3));
        }

        public static string GetWordWithoutSpaces(string word)
        {
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
    }
}
