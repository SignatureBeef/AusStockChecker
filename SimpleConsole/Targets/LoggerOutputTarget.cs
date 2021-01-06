using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleConsole.Targets
{
    public class LoggerOutputTarget : IOutputTarget
    {
        FileStreamTarget _fs = new FileStreamTarget();
        ConsoleTarget _ct = new ConsoleTarget();

        public StatusBar AddStatusBar(StatusBar statusBar)
        {
            _fs.AddStatusBar(statusBar);
            return _ct.AddStatusBar(statusBar);
        }
        public StatusBar RemoveStatusBar(StatusBar statusBar)
        {
            _fs.AddStatusBar(statusBar);
            return _ct.AddStatusBar(statusBar);
        }

        public IOutputTarget LocalTime(ConsoleColor? color = null)
        {
            _fs.LocalTime(color);
            _ct.LocalTime(color);
            return this;
        }

        #region colors
        public IOutputTarget Color(string message, ConsoleColor? color = null)
        {
            _fs.Color(message, color);
            _ct.Color(message, color);
            return this;
        }

        public IOutputTarget Black(string message) => Color(message, ConsoleColor.Black);
        public IOutputTarget DarkBlue(string message) => Color(message, ConsoleColor.DarkBlue);
        public IOutputTarget DarkGreen(string message) => Color(message, ConsoleColor.DarkGreen);
        public IOutputTarget DarkCyan(string message) => Color(message, ConsoleColor.DarkCyan);
        public IOutputTarget DarkRed(string message) => Color(message, ConsoleColor.DarkRed);
        public IOutputTarget DarkMagenta(string message) => Color(message, ConsoleColor.DarkMagenta);
        public IOutputTarget DarkYellow(string message) => Color(message, ConsoleColor.DarkYellow);
        public IOutputTarget Gray(string message) => Color(message, ConsoleColor.Gray);
        public IOutputTarget DarkGray(string message) => Color(message, ConsoleColor.DarkGray);
        public IOutputTarget Blue(string message) => Color(message, ConsoleColor.Blue);
        public IOutputTarget Green(string message) => Color(message, ConsoleColor.Green);
        public IOutputTarget Cyan(string message) => Color(message, ConsoleColor.Cyan);
        public IOutputTarget Red(string message) => Color(message, ConsoleColor.Red);
        public IOutputTarget Magenta(string message) => Color(message, ConsoleColor.Magenta);
        public IOutputTarget Yellow(string message) => Color(message, ConsoleColor.Yellow);
        public IOutputTarget White(string message) => Color(message, ConsoleColor.White);
        #endregion

        #region Shims
        public IOutputTarget Write(char[] buffer, int index, int count, ConsoleColor? color = null)
        {
            _fs.Write(buffer, index, count, color);
            _ct.Write(buffer, index, count, color);
            return this;
        }
#nullable enable
        public IOutputTarget Write(char[]? buffer, ConsoleColor? color = null)
        {
            _fs.Write(buffer, color);
            _ct.Write(buffer, color);
            return this;
        }
        public IOutputTarget Write(object? value, ConsoleColor? color = null)
        {
            _fs.Write(value, color);
            _ct.Write(value, color);
            return this;
        }
#nullable restore
        public IOutputTarget Write(bool value, ConsoleColor? color = null)
        {
            _fs.Write(value, color);
            _ct.Write(value, color);
            return this;
        }
        public IOutputTarget Write(decimal value, ConsoleColor? color = null)
        {
            _fs.Write(value, color);
            _ct.Write(value, color);
            return this;
        }
        public IOutputTarget Write(char value, ConsoleColor? color = null)
        {
            _fs.Write(value, color);
            _ct.Write(value, color);
            return this;
        }
        public IOutputTarget Write(long value, ConsoleColor? color = null)
        {
            _fs.Write(value, color);
            _ct.Write(value, color);
            return this;
        }
        public IOutputTarget Write(double value, ConsoleColor? color = null)
        {
            _fs.Write(value, color);
            _ct.Write(value, color);
            return this;
        }
        public IOutputTarget Write(float value, ConsoleColor? color = null)
        {
            _fs.Write(value, color);
            _ct.Write(value, color);
            return this;
        }
#nullable enable
        public IOutputTarget Write(string? value, ConsoleColor? color = null)
        {
            _fs.Write(value, color);
            _ct.Write(value, color);
            return this;
        }
        public IOutputTarget Write(string format, object? arg0, ConsoleColor? color = null)
        {
            _fs.Write(format, arg0, color);
            _ct.Write(format, arg0, color);
            return this;
        }
        public IOutputTarget Write(string format, object? arg0, object? arg1, ConsoleColor? color = null)
        {
            _fs.Write(format, arg0, arg1, color);
            _ct.Write(format, arg0, arg1, color);
            return this;
        }
        public IOutputTarget Write(string format, object? arg0, object? arg1, object? arg2, ConsoleColor? color = null)
        {
            _fs.Write(format, arg0, arg1, arg2, color);
            _ct.Write(format, arg0, arg1, arg2, color);
            return this;
        }
        public IOutputTarget Write(string format, params object?[]? arg)
        {
            _fs.Write(format, arg);
            _ct.Write(format, arg);
            return this;
        }
#nullable restore
        public IOutputTarget Write(uint value, ConsoleColor? color = null)
        {
            _fs.Write(value, color);
            _ct.Write(value, color);
            return this;
        }
        public IOutputTarget Write(ulong value, ConsoleColor? color = null)
        {
            _fs.Write(value, color);
            _ct.Write(value, color);
            return this;
        }
        public IOutputTarget Write(int value, ConsoleColor? color = null)
        {
            _fs.Write(value, color);
            _ct.Write(value, color);
            return this;
        }
        public IOutputTarget WriteLine(uint value, ConsoleColor? color = null)
        {
            _fs.Write(value, color);
            _ct.Write(value, color);
            return this;
        }
        public IOutputTarget WriteLine()
        {
            _fs.WriteLine();
            _ct.WriteLine();
            return this;
        }
        public IOutputTarget WriteLine(bool value, ConsoleColor? color = null)
        {
            _fs.WriteLine(value, color);
            _ct.WriteLine(value, color);
            return this;
        }
        public IOutputTarget WriteLine(ulong value, ConsoleColor? color = null)
        {
            _fs.WriteLine(value, color);
            _ct.WriteLine(value, color);
            return this;
        }
        public IOutputTarget WriteLine(decimal value, ConsoleColor? color = null)
        {
            _fs.WriteLine(value, color);
            _ct.WriteLine(value, color);
            return this;
        }
        public IOutputTarget WriteLine(double value, ConsoleColor? color = null)
        {
            _fs.WriteLine(value, color);
            _ct.WriteLine(value, color);
            return this;
        }
        public IOutputTarget WriteLine(int value, ConsoleColor? color = null)
        {
            _fs.WriteLine(value, color);
            _ct.WriteLine(value, color);
            return this;
        }
        public IOutputTarget WriteLine(long value, ConsoleColor? color = null)
        {
            _fs.WriteLine(value, color);
            _ct.WriteLine(value, color);
            return this;
        }
        public IOutputTarget WriteLine(float value, ConsoleColor? color = null)
        {
            _fs.WriteLine(value, color);
            _ct.WriteLine(value, color);
            return this;
        }
#nullable enable
        public IOutputTarget WriteLine(char[]? buffer, ConsoleColor? color = null)
        {
            _fs.WriteLine(buffer, color);
            _ct.WriteLine(buffer, color);
            return this;
        }
        public IOutputTarget WriteLine(object? value, ConsoleColor? color = null)
        {
            _fs.WriteLine(value, color);
            _ct.WriteLine(value, color);
            return this;
        }
        public IOutputTarget WriteLine(string? value, ConsoleColor? color = null)
        {
            _fs.WriteLine(value, color);
            _ct.WriteLine(value, color);
            return this;
        }
        public IOutputTarget WriteLine(string format, object? arg0, ConsoleColor? color = null)
        {
            _fs.WriteLine(format, arg0, color);
            _ct.WriteLine(format, arg0, color);
            return this;
        }
        public IOutputTarget WriteLine(string format, object? arg0, object? arg1, ConsoleColor? color = null)
        {
            _fs.WriteLine(format, arg0, arg1, color);
            _ct.WriteLine(format, arg0, arg1, color);
            return this;
        }
        public IOutputTarget WriteLine(string format, object? arg0, object? arg1, object? arg2, ConsoleColor? color = null)
        {
            _fs.WriteLine(format, arg0, arg1, arg2, color);
            _ct.WriteLine(format, arg0, arg1, arg2, color);
            return this;
        }
        public IOutputTarget WriteLine(string format, params object?[]? arg)
        {
            _fs.WriteLine(format, arg);
            _ct.WriteLine(format, arg);
            return this;
        }
#nullable restore
        public IOutputTarget WriteLine(char[] buffer, int index, int count, ConsoleColor? color = null)
        {
            _fs.WriteLine(buffer, color);
            _ct.WriteLine(buffer, color);
            return this;
        }
        public IOutputTarget WriteLine(char value, ConsoleColor? color = null)
        {
            _fs.WriteLine(value, color);
            _ct.WriteLine(value, color);
            return this;
        }
        #endregion

        public IOutputTarget Gradient(string text, System.ConsoleColor[] gradient = null, GradientPattern pattern = GradientPattern.Letter)
        {
            _fs.Gradient(text, gradient, pattern);
            _ct.Gradient(text, gradient, pattern);
            return this;
        }

        public void Dispose()
        {
            _fs.Dispose();
            _fs = null;
            _ct.Dispose();
            _ct = default(ConsoleTarget);
        }
    }
}
