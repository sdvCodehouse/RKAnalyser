using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using PagedList;
using RunkeeperAnalyser.Domain;

namespace RunkeeperAnalyser.ViewModels
{
    public class IndexViewModel
    {
        public IndexViewModel(){}

        public IndexViewModel(NameValueCollection queryString)
        {
            if (queryString != null)
            {
                DistanceFrom = GetNullableInt(queryString["DistanceFrom"]);
                DistanceTo = GetNullableInt(queryString["DistanceTo"]);

                SortTerm = queryString["sortTerm"] ?? "DateDesc";

                DateFrom = GetNullableDateTime(queryString["DateFrom"]);
                DateTo = GetNullableDateTime(queryString["DateTo"]);
            }
        }

        public string SortTerm { get; set; }

        public IPagedList<ExerciseSession> ExerciseSessions { get; set; }

        [DisplayName("From")]
        public int? DistanceFrom { get; set; }

        [DisplayName("To")]
        public int? DistanceTo { get; set; }

        [DisplayName("From")]
        public DateTime? DateFrom { get; set; }

        [DisplayName("To")]
        public DateTime? DateTo { get; set; }

        public int? GetNullableInt(string value)
        {
            int result;
            if (!string.IsNullOrWhiteSpace(value) && int.TryParse(value, out result))
                return result;

            return null;
        }

        public DateTime? GetNullableDateTime(string value)
        {
            DateTime result;
            if (!string.IsNullOrWhiteSpace(value) && DateTime.TryParse(value, out result))
                return result;

            return null;
        }
    }
}