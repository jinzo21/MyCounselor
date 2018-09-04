using MyCounselor.Requests;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MyCounselor.Services
{
    public class CourseService
    {
       public List<Course> GetAll()
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["MyCounselorDB"].ConnectionString))
            {
                con.Open();

                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "Select * from lausd_courses_18_19";

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    var courses = new List<Course>();

                    while (reader.Read())
                    {
                        courses.Add(new Course
                        {
                            Id = (int)reader["id"],
                            AG = (string)reader["ag_id"],
                            CourseTitle = (string)reader["course_title"],
                            Abbreviation = (string)reader["abbreviation"],
                            DistrictId = (string)reader["district_id"],
                            UCHoners = (string)reader["uc_honors_designation"],
                            CTE = (bool)reader["cte_designation"],
                            GradeRecommendation = (string)reader["grade_recommendation"],
                            Credits = (decimal)reader["credits"],
                            DateCreated = (DateTime)reader["date_entered"],
                            DateUpdated = (DateTime)reader["date_modified"]
                        });
                    }

                    return courses;
                }
            }
        }
    }
}