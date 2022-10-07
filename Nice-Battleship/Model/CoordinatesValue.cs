using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nice_Battleship.Model
{
    internal class CoordinatesValue
    {
        public static Dictionary<char, int> FillDictionary()
        {
            Dictionary<char, int> coordinate =
                     new Dictionary<char, int>
                     {
                         { 'A', 1 },
                         { 'B', 2 },
                         { 'C', 3 },
                         { 'D', 4 },
                         { 'E', 5 },
                         { 'F', 6 },
                         { 'G', 7 },
                         { 'H', 8 },
                         { 'I', 9 },
                         { 'J', 10 }
                     };

            return coordinate;
        }
    }
}
