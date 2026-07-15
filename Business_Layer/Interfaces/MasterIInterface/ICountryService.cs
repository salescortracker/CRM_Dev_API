using Business_Layer.DTOs.MasterDTO_s;
using Shared.CommonModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Interfaces.MasterIInterface
{
    public interface ICountryService
    {
        Task<ApiResponse<string>> CreateCountry(CountryDto dto);

        Task<ApiResponse<string>> UpdateCountry(CountryDto dto);

        Task<ApiResponse<string>> DeleteCountry(int id);

        Task<ApiResponse<List<CountryDto>>> GetCountries();

        Task<ApiResponse<CountryDto>> GetCountryById(int id);
    }
}
