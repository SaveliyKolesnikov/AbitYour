using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using AbitYour.Models.ExtensionMethods;
using AngleSharp;

namespace AbitYour.Models.Parsers
{
    public class VstupInfoParser : IStudentsListFetcher
    {
        public readonly string Url;

        public VstupInfoParser(string url)
        {
            Url = url;
        }

        public List<Student> GetStudents(string userName, double userScore, ref Student currentStudent)
        {
            var res = new List<Student>();

            var config = Configuration.Default.WithDefaultLoader();
            var document = BrowsingContext.New(config).OpenAsync(Url).GetAwaiter().GetResult();

            const string rowSelector = "div.row table.tablesaw-sortable tbody tr ";
            var rows = document.QuerySelectorAll(rowSelector);

            // Checker on status column.
            var checkOnLetter = new Regex("[а-яіiїє]+", RegexOptions.IgnoreCase);
            foreach (var row in rows)
            {
                const string cellSelector = "td";
                var cells = row.QuerySelectorAll(cellSelector).ToArray();
                var currentIndex = 0;
                if (cells.Length < 5)
                    continue;

                var number = int.Parse(cells[currentIndex++].TextContent.Trim());

                var name = cells[currentIndex++].TextContent.Trim()
                    .Replace("і", "i").Replace("І", "I")
                    .Replace("ї", "i").Replace("Ї", "I");

                // Проверка следующего пользователя на контарктный приоритет.
                if (cells[currentIndex].TextContent.Trim().Equals("к", StringComparison.InvariantCultureIgnoreCase))
                    continue;

                if (checkOnLetter.IsMatch(cells[currentIndex].TextContent.Trim()))
                    currentIndex++;

                // Проверка следующего пользователя на контарктный приоритет.
                if (cells[currentIndex].TextContent.Trim().Equals("к", StringComparison.InvariantCultureIgnoreCase))
                    continue;

                if (!int.TryParse(cells[currentIndex++].TextContent.Trim(), out var priority))
                    priority = 0;

                if (checkOnLetter.IsMatch(cells[currentIndex].TextContent.Trim()))
                    currentIndex++;

                var score = cells[currentIndex].TextContent.Trim().ParseToDouble();
                var student = new Student(number, name, priority, score);
                res.Add(student);

                if (currentStudent is null &&
                    student.Name.StartsWith(userName, StringComparison.CurrentCultureIgnoreCase) &&
                    Math.Abs(student.Score - userScore) < 0.001)
                {
                    currentStudent = student;
                }
            }

            return res;
        }
    }
}
