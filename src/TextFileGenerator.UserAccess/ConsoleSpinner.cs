// TextFileGenerator
// Copyright (C) 2009-2011 Dust in the Wind
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Threading;

namespace DustInTheWind.TextFileGenerator.ConsoleApplication.Services
{
    internal class ConsoleSpinner : IDisposable
    {
        private bool isDisposed;
        private int counter;
        private readonly string[] sequence;
        private readonly Timer timer;

        public ConsoleSpinner()
        {
            //sequence = new[] { "/", "-", "\\", "|" };
            //sequence = new[] { ".", "o", "0", "o" };
            //sequence = new[] { "+", "x" };
            //sequence = new[] { "V", "<", "^", ">" };
            //sequence = new[] { ".   ", "..  ", "... ", "...." };
            sequence = new[] { "[.   ]", "[..  ]", "[... ]", "[....]", "[ ...]", "[  ..]", "[   .]", "[    ]" };

            timer = new Timer(HandleTimerElapsed);
        }

        public void Start()
        {
            if (isDisposed)
                throw new ObjectDisposedException(GetType().FullName);

            counter = 0;
            Console.CursorVisible = false;

            timer.Change(TimeSpan.Zero, TimeSpan.FromMilliseconds(500));
        }

        public void Stop()
        {
            if (isDisposed)
                throw new ObjectDisposedException(GetType().FullName);

            timer.Change(TimeSpan.FromTicks(-1), TimeSpan.FromTicks(-1));

            WriteAndGoBack(new string(' ', sequence[counter].Length));

            Console.CursorVisible = true;
        }

        private void HandleTimerElapsed(object o)
        {
            Turn();
        }

        private void Turn()
        {
            counter++;

            if (counter >= sequence.Length)
                counter = 0;

            WriteAndGoBack(sequence[counter]);
        }

        private static void WriteAndGoBack(string text)
        {
            int left = Console.CursorLeft;
            int top = Console.CursorTop;

            Console.Write(text);
            Console.SetCursorPosition(left, top);
        }

        public void Dispose()
        {
            if (isDisposed)
                return;

            timer.Dispose();

            isDisposed = true;
        }
    }
}