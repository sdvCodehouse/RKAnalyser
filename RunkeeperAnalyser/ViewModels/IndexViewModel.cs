using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using PagedList;
using RunkeeperAnalyser.Domain;
using RunkeeperAnalyser.Domain.Extensions;

namespace RunkeeperAnalyser.ViewModels
{
    public class IndexViewModel
    {
        private string _sortTerm;
        private IEnumerable<string> _activities;

        public IndexViewModel() {}

        public IndexViewModel(NameValueCollection queryString)
        {
            if (queryString != null)
            {
                DistanceFrom = queryString["DistanceFrom"].ToNullableInt();
                DistanceTo = queryString["DistanceTo"].ToNullableInt();

                SortTerm = queryString["sortTerm"];

                DateFrom = queryString["DateFrom"].ToNullableDateTime();
                DateTo = queryString["DateTo"].ToNullableDateTime();

                if (!string.IsNullOrWhiteSpace(queryString["activities"]))
                {
                    _activities = queryString["activities"].Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                }

            }
        }

        
        public IPagedList<ExerciseSession> ExerciseSessions { get; set; }

        [DisplayName("From")]
        public int? DistanceFrom { get; set; }

        [DisplayName("To")]
        public int? DistanceTo { get; set; }

        [DisplayName("From")]
        public DateTime? DateFrom { get; set; }

        [DisplayName("To")]
        public DateTime? DateTo { get; set; }

        public string SortTerm
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_sortTerm))
                    _sortTerm = "DateDesc";  // default sort order
                return _sortTerm;
            }
            set { _sortTerm = value; }
        }

        public IEnumerable<string> Activities
        {
            get { return _activities ?? (_activities = new[] {"showAll"}); }
            set { _activities = value; }
        }

        public IEnumerable<ActivityType> ActivityTypes { get; set; }
    }
}