using Business_Layer.DTOs.User;
using Business_Layer.Interfaces.AuditLog;
using Business_Layer.Interfaces.CommonInterfaces;
using Business_Layer.Interfaces.User;
using DataAccess_Layers.Entities;
using DataAccess_Layers.Repositories;
using Newtonsoft.Json;
using Serilog;
using Shared.CommonModels;
using Shared.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Services.User
{
    public class ContactService : IContactService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuditService _auditService;
        private readonly ICurrentUserService _currentUserService;

        public ContactService(
            IUnitOfWork unitOfWork,
            IAuditService auditService,
            ICurrentUserService currentUserService)
        {
            _unitOfWork = unitOfWork;
            _auditService = auditService;
            _currentUserService = currentUserService;
        }


        // =====================================================
        // CREATE CONTACT
        // =====================================================

        public async Task<ApiResponse<string>> CreateContact(ContactDto dto)
        {
            try
            {
                // ---------------------------------------------
                // VALIDATION
                // ---------------------------------------------

                if (string.IsNullOrWhiteSpace(dto.FirstName))
                    throw new CustomException("First Name is required.");

                if (string.IsNullOrWhiteSpace(dto.BusinessEmail))
                    throw new CustomException("Business Email is required.");

                if (string.IsNullOrWhiteSpace(dto.Phone))
                    throw new CustomException("Phone is required.");

                if (dto.CompanyInformationId <= 0)
                    throw new CustomException("Company is required.");


                // ---------------------------------------------
                // COMPANY VALIDATION
                // ---------------------------------------------

                var company =
                    (await _unitOfWork.Repository<CompanyInformation>()
                        .FindAsync(x =>
                            x.CompanyInformationId ==
                                dto.CompanyInformationId &&
                            x.IsActive &&
                            !x.IsDeleted))
                    .FirstOrDefault();

                if (company == null)
                    throw new CustomException(
                        "Selected company not found.");


                // ---------------------------------------------
                // DUPLICATE BUSINESS EMAIL
                // ---------------------------------------------

                var duplicateEmail =
                    await _unitOfWork.Repository<ContactInformation>()
                        .FindAsync(x =>
                            x.BusinessEmail.ToLower() ==
                                dto.BusinessEmail.Trim().ToLower() &&
                            x.CompanyId == dto.CompanyId &&
                            x.RegionId == dto.RegionId &&
                            !x.IsDeleted);

                if (duplicateEmail.Any())
                    throw new CustomException(
                        "A contact with this business email already exists.");


                // ---------------------------------------------
                // GENERATE CONTACT NUMBER
                // ---------------------------------------------

                var existingContacts =
                    await _unitOfWork.Repository<ContactInformation>()
                        .GetAllAsync();

                int nextNumber =
                    existingContacts.Count() + 1;

                string contactNumber =
                    $"CON-{DateTime.Now:yyyy}-{nextNumber:D5}";


                // ---------------------------------------------
                // CREATE ENTITY
                // ---------------------------------------------

                ContactInformation contact =
                    new ContactInformation
                    {
                        ContactNumber =
                            contactNumber,

                        Salutation =
                            dto.Salutation.Trim(),

                        FirstName =
                            dto.FirstName.Trim(),

                        LastName =
                            string.IsNullOrWhiteSpace(dto.LastName)
                                ? null
                                : dto.LastName.Trim(),

                        Designation =
                            dto.Designation,

                        Department =
                            dto.Department,


                        // Company
                        CompanyInformationId =
                            dto.CompanyInformationId,

                        ContactTypeId =
                            dto.ContactTypeId,

                        RelationshipId =
                            dto.RelationshipId,


                        // Contact Information
                        BusinessEmail =
                            dto.BusinessEmail.Trim(),

                        Phone =
                            dto.Phone.Trim(),

                        AlternatePhone =
                            dto.AlternatePhone,

                        Website =
                            dto.Website,


                        // Address
                        AddressLine1 =
                            dto.AddressLine1,

                        AddressLine2 =
                            dto.AddressLine2,

                        City =
                            dto.City,

                        StateId =
                            dto.StateId,

                        CountryId =
                            dto.CountryId,

                        PostalCode =
                            dto.PostalCode,


                        // Additional
                        Notes =
                            dto.Notes,


                        // Tenant
                        CompanyId =
                            dto.CompanyId,

                        RegionId =
                            dto.RegionId,


                        // Status
                        IsActive =
                            true,

                        IsDeleted =
                            false,


                        // Audit
                        CreatedBy =
                            _currentUserService.UserId,

                        CreatedAt =
                            DateTime.Now
                    };


                await _unitOfWork
                    .Repository<ContactInformation>()
                    .AddAsync(contact);

                await _unitOfWork.CompleteAsync();


                // ---------------------------------------------
                // AUDIT
                // ---------------------------------------------

                await _auditService.LogAsync(
                    "ContactInformation",
                    "INSERT",
                    contact.ContactInformationId,
                    "",
                    JsonConvert.SerializeObject(contact),
                    _currentUserService.UserId);


                return new ApiResponse<string>
                {
                    Success = true,
                    Message =
                        "Contact Created Successfully",

                    Data =
                        contact.ContactNumber
                };
            }
            catch (Exception ex)
            {
                Log.Error(
                    ex,
                    "Error while creating contact");

                throw;
            }
        }


        // =====================================================
        // UPDATE CONTACT
        // =====================================================

        public async Task<ApiResponse<string>> UpdateContact(ContactDto dto)
        {
            try
            {
                var contact =
                    (await _unitOfWork.Repository<ContactInformation>()
                        .FindAsync(x =>
                            x.ContactInformationId ==
                                dto.ContactInformationId &&
                            !x.IsDeleted))
                    .FirstOrDefault();

                if (contact == null)
                    throw new CustomException(
                        "Contact not found.");


                // ---------------------------------------------
                // VALIDATION
                // ---------------------------------------------

                if (string.IsNullOrWhiteSpace(dto.FirstName))
                    throw new CustomException(
                        "First Name is required.");

                if (string.IsNullOrWhiteSpace(dto.BusinessEmail))
                    throw new CustomException(
                        "Business Email is required.");

                if (string.IsNullOrWhiteSpace(dto.Phone))
                    throw new CustomException(
                        "Phone is required.");

                if (dto.CompanyInformationId <= 0)
                    throw new CustomException(
                        "Company is required.");


                // ---------------------------------------------
                // COMPANY VALIDATION
                // ---------------------------------------------

                var company =
                    (await _unitOfWork.Repository<CompanyInformation>()
                        .FindAsync(x =>
                            x.CompanyInformationId ==
                                dto.CompanyInformationId &&
                            x.IsActive &&
                            !x.IsDeleted))
                    .FirstOrDefault();

                if (company == null)
                    throw new CustomException(
                        "Selected company not found.");


                // ---------------------------------------------
                // DUPLICATE EMAIL
                // ---------------------------------------------

                var duplicateEmail =
                    await _unitOfWork.Repository<ContactInformation>()
                        .FindAsync(x =>
                            x.ContactInformationId !=
                                dto.ContactInformationId &&

                            x.BusinessEmail.ToLower() ==
                                dto.BusinessEmail.Trim().ToLower() &&

                            x.CompanyId ==
                                dto.CompanyId &&

                            x.RegionId ==
                                dto.RegionId &&

                            !x.IsDeleted);

                if (duplicateEmail.Any())
                    throw new CustomException(
                        "A contact with this business email already exists.");


                string oldValues =
                    JsonConvert.SerializeObject(contact);


                // ---------------------------------------------
                // UPDATE
                // ---------------------------------------------

                contact.Salutation =
                    dto.Salutation.Trim();

                contact.FirstName =
                    dto.FirstName.Trim();

                contact.LastName =
                    string.IsNullOrWhiteSpace(dto.LastName)
                        ? null
                        : dto.LastName.Trim();

                contact.Designation =
                    dto.Designation;

                contact.Department =
                    dto.Department;


                contact.CompanyInformationId =
                    dto.CompanyInformationId;

                contact.ContactTypeId =
                    dto.ContactTypeId;

                contact.RelationshipId =
                    dto.RelationshipId;


                contact.BusinessEmail =
                    dto.BusinessEmail.Trim();

                contact.Phone =
                    dto.Phone.Trim();

                contact.AlternatePhone =
                    dto.AlternatePhone;

                contact.Website =
                    dto.Website;


                contact.AddressLine1 =
                    dto.AddressLine1;

                contact.AddressLine2 =
                    dto.AddressLine2;

                contact.City =
                    dto.City;

                contact.StateId =
                    dto.StateId;

                contact.CountryId =
                    dto.CountryId;

                contact.PostalCode =
                    dto.PostalCode;


                contact.Notes =
                    dto.Notes;


                contact.CompanyId =
                    dto.CompanyId;

                contact.RegionId =
                    dto.RegionId;

                contact.IsActive =
                    dto.IsActive;


                contact.ModifiedBy =
                    _currentUserService.UserId;

                contact.ModifiedAt =
                    DateTime.Now;


                _unitOfWork
                    .Repository<ContactInformation>()
                    .Update(contact);

                await _unitOfWork.CompleteAsync();


                // ---------------------------------------------
                // AUDIT
                // ---------------------------------------------

                await _auditService.LogAsync(
                    "ContactInformation",
                    "UPDATE",
                    contact.ContactInformationId,
                    oldValues,
                    JsonConvert.SerializeObject(contact),
                    _currentUserService.UserId);


                return new ApiResponse<string>
                {
                    Success = true,
                    Message =
                        "Contact Updated Successfully",

                    Data =
                        contact.ContactNumber
                };
            }
            catch (Exception ex)
            {
                Log.Error(
                    ex,
                    "Error while updating contact");

                throw;
            }
        }


        // =====================================================
        // DELETE CONTACT
        // =====================================================

        public async Task<ApiResponse<string>> DeleteContact(int id)
        {
            try
            {
                var contact =
                    (await _unitOfWork.Repository<ContactInformation>()
                        .FindAsync(x =>
                            x.ContactInformationId == id &&
                            !x.IsDeleted))
                    .FirstOrDefault();

                if (contact == null)
                    throw new CustomException(
                        "Contact not found.");


                string oldValues =
                    JsonConvert.SerializeObject(contact);


                // ---------------------------------------------
                // SOFT DELETE
                // ---------------------------------------------

                contact.IsDeleted = true;

                contact.IsActive = false;

                contact.ModifiedBy =
                    _currentUserService.UserId;

                contact.ModifiedAt =
                    DateTime.Now;


                _unitOfWork
                    .Repository<ContactInformation>()
                    .Update(contact);

                await _unitOfWork.CompleteAsync();


                // ---------------------------------------------
                // AUDIT
                // ---------------------------------------------

                await _auditService.LogAsync(
                    "ContactInformation",
                    "DELETE",
                    contact.ContactInformationId,
                    oldValues,
                    "",
                    _currentUserService.UserId);


                return new ApiResponse<string>
                {
                    Success = true,
                    Message =
                        "Contact Deleted Successfully",

                    Data =
                        contact.ContactNumber
                };
            }
            catch (Exception ex)
            {
                Log.Error(
                    ex,
                    "Error while deleting contact");

                throw;
            }
        }


        // =====================================================
        // GET ALL CONTACTS
        // =====================================================

        public async Task<ApiResponse<List<ContactDto>>> GetContacts()
        {
            var contacts =
                await _unitOfWork.Repository<ContactInformation>()
                    .GetAllAsync();

            var companies =
                await _unitOfWork.Repository<CompanyInformation>()
                    .GetAllAsync();

            var contactTypes =
                await _unitOfWork.Repository<ContactType>()
                    .GetAllAsync();

            var relationships =
                await _unitOfWork.Repository<Relationship>()
                    .GetAllAsync();

            var countries =
                await _unitOfWork.Repository<Country>()
                    .GetAllAsync();

            var states =
                await _unitOfWork.Repository<StateMaster>()
                    .GetAllAsync();


            var result =
                (from c in contacts

                 // Company
                 join company in companies
                     on c.CompanyInformationId
                     equals company.CompanyInformationId
                     into companyGroup

                 from company in
                     companyGroup.DefaultIfEmpty()


                     // Contact Type
                 join ct in contactTypes
                     on c.ContactTypeId
                     equals (int?)ct.ContactTypeId
                     into contactTypeGroup

                 from ct in
                     contactTypeGroup.DefaultIfEmpty()


                     // Relationship
                 join r in relationships
                     on c.RelationshipId
                     equals (int?)r.RelationshipId
                     into relationshipGroup

                 from r in
                     relationshipGroup.DefaultIfEmpty()


                     // Country
                 join country in countries
                     on c.CountryId
                     equals (int?)country.CountryId
                     into countryGroup

                 from country in
                     countryGroup.DefaultIfEmpty()


                     // State
                 join state in states
                     on c.StateId
                     equals (int?)state.StateId
                     into stateGroup

                 from state in
                     stateGroup.DefaultIfEmpty()


                 where !c.IsDeleted


                 select new ContactDto
                 {
                     ContactInformationId =
                         c.ContactInformationId,

                     ContactNumber =
                         c.ContactNumber,


                     // Personal
                     Salutation =
                         c.Salutation,

                     FirstName =
                         c.FirstName,

                     LastName =
                         c.LastName,

                     Designation =
                         c.Designation,

                     Department =
                         c.Department,


                     // Company
                     CompanyInformationId =
                         c.CompanyInformationId,

                     CompanyName =
                         company != null
                             ? company.CompanyName
                             : null,


                     // Contact Type
                     ContactTypeId =
                         c.ContactTypeId,

                     ContactTypeName =
                         ct != null
                             ? ct.ContactTypeName
                             : null,


                     // Relationship
                     RelationshipId =
                         c.RelationshipId,

                     RelationshipName =
                         r != null
                             ? r.RelationshipName
                             : null,


                     // Contact
                     BusinessEmail =
                         c.BusinessEmail,

                     Phone =
                         c.Phone,

                     AlternatePhone =
                         c.AlternatePhone,

                     Website =
                         c.Website,


                     // Address
                     AddressLine1 =
                         c.AddressLine1,

                     AddressLine2 =
                         c.AddressLine2,

                     City =
                         c.City,

                     StateId =
                         c.StateId,

                     StateName =
                         state != null
                             ? state.StateName
                             : null,

                     CountryId =
                         c.CountryId,

                     CountryName =
                         country != null
                             ? country.CountryName
                             : null,

                     PostalCode =
                         c.PostalCode,


                     // Additional
                     Notes =
                         c.Notes,


                     // Tenant
                     CompanyId =
                         c.CompanyId,

                     RegionId =
                         c.RegionId,


                     // Audit
                     IsActive =
                         c.IsActive,

                     CreatedAt =
                         c.CreatedAt,

                     ModifiedAt =
                         c.ModifiedAt
                 })
                .OrderByDescending(x =>
                    x.ContactInformationId)
                .ToList();


            return new ApiResponse<List<ContactDto>>
            {
                Success = true,
                Message = "Success",
                Data = result
            };
        }


        // =====================================================
        // GET CONTACT BY ID
        // =====================================================

        public async Task<ApiResponse<ContactDto>> GetContactById(int id)
        {
            var contact =
                (await _unitOfWork.Repository<ContactInformation>()
                    .FindAsync(x =>
                        x.ContactInformationId == id &&
                        !x.IsDeleted))
                .FirstOrDefault();

            if (contact == null)
                throw new CustomException(
                    "Contact not found.");


            var company =
                (await _unitOfWork.Repository<CompanyInformation>()
                    .FindAsync(x =>
                        x.CompanyInformationId ==
                            contact.CompanyInformationId))
                .FirstOrDefault();


            var contactType =
                contact.ContactTypeId.HasValue
                    ? (await _unitOfWork.Repository<ContactType>()
                        .FindAsync(x =>
                            x.ContactTypeId ==
                                contact.ContactTypeId.Value))
                        .FirstOrDefault()
                    : null;


            var relationship =
                contact.RelationshipId.HasValue
                    ? (await _unitOfWork.Repository<Relationship>()
                        .FindAsync(x =>
                            x.RelationshipId ==
                                contact.RelationshipId.Value))
                        .FirstOrDefault()
                    : null;


            var country =
                contact.CountryId.HasValue
                    ? (await _unitOfWork.Repository<Country>()
                        .FindAsync(x =>
                            x.CountryId ==
                                contact.CountryId.Value))
                        .FirstOrDefault()
                    : null;


            var state =
                contact.StateId.HasValue
                    ? (await _unitOfWork.Repository<StateMaster>()
                        .FindAsync(x =>
                            x.StateId ==
                                contact.StateId.Value))
                        .FirstOrDefault()
                    : null;


            return new ApiResponse<ContactDto>
            {
                Success = true,
                Message = "Success",

                Data = new ContactDto
                {
                    ContactInformationId =
                        contact.ContactInformationId,

                    ContactNumber =
                        contact.ContactNumber,


                    // Personal
                    Salutation =
                        contact.Salutation,

                    FirstName =
                        contact.FirstName,

                    LastName =
                        contact.LastName,

                    Designation =
                        contact.Designation,

                    Department =
                        contact.Department,


                    // Company
                    CompanyInformationId =
                        contact.CompanyInformationId,

                    CompanyName =
                        company?.CompanyName,


                    ContactTypeId =
                        contact.ContactTypeId,

                    ContactTypeName =
                        contactType?.ContactTypeName,


                    RelationshipId =
                        contact.RelationshipId,

                    RelationshipName =
                        relationship?.RelationshipName,


                    // Contact
                    BusinessEmail =
                        contact.BusinessEmail,

                    Phone =
                        contact.Phone,

                    AlternatePhone =
                        contact.AlternatePhone,

                    Website =
                        contact.Website,


                    // Address
                    AddressLine1 =
                        contact.AddressLine1,

                    AddressLine2 =
                        contact.AddressLine2,

                    City =
                        contact.City,

                    StateId =
                        contact.StateId,

                    StateName =
                        state?.StateName,

                    CountryId =
                        contact.CountryId,

                    CountryName =
                        country?.CountryName,

                    PostalCode =
                        contact.PostalCode,


                    // Additional
                    Notes =
                        contact.Notes,


                    // Tenant
                    CompanyId =
                        contact.CompanyId,

                    RegionId =
                        contact.RegionId,


                    // Audit
                    IsActive =
                        contact.IsActive,

                    CreatedAt =
                        contact.CreatedAt,

                    ModifiedAt =
                        contact.ModifiedAt
                }
            };
        }

    }
}
