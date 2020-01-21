using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NPage
{
    public static partial class NConsole
    {
        /// <summary>
        /// Create console pagination in an asynchronous manner.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="template"></param>
        /// <param name="pageSize"></param>
        /// <param name="interval"></param>
        /// <param name="additionalActionListener"></param>
        /// <returns></returns>
        public static async Task PaginationAsync<T>(IEnumerable<T> data, Action<T> template, int pageSize = 5, int interval = 300, Action<System.ConsoleKey> additionalActionListener = null)
        {
            if (!data.Any()) return;
            if (template == null)
            {
                template = (d) => Console.WriteLine(d);
            }
            var availablePages = (int)Math.Ceiling((double)data.Count() / pageSize);
            var currentPage = 0;
            Task.Run(ActionListenerThread);
            while (true)
            {
                Console.Clear();
                Console.WriteLine();
                var pageText = $"< Page {currentPage + 1} / {availablePages} >";
                Console.SetCursorPosition((Console.WindowWidth - pageText.Length) / 2, Console.CursorTop);
                Console.WriteLine(pageText);
                foreach (var element in data.Skip(currentPage * pageSize).Take(pageSize))
                {
                    template(element);
                }
                await Task.Delay(interval);
            }
            void ActionListenerThread()
            {
                while (true)
                {
                    var action = Console.ReadKey(true).Key;
                    switch (action)
                    {
                        case ConsoleKey.LeftArrow:
                            if (currentPage > 0)
                                currentPage--;
                            break;
                        case ConsoleKey.RightArrow:
                            if (currentPage < availablePages - 1)
                                currentPage++;
                            break;
                        case ConsoleKey.Home:
                            currentPage = 0;
                            break;
                        case ConsoleKey.End:
                            currentPage = availablePages - 1;
                            break;
                        default:
                            additionalActionListener?.Invoke(action);
                            break;
                    }
                }
            }
        }
        /// <summary>
        /// Create console pagination.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="template"></param>
        /// <param name="pageSize"></param>
        /// <param name="interval"></param>
        /// <param name="additionalActionListener"></param>
        public static void Pagination<T>(IEnumerable<T> data, Action<T> template, int pageSize = 5, int interval = 300, Action<System.ConsoleKey> additionalActionListener = null)
        {
            if (!data.Any()) return;
            if (template == null)
            {
                template = (d) => Console.WriteLine(d);
            }
            var availablePages = (int)Math.Ceiling((double)data.Count() / pageSize);
            var currentPage = 0;
            Task.Run(ActionListenerThread);
            while (true)
            {
                Console.Clear();
                Console.WriteLine();
                var pageText = $"< Page {currentPage + 1} / {availablePages} >";
                Console.SetCursorPosition((Console.WindowWidth - pageText.Length) / 2, Console.CursorTop);
                Console.WriteLine(pageText);
                foreach (var element in data.Skip(currentPage * pageSize).Take(pageSize))
                {
                    template(element);
                }
                Thread.Sleep(interval);
            }
            void ActionListenerThread()
            {
                while (true)
                {
                    var action = Console.ReadKey(true).Key;
                    switch (action)
                    {
                        case ConsoleKey.LeftArrow:
                            if (currentPage > 0)
                                currentPage--;
                            break;
                        case ConsoleKey.RightArrow:
                            if (currentPage < availablePages - 1)
                                currentPage++;
                            break;
                        case ConsoleKey.Home:
                            currentPage = 0;
                            break;
                        case ConsoleKey.End:
                            currentPage = availablePages - 1;
                            break;
                        default:
                            additionalActionListener?.Invoke(action);
                            break;
                    }
                }
            }
        }
    }
}
