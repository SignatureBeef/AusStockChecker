using Figgle;
using System;
using System.Collections.Generic;
using SysConsole = System.Console;

// YEH YEH, NO JUDGMENT ON ALL THE LOCKS K THANKS. to be fair i was in a rush wanting my new gpu so i wasn't going to implement 
// any custom console handler for it all, and by the time i started caring i have one so now this is yours - a PR is welcomed of course.

namespace SimpleConsole.Targets
{
    public struct ConsoleTarget : IOutputTarget
    {
        static object _lock = new object();
        static List<StatusBar> StatusBars { get; set; } = new List<StatusBar>();

        static ConsoleTarget()
        {
            SysConsole.ForegroundColor = ConsoleColor.White;
        }

        public StatusBar AddStatusBar(StatusBar statusBar)
        {
            lock (_lock)
            {
                StatusBars.Add(statusBar);
            }
            return statusBar;
        }

        public StatusBar RemoveStatusBar(StatusBar statusBar)
        {
            lock (_lock)
            {
                StatusBars.Remove(statusBar);
            }
            return statusBar;
        }

        private static void UpdateStatusBars()
        {
            lock (_lock)
            {
                var posX = SysConsole.CursorLeft;
                var posY = SysConsole.CursorTop;
                var consumedX = 0;
                foreach (var bar in StatusBars)
                {
                    SysConsole.CursorLeft = SysConsole.BufferWidth - bar.Text.Length - consumedX;
                    SysConsole.Write(bar.Text);
                    consumedX += bar.Text.Length;
                }
                SysConsole.CursorLeft = posX;
                SysConsole.CursorTop = posY;
            }
        }
        private ConsoleTarget Action(Action callback, ConsoleColor? color = null)
        {
            UpdateStatusBars();
            if (color != null)
            {
                lock (_lock)
                {
                    var before = SysConsole.ForegroundColor;
                    SysConsole.ForegroundColor = color.Value;
                    callback();
                    SysConsole.ForegroundColor = before;
                }
            }
            else lock (_lock) callback();
            return this;
        }

        public IOutputTarget LocalTime(ConsoleColor? color = null) => Action(() => SysConsole.Write($"{System.DateTime.Now:yyyy-MM-dd HH:mm:ss}"), color);

        #region colors
        public IOutputTarget Color(string message, ConsoleColor? color = null) => Action(() => SysConsole.Write(message), color);

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
        public IOutputTarget Write(char[] buffer, int index, int count, ConsoleColor? color = null) => Action(() => SysConsole.Write(buffer, index, count), color);
#nullable enable
        public IOutputTarget Write(char[]? buffer, ConsoleColor? color = null) => Action(() => SysConsole.Write(buffer), color);
        public IOutputTarget Write(object? value, ConsoleColor? color = null) => Action(() => SysConsole.Write(value), color);
#nullable restore
        public IOutputTarget Write(bool value, ConsoleColor? color = null) => Action(() => SysConsole.Write(value), color);
        public IOutputTarget Write(decimal value, ConsoleColor? color = null) => Action(() => SysConsole.Write(value), color);
        public IOutputTarget Write(char value, ConsoleColor? color = null) => Action(() => SysConsole.Write(value), color);
        public IOutputTarget Write(long value, ConsoleColor? color = null) => Action(() => SysConsole.Write(value), color);
        public IOutputTarget Write(double value, ConsoleColor? color = null) => Action(() => SysConsole.Write(value), color);
        public IOutputTarget Write(float value, ConsoleColor? color = null) => Action(() => SysConsole.Write(value), color);
#nullable enable
        public IOutputTarget Write(string? value, ConsoleColor? color = null) => Action(() => SysConsole.Write(value), color);
        public IOutputTarget Write(string format, object? arg0, ConsoleColor? color = null) => Action(() => SysConsole.Write(format, arg0), color);
        public IOutputTarget Write(string format, object? arg0, object? arg1, ConsoleColor? color = null) => Action(() => SysConsole.Write(format, arg0, arg1), color);
        public IOutputTarget Write(string format, object? arg0, object? arg1, object? arg2, ConsoleColor? color = null) => Action(() => SysConsole.Write(format, arg0, arg1, arg2), color);
        public IOutputTarget Write(string format, params object?[]? arg) => Action(() => SysConsole.Write(format, arg));
#nullable restore
        public IOutputTarget Write(uint value, ConsoleColor? color = null) => Action(() => SysConsole.Write(value), color);
        public IOutputTarget Write(ulong value, ConsoleColor? color = null) => Action(() => SysConsole.Write(value), color);
        public IOutputTarget Write(int value, ConsoleColor? color = null) => Action(() => SysConsole.Write(value), color);
        public IOutputTarget WriteLine(uint value, ConsoleColor? color = null) => Action(() => SysConsole.WriteLine(value), color);
        public IOutputTarget WriteLine() => Action(() => SysConsole.WriteLine());
        public IOutputTarget WriteLine(bool value, ConsoleColor? color = null) => Action(() => SysConsole.WriteLine(value), color);
        public IOutputTarget WriteLine(ulong value, ConsoleColor? color = null) => Action(() => SysConsole.WriteLine(value), color);
        public IOutputTarget WriteLine(decimal value, ConsoleColor? color = null) => Action(() => SysConsole.WriteLine(value), color);
        public IOutputTarget WriteLine(double value, ConsoleColor? color = null) => Action(() => SysConsole.WriteLine(value), color);
        public IOutputTarget WriteLine(int value, ConsoleColor? color = null) => Action(() => SysConsole.WriteLine(value), color);
        public IOutputTarget WriteLine(long value, ConsoleColor? color = null) => Action(() => SysConsole.WriteLine(value), color);
        public IOutputTarget WriteLine(float value, ConsoleColor? color = null) => Action(() => SysConsole.WriteLine(value), color);
#nullable enable
        public IOutputTarget WriteLine(char[]? buffer, ConsoleColor? color = null) => Action(() => SysConsole.WriteLine(buffer), color);
        public IOutputTarget WriteLine(object? value, ConsoleColor? color = null) => Action(() => SysConsole.WriteLine(value), color);
        public IOutputTarget WriteLine(string? value, ConsoleColor? color = null) => Action(() => SysConsole.WriteLine(value), color);
        public IOutputTarget WriteLine(string format, object? arg0, ConsoleColor? color = null) => Action(() => SysConsole.WriteLine(format, arg0), color);
        public IOutputTarget WriteLine(string format, object? arg0, object? arg1, ConsoleColor? color = null) => Action(() => SysConsole.WriteLine(format, arg0, arg1), color);
        public IOutputTarget WriteLine(string format, object? arg0, object? arg1, object? arg2, ConsoleColor? color = null) => Action(() => SysConsole.WriteLine(format, arg0, arg1, arg2), color);
        public IOutputTarget WriteLine(string format, params object?[]? arg) => Action(() => SysConsole.WriteLine(format, arg));
#nullable restore
        public IOutputTarget WriteLine(char[] buffer, int index, int count, ConsoleColor? color = null) => Action(() => SysConsole.WriteLine(buffer, index, count), color);
        public IOutputTarget WriteLine(char value, ConsoleColor? color = null) => Action(() => SysConsole.WriteLine(value), color);
        #endregion

        public IOutputTarget Gradient(string text, System.ConsoleColor[] gradient = null, GradientPattern pattern = GradientPattern.Letter)
        {
            int word = 0;

            if (gradient == null)
            {
                if (pattern == GradientPattern.Letter)
                {
                    gradient = this.ColorRange(text.Length, 1, 15);
                }
                else
                {
                    var words = text.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    gradient = this.ColorRange(words.Length, 7, 15);
                }
            }

            UpdateStatusBars();

            // break down to row/col and change color as required
            lock (_lock)
            {
                var before = SysConsole.ForegroundColor;
                var initialY = SysConsole.CursorTop;

                for (var i = 0; i < text.Length; i++)
                {
                    var chr = text[i];
                    var empty = chr == ' ';
                    var str = chr.ToString();
                    var rendered = FiggleFonts.Standard.Render(str);
                    var rows = rendered.Split("\r\n", System.StringSplitOptions.RemoveEmptyEntries);

                    if (pattern == GradientPattern.Letter)
                        SysConsole.ForegroundColor = gradient[i];

                    if (empty)
                        word++;
                    if (pattern == GradientPattern.Word)
                        SysConsole.ForegroundColor = gradient[word];

                    var startX = SysConsole.CursorLeft;
                    var startY = SysConsole.CursorTop;
                    var endX = 0;
                    foreach (var row in rows)
                    {
                        SysConsole.CursorLeft = startX;
                        SysConsole.CursorTop = startY++;
                        SysConsole.Write(row);
                        endX = SysConsole.CursorLeft;
                    }

                    if (i != text.Length - 1)
                    {
                        SysConsole.CursorLeft = endX + 1;
                        SysConsole.CursorTop = initialY;
                    }
                    else
                    {
                        SysConsole.WriteLine();
                    }
                }

                SysConsole.ForegroundColor = before;
            }
            return this;
        }

        public void Dispose()
        {
        }
    }
}
