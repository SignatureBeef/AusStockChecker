using System;

namespace SimpleConsole.Targets
{
    public interface IOutputTarget : IDisposable
    {
        StatusBar AddStatusBar(StatusBar statusBar);
        StatusBar RemoveStatusBar(StatusBar statusBar);

        #region colors
        IOutputTarget Black(string message);
        IOutputTarget DarkBlue(string message);
        IOutputTarget DarkGreen(string message);
        IOutputTarget DarkCyan(string message);
        IOutputTarget DarkRed(string message);
        IOutputTarget DarkMagenta(string message);
        IOutputTarget DarkYellow(string message);
        IOutputTarget Gray(string message);
        IOutputTarget DarkGray(string message);
        IOutputTarget Blue(string message);
        IOutputTarget Green(string message);
        IOutputTarget Cyan(string message);
        IOutputTarget Red(string message);
        IOutputTarget Magenta(string message);
        IOutputTarget Yellow(string message);
        IOutputTarget White(string message);
        #endregion

        #region Shims
        IOutputTarget LocalTime(ConsoleColor? color = null);

        IOutputTarget Gradient(string text, System.ConsoleColor[] gradient = null, GradientPattern pattern = GradientPattern.Letter);


        IOutputTarget Write(char[] buffer, int index, int count, ConsoleColor? color = null);
#nullable enable
        IOutputTarget Write(char[]? buffer, ConsoleColor? color = null);
        IOutputTarget Write(object? value, ConsoleColor? color = null);
#nullable restore
        IOutputTarget Write(bool value, ConsoleColor? color = null);
        IOutputTarget Write(decimal value, ConsoleColor? color = null);
        IOutputTarget Write(char value, ConsoleColor? color = null);
        IOutputTarget Write(long value, ConsoleColor? color = null);
        IOutputTarget Write(double value, ConsoleColor? color = null);
        IOutputTarget Write(float value, ConsoleColor? color = null);
#nullable enable
        IOutputTarget Write(string? value, ConsoleColor? color = null);
        IOutputTarget Write(string format, object? arg0, ConsoleColor? color = null);
        IOutputTarget Write(string format, object? arg0, object? arg1, ConsoleColor? color = null);
        IOutputTarget Write(string format, object? arg0, object? arg1, object? arg2, ConsoleColor? color = null);
        IOutputTarget Write(string format, params object?[]? arg);
#nullable restore
        IOutputTarget Write(uint value, ConsoleColor? color = null);
        IOutputTarget Write(ulong value, ConsoleColor? color = null);
        IOutputTarget Write(int value, ConsoleColor? color = null);
        IOutputTarget WriteLine(uint value, ConsoleColor? color = null);
        IOutputTarget WriteLine();
        IOutputTarget WriteLine(bool value, ConsoleColor? color = null);
        IOutputTarget WriteLine(ulong value, ConsoleColor? color = null);
#nullable enable
        IOutputTarget WriteLine(char[]? buffer, ConsoleColor? color = null);
#nullable restore
        IOutputTarget WriteLine(decimal value, ConsoleColor? color = null);
        IOutputTarget WriteLine(double value, ConsoleColor? color = null);
        IOutputTarget WriteLine(int value, ConsoleColor? color = null);
        IOutputTarget WriteLine(long value, ConsoleColor? color = null);
        IOutputTarget WriteLine(float value, ConsoleColor? color = null);
#nullable enable
        IOutputTarget WriteLine(object? value, ConsoleColor? color = null);
        IOutputTarget WriteLine(string? value, ConsoleColor? color = null);
        IOutputTarget WriteLine(string format, object? arg0, ConsoleColor? color = null);
        IOutputTarget WriteLine(string format, object? arg0, object? arg1, ConsoleColor? color = null);
        IOutputTarget WriteLine(string format, object? arg0, object? arg1, object? arg2, ConsoleColor? color = null);
        IOutputTarget WriteLine(string format, params object?[]? arg);
#nullable restore
        IOutputTarget WriteLine(char[] buffer, int index, int count, ConsoleColor? color = null);
        IOutputTarget WriteLine(char value, ConsoleColor? color = null);
        #endregion
    }
}
