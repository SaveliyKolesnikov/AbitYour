using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AbitYour.Models.ExtensionMethods;
using AngleSharp.Parser.Html;

namespace AbitYour.Models.Parsers
{
    public class VstupEdboParser : IStudentsListFetcher
    {
        public readonly string Url;

        public VstupEdboParser(string url)
        {
            Url = url;
        }

        public List<Student> GetStudents(string userName, double userScore, ref Student currentStudent)
        {
            var res = new List<Student>();

            var sourse = PhantomJsDriverProvider.GetPageContent(Url);
            var parser = new HtmlParser();
            var document = parser.Parse(sourse);
            const string rowSelector = "div#offer-requests-body > div.offer-request ";
            var rows = document.QuerySelectorAll(rowSelector);

            foreach (var row in rows)
            {
                const string classSample = "div.offer-request-{0} > span";
                var numberTag = row.QuerySelector(String.Format(classSample, "n"));
                var nameTag = row.QuerySelector(String.Format(classSample, "fio"));
                var priorityTag = row.QuerySelector(String.Format(classSample, "pr"));
                var scoreTag = row.QuerySelector(String.Format(classSample, "kb"));

                var number = int.Parse(numberTag.TextContent.Trim());

                var name = nameTag.TextContent.Trim()
                    .Replace("і", "i").Replace("І", "I")
                    .Replace("ї", "i").Replace("Ї", "I");

                if (!int.TryParse(priorityTag.TextContent.Trim(), out var priority))
                    priority = -1;

                var score = scoreTag.TextContent.Trim().ParseToDouble();

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
