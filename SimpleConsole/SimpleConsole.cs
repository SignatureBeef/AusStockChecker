using SimpleConsole.Targets;
using System;

namespace SimpleConsole
{
    public static class SimpleConsole
    {
        //private static IOutputTarget _target = new ConsoleTarget();
        private static IOutputTarget _target = new LoggerOutputTarget();

        public static void SetOutputTarget(IOutputTarget target) => _target = target;

        public static StatusBar AddStatusBar(StatusBar statusBar) => _target.AddStatusBar(statusBar);

        #region colors
        public static IOutputTarget Black(string message) => _target.Black(message);
        public static IOutputTarget DarkBlue(string message) => _target.DarkBlue(message);
        public static IOutputTarget DarkGreen(string message) => _target.DarkGreen(message);
        public static IOutputTarget DarkCyan(string message) => _target.DarkCyan(message);
        public static IOutputTarget DarkRed(string message) => _target.DarkRed(message);
        public static IOutputTarget DarkMagenta(string message) => _target.DarkMagenta(message);
        public static IOutputTarget DarkYellow(string message) => _target.DarkYellow(message);
        public static IOutputTarget Gray(string message) => _target.Gray(message);
        public static IOutputTarget DarkGray(string message) => _target.DarkGray(message);
        public static IOutputTarget Blue(string message) => _target.Blue(message);
        public static IOutputTarget Green(string message) => _target.Green(message);
        public static IOutputTarget Cyan(string message) => _target.Cyan(message);
        public static IOutputTarget Red(string message) => _target.Red(message);
        public static IOutputTarget Magenta(string message) => _target.Magenta(message);
        public static IOutputTarget Yellow(string message) => _target.Yellow(message);
        public static IOutputTarget White(string message) => _target.White(message);
        #endregion

        #region Shims
        public static IOutputTarget LocalTime(ConsoleColor? color = null) => _target.LocalTime(color);

        public static IOutputTarget Gradient(string text, System.ConsoleColor[] gradient = null, GradientPattern pattern = GradientPattern.Letter) => _target.Gradient(text, gradient, pattern);

        public static IOutputTarget Write(char[] buffer, int index, int count, ConsoleColor? color = null) => _target.Write(buffer, index, count, color);
#nullable enable
        public static IOutputTarget Write(char[]? buffer, ConsoleColor? color = null) => _target.Write(buffer, color);
        public static IOutputTarget Write(object? value, ConsoleColor? color = null) => _target.Write(value, color);
#nullable restore
        public static IOutputTarget Write(bool value, ConsoleColor? color = null) => _target.Write(value, color);
        public static IOutputTarget Write(decimal value, ConsoleColor? color = null) => _target.Write(value, color);
        public static IOutputTarget Write(char value, ConsoleColor? color = null) => _target.Write(value, color);
        public static IOutputTarget Write(long value, ConsoleColor? color = null) => _target.Write(value, color);
        public static IOutputTarget Write(double value, ConsoleColor? color = null) => _target.Write(value, color);
        public static IOutputTarget Write(float value, ConsoleColor? color = null) => _target.Write(value, color);
#nullable enable
        public static IOutputTarget Write(string? value, ConsoleColor? color = null) => _target.Write(value, color);
        public static IOutputTarget Write(string format, object? arg0, ConsoleColor? color = null) => _target.Write(format, arg0, color);
        public static IOutputTarget Write(string format, object? arg0, object? arg1, ConsoleColor? color = null) => _target.Write(format, arg0, arg1, color);
        public static IOutputTarget Write(string format, object? arg0, object? arg1, object? arg2, ConsoleColor? color = null) => _target.Write(format, arg0, arg1, arg2, color);
        public static IOutputTarget Write(string format, params object?[]? arg) => _target.Write(format, arg);
#nullable restore
        public static IOutputTarget Write(uint value, ConsoleColor? color = null) => _target.Write(value, color);
        public static IOutputTarget Write(ulong value, ConsoleColor? color = null) => _target.Write(value, color);
        public static IOutputTarget Write(int value, ConsoleColor? color = null) => _target.Write(value, color);
        public static IOutputTarget WriteLine(uint value, ConsoleColor? color = null) => _target.WriteLine(value, color);
        public static IOutputTarget WriteLine() => _target.WriteLine();
        public static IOutputTarget WriteLine(bool value, ConsoleColor? color = null) => _target.WriteLine(value, color);
        public static IOutputTarget WriteLine(ulong value, ConsoleColor? color = null) => _target.WriteLine(value, color);
#nullable enable
        public static IOutputTarget WriteLine(char[]? buffer, ConsoleColor? color = null) => _target.WriteLine(buffer, color);
#nullable restore
        public static IOutputTarget WriteLine(decimal value, ConsoleColor? color = null) => _target.WriteLine(value, color);
        public static IOutputTarget WriteLine(double value, ConsoleColor? color = null) => _target.WriteLine(value, color);
        public static IOutputTarget WriteLine(int value, ConsoleColor? color = null) => _target.WriteLine(value, color);
        public static IOutputTarget WriteLine(long value, ConsoleColor? color = null) => _target.WriteLine(value, color);
        public static IOutputTarget WriteLine(float value, ConsoleColor? color = null) => _target.WriteLine(value, color);
#nullable enable
        public static IOutputTarget WriteLine(object? value, ConsoleColor? color = null) => _target.WriteLine(value, color);
        public static IOutputTarget WriteLine(string? value, ConsoleColor? color = null) => _target.WriteLine(value, color);
        public static IOutputTarget WriteLine(string format, object? arg0, ConsoleColor? color = null) => _target.WriteLine(format, arg0, color);
        public static IOutputTarget WriteLine(string format, object? arg0, object? arg1, ConsoleColor? color = null) => _target.WriteLine(format, arg0, arg1, color);
        public static IOutputTarget WriteLine(string format, object? arg0, object? arg1, object? arg2, ConsoleColor? color = null) => _target.WriteLine(format, arg0, arg1, arg2, color);
        public static IOutputTarget WriteLine(string format, params object?[]? arg) => _target.WriteLine(format, arg);
#nullable restore
        public static IOutputTarget WriteLine(char[] buffer, int index, int count, ConsoleColor? color = null) => _target.WriteLine(buffer, index, count, color);
        public static IOutputTarget WriteLine(char value, ConsoleColor? color = null) => _target.WriteLine(value, color);
        #endregion
    }
}
