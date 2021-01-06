using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleConsole.Targets
{
    public static class TargetExtensions
    {
        public static System.ConsoleColor[] ColorRange(int elements, int min = 0, int max = 16)
        {
            var range = new System.ConsoleColor[elements];
            for (var c = 0; c < range.Length; c++)
            {
                var rnd = new System.Random();
                range[c] = (System.ConsoleColor)rnd.Next(min, max);
            }
            return range;
        }

        public static System.ConsoleColor[] ColorRange(this IOutputTarget target, int elements, int min = 0, int max = 16) => ColorRange(elements, min, max);
    }
}
