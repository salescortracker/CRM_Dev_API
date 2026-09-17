using Business_Layer.DTOs.Admin;
using Shared.CommonModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Interfaces.Adminsevices
{
    public interface ICustomerSupportTicketService
    {
        Task<ApiResponse<string>> CreateCustomerSupportTicket(
           CustomerSupportTicketDto dto);

        Task<ApiResponse<string>> UpdateCustomerSupportTicket(
            CustomerSupportTicketDto dto);

        Task<ApiResponse<string>> DeleteCustomerSupportTicket(
            int id);

        Task<ApiResponse<List<CustomerSupportTicketDto>>>
            GetCustomerSupportTickets();

        Task<ApiResponse<CustomerSupportTicketDto>>
            GetCustomerSupportTicketById(int id);

        Task<ApiResponse<string>> CreateTicketCategory(
           TicketCategoryDto dto);

        Task<ApiResponse<string>> UpdateTicketCategory(
            TicketCategoryDto dto);

        Task<ApiResponse<string>> DeleteTicketCategory(
            int id);

        Task<ApiResponse<List<TicketCategoryDto>>>
            GetTicketCategories();

        Task<ApiResponse<TicketCategoryDto>>
            GetTicketCategoryById(int id);

        Task<ApiResponse<string>> CreateKnowledgeBase(
           KnowledgeBaseDto dto);

        Task<ApiResponse<string>> UpdateKnowledgeBase(
            KnowledgeBaseDto dto);

        Task<ApiResponse<string>> DeleteKnowledgeBase(
            int id);

        Task<ApiResponse<List<KnowledgeBaseDto>>>
            GetKnowledgeBases();

        Task<ApiResponse<KnowledgeBaseDto>>
            GetKnowledgeBaseById(int id);

        Task<ApiResponse<string>> CreateFaqManagement(
            FaqManagementDto dto);

        Task<ApiResponse<string>> UpdateFaqManagement(
            FaqManagementDto dto);

        Task<ApiResponse<string>> DeleteFaqManagement(
            int id);

        Task<ApiResponse<List<FaqManagementDto>>>
            GetFaqManagements();

        Task<ApiResponse<FaqManagementDto>>
            GetFaqManagementById(int id);
    }
}
