using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nice_Battleship.Model
{
    internal class DictionaryValues
    {
        public static Dictionary<char, int> PositionDictionary()
        {
            Dictionary<char, int> position =
                     new Dictionary<char, int>
                     {
                         { 'V', 1 },
                         { 'H', 2 }
                     };

            return position;
        }

        public static Dictionary<char, bool> PositionManual()
        {
            Dictionary<char, bool> position =
                     new Dictionary<char, bool>
                     {
                         { 'Y', false },
                         { 'N', true }
                     };

            return position;
        }
    }
}
