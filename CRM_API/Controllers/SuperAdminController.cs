using Business_Layer.DTOs.SuperAdmin;
using Business_Layer.Interfaces.SuperAdminInterface;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRM_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperAdminController : ControllerBase
    {
        private readonly IPlanService _planService;
        private readonly IOrganizationService _organizationService;
        private readonly IWebHostEnvironment _env;
        private readonly IWorkflowAndAutomation _workflowAndAutomation;
        //private readonly IModuleConfigurationService _moduleConfigurationService;

        public SuperAdminController(IPlanService planService, IOrganizationService organizationService, IWebHostEnvironment env, IWorkflowAndAutomation workflowAndAutomation)
        {
            _planService = planService;
            _organizationService = organizationService;
            _env = env;
            _workflowAndAutomation = workflowAndAutomation;
        }

        #region SUBSCRIPTION PLAN

        [HttpPost("createplan")]
        public async Task<IActionResult> CreatePlan(PlanDto dto)
        {
            return Ok(await _planService.CreatePlan(dto));
        }

        [HttpPost("updateplan")]
        public async Task<IActionResult> UpdatePlan(PlanDto dto)
        {
            return Ok(await _planService.UpdatePlan(dto));
        }

        [HttpPost("deleteplan/{id}")]
        public async Task<IActionResult> DeletePlan(int id)
        {
            return Ok(await _planService.DeletePlan(id));
        }

        [HttpGet("getallplan")]
        public async Task<IActionResult> GetPlans()
        {
            return Ok(await _planService.GetPlans());
        }

        [HttpGet("getbyidplan/{id}")]
        public async Task<IActionResult> GetPlanById(int id)
        {
            return Ok(await _planService.GetPlanById(id));
        }

        #endregion
        #region ORGANIZATIONS

        [HttpPost("createorganization")]
        public async Task<IActionResult> CreateOrganization([FromForm] OrganizationDto dto)
        {
            string root = _env.WebRootPath ??
                          Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");

            string path = Path.Combine(root, "Uploads", "OrganizationLogo");

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            if (dto.LogoFile != null && dto.LogoFile.Length > 0)
            {
                string fileName = $"{Guid.NewGuid()}_{dto.LogoFile.FileName}";

                string fullPath = Path.Combine(path, fileName);

                using var stream = new FileStream(fullPath, FileMode.Create);

                await dto.LogoFile.CopyToAsync(stream);

                dto.LogoUrl = $"Uploads/OrganizationLogo/{fileName}";
            }

            return Ok(await _organizationService.CreateOrganization(dto));
        }
        [HttpPost("updateorganization")]
        public async Task<IActionResult> UpdateOrganization([FromForm] OrganizationDto dto)
        {
            string root = _env.WebRootPath ??
                          Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");

            string path = Path.Combine(root, "Uploads", "OrganizationLogo");

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            if (dto.LogoFile != null && dto.LogoFile.Length > 0)
            {
                string fileName = $"{Guid.NewGuid()}_{dto.LogoFile.FileName}";

                string fullPath = Path.Combine(path, fileName);

                using var stream = new FileStream(fullPath, FileMode.Create);

                await dto.LogoFile.CopyToAsync(stream);

                dto.LogoUrl = $"Uploads/OrganizationLogo/{fileName}";
            }

            return Ok(await _organizationService.UpdateOrganization(dto));
        }
        [HttpPost("deleteorganization/{id}")]
        public async Task<IActionResult> DeleteOrganization(int id)
        {
            return Ok(await _organizationService.DeleteOrganization(id));
        }
        [HttpGet("getallorganization")]
        public async Task<IActionResult> GetOrganizations()
        {
            return Ok(await _organizationService.GetOrganizations());
        }
        [HttpGet("getorganizationbyid/{id}")]
        public async Task<IActionResult> GetOrganizationById(int id)
        {
            return Ok(await _organizationService.GetOrganizationById(id));
        }
        #endregion

        #region WORKFLOW RULES

        [HttpPost("createworkflowrule")]
        public async Task<IActionResult> CreateWorkflowRule(
            WorkflowRuleDto dto)
        {
            return Ok(
                await _workflowAndAutomation
                    .CreateWorkflowRule(dto));
        }


        [HttpPost("updateworkflowrule")]
        public async Task<IActionResult> UpdateWorkflowRule(
            WorkflowRuleDto dto)
        {
            return Ok(
                await _workflowAndAutomation
                    .UpdateWorkflowRule(dto));
        }


        [HttpPost("deleteworkflowrule/{id}")]
        public async Task<IActionResult> DeleteWorkflowRule(
            int id)
        {
            return Ok(
                await _workflowAndAutomation
                    .DeleteWorkflowRule(id));
        }


        [HttpGet("getallworkflowrules")]
        public async Task<IActionResult> GetWorkflowRules()
        {
            return Ok(
                await _workflowAndAutomation
                    .GetWorkflowRules());
        }


        [HttpGet("getworkflowrulebyid/{id}")]
        public async Task<IActionResult> GetWorkflowRuleById(
            int id)
        {
            return Ok(
                await _workflowAndAutomation
                    .GetWorkflowRuleById(id));
        }

        #endregion
        #region WORKFLOW RULE CONDITIONS

        [HttpPost("createworkflowrulecondition")]
        public async Task<IActionResult> CreateWorkflowRuleCondition(
            WorkflowRuleConditionDto dto)
        {
            return Ok(
                await _workflowAndAutomation
                    .CreateWorkflowRuleCondition(dto));
        }


        [HttpPost("updateworkflowrulecondition")]
        public async Task<IActionResult> UpdateWorkflowRuleCondition(
            WorkflowRuleConditionDto dto)
        {
            return Ok(
                await _workflowAndAutomation
                    .UpdateWorkflowRuleCondition(dto));
        }


        [HttpPost("deleteworkflowrulecondition/{id}")]
        public async Task<IActionResult> DeleteWorkflowRuleCondition(
            int id)
        {
            return Ok(
                await _workflowAndAutomation
                    .DeleteWorkflowRuleCondition(id));
        }


        [HttpGet("getallworkflowruleconditions")]
        public async Task<IActionResult> GetWorkflowRuleConditions()
        {
            return Ok(
                await _workflowAndAutomation   
                    .GetWorkflowRuleConditions());
        }


        [HttpGet("getworkflowruleconditionbyid/{id}")]
        public async Task<IActionResult> GetWorkflowRuleConditionById(
            int id)
        {
            return Ok(
                await _workflowAndAutomation
                    .GetWorkflowRuleConditionById(id));
        }


        [HttpGet("getconditionsbyworkflowruleid/{workflowRuleId}")]
        public async Task<IActionResult> GetConditionsByWorkflowRuleId(
            int workflowRuleId)
        {
            return Ok(
                await _workflowAndAutomation
                    .GetConditionsByWorkflowRuleId(workflowRuleId));
        }

        #endregion
        #region WORKFLOW RULE ACTIONS

        [HttpPost("createworkflowruleaction")]
        public async Task<IActionResult> CreateWorkflowRuleAction(
            WorkflowRuleActionDto dto)
        {
            return Ok(
                await _workflowAndAutomation
                    .CreateWorkflowRuleAction(dto));
        }


        [HttpPost("updateworkflowruleaction")]
        public async Task<IActionResult> UpdateWorkflowRuleAction(
            WorkflowRuleActionDto dto)
        {
            return Ok(
                await _workflowAndAutomation
                    .UpdateWorkflowRuleAction(dto));
        }


        [HttpPost("deleteworkflowruleaction/{id}")]
        public async Task<IActionResult> DeleteWorkflowRuleAction(
            int id)
        {
            return Ok(
                await _workflowAndAutomation
                    .DeleteWorkflowRuleAction(id));
        }


        [HttpGet("getallworkflowruleactions")]
        public async Task<IActionResult> GetWorkflowRuleActions()
        {
            return Ok(
                await _workflowAndAutomation
                    .GetWorkflowRuleActions());
        }


        [HttpGet("getworkflowruleactionbyid/{id}")]
        public async Task<IActionResult> GetWorkflowRuleActionById(
            int id)
        {
            return Ok(
                await _workflowAndAutomation
                    .GetWorkflowRuleActionById(id));
        }
        #endregion
        #region APPROVAL WORKFLOW

        [HttpPost("createapprovalworkflow")]
        public async Task<IActionResult> CreateApprovalWorkflow(
            ApprovalWorkflowDto dto)
        {
            return Ok(
                await _workflowAndAutomation
                    .CreateApprovalWorkflow(dto));
        }


        [HttpPost("updateapprovalworkflow")]
        public async Task<IActionResult> UpdateApprovalWorkflow(
            ApprovalWorkflowDto dto)
        {
            return Ok(
                await _workflowAndAutomation
                    .UpdateApprovalWorkflow(dto));
        }


        [HttpPost("deleteapprovalworkflow/{id}")]
        public async Task<IActionResult> DeleteApprovalWorkflow(
            int id)
        {
            return Ok(
                await _workflowAndAutomation
                    .DeleteApprovalWorkflow(id));
        }


        [HttpGet("getallapprovalworkflows")]
        public async Task<IActionResult> GetApprovalWorkflows()
        {
            return Ok(
                await _workflowAndAutomation
                    .GetApprovalWorkflows());
        }


        [HttpGet("getapprovalworkflowbyid/{id}")]
        public async Task<IActionResult> GetApprovalWorkflowById(
            int id)
        {
            return Ok(
                await _workflowAndAutomation
                    .GetApprovalWorkflowById(id));
        }

        #endregion
        #region APPROVAL WORKFLOW LEVELS

        [HttpPost("createapprovalworkflowlevel")]
        public async Task<IActionResult> CreateApprovalWorkflowLevel(
            ApprovalWorkflowLevelDto dto)
        {
            return Ok(
                await _workflowAndAutomation
                    .CreateApprovalWorkflowLevel(dto));
        }


        [HttpPost("updateapprovalworkflowlevel")]
        public async Task<IActionResult> UpdateApprovalWorkflowLevel(
            ApprovalWorkflowLevelDto dto)
        {
            return Ok(
                await _workflowAndAutomation
                    .UpdateApprovalWorkflowLevel(dto));
        }


        [HttpPost("deleteapprovalworkflowlevel/{id}")]
        public async Task<IActionResult> DeleteApprovalWorkflowLevel(
            int id)
        {
            return Ok(
                await _workflowAndAutomation
                    .DeleteApprovalWorkflowLevel(id));
        }


        [HttpGet("getallapprovalworkflowlevels")]
        public async Task<IActionResult> GetApprovalWorkflowLevels()
        {
            return Ok(
                await _workflowAndAutomation
                    .GetApprovalWorkflowLevels());
        }


        [HttpGet("getapprovalworkflowlevelbyid/{id}")]
        public async Task<IActionResult> GetApprovalWorkflowLevelById(
            int id)
        {
            return Ok(
                await _workflowAndAutomation
                    .GetApprovalWorkflowLevelById(id));
        }
        #endregion

    }
}
