using DataParser.Requests;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataParser.Services
{
    public class CSVParserService
    {
        /// <summary>
        /// Gets class data from given CSV file path
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public List<CSVDistrictClass> GetClassData(string filePath)
        {
            string error = "";

            try
            {
                // Open & Read file path
                var reader = new StreamReader(File.OpenRead(filePath));
                List<CSVDistrictClass> classDataList = new List<CSVDistrictClass>();

                // Skip first line: Header row is not needed in data
                string headerRow = reader.ReadLine();

                // Continue reading until strean reader finished
                while (!reader.EndOfStream)
                {
                    string row = reader.ReadLine();
                    string[] parsedRow = row.Split('|', ',', ';');

                    error = parsedRow[0];

                    CSVDistrictClass districtClass = new CSVDistrictClass()
                    {
                        CourseTitle = parsedRow[0],
                        DistrictNumber = parsedRow[1],
                        Abbreviation = parsedRow[2],
                        UCHoners = parsedRow[3],
                        CTE = parsedRow[4],
                        GradeRecommendation = parsedRow[5],
                        AG = parsedRow[6],
                        Credits = Convert.ToDecimal(parsedRow[7])
                    };

                    // Push data into list
                    classDataList.Add(districtClass);
                }

                reader.Close();

                // Return list
                return classDataList;
            }
            catch (Exception ex)
            {
                Console.WriteLine(error);
                throw ex;
            }
        }

        /// <summary>
        /// Returns list of Course number with Suggested Grade to take
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public List<CourseGradeSuggestion> GetReccommendedYear(string filePath)
        {
            try
            {
                List<CourseGradeSuggestion> courseSuggestions = new List<CourseGradeSuggestion>();

                List<string[]> completeData = new List<string[]>();

                // Open & Read file path
                var reader = new StreamReader(File.OpenRead(filePath));

                // Continue reading until strean reader finished
                while (!reader.EndOfStream)
                {
                    string row = reader.ReadLine();
                    string[] parsedRow = row.Split((string[])null, StringSplitOptions.RemoveEmptyEntries);
                    completeData.Add(parsedRow);
                }

                reader.Close();

                for (int i = 0; i < completeData.Count; i++)
                {
                    if (completeData[i].Count() == 0)
                    {
                        continue;
                    }

                    if (completeData[i][0].Contains(","))
                    {
                        completeData[i][0] = completeData[i][0].Replace(",", string.Empty);
                    }

                    string quote = "\"";

                    if (completeData[i][0].Contains(quote))
                    {
                        completeData[i][0] = completeData[i][0].Replace(quote, string.Empty);
                    }

                    CourseGradeSuggestion courseSuggestion = new CourseGradeSuggestion
                    {
                        CourseNumber = completeData[i][0]
                    };

                    courseSuggestions.Add(courseSuggestion);

                    i = i + 5;
                }

                courseSuggestions.Count();


                int courseListCount = 0;
                for (int i = 0; i < completeData.Count; i++)
                {
                    if (completeData[i].Count() == 2)
                    {
                        i = i + 1;
                        continue;
                    }

                    if (completeData[i][6] == "+")
                    {
                        courseSuggestions[courseListCount].SuggestedGrades = completeData[i][9];
                        courseListCount++;
                        i = i + 5;
                        continue;
                    }

                    if (completeData[i][6] == "A-G")
                    {
                        courseSuggestions[courseListCount].SuggestedGrades = completeData[i][5];
                        courseListCount++;
                        i = i + 5;
                        continue;
                    }

                    if (completeData[i][6] == "2.5")
                    {
                        courseSuggestions[courseListCount].SuggestedGrades = completeData[i][10];
                        courseListCount++;
                        i = i + 5;
                        continue;
                    }

                    if (completeData[i][6] == "(5.0")
                    {
                        courseSuggestions[courseListCount].SuggestedGrades = completeData[i][10];
                        courseListCount++;
                        i = i + 5;
                        continue;
                    }

                    if (completeData[i][6] == "5.0)")
                    {
                        courseSuggestions[courseListCount].SuggestedGrades = completeData[i][8];
                        courseListCount++;
                        i = i + 5;
                        continue;
                    }

                    if (completeData[i][6] == "5.0")
                    {
                        courseSuggestions[courseListCount].SuggestedGrades = completeData[i][8];
                        courseListCount++;
                        i = i + 5;
                        continue;
                    }

                    if (completeData[i][6] == "Grade(s):")
                    {
                        courseSuggestions[courseListCount].SuggestedGrades = completeData[i][7];
                        courseListCount++;
                        i = i + 5;
                        continue;
                    }

                    if (completeData[i][6] == "Credits:")
                    {
                        courseSuggestions[courseListCount].SuggestedGrades = completeData[i][9];
                        courseListCount++;
                        i = i + 5;
                        continue;
                    }

                    if (completeData[i][6] == "0.0")
                    {
                        courseSuggestions[courseListCount].SuggestedGrades = completeData[i][8];
                        courseListCount++;
                        i = i + 5;
                        continue;
                    }

                    if (completeData[i][6] == "10.0")
                    {
                        courseSuggestions[courseListCount].SuggestedGrades = completeData[i][11];
                        courseListCount++;
                        i = i + 5;
                        continue;
                    }

                    courseSuggestions[courseListCount].SuggestedGrades = completeData[i][6];
                    courseListCount++;

                    i = i + 5;
                }

                // Return list
                return courseSuggestions;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// Prints out list of class data from Scrapper to given CSV file path
        /// </summary>
        /// <param name="lisOfClasses"></param>
        /// <param name="csvFilePath"></param>
        public void PrintClassData(List<CSVDistrictClass> lisOfClasses, string csvFilePath)
        {
            try
            {
                // Loop through each class data row 
                foreach (var classData in lisOfClasses)
                {
                    // Create string to print in CSV
                    StringBuilder classDataString = new StringBuilder();
                    classDataString.Append(classData.CourseTitle).Append(",")
                        .Append(classData.DistrictNumber).Append(",")
                        .Append(classData.Abbreviation).Append(",")
                        .Append(classData.UCHoners).Append(",")
                        .Append(classData.CTE).Append(",")
                        .Append(classData.GradeRecommendation).Append(",")
                        .Append(classData.AG).Append(",")
                        .Append(classData.Credits.ToString());

                    using (FileStream fileStream = new FileStream(csvFilePath, FileMode.Append, FileAccess.Write))
                    {
                        using (StreamWriter writer = new StreamWriter(fileStream))
                        {
                            writer.WriteLine(classDataString.ToString());
                        }
                    }

                    Console.WriteLine(classDataString.ToString());
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<CSVDistrictClass> FormatClassData(List<CSVDistrictClass> listOfClasses)
        {
            string error = "";

            try
            {
                List<CSVDistrictClass> listOfFormattedClasses = new List<CSVDistrictClass>();

                foreach (var originalClass in listOfClasses)
                //foreach (var originalClass in listOfClasses.Where(x => x.CourseTitle.First().ToString() != "H"))
                {
                    error = originalClass.CourseTitle;

                    // Verify if course is one semester or two
                    if (originalClass.DistrictNumber.IndexOf("/") != -1)
                    {
                        CSVDistrictClass classA = new CSVDistrictClass();
                        CSVDistrictClass classB = new CSVDistrictClass();

                        // Verify if not an Honors Course
                        if (originalClass.CourseTitle.First().ToString() != "H")
                        {
                            // District Number
                            int indexOfSlashMark = originalClass.DistrictNumber.IndexOf("/");
                            classA.DistrictNumber = originalClass.DistrictNumber.Remove(indexOfSlashMark);

                            var classBLastDigit = Convert.ToInt32(classA.DistrictNumber) + 1;
                            classB.DistrictNumber = classBLastDigit.ToString();
                        }
                        else
                        {
                            // District Number
                            int indexOfSlashMark = originalClass.DistrictNumber.IndexOf("/");
                            int districtNumber = Convert.ToInt32(originalClass.DistrictNumber.Remove(indexOfSlashMark));

                            classA.DistrictNumber = districtNumber.ToString() + "H";
                            classB.DistrictNumber = (districtNumber + 1).ToString() + "H";
                        }

                        // For data sets containing "AB"
                        if (originalClass.CourseTitle.Contains("AB"))
                        {
                            // Course Title
                            classA.CourseTitle = originalClass.CourseTitle.Replace("AB", "A");
                            classB.CourseTitle = originalClass.CourseTitle.Replace("AB", "B");

                            // Abbreviation
                            classA.Abbreviation = originalClass.Abbreviation.Replace("AB", "A");
                            classB.Abbreviation = originalClass.Abbreviation.Replace("AB", "B");
                        }
                        // For data sets containing "2B"
                        else if (originalClass.CourseTitle.Contains("2AB"))
                        {
                            classA.CourseTitle = originalClass.CourseTitle.Replace("AB", "2A");
                            classB.CourseTitle = originalClass.CourseTitle.Replace("AB", "2B");

                            classA.Abbreviation = originalClass.Abbreviation.Replace("AB", "2A");
                            classB.Abbreviation = originalClass.Abbreviation.Replace("AB", "2B");
                        }
                        // For data sets containing "3AB"
                        else if (originalClass.CourseTitle.Contains("3AB"))
                        {
                            classA.CourseTitle = originalClass.CourseTitle.Replace("AB", "3A");
                            classB.CourseTitle = originalClass.CourseTitle.Replace("AB", "3B");

                            classA.Abbreviation = originalClass.Abbreviation.Replace("AB", "3A");
                            classB.Abbreviation = originalClass.Abbreviation.Replace("AB", "3B");
                        }
                        else
                        {
                            classA.CourseTitle = originalClass.CourseTitle;
                            classB.CourseTitle = originalClass.CourseTitle;

                            classA.Abbreviation = originalClass.Abbreviation.Replace("AB", "A");
                            classB.Abbreviation = originalClass.Abbreviation.Replace("AB", "B");
                        }

                        // UC Status
                        if (originalClass.UCHoners == "AP" || originalClass.UCHoners == "Honors" || originalClass.UCHoners == "IB")
                        {
                            classA.UCHoners = originalClass.UCHoners;
                            classB.UCHoners = originalClass.UCHoners;
                        }
                        else
                        {
                            classA.UCHoners = "NULL";
                            classB.UCHoners = "NULL";
                        }

                        // CTE
                        if (originalClass.CTE == "X")
                        {
                            classA.CTE = "TRUE";
                            classB.CTE = "TRUE";
                        }
                        else
                        {
                            classA.CTE = "FALSE";
                            classB.CTE = "FALSE";
                        }


                        classA.GradeRecommendation = originalClass.GradeRecommendation;
                        classB.GradeRecommendation = originalClass.GradeRecommendation;

                        // A-G
                        classA.AG = originalClass.AG;
                        classB.AG = originalClass.AG;

                        // Credits
                        classA.Credits = originalClass.Credits;
                        classB.Credits = originalClass.Credits;

                        listOfFormattedClasses.Add(classA);
                        listOfFormattedClasses.Add(classB);
                    }
                    // Single Course
                    else
                    {
                        CSVDistrictClass singleSemesterClass = new CSVDistrictClass();

                        singleSemesterClass.CourseTitle = originalClass.CourseTitle;
                        singleSemesterClass.DistrictNumber = originalClass.DistrictNumber;
                        singleSemesterClass.Abbreviation = originalClass.Abbreviation;
                        singleSemesterClass.GradeRecommendation = originalClass.GradeRecommendation;
                        singleSemesterClass.AG = originalClass.AG;
                        singleSemesterClass.Credits = originalClass.Credits;

                        // UC Status
                        if (originalClass.UCHoners == "AP" || originalClass.UCHoners == "Honors" || originalClass.UCHoners == "IB")
                        {
                            singleSemesterClass.UCHoners = originalClass.UCHoners;
                        }
                        else
                        {
                            singleSemesterClass.UCHoners = "NULL";
                        }

                        // CTE
                        if (originalClass.CTE == "X")
                        {
                            singleSemesterClass.CTE = "TRUE";
                        }
                        else
                        {
                            singleSemesterClass.CTE = "FALSE";
                        }


                        listOfFormattedClasses.Add(singleSemesterClass);
                    }
                }

                return listOfFormattedClasses;
            }
            catch (Exception)
            {
                Console.WriteLine(error);
                throw;
            }
            
        }

        public List<CSVDistrictClass> UpdateGradeSuggestion(List<CSVDistrictClass> currentFileData)
        {
            string reccommendedYearFilePath = @"C:\Users\31412\Desktop\LAUSD\LAUSDCourseCatelogNotFormatted.txt";

            List<CourseGradeSuggestion> courseSuggestions = GetReccommendedYear(reccommendedYearFilePath);
            List<CSVDistrictClass> updatedCourseValues = new List<CSVDistrictClass>();

            foreach (var course in currentFileData)
            {
                foreach (var suggestion in courseSuggestions)
                {
                    if (course.DistrictNumber == suggestion.CourseNumber)
                    {
                        course.GradeRecommendation = suggestion.SuggestedGrades;
                        updatedCourseValues.Add(course);
                    }

                }
            }

            foreach (var course in updatedCourseValues)
            {
                Console.WriteLine(course.DistrictNumber);
                Console.WriteLine(course.GradeRecommendation);
            }

            return updatedCourseValues;

        }
    }
}
