using Business_Layer.DTOs.SuperAdmin;
using Shared.CommonModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Interfaces.SuperAdminInterface
{
    public interface IModuleConfigurationService
    {
        #region Lead Setting
        Task<ApiResponse<string>> CreateLeadSetting(LeadSettingDto dto);

        Task<ApiResponse<string>> UpdateLeadSetting(LeadSettingDto dto);

        Task<ApiResponse<string>> DeleteLeadSetting(int id);

        Task<ApiResponse<List<LeadSettingDto>>> GetLeadSettings();

        Task<ApiResponse<LeadSettingDto>> GetLeadSettingById(int id);
        #endregion
        #region Pipeline Setting
        Task<ApiResponse<string>> CreatePipelineSetting(
           PipelineSettingDto dto);

        Task<ApiResponse<string>> UpdatePipelineSetting(
            PipelineSettingDto dto);

        Task<ApiResponse<string>> DeletePipelineSetting(
            int id);

        Task<ApiResponse<List<PipelineSettingDto>>>
            GetPipelineSettings();

        Task<ApiResponse<PipelineSettingDto>>
            GetPipelineSettingById(int id);
        #endregion
        #region Opportunity Stage
        Task<ApiResponse<string>> CreateOpportunityStage(
            OpportunityStageDto dto);

        Task<ApiResponse<string>> UpdateOpportunityStage(
            OpportunityStageDto dto);

        Task<ApiResponse<string>> DeleteOpportunityStage(
            int id);

        Task<ApiResponse<List<OpportunityStageDto>>>
            GetOpportunityStages();

        Task<ApiResponse<OpportunityStageDto>>
            GetOpportunityStageById(int id);
        #endregion
        #region Activity Type
        Task<ApiResponse<string>> CreateActivityType(CrmActivityTypeDto dto);
        Task<ApiResponse<string>> UpdateActivityType(CrmActivityTypeDto dto);
        Task<ApiResponse<string>> DeleteActivityType(int id);
        Task<ApiResponse<List<CrmActivityTypeDto>>> GetActivityTypes();
        Task<ApiResponse<CrmActivityTypeDto>> GetActivityTypeById(int id);
        #endregion
        #region Source
        Task<ApiResponse<string>> CreateSource(CrmSourceDto dto);
        Task<ApiResponse<string>> UpdateSource(CrmSourceDto dto);
        Task<ApiResponse<string>> DeleteSource(int id);
        Task<ApiResponse<List<CrmSourceDto>>> GetSources();
        Task<ApiResponse<CrmSourceDto>> GetSourceById(int id);
        #endregion
        #region Industry
        Task<ApiResponse<string>> CreateIndustry(CrmIndustryDto dto);
        Task<ApiResponse<string>> UpdateIndustry(CrmIndustryDto dto);
        Task<ApiResponse<string>> DeleteIndustry(int id);
        Task<ApiResponse<List<CrmIndustryDto>>> GetIndustries();
        Task<ApiResponse<CrmIndustryDto>> GetIndustryById(int id);
        #endregion
        #region Territory
        Task<ApiResponse<string>> CreateTerritory(CrmTerritoryDto dto);
        Task<ApiResponse<string>> UpdateTerritory(CrmTerritoryDto dto);
        Task<ApiResponse<string>> DeleteTerritory(int id);
        Task<ApiResponse<List<CrmTerritoryDto>>> GetTerritories();
        Task<ApiResponse<CrmTerritoryDto>> GetTerritoryById(int id);
        #endregion
        #region Sales Target
        Task<ApiResponse<string>> CreateSalesTarget(CrmSalesTargetDto dto);
        Task<ApiResponse<string>> UpdateSalesTarget(CrmSalesTargetDto dto);
        Task<ApiResponse<string>> DeleteSalesTarget(int id);
        Task<ApiResponse<List<CrmSalesTargetDto>>> GetSalesTargets();
        Task<ApiResponse<CrmSalesTargetDto>> GetSalesTargetById(int id);
        #endregion
        #region Number Series
        Task<ApiResponse<string>> CreateNumberSeries(CrmNumberSeriesDto dto);
        Task<ApiResponse<string>> UpdateNumberSeries(CrmNumberSeriesDto dto);
        Task<ApiResponse<string>> DeleteNumberSeries(int id);
        Task<ApiResponse<List<CrmNumberSeriesDto>>> GetNumberSeries();
        Task<ApiResponse<CrmNumberSeriesDto>> GetNumberSeriesById(int id);
        #endregion
        #region Custom Field
        Task<ApiResponse<string>> CreateCustomField(CrmCustomFieldDto dto);
        Task<ApiResponse<string>> UpdateCustomField(CrmCustomFieldDto dto);
        Task<ApiResponse<string>> DeleteCustomField(int id);
        Task<ApiResponse<List<CrmCustomFieldDto>>> GetCustomFields();
        Task<ApiResponse<CrmCustomFieldDto>> GetCustomFieldById(int id);
        #endregion
    }
}
