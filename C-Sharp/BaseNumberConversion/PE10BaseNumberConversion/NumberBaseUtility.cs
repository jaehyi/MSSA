using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PE10BaseNumberConversion
{
    public class NumberBaseUtility
    {
        public static char[] ValidBinaryCharacters = { '0', '1' };
        public static char[] ValidOctalCharacters = { '0', '1', '2', '3', '4', '5', '6', '7' };
        public static char[] ValidDecimalCharacters = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
        public static char[] ValidHexCharacters = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F' };
        public static Dictionary<int, string> HexLetters = new Dictionary<int, string>()
            { {0,"0"},{1,"1"},{2,"2"},{3,"3"},{4,"4"},{5,"5"},{6,"6"},{7,"7"},{8,"8"},{9,"9"},{10,"A"},{11,"B"},{12,"C"},{13,"D"},{14,"E"},{15,"F"}};
        public static Dictionary<string, int> HexNumbers = new Dictionary<string, int>()
            { {"0",0}, {"1",1},{"2",2},{"3",3},{"4",4},{"5",5},{"6",6},{"7",7},{"8",8},{"9",9},{"A",10},{"B",11},{"C",12},{"D",13},{"E",14},{"F",15}};
    }
}
