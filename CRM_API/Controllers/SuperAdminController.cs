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
        private readonly ICommunicationService _communicationService;
        //private readonly IModuleConfigurationService _moduleConfigurationService;

        public SuperAdminController(IPlanService planService, IOrganizationService organizationService, IWebHostEnvironment env, IWorkflowAndAutomation workflowAndAutomation, ICommunicationService communicationService)
        {
            _planService = planService;
            _organizationService = organizationService;
            _env = env;
            _workflowAndAutomation = workflowAndAutomation;
            _communicationService = communicationService;
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
        #region AUTO ASSIGNMENT RULE

        [HttpPost("createautoassignmentrule")]
        public async Task<IActionResult> CreateAutoAssignmentRule(
            AutoAssignmentRuleDto dto)
        {
            return Ok(
                await _workflowAndAutomation
                    .CreateAutoAssignmentRule(dto));
        }


        [HttpPost("updateautoassignmentrule")]
        public async Task<IActionResult> UpdateAutoAssignmentRule(
            AutoAssignmentRuleDto dto)
        {
            return Ok(
                await _workflowAndAutomation
                    .UpdateAutoAssignmentRule(dto));
        }


        [HttpPost("deleteautoassignmentrule/{id}")]
        public async Task<IActionResult> DeleteAutoAssignmentRule(
            int id)
        {
            return Ok(
                await _workflowAndAutomation
                    .DeleteAutoAssignmentRule(id));
        }


        [HttpGet("getallautoassignmentrules")]
        public async Task<IActionResult> GetAutoAssignmentRules()
        {
            return Ok(
                await _workflowAndAutomation
                    .GetAutoAssignmentRules());
        }


        [HttpGet("getautoassignmentrulebyid/{id}")]
        public async Task<IActionResult> GetAutoAssignmentRuleById(
            int id)
        {
            return Ok(
                await _workflowAndAutomation
                    .GetAutoAssignmentRuleById(id));
        }

        #endregion

        #region AUTO ASSIGNMENT CONDITION



        // =====================================================
        // CREATE AUTO ASSIGNMENT CONDITION
        // =====================================================

        [HttpPost("createautoassignmentcondition")]
        public async Task<IActionResult> CreateAutoAssignmentCondition(
            AutoAssignmentConditionDto dto)
        {
            return Ok(
                await _workflowAndAutomation
                    .CreateAutoAssignmentCondition(dto));
        }


        // =====================================================
        // UPDATE AUTO ASSIGNMENT CONDITION
        // =====================================================

        [HttpPost("updateautoassignmentcondition")]
        public async Task<IActionResult> UpdateAutoAssignmentCondition(
            AutoAssignmentConditionDto dto)
        {
            return Ok(
                await _workflowAndAutomation
                    .UpdateAutoAssignmentCondition(dto));
        }


        // =====================================================
        // DELETE AUTO ASSIGNMENT CONDITION
        // =====================================================

        [HttpPost("deleteautoassignmentcondition/{id}")]
        public async Task<IActionResult> DeleteAutoAssignmentCondition(
            int id)
        {
            return Ok(
                await _workflowAndAutomation
                    .DeleteAutoAssignmentCondition(id));
        }


        // =====================================================
        // GET ALL AUTO ASSIGNMENT CONDITIONS
        // =====================================================

        [HttpGet("getallautoassignmentconditions")]
        public async Task<IActionResult> GetAutoAssignmentConditions()
        {
            return Ok(
                await _workflowAndAutomation
                    .GetAutoAssignmentConditions());
        }


        // =====================================================
        // GET AUTO ASSIGNMENT CONDITION BY ID
        // =====================================================

        [HttpGet("getautoassignmentconditionbyid/{id}")]
        public async Task<IActionResult> GetAutoAssignmentConditionById(
            int id)
        {
            return Ok(
                await _workflowAndAutomation
                    .GetAutoAssignmentConditionById(id));
        }

        #endregion
        #region ESCALATION RULE

        // =====================================================
        // CREATE ESCALATION RULE
        // =====================================================

        [HttpPost("createescalationrule")]
        public async Task<IActionResult> CreateEscalationRule(
            EscalationRuleDto dto)
        {
            return Ok(
                await _workflowAndAutomation
                    .CreateEscalationRule(dto));
        }


        // =====================================================
        // UPDATE ESCALATION RULE
        // =====================================================

        [HttpPost("updateescalationrule")]
        public async Task<IActionResult> UpdateEscalationRule(
            EscalationRuleDto dto)
        {
            return Ok(
                await _workflowAndAutomation
                    .UpdateEscalationRule(dto));
        }


        // =====================================================
        // DELETE ESCALATION RULE
        // =====================================================

        [HttpPost("deleteescalationrule/{id}")]
        public async Task<IActionResult> DeleteEscalationRule(
            int id)
        {
            return Ok(
                await _workflowAndAutomation
                    .DeleteEscalationRule(id));
        }


        // =====================================================
        // GET ALL ESCALATION RULES
        // =====================================================

        [HttpGet("getallescalationrules")]
        public async Task<IActionResult> GetEscalationRules()
        {
            return Ok(
                await _workflowAndAutomation
                    .GetEscalationRules());
        }


        // =====================================================
        // GET ESCALATION RULE BY ID
        // =====================================================

        [HttpGet("getescalationrulebyid/{id}")]
        public async Task<IActionResult> GetEscalationRuleById(
            int id)
        {
            return Ok(
                await _workflowAndAutomation
                    .GetEscalationRuleById(id));
        }

        #endregion
        #region SLA RULE

        // =====================================================
        // CREATE SLA RULE
        // =====================================================

        [HttpPost("createslarule")]
        public async Task<IActionResult> CreateSlarule(
            SlaruleDto dto)
        {
            return Ok(
                await _workflowAndAutomation
                    .CreateSlarule(dto));
        }


        // =====================================================
        // UPDATE SLA RULE
        // =====================================================

        [HttpPost("updateslarule")]
        public async Task<IActionResult> UpdateSlarule(
            SlaruleDto dto)
        {
            return Ok(
                await _workflowAndAutomation
                    .UpdateSlarule(dto));
        }


        // =====================================================
        // DELETE SLA RULE
        // =====================================================

        [HttpPost("deleteslarule/{id}")]
        public async Task<IActionResult> DeleteSlarule(
            int id)
        {
            return Ok(
                await _workflowAndAutomation
                    .DeleteSlarule(id));
        }


        // =====================================================
        // GET ALL SLA RULES
        // =====================================================

        [HttpGet("getslarules")]
        public async Task<IActionResult> GetSlarules()
        {
            return Ok(
                await _workflowAndAutomation
                    .GetSlarules());
        }


        // =====================================================
        // GET SLA RULE BY ID
        // =====================================================

        [HttpGet("getslarulebyid/{id}")]
        public async Task<IActionResult> GetSlaruleById(
            int id)
        {
            return Ok(
                await _workflowAndAutomation
                    .GetSlaruleById(id));
        }

        #endregion

        #region EMAIL AUTOMATION

        // =====================================================
        // CREATE EMAIL AUTOMATION
        // =====================================================

        [HttpPost("createemailautomation")]
        public async Task<IActionResult> CreateEmailAutomation(
            EmailAutomationDto dto)
        {
            return Ok(
                await _workflowAndAutomation
                    .CreateEmailAutomation(dto));
        }


        // =====================================================
        // UPDATE EMAIL AUTOMATION
        // =====================================================

        [HttpPost("updateemailautomation")]
        public async Task<IActionResult> UpdateEmailAutomation(
            EmailAutomationDto dto)
        {
            return Ok(
                await _workflowAndAutomation
                    .UpdateEmailAutomation(dto));
        }


        // =====================================================
        // DELETE EMAIL AUTOMATION
        // =====================================================

        [HttpPost("deleteemailautomation/{id}")]
        public async Task<IActionResult> DeleteEmailAutomation(
            int id)
        {
            return Ok(
                await _workflowAndAutomation
                    .DeleteEmailAutomation(id));
        }


        // =====================================================
        // GET ALL EMAIL AUTOMATIONS
        // =====================================================

        [HttpGet("getallemailautomations")]
        public async Task<IActionResult> GetEmailAutomations()
        {
            return Ok(
                await _workflowAndAutomation
                    .GetEmailAutomations());
        }


        // =====================================================
        // GET EMAIL AUTOMATION BY ID
        // =====================================================

        [HttpGet("getemailautomationbyid/{id}")]
        public async Task<IActionResult> GetEmailAutomationById(
            int id)
        {
            return Ok(
                await _workflowAndAutomation
                    .GetEmailAutomationById(id));
        }

        #endregion

        #region EMAIL AUTOMATION RECIPIENT

        // =====================================================
        // CREATE EMAIL AUTOMATION RECIPIENT
        // =====================================================

        [HttpPost("createemailautomationrecipient")]
        public async Task<IActionResult> CreateEmailAutomationRecipient(
            EmailAutomationRecipientDto dto)
        {
            return Ok(
                await _workflowAndAutomation
                    .CreateEmailAutomationRecipient(dto));
        }


        // =====================================================
        // UPDATE EMAIL AUTOMATION RECIPIENT
        // =====================================================

        [HttpPost("updateemailautomationrecipient")]
        public async Task<IActionResult> UpdateEmailAutomationRecipient(
            EmailAutomationRecipientDto dto)
        {
            return Ok(
                await _workflowAndAutomation
                    .UpdateEmailAutomationRecipient(dto));
        }


        // =====================================================
        // DELETE EMAIL AUTOMATION RECIPIENT
        // =====================================================

        [HttpPost("deleteemailautomationrecipient/{id}")]
        public async Task<IActionResult> DeleteEmailAutomationRecipient(
            int id)
        {
            return Ok(
                await _workflowAndAutomation
                    .DeleteEmailAutomationRecipient(id));
        }


        // =====================================================
        // GET ALL EMAIL AUTOMATION RECIPIENTS
        // =====================================================

        [HttpGet("getallemailautomationrecipients")]
        public async Task<IActionResult> GetEmailAutomationRecipients()
        {
            return Ok(
                await _workflowAndAutomation
                    .GetEmailAutomationRecipients());
        }


        // =====================================================
        // GET EMAIL AUTOMATION RECIPIENT BY ID
        // =====================================================

        [HttpGet("getemailautomationrecipientbyid/{id}")]
        public async Task<IActionResult> GetEmailAutomationRecipientById(
            int id)
        {
            return Ok(
                await _workflowAndAutomation
                    .GetEmailAutomationRecipientById(id));
        }

        #endregion

        #region SCHEDULED JOB

        // =====================================================
        // CREATE SCHEDULED JOB
        // =====================================================

        [HttpPost("createscheduledjob")]
        public async Task<IActionResult> CreateScheduledJob(
            ScheduledJobDto dto)
        {
            return Ok(
                await _workflowAndAutomation
                    .CreateScheduledJob(dto));
        }


        // =====================================================
        // UPDATE SCHEDULED JOB
        // =====================================================

        [HttpPost("updatescheduledjob")]
        public async Task<IActionResult> UpdateScheduledJob(
            ScheduledJobDto dto)
        {
            return Ok(
                await _workflowAndAutomation
                    .UpdateScheduledJob(dto));
        }


        // =====================================================
        // DELETE SCHEDULED JOB
        // =====================================================

        [HttpPost("deletescheduledjob/{id}")]
        public async Task<IActionResult> DeleteScheduledJob(
            int id)
        {
            return Ok(
                await _workflowAndAutomation
                    .DeleteScheduledJob(id));
        }


        // =====================================================
        // GET ALL SCHEDULED JOBS
        // =====================================================

        [HttpGet("getallscheduledjobs")]
        public async Task<IActionResult> GetScheduledJobs()
        {
            return Ok(
                await _workflowAndAutomation
                    .GetScheduledJobs());
        }


        // =====================================================
        // GET SCHEDULED JOB BY ID
        // =====================================================

        [HttpGet("getscheduledjobbyid/{id}")]
        public async Task<IActionResult> GetScheduledJobById(
            int id)
        {
            return Ok(
                await _workflowAndAutomation
                    .GetScheduledJobById(id));
        }

        #endregion
        #region Communication Module
        #region COMMUNICATION EMAIL

        [HttpPost("createcommunicationemail")]
        public async Task<IActionResult> CreateCommunicationEmail(
            CommunicationEmailDto dto)
        {
            return Ok(
                await _communicationService
                    .CreateCommunicationEmail(dto));
        }

        [HttpPost("updatecommunicationemail")]
        public async Task<IActionResult> UpdateCommunicationEmail(
            CommunicationEmailDto dto)
        {
            return Ok(
                await _communicationService
                    .UpdateCommunicationEmail(dto));
        }

        [HttpPost("deletecommunicationemail/{id}")]
        public async Task<IActionResult> DeleteCommunicationEmail(int id)
        {
            return Ok(
                await _communicationService
                    .DeleteCommunicationEmail(id));
        }

        [HttpGet("getallcommunicationemail")]
        public async Task<IActionResult> GetCommunicationEmails()
        {
            return Ok(
                await _communicationService
                    .GetCommunicationEmails());
        }

        [HttpGet("getbyidcommunicationemail/{id}")]
        public async Task<IActionResult> GetCommunicationEmailById(int id)
        {
            return Ok(
                await _communicationService
                    .GetCommunicationEmailById(id));
        }

        #endregion

        #region COMMUNICATION SMS

        [HttpPost("createcommunicationsms")]
        public async Task<IActionResult> CreateCommunicationSMS(
            CommunicationSMSDto dto)
        {
            return Ok(
                await _communicationService
                    .CreateCommunicationSMS(dto));
        }

        [HttpPost("updatecommunicationsms")]
        public async Task<IActionResult> UpdateCommunicationSMS(
            CommunicationSMSDto dto)
        {
            return Ok(
                await _communicationService
                    .UpdateCommunicationSMS(dto));
        }

        [HttpPost("deletecommunicationsms/{id}")]
        public async Task<IActionResult> DeleteCommunicationSMS(int id)
        {
            return Ok(
                await _communicationService
                    .DeleteCommunicationSMS(id));
        }

        [HttpGet("getallcommunicationsms")]
        public async Task<IActionResult> GetCommunicationSMS()
        {
            return Ok(
                await _communicationService
                    .GetCommunicationSMS());
        }

        [HttpGet("getbyidcommunicationsms/{id}")]
        public async Task<IActionResult> GetCommunicationSMSById(int id)
        {
            return Ok(
                await _communicationService
                    .GetCommunicationSMSById(id));
        }

        #endregion
        #region COMMUNICATION WHATSAPP

        [HttpPost("createcommunicationwhatsapp")]
        public async Task<IActionResult> CreateCommunicationWhatsApp(
            CommunicationWhatsAppDto dto)
        {
            return Ok(
                await _communicationService
                    .CreateCommunicationWhatsApp(dto));
        }

        [HttpPost("updatecommunicationwhatsapp")]
        public async Task<IActionResult> UpdateCommunicationWhatsApp(
            CommunicationWhatsAppDto dto)
        {
            return Ok(
                await _communicationService
                    .UpdateCommunicationWhatsApp(dto));
        }

        [HttpPost("deletecommunicationwhatsapp/{id}")]
        public async Task<IActionResult> DeleteCommunicationWhatsApp(int id)
        {
            return Ok(
                await _communicationService
                    .DeleteCommunicationWhatsApp(id));
        }

        [HttpGet("getallcommunicationwhatsapp")]
        public async Task<IActionResult> GetCommunicationWhatsApps()
        {
            return Ok(
                await _communicationService
                    .GetCommunicationWhatsApps());
        }

        [HttpGet("getbyidcommunicationwhatsapp/{id}")]
        public async Task<IActionResult> GetCommunicationWhatsAppById(int id)
        {
            return Ok(
                await _communicationService
                    .GetCommunicationWhatsAppById(id));
        }

        #endregion
        #region COMMUNICATION VOICE

        [HttpPost("createcommunicationvoice")]
        public async Task<IActionResult> CreateCommunicationVoice(
            CommunicationVoiceDto dto)
        {
            return Ok(
                await _communicationService
                    .CreateCommunicationVoice(dto));
        }

        [HttpPost("updatecommunicationvoice")]
        public async Task<IActionResult> UpdateCommunicationVoice(
            CommunicationVoiceDto dto)
        {
            return Ok(
                await _communicationService
                    .UpdateCommunicationVoice(dto));
        }

        [HttpPost("deletecommunicationvoice/{id}")]
        public async Task<IActionResult> DeleteCommunicationVoice(int id)
        {
            return Ok(
                await _communicationService
                    .DeleteCommunicationVoice(id));
        }

        [HttpGet("getallcommunicationvoice")]
        public async Task<IActionResult> GetCommunicationVoices()
        {
            return Ok(
                await _communicationService
                    .GetCommunicationVoices());
        }

        [HttpGet("getbyidcommunicationvoice/{id}")]
        public async Task<IActionResult> GetCommunicationVoiceById(int id)
        {
            return Ok(
                await _communicationService
                    .GetCommunicationVoiceById(id));
        }

        #endregion
        #region EMAIL TEMPLATE

        [HttpPost("createemailtemplate")]
        public async Task<IActionResult> CreateEmailTemplate(
            CommunicationEmailTemplateDto dto)
        {
            return Ok(
                await _communicationService
                    .CreateEmailTemplate(dto));
        }

        [HttpPost("updateemailtemplate")]
        public async Task<IActionResult> UpdateEmailTemplate(
            CommunicationEmailTemplateDto dto)
        {
            return Ok(
                await _communicationService
                    .UpdateEmailTemplate(dto));
        }

        [HttpPost("deleteemailtemplate/{id}")]
        public async Task<IActionResult> DeleteEmailTemplate(int id)
        {
            return Ok(
                await _communicationService
                    .DeleteEmailTemplate(id));
        }

        [HttpGet("getallemailtemplate")]
        public async Task<IActionResult> GetEmailTemplates()
        {
            return Ok(
                await _communicationService
                    .GetEmailTemplates());
        }

        [HttpGet("getbyidemailtemplate/{id}")]
        public async Task<IActionResult> GetEmailTemplateById(int id)
        {
            return Ok(
                await _communicationService
                    .GetEmailTemplateById(id));
        }

        #endregion
        #region SMS TEMPLATE

        [HttpPost("createsmstemplate")]
        public async Task<IActionResult> CreateSMSTemplate(
            CommunicationSMSTemplateDto dto)
        {
            return Ok(
                await _communicationService
                    .CreateSMSTemplate(dto));
        }


        [HttpPost("updatesmstemplate")]
        public async Task<IActionResult> UpdateSMSTemplate(
            CommunicationSMSTemplateDto dto)
        {
            return Ok(
                await _communicationService
                    .UpdateSMSTemplate(dto));
        }


        [HttpPost("deletesmstemplate/{id}")]
        public async Task<IActionResult> DeleteSMSTemplate(int id)
        {
            return Ok(
                await _communicationService
                    .DeleteSMSTemplate(id));
        }


        [HttpGet("getallsmsTemplate")]
        public async Task<IActionResult> GetSMSTemplates()
        {
            return Ok(
                await _communicationService
                    .GetSMSTemplates());
        }


        [HttpGet("getbysmstemplate/{id}")]
        public async Task<IActionResult> GetSMSTemplateById(int id)
        {
            return Ok(
                await _communicationService
                    .GetSMSTemplateById(id));
        }

        #endregion
        #region WHATSAPP TEMPLATE

        [HttpPost("createwhatsapptemplate")]
        public async Task<IActionResult> CreateWhatsAppTemplate(
            CommunicationWhatsAppTemplateDto dto)
        {
            return Ok(
                await _communicationService
                    .CreateWhatsAppTemplate(dto));
        }


        [HttpPost("updatewhatsapptemplate")]
        public async Task<IActionResult> UpdateWhatsAppTemplate(
            CommunicationWhatsAppTemplateDto dto)
        {
            return Ok(
                await _communicationService
                    .UpdateWhatsAppTemplate(dto));
        }


        [HttpPost("deletewhatsapptemplate/{id}")]
        public async Task<IActionResult> DeleteWhatsAppTemplate(int id)
        {
            return Ok(
                await _communicationService
                    .DeleteWhatsAppTemplate(id));
        }


        [HttpGet("getallwhatsapptemplate")]
        public async Task<IActionResult> GetWhatsAppTemplates()
        {
            return Ok(
                await _communicationService
                    .GetWhatsAppTemplates());
        }


        [HttpGet("getbywhatsapptemplate/{id}")]
        public async Task<IActionResult> GetWhatsAppTemplateById(int id)
        {
            return Ok(
                await _communicationService
                    .GetWhatsAppTemplateById(id));
        }

        #endregion
        #region NOTIFICATION TEMPLATE

        [HttpPost("createnotificationtemplate")]
        public async Task<IActionResult> CreateNotificationTemplate(
            CommunicationNotificationTemplateDto dto)
        {
            return Ok(
                await _communicationService
                    .CreateNotificationTemplate(dto));
        }


        [HttpPost("updatenotificationtemplate")]
        public async Task<IActionResult> UpdateNotificationTemplate(
            CommunicationNotificationTemplateDto dto)
        {
            return Ok(
                await _communicationService
                    .UpdateNotificationTemplate(dto));
        }


        [HttpPost("deletenotificationtemplate/{id}")]
        public async Task<IActionResult> DeleteNotificationTemplate(int id)
        {
            return Ok(
                await _communicationService
                    .DeleteNotificationTemplate(id));
        }


        [HttpGet("getallnotificationtemplate")]
        public async Task<IActionResult> GetNotificationTemplates()
        {
            return Ok(
                await _communicationService
                    .GetNotificationTemplates());
        }


        [HttpGet("getbynotificationtemplate/{id}")]
        public async Task<IActionResult> GetNotificationTemplateById(int id)
        {
            return Ok(
                await _communicationService
                    .GetNotificationTemplateById(id));
        }

        #endregion
        #endregion


    }
}
