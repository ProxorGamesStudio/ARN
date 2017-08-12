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

        public struct Math
        {
            public static float arcDistance(float radios, Vector3 first, Vector3 second)
            {
                return Mathf.PI * radios * Vector3.Angle(first, second) / 180f;
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

    public class Triple<T, U, K>
    {
        public Triple()
        {
        }

        public Triple(T first, U second, K threeth)
        {
            this.First = first;
            this.Second = second;
            this.Threeth = threeth;
        }

        public T First { get; set; }
        public U Second { get; set; }
        public K Threeth { get; set; }
    };

   
}


