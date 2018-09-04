using DataParser.Requests;
using DataParser.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataParser
{
    class Program
    {
        static void Main(string[] args)
        {
            string collegePrepElecCSVFilePath = @"C:\Users\31412\Desktop\LAUSD\LAUSDCollegePrepElecClasses18.txt";
            string finishedCollegePrepElecCSVFilePath = @"C:\Users\31412\Desktop\LAUSD\FinishedLAUSDCollegePrepElecClasses18.txt";

            string englishCSVFilePath = @"C:\Users\31412\Desktop\LAUSD\LAUSDEnglishClasses18.txt";
            string finishedEnglishCSVFilePath = @"C:\Users\31412\Desktop\LAUSD\FinishedLAUSDEnglishClasses18.txt";

            string foreignLangCSVFilePath = @"C:\Users\31412\Desktop\LAUSD\LAUSDForeignLangClasses18.txt";
            string finishedForeignLangCSVFilePath = @"C:\Users\31412\Desktop\LAUSD\FinishedLAUSDForeignLangClasses18.txt";

            string labScienceCSVFilePath = @"C:\Users\31412\Desktop\LAUSD\LAUSDLabScienceClasses18.txt";
            string finishedLabScienceCSVFilePath = @"C:\Users\31412\Desktop\LAUSD\FinishedLAUSDLabScienceClasses18.txt";

            string historyCSVFilePath = @"C:\Users\31412\Desktop\LAUSD\LAUSDHistorySocialScienceClasses18.txt";
            string finishedHistoryCSVFilePath = @"C:\Users\31412\Desktop\LAUSD\FinishedLAUSDHistorySocialScienceClasses18.txt";

            string mathCSVFilePath = @"C:\Users\31412\Desktop\LAUSD\LAUSDMathClasses18.txt";
            string finishedMathCSVFilePath = @"C:\Users\31412\Desktop\LAUSD\FinishedLAUSDMathClasses18.txt";

            string visualPerfArtsCSVFilePath = @"C:\Users\31412\Desktop\LAUSD\LAUSDVisualPerfArtsClasses18.txt";
            string finishedVisualPerfArtsCSVFilePath = @"C:\Users\31412\Desktop\LAUSD\FinishedLAUSDVisualPerfArtsClasses18.txt";

            string lausdCoursesFile = @"C:\Users\31412\Desktop\LAUSD\LAUSDCourseCatelogNotFormatted.txt";

            WebScraper webScrapper = new WebScraper();
            CSVParserService csvParserVice = new CSVParserService();

            List<CSVDistrictClass> listOfFormattedClasses = csvParserVice.GetClassData(historyCSVFilePath);
            List<CSVDistrictClass> updatedListOfFormattedClasses = csvParserVice.UpdateGradeSuggestion(listOfFormattedClasses);
            csvParserVice.PrintClassData(updatedListOfFormattedClasses, finishedHistoryCSVFilePath);

            Console.ReadLine();

        }
    }
}
