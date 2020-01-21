using NPage.Struct;
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
        public static async Task PaginationAsync<T>(IEnumerable<T> data, Action<T> template, CancellationToken cancellationToken, int pageSize = 5, int interval = 300, Action<System.ConsoleKey> additionalActionListener = null)
        {
            if (!data.Any()) return;
            if (template == null)
            {
                template = (d) => Console.WriteLine(d);
            }
            var availablePages = (int)Math.Ceiling((double)data.Count() / pageSize);
            var currentPage = 0;
            Task.Run(ActionListenerThread);
            while (!cancellationToken.IsCancellationRequested)
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
        public static void Pagination<T>(IEnumerable<T> data, Action<T> template, CancellationToken cancellationToken, int pageSize = 5, int interval = 300, Action<System.ConsoleKey> additionalActionListener = null)
        {
            if (!data.Any()) return;
            if (template == null)
            {
                template = (d) => Console.WriteLine(d);
            }
            var availablePages = (int)Math.Ceiling((double)data.Count() / pageSize);
            var currentPage = 0;
            Task.Run(ActionListenerThread);
            while (!cancellationToken.IsCancellationRequested)
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
        /// <summary>
        /// Display custom tab.
        /// </summary>
        /// <param name="switchableTab"></param>
        /// <param name="cancellationToken"></param>
        /// <param name="interval"></param>
        /// <param name="additionalActionListener"></param>
        public static void DisplayTab(SwitchableTab switchableTab, CancellationToken cancellationToken, int interval = 300, Action<System.ConsoleKey> additionalActionListener = null)
        {
            Task.Run(ActionListenerThread);
            var linebreak = "--------------";
            while (!cancellationToken.IsCancellationRequested)
            {
                var tab = switchableTab.Current();
                Console.Clear();
                Console.WriteLine();
                Console.SetCursorPosition((Console.WindowWidth - tab.Header.Length) / 2, Console.CursorTop);
                Console.WriteLine(tab.Header);
                Console.SetCursorPosition((Console.WindowWidth - linebreak.Length) / 2, Console.CursorTop);
                Console.WriteLine(linebreak);
                tab.Body?.Invoke();
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
                            switchableTab.Previous();
                            break;
                        case ConsoleKey.RightArrow:
                            switchableTab.Next();
                            break;
                        case ConsoleKey.Home:
                            switchableTab.GoToFirst();
                            break;
                        case ConsoleKey.End:
                            switchableTab.GoToLast();
                            break;
                        default:
                            additionalActionListener?.Invoke(action);
                            break;
                    }
                }
            }

        }
        /// <summary>
        /// Display custom tab in an asynchronous manner.
        /// </summary>
        /// <param name="switchableTab"></param>
        /// <param name="cancellationToken"></param>
        /// <param name="interval"></param>
        /// <param name="additionalActionListener"></param>
        /// <returns></returns>
        public static async Task DisplayTabAsync(SwitchableTab switchableTab, CancellationToken cancellationToken, int interval = 300, Action<System.ConsoleKey> additionalActionListener = null)
        {
            Task.Run(ActionListenerThread);
            var linebreak = "--------------";
            while (!cancellationToken.IsCancellationRequested)
            {
                var tab = switchableTab.Current();
                Console.Clear();
                Console.WriteLine();
                Console.SetCursorPosition((Console.WindowWidth - tab.Header.Length) / 2, Console.CursorTop);
                Console.WriteLine(tab.Header);
                Console.SetCursorPosition((Console.WindowWidth - linebreak.Length) / 2, Console.CursorTop);
                Console.WriteLine(linebreak);
                tab.Body?.Invoke();
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
                            switchableTab.Previous();
                            break;
                        case ConsoleKey.RightArrow:
                            switchableTab.Next();
                            break;
                        case ConsoleKey.Home:
                            switchableTab.GoToFirst();
                            break;
                        case ConsoleKey.End:
                            switchableTab.GoToLast();
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
