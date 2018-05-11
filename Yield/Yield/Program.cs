using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Yield
{
    class Program
    {
        private static int _timesCalled;
        private static Stopwatch _classicStopwatch;

        private static int _yieldTimesCalled;
        private static Stopwatch _yieldStopwatch;

        private static int _linqTimesCalled;
        private static Stopwatch _linqStopwatch;

        static void Main(string[] args)
        {
            var range = Enumerable.Range(1, 100).ToList();

            _classicStopwatch = new Stopwatch();
            _yieldStopwatch = new Stopwatch();
            _linqStopwatch = new Stopwatch();

            TheClassicWay(range, true);
            TheYieldWay(range, true);
            TheLinqWay(range, true);

            Console.WriteLine();
            Console.WriteLine($"Classic: ForEach was called {_timesCalled} during {_classicStopwatch.Elapsed} milliseconds");
            Console.WriteLine($"Yield: ForEach was called {_yieldTimesCalled} during {_yieldStopwatch.Elapsed} milliseconds");
            Console.WriteLine($"Linq: bForEach was called {_linqTimesCalled} during {_linqStopwatch.Elapsed} milliseconds");

            Console.ReadKey();
        }

        static void TheClassicWay(IEnumerable<int> range, bool showResults = false)
        {
            _timesCalled = 0;
            _classicStopwatch.Start();

            var filteredRange = new List<int>();
            foreach (var i in range)
            {
                _timesCalled += 1;
                if (IsFiz(i)) filteredRange.Add(i);
            }

            foreach (var i in filteredRange)
            {
                _timesCalled += 1;
                if (IsBuzz(i) && showResults) Console.WriteLine(i);
            }

            _classicStopwatch.Stop();
            Console.WriteLine();
        }

        static void TheYieldWay(IEnumerable<int> range, bool showResults = false)
        {
            _yieldTimesCalled = 0;
            
            _yieldStopwatch.Start();
            foreach (var i in GetBuzz(GetFiz(range)))
            {
                _yieldTimesCalled += 1;
                if (showResults)
                    Console.WriteLine($"fizbuzz {i}");
            }

            _yieldStopwatch.Stop();
            Console.WriteLine();
        }

        static void TheLinqWay(IEnumerable<int> range, bool showResults = false)
        {
            _linqTimesCalled = 0;
            _linqStopwatch.Start();

            foreach (var i in range.Where(i => IsFiz(i) && IsBuzz(i)))
            {
                _linqTimesCalled += 1;
                if (showResults)
                    Console.WriteLine($"fizbuzz {i}");
            }

            _linqStopwatch.Stop();
            Console.WriteLine();
        }

        static IEnumerable<int> GetFiz(IEnumerable<int> range)
        {
            foreach (var i in range)
            {
                if (IsFiz(i)) yield return i;
            }
        }

        static IEnumerable<int> GetBuzz(IEnumerable<int> range)
        {
            foreach (var i in range)
            {
                if (IsBuzz(i)) yield return i;
            }
        }

        static bool IsFiz(int i)
        {
            return i % 3 == 0;
        }

        static bool IsBuzz(int i)
        {
            return i % 5 == 0;
        }
    }
}
