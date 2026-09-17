using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.DTOs.Admin
{
    public class LicenseManagementDto
    {
        public int LicenseManagementId { get; set; }

        public string LicenseName { get; set; } = string.Empty;

        public string LicenseCode { get; set; } = string.Empty;

        public int CompanyId { get; set; }

        public int LicenseTypeId { get; set; }

        public int? MaximumUsers { get; set; }

        public decimal? StorageLimitGb { get; set; }

        public DateOnly? LicenseStartDate { get; set; }

        public DateOnly? LicenseExpiryDate { get; set; }

        public long? ApicallsPerMonth { get; set; }

        public string? SupportLevel { get; set; }

        public bool Crmmodule { get; set; }

        public bool SalesModule { get; set; }

        public bool MarketingModule { get; set; }

        public bool SupportModule { get; set; }

        public bool Apiaccess { get; set; }

        public bool MobileApp { get; set; }

        public bool ActiveLicense { get; set; }
    }
}
