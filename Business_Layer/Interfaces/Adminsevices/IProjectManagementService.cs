using Business_Layer.DTOs.Admin;
using Shared.CommonModels;

namespace Business_Layer.Interfaces.Adminsevices
{
    public interface IProjectManagementService
    {
        Task<ApiResponse<string>> CreateProjectManagement(
           ProjectManagementDto dto);

        Task<ApiResponse<string>> UpdateProjectManagement(
            ProjectManagementDto dto);

        Task<ApiResponse<string>> DeleteProjectManagement(
            int id);

        Task<ApiResponse<List<ProjectManagementDto>>>
            GetProjectManagements();

        Task<ApiResponse<ProjectManagementDto>>
            GetProjectManagementById(int id);



        Task<ApiResponse<string>> CreateProjectMilestone(
            ProjectMilestoneDto dto);

        Task<ApiResponse<string>> UpdateProjectMilestone(
            ProjectMilestoneDto dto);

        Task<ApiResponse<string>> DeleteProjectMilestone(
            int id);

        Task<ApiResponse<List<ProjectMilestoneDto>>>
            GetProjectMilestones();

        Task<ApiResponse<ProjectMilestoneDto>>
            GetProjectMilestoneById(int id);

        Task<ApiResponse<string>> CreateProjectTask(
           ProjectTaskDto dto);

        Task<ApiResponse<string>> UpdateProjectTask(
            ProjectTaskDto dto);

        Task<ApiResponse<string>> DeleteProjectTask(
            int id);

        Task<ApiResponse<List<ProjectTaskDto>>>
            GetProjectTasks();

        Task<ApiResponse<ProjectTaskDto>>
            GetProjectTaskById(int id);

        Task<ApiResponse<string>> CreateProjectDocument(
           ProjectDocumentDto dto);

        Task<ApiResponse<string>> UpdateProjectDocument(
            ProjectDocumentDto dto);

        Task<ApiResponse<string>> DeleteProjectDocument(
            int id);

        Task<ApiResponse<List<ProjectDocumentDto>>>
            GetProjectDocuments();

        Task<ApiResponse<ProjectDocumentDto>>
            GetProjectDocumentById(int id);

        Task<ApiResponse<string>> CreateTimesheet(
            TimesheetManagementDto dto);

        Task<ApiResponse<string>> UpdateTimesheet(
            TimesheetManagementDto dto);

        Task<ApiResponse<string>> DeleteTimesheet(
            int id);

        Task<ApiResponse<List<TimesheetManagementDto>>>
            GetTimesheets();

        Task<ApiResponse<TimesheetManagementDto>>
            GetTimesheetById(int id);
    }
}
