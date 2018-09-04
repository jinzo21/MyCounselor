using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyCounselor.Requests
{
    public class Course
    {
        public int Id{ get; set; }
        public string CourseTitle { get; set; }
        public string DistrictId { get; set; }
        public string Abbreviation { get; set; }
        public string UCHoners { get; set; }
        public bool CTE { get; set; }
        public string GradeRecommendation { get; set; }
        public string AG { get; set; }
        public decimal Credits { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
    }
}