using AngleSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AbitYour.Models.ExtensionMethods;
using AbitYour.Models.NewDayTimer;
using AbitYour.Models.Parsers;
using AngleSharp.Parser.Html;
using OpenQA.Selenium.PhantomJS;


namespace AbitYour.Models
{
    public class AbitYour : IAbitYourParser
    {
        enum ParsedSite
        {
            VstupInfo,
            VstupOsvita,
            Edbo
        }

        public AbitYour()
        {
            INewDayEvent newDay = NewDayListener.Instance;
            newDay.OnNewDay += _cachedResult.Clear;
        }

        private readonly Dictionary<string, IEnumerable<Student>> _cachedResult = new Dictionary<string, IEnumerable<Student>>();
        private static readonly IReadOnlyDictionary<string, ParsedSite> _maintaincedSites = new Dictionary<string, ParsedSite>
        {
            ["vstup.info"] = ParsedSite.VstupInfo,
            ["vstup.osvita.ua"] = ParsedSite.VstupOsvita
        };

        public async Task<IResultList> ParseAsync(string url, string userName, string userScore)
        {
            url = url.Trim();
            userName = userName.Trim()
                .Replace("і","i").Replace("І","I")
                .Replace("ї","i").Replace("Ї","I");
            var score = userScore.Trim().ParseToDouble();

            #region Validation

            var invalidUrl = CreateInvalidUrlMessage();
            var parsedSite = DefineParsedSite(url) ?? throw new ArgumentException(invalidUrl);



            if (string.IsNullOrEmpty(userName) || 
                Regex.IsMatch(userName, "[0-9]") || 
                userName.Split(new[] {' ', '\n'}).Length != 1)
                throw new ArgumentException("Некорректная фамилия.");
            
            if (!Regex.IsMatch(userScore, @"^\d{3}([.,]\d{1,3})?$") || score.Equals(double.NaN))
                throw new ArgumentException("Некорректный балл");

            #endregion
            
            List<Student> studentsList;
            Student currentStudent = null;
            
            if (_cachedResult.ContainsKey(url))
            {
                studentsList = _cachedResult[url].ToList();
                currentStudent = studentsList.Find(student =>
                    Math.Abs(student.Score - score) < 0.0001 &&
                    student.Name.StartsWith(userName, StringComparison.CurrentCultureIgnoreCase));
            }
            else
            {
                IStudentsListFetcher parser;

                switch (parsedSite)
                {
                    case ParsedSite.VstupInfo:
                        parser = new VstupInfoParser(url);
                        break;
                    case ParsedSite.VstupOsvita:
                        parser = new VstupOsvitaParser(url);
                        break;
                    case ParsedSite.Edbo:
                        parser = new VstupEdboParser(url);
                        break;
                    default:
                        throw new ArgumentException(invalidUrl);
                }
                
                try
                {
                    studentsList = await parser.GetStudentsAsync(userName, score) ?? throw new ArgumentException(invalidUrl);
                    currentStudent = studentsList.Find(student =>
                        student.Name.StartsWith(userName, StringComparison.CurrentCultureIgnoreCase) &&
                        Math.Abs(student.Score - score) < 0.001);
                    _cachedResult[url] = studentsList;
                }
                catch (ArgumentException)
                {
                    throw new ArgumentException(invalidUrl);
                }
            }


            if (currentStudent is null)
            {
                throw new ArgumentException("Ошибка! Абитуриент с такими данными не найден.");
            }

            var resultList = new ResultList(studentsList.ToList(), currentStudent);

            return resultList;
        }

        private string CreateInvalidUrlMessage()
        {
            var invalidUrl = new StringBuilder();

            invalidUrl.Append("Ссылка должна указывать на страницу со списком вашей специализации с сайтов: ");

            foreach (var maintaincedSite in _maintaincedSites.Keys)
            {
                invalidUrl.Append(maintaincedSite + ", ");
            }

            invalidUrl.Remove(invalidUrl.Length - 2, 2);
            invalidUrl.Append(".");

            return invalidUrl.ToString();
        }

        private ParsedSite? DefineParsedSite(string url)
        {
            foreach (var maintaincedSitesUrl in _maintaincedSites.Keys)
            {
                if (url.Contains(maintaincedSitesUrl))
                    return _maintaincedSites[maintaincedSitesUrl];
            }

            return null;
        }


    }
}