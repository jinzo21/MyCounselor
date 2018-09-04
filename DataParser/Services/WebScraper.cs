using AngleSharp.Parser.Html;
using DataParser.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DataParser.Services
{
    public class WebScraper
    {

        public List<CSVDistrictClass> ScrapeCollegePrepElec()
        {
            string websitePath = @"https://achieve.lausd.net/Page/8043";

            using (var client = new HttpClient())
            {
                // Download the HTML from given path
                var html = client.GetStringAsync(websitePath).Result;

                // Parse returned HTML
                var parser = new HtmlParser();

                // Select DIV containing class data
                var classDataDiv = parser.Parse(html).QuerySelectorAll("div.ui-article-description")[1];

                // Select Rows inside DIV
                var classDataRow = classDataDiv.QuerySelectorAll("tr");

                List<CSVDistrictClass> listOfClassData = new List<CSVDistrictClass>();

                foreach (var element in classDataRow.Skip(1))
                {
                    CSVDistrictClass classData = new CSVDistrictClass();
                    classData.CourseTitle = element.QuerySelectorAll("td")[0].TextContent;
                    classData.DistrictNumber = element.QuerySelectorAll("td")[1].TextContent;
                    classData.Abbreviation = element.QuerySelectorAll("td")[2].TextContent;
                    classData.UCHoners = element.QuerySelectorAll("td")[3].TextContent;
                    classData.CTE = element.QuerySelectorAll("td")[4].TextContent;
                    classData.Credits = 5;
                    classData.AG = "G";

                    listOfClassData.Add(classData);
                }

                return listOfClassData;
            }
        }

        public List<CSVDistrictClass> ScrapeELAPage()
        {
            string websitePath = @"https://achieve.lausd.net/Page/8041";

            using (var client = new HttpClient())
            {
                // Download the HTML from given path
                var html = client.GetStringAsync(websitePath).Result;

                // Parse returned HTML
                var parser = new HtmlParser();

                // Select DIV containing class data
                var classDataDiv = parser.Parse(html).QuerySelectorAll("div.ui-article-description")[1];

                // Select Rows inside DIV
                var classDataRow = classDataDiv.QuerySelectorAll("tr");

                List<CSVDistrictClass> listOfClassData = new List<CSVDistrictClass>();

                foreach (var element in classDataRow.Skip(1))
                {
                    CSVDistrictClass classData = new CSVDistrictClass();
                    classData.CourseTitle = element.QuerySelectorAll("td")[0].TextContent;
                    classData.DistrictNumber = element.QuerySelectorAll("td")[1].TextContent;
                    classData.Abbreviation = element.QuerySelectorAll("td")[2].TextContent;
                    classData.UCHoners = element.QuerySelectorAll("td")[4].TextContent;
                    classData.CTE = element.QuerySelectorAll("td")[5].TextContent;
                    classData.Credits = 5;
                    classData.AG = "E";

                    listOfClassData.Add(classData);
                }

                return listOfClassData;
            }
        }

        public List<CSVDistrictClass> ScrapeVisualArtsPage()
        {
            string websitePath = @"https://achieve.lausd.net/Page/8042";

            using (var client = new HttpClient())
            {
                // Download the HTML from given path
                var html = client.GetStringAsync(websitePath).Result;

                // Parse returned HTML
                var parser = new HtmlParser();

                // Select DIV containing class data
                var classDataDiv = parser.Parse(html).QuerySelectorAll("div.ui-article-description")[1];

                // Select Rows inside DIV
                var classDataRow = classDataDiv.QuerySelectorAll("tr");

                List<CSVDistrictClass> listOfClassData = new List<CSVDistrictClass>();

                foreach (var element in classDataRow.Skip(1))
                {
                    CSVDistrictClass classData = new CSVDistrictClass();
                    classData.CourseTitle = element.QuerySelectorAll("td")[0].TextContent;
                    classData.DistrictNumber = element.QuerySelectorAll("td")[1].TextContent;
                    classData.Abbreviation = element.QuerySelectorAll("td")[2].TextContent;
                    classData.UCHoners = element.QuerySelectorAll("td")[3].TextContent;
                    classData.CTE = element.QuerySelectorAll("td")[4].TextContent;
                    classData.Credits = 5;
                    classData.AG = "E";

                    listOfClassData.Add(classData);
                }

                return listOfClassData;
            }
        }
    }
}

