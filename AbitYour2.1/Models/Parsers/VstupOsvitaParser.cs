using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AbitYour.Models.ExtensionMethods;
using AbitYour.Models.Parsers;
using AngleSharp;
using AngleSharp.Dom;

namespace AbitYour.Models.Parsers
{
    public class VstupOsvitaParser : IStudentsListFetcher
    {
        private readonly string _url;

        public VstupOsvitaParser(string url)
        {
            _url = url;
        }

        public async Task<List<Student>> GetStudentsAsync(string userName, double userScore)
        {
            var res = new List<Student>();

            var config = Configuration.Default.WithDefaultLoader();
            var document = await BrowsingContext.New(config).OpenAsync(_url);

            const string rowSelector = "table.rwd-table > tbody > tr.rstatus6";
            var rows = document.QuerySelectorAll(rowSelector);

            if (rows.Length == 0)
                throw new ArgumentException("Incorrect url.");

            foreach (var row in rows)
            {
                const string cellSelector = "td";
                var cells = row.QuerySelectorAll(cellSelector);

                var number = int.Parse(cells.First(cell => DataThAttributeComparer(cell, "№")).TextContent.Trim());

                var name = cells.First(cell => DataThAttributeComparer(cell, "ПІБ")).TextContent.Trim()
                    .Replace("і", "i").Replace("І", "I")
                    .Replace("ї", "i").Replace("Ї", "I");
                

                if (!int.TryParse(cells.First(cell => DataThAttributeComparer(cell, "П")).TextContent.Trim(), out var priority))
                    priority = 0;

                var score = cells.First(cell => DataThAttributeComparer(cell, "Бал")).TextContent.Trim().ParseToDouble();
                var student = new Student(number, name, priority, score);
                res.Add(student);
            }

            return res;

            bool DataThAttributeComparer(IElement cell, string value)
            {
                if (cell is null) return false;

                return cell.GetAttribute("data-th") == value;
            }
        }
    }
}
