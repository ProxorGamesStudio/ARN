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

        public struct Display
        {
            public static void SetupFactors(Vector2[] vectors, ref Pair<float, float>[] factors)
            {
                for (int i = 0; i < vectors.Length; i++)
                {
                    factors[i] = new Pair<float, float>();
                    factors[i].First = Screen.width / vectors[i].x;
                    factors[i].Second = Screen.height / vectors[i].y;
                }
            }

            public static Vector2 Update(Vector2 inputScreenSize)
            {
                return new Vector2(Screen.width / inputScreenSize.x, Screen.height / inputScreenSize.y);
            }

            public unsafe static void ChangeUI(ref Vector2[] start, ref Vector2[] target, ref Pair<float, float>[] factors)
            {
                for(int i = 0; i < start.Length; i++)
                {
                    start[i] = Update(new Vector2(factors[i].First, factors[i].Second));
                    target[i] = new Vector2(Screen.width/factors[i].First, Screen.height/factors[i].Second);
                }
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


