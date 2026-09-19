using Business_Layer.DTOs.SuperAdmin;
using Shared.CommonModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business_Layer.Interfaces.SuperAdminInterface
{
    public interface IInvoiceMasterService
    {
        Task<ApiResponse<string>> CreateInvoice(InvoiceMasterDto dto);

        Task<ApiResponse<string>> UpdateInvoice(InvoiceMasterDto dto);

        Task<ApiResponse<string>> DeleteInvoice(int id);

        Task<ApiResponse<List<InvoiceMasterDto>>> GetInvoices();

        Task<ApiResponse<InvoiceMasterDto>> GetInvoiceById(int id);
    }
}
