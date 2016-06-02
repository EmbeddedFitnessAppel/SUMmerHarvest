using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.helpers
{
    public static class Number
    {

        public static int AssertMinInt(int number, int min)
        {
            if(number<min)
            {
                number = min;
            }
            return number;
        }
    }
}
