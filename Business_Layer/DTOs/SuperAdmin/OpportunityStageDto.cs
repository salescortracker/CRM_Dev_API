using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.DTOs.SuperAdmin
{
    public class OpportunityStageDto
    {
        public int OpportunityStageId { get; set; }

        public string StageName { get; set; } = string.Empty;

        public string StageCode { get; set; } = string.Empty;

        public int Probability { get; set; }

        public int StageOrder { get; set; }

        public string ForecastCategory { get; set; } = string.Empty;

        public string StageType { get; set; } = string.Empty;

        public string Status { get; set; } = "Active";

        public bool WonStage { get; set; }

        public bool LostStage { get; set; }
    }
}
