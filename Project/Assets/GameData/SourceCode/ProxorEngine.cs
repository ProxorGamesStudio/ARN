using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProxorNetwork
{
    public class ProxorEngine
    {
        public struct Colors
        {
            public static Color ColorNonAlpha(Color inputColor)
            {
                return new Color(inputColor.r, inputColor.g, inputColor.b, 0);
            }

            public static Color DefalutColor(Color inputColor)
            {
                return new Color(inputColor.r, inputColor.g, inputColor.b, 1);
            }
        }
    }

    public class Pair<T, U>
    {
        public Pair()
        {
        }

        public Pair(T first, U second)
        {
            this.First = first;
            this.Second = second;
        }

        public T First { get; set; }
        public U Second { get; set; }
    };

}


