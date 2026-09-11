using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.DTOs.Admin
{
    public class HolidayCalendarDto
    {
        public int HolidayCalendarId { get; set; }

        public string HolidayName { get; set; } = string.Empty;

        public DateOnly HolidayDate { get; set; }

        public string? HolidayType { get; set; }

        public int? BranchId { get; set; }

        public int? BusinessUnitId { get; set; }

        public int? DepartmentId { get; set; }

        public string? HolidayCategory { get; set; }

        public string? ApplicableFor { get; set; }

        public int? CountryId { get; set; }

        public int? StateId { get; set; }

        public bool RecurringHoliday { get; set; }

        public int Year { get; set; }

        public string? Description { get; set; }

    }
}
