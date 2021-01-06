using System;
using System.Collections.Generic;
using System.IO;

namespace SimpleConsole.Targets
{
    public class FileStreamTarget : IOutputTarget
    {
        StreamWriter _writer;
        static object _lock = new object();
        static List<StatusBar> StatusBars { get; set; } = new List<StatusBar>();

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

        private FileStreamTarget Write(string text)
        {
            lock (_writer)
            {
                _writer.Write(text);
                _writer.Flush();
            }
            return this;
        }
        private void WriteLine(string text)
        {
            _writer.WriteLine(text);
            _writer.Flush();
        }

        public FileStreamTarget(string filePath = null)
        {
            if (String.IsNullOrWhiteSpace(filePath))
            {
                const string root = "logs";
                Directory.CreateDirectory(root);
                filePath = Path.Combine(root, $"{DateTime.Now:yyyyMMdd}.txt");
            }

            _writer = new StreamWriter(filePath, true);
        }

        public void Dispose()
        {
            _writer?.Dispose();
            _writer = null;
        }


        private void WriteStatusBars()
        {
            //foreach (var bar in StatusBars)
            //{
            //    WriteLine(bar.Text);
            //}
        }

        private FileStreamTarget Action(Action callback)
        {
            lock (_lock)
            {
                callback();
                WriteStatusBars();
            }
            return this;
        }

        public IOutputTarget LocalTime(ConsoleColor? color = null) => Write($"{System.DateTime.Now:yyyy-MM-dd HH:mm:ss}");

        #region colors
        public IOutputTarget Color(string message, ConsoleColor? color = null) => Write(message);

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
        public IOutputTarget Write(char[] buffer, int index, int count, ConsoleColor? color = null) => Write(new string(buffer, index, count));
#nullable enable
        public IOutputTarget Write(char[]? buffer, ConsoleColor? color = null) => Write(new string(buffer));
        public IOutputTarget Write(object? value, ConsoleColor? color = null) => Write(value == null ? "<null>" : value.ToString());
#nullable restore
        public IOutputTarget Write(bool value, ConsoleColor? color = null) => Write(value.ToString());
        public IOutputTarget Write(decimal value, ConsoleColor? color = null) => Write(value.ToString());
        public IOutputTarget Write(char value, ConsoleColor? color = null) => Write(value.ToString());
        public IOutputTarget Write(long value, ConsoleColor? color = null) => Write(value.ToString());
        public IOutputTarget Write(double value, ConsoleColor? color = null) => Write(value.ToString());
        public IOutputTarget Write(float value, ConsoleColor? color = null) => Write(value.ToString());
#nullable enable
        public IOutputTarget Write(string? value, ConsoleColor? color = null) => Write(value == null ? "<null>" : value);
        public IOutputTarget Write(string format, object? arg0, ConsoleColor? color = null) => Write(String.Format(format, arg0));
        public IOutputTarget Write(string format, object? arg0, object? arg1, ConsoleColor? color = null) => Write(String.Format(format, arg0, arg1));
        public IOutputTarget Write(string format, object? arg0, object? arg1, object? arg2, ConsoleColor? color = null) => Write(String.Format(format, arg0, arg1, arg2));
        public IOutputTarget Write(string format, params object?[]? arg) => Write(String.Format(format, arg));
#nullable restore
        public IOutputTarget Write(uint value, ConsoleColor? color = null) => Write(value.ToString());
        public IOutputTarget Write(ulong value, ConsoleColor? color = null) => Write(value.ToString());
        public IOutputTarget Write(int value, ConsoleColor? color = null) => Write(value.ToString());
        public IOutputTarget WriteLine(uint value, ConsoleColor? color = null) => Action(() => WriteLine(value.ToString()));
        public IOutputTarget WriteLine() => Action(() => WriteLine());
        public IOutputTarget WriteLine(bool value, ConsoleColor? color = null) => Action(() => WriteLine(value.ToString()));
        public IOutputTarget WriteLine(ulong value, ConsoleColor? color = null) => Action(() => WriteLine(value.ToString()));
        public IOutputTarget WriteLine(decimal value, ConsoleColor? color = null) => Action(() => WriteLine(value.ToString()));
        public IOutputTarget WriteLine(double value, ConsoleColor? color = null) => Action(() => WriteLine(value.ToString()));
        public IOutputTarget WriteLine(int value, ConsoleColor? color = null) => Action(() => WriteLine(value.ToString()));
        public IOutputTarget WriteLine(long value, ConsoleColor? color = null) => Action(() => WriteLine(value.ToString()));
        public IOutputTarget WriteLine(float value, ConsoleColor? color = null) => Action(() => WriteLine(value.ToString()));
#nullable enable
        public IOutputTarget WriteLine(char[]? buffer, ConsoleColor? color = null) => Action(() => WriteLine(buffer == null ? "<null>" : new string(buffer)));
        public IOutputTarget WriteLine(object? value, ConsoleColor? color = null) => Action(() => WriteLine(value == null ? "<null>" : value.ToString()));
        public IOutputTarget WriteLine(string? value, ConsoleColor? color = null) => Action(() => WriteLine(value == null ? "<null>" : value));
        public IOutputTarget WriteLine(string format, object? arg0, ConsoleColor? color = null) => Action(() => WriteLine(String.Format(format, arg0)));
        public IOutputTarget WriteLine(string format, object? arg0, object? arg1, ConsoleColor? color = null) => Action(() => WriteLine(String.Format(format, arg0, arg1)));
        public IOutputTarget WriteLine(string format, object? arg0, object? arg1, object? arg2, ConsoleColor? color = null) => Action(() => WriteLine(String.Format(format, arg0, arg1, arg2)));
        public IOutputTarget WriteLine(string format, params object?[]? arg) => Action(() => WriteLine(String.Format(format, arg)));
#nullable restore
        public IOutputTarget WriteLine(char[] buffer, int index, int count, ConsoleColor? color = null) => Action(() => WriteLine(new string(buffer, index, count)));
        public IOutputTarget WriteLine(char value, ConsoleColor? color = null) => Action(() => WriteLine(value.ToString()));
        #endregion

        public IOutputTarget Gradient(string text, System.ConsoleColor[] gradient = null, GradientPattern pattern = GradientPattern.Letter) => Action(() => WriteLine(text));
    }
}
