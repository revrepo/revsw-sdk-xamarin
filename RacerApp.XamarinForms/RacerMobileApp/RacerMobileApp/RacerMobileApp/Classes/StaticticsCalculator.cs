using RacerMobileApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RacerMobileApp.Classes
{
   public class StaticticsCalculator
    {
        public static string CalculateMinValue(List<TestResult> list)
        {
            return  (list.Any() ? list.Min(result => result.DurationMs) : 0).ToString();
        }

        public static string CalculateMaxValue(List<TestResult> list)
        {
            return (list.Any() ? list.Max(result => result.DurationMs) : 0).ToString();
        }

        public static string CalculateAverageValue(List<TestResult> list)
        {
            
            long sum = 0;

            foreach(var result in list)
            {
                sum += result.DurationMs;
            }

            return (sum / list.Count).ToString();
        }

        public static string CalculateMedianaValue(List<TestResult> list)
        {
            if (list.Count > 1)
            {
                List<TestResult> SortedList = list.OrderBy(o => o.DurationMs).ToList();

                var result = SortedList.Count % 2 == 0 ? SortedList[SortedList.Count / 2] : SortedList[(SortedList.Count - 1) / 2];

                return result.DurationMs.ToString();
            }
            else
            {
                return list[0].DurationMs.ToString();
            }
          
        }

        public static string CalculateStandardDeviation(List<TestResult> list)
        {
            var values = new List<long>();

            foreach(var res in list)
            {
                values.Add(res.DurationMs);
            }
            var x = values as IEnumerable<long>;

            double avg = x.Average();
            return Math.Round((Math.Sqrt(x.Average(v => Math.Pow(v - avg, 2)))),2).ToString();
        }

        public static string CalculateWeighteedAverage(List<TestResult> list)
        {
            var x = new List<long>();

            foreach (var res in list)
            {
                x.Add(res.DurationMs);
            }
            var values = x as IEnumerable<long>;

            double aggregate = 0;
            double weight;
            int item = 1;

            int count = values.Count();

            foreach (var d in values)
            {
                weight = ((double)item) / (double)count;
                aggregate += d * weight;
                count++;
            }

            return Math.Round(((double)aggregate / (double)count),2).ToString();
        }

    }
}
