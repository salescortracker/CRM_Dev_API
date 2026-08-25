using Business_Layer.DTOs.MasterDTO_s;
using Business_Layer.DTOs.User;
using Shared.CommonModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Interfaces.User
{
    public interface ILeadService
    {
        // =====================================================
        // LEAD CRUD
        // =====================================================

        Task<ApiResponse<string>> CreateLead(LeadDto dto);

        Task<ApiResponse<string>> UpdateLead(LeadDto dto);

        Task<ApiResponse<string>> DeleteLead(int id);

        Task<ApiResponse<List<LeadDto>>> GetLeads();

        Task<ApiResponse<LeadDto>> GetLeadById(int id);


        // =====================================================
        // LEAD DROPDOWNS
        // =====================================================
        Task<ApiResponse<List<CompanyTypeDto>>> GetCompanyTypes();

        Task<ApiResponse<List<LeadTypeDto>>> GetLeadTypes();

        Task<ApiResponse<List<LeadSourceDto>>> GetLeadSources();

        Task<ApiResponse<List<IndustryDto>>> GetIndustries();

        Task<ApiResponse<List<CountryDto>>> GetCountries();

        Task<ApiResponse<List<StateDto>>> GetStates(int? countryId);
    }
}
