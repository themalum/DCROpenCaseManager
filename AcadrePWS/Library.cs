using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcadrePWS
{
    public static class CaseManagement
    {
        /* Call this before calling any other method */
        public static void ActingFor(
            /* A short text description of the system making these calls */
            string user)
        {
            Acadre.PWSHeaderExtension.User = user;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="personNameForAddressingName"></param>
        /// <param name="personCivilRegistrationNumber"></param>
        /// <param name="caseFileTypeCode"></param>
        /// <param name="accessCode"></param>
        /// <param name="caseFileTitleText"></param>
        /// <param name="journalizingCode"></param>
        /// <param name="facet"></param>
        /// <param name="caseResponsible"></param>
        /// <param name="administrativeUnit"></param>
        /// <param name="caseContent"></param>
        /// <param name="caseFileDisposalCode"></param>
        /// <param name="deletionCode"></param>
        /// <param name="caseRestrictedFromPublicText"></param>
        /// <returns></returns>
        public static string CreateCase(
            /* Case party's name */
            string personNameForAddressingName,
            /* Case party's CPR number */
            string personCivilRegistrationNumber,
            /* Case type. One of the following:
			 * "EMSAG" (emnesag)
			 * "BGSAG" (borgersag)
			 * "EJSAG" (ejendomssag)
			 * "PERSAG" (personalesag)
			 * "BYGGESAG" (byggesag) */
            string caseFileTypeCode,
            /* Security code. One of the following:
			 * "BO" (borgersag)
			 * "KK" (kommunekode)
			 * "LP" (lukket punkt)
			 * "PP" (personpunkt) */
            string accessCode,
            /* Case title */
            string caseFileTitleText,
            /* KLE journalizing code (http://www.kle-online.dk/emneplan/00/) */
            string journalizingCode,
            /* KLE facet for the specified journalizing code */
            string facet,
            /* Username of the user creating this case; get from Active Directory
             * (the AcadreServiceFactory.GetConfigurationService7().GetUserList(...) method will
             * return a full list) */
            string caseResponsible,
            /* Identifier of the administrative unit; should probably be "80" for "Løn og personale"
             * (the AcadreServiceFactory.GetConfigurationService7().GetAdminUnitList(...) method
             * will return a full list) */
            string administrativeUnit,
            /* Case content */
            string caseContent,
            /* Discard code. One of the following(?):
             * "B" (bevares),
             * "K" (kasseres),
             * "K5" (kasseres efter 5 år),
             * "K10" (kasseres efter 10 år)
             * "K20" (kasseres efter 20 år) */
            string caseFileDisposalCode,
            /* Deletion code; "P1800D" seems to be the standard value here */
            string deletionCode,
            string caseRestrictedFromPublicText
            )
        {
            var caseService = Acadre.AcadreServiceFactory.GetCaseService7();
            var contactService = Acadre.AcadreServiceFactory.GetContactService7();
            var configurationService = Acadre.AcadreServiceFactory.GetConfigurationService7();

            // look up contact by cprnumber
            var searchContactCriterion = new AcadreServiceV7.SearchContactCriterionType2();
            searchContactCriterion.ContactTypeName = "Person";
            searchContactCriterion.SearchTerm = personCivilRegistrationNumber;

            AcadreServiceV7.ContactSearchResponseType[] foundContacts =
                contactService.SearchContacts(searchContactCriterion);

            string contactGUID;
            if (foundContacts.Length > 0)
            {
                // contact already exists, read GUID and name
                contactGUID = foundContacts.First().GUID;
            }
            else
            {
                // contact doesn't exist - create it and assign GUID
                var contact = new AcadreServiceV7.PersonType2();
                contact.PersonCivilRegistrationIdentifierStatusCode = "0";
                contact.PersonCivilRegistrationIdentifier = personCivilRegistrationNumber;
                contact.PersonNameForAddressingName = personNameForAddressingName;
                contactGUID = contactService.CreateContact(contact);
            }

            // create the case
            var createCaseRequest = new AcadreServiceV7.CreateCaseRequestType();

            AcadreServiceV7.CaseFileType3 caseFile = new AcadreServiceV7.CaseFileType3();
            caseFile.CaseFileTypeCode = caseFileTypeCode;
            caseFile.Year = DateTime.Now.Year.ToString();
            caseFile.CreationDate = DateTime.Now;
            caseFile.CaseFileTitleText = personCivilRegistrationNumber; // must be set to contact cpr number for BGSAG
            caseFile.TitleUnofficialIndicator = false;
            caseFile.TitleAlternativeText = personNameForAddressingName; // must be set to contact name for BGSAG
            caseFile.RestrictedFromPublicText = caseRestrictedFromPublicText;
            caseFile.CaseFileStatusCode = "B";
            caseFile.CaseFileDisposalCode = caseFileDisposalCode;
            caseFile.DeletionCode = deletionCode;
            caseFile.AccessCode = accessCode;

            caseFile.AdministrativeUnit = new AcadreServiceV7.AdministrativeUnitType[]
                {
                new AcadreServiceV7.AdministrativeUnitType() { AdministrativeUnitReference=administrativeUnit }
                };

            caseFile.CustomFieldCollection = new AcadreServiceV7.CustomField[]
                {
                    new AcadreServiceV7.CustomField(){Name = "df1",Value = caseContent}
                    ,new AcadreServiceV7.CustomField(){Name = "df25",Value = contactGUID}
                };

            caseFile.Classification = new AcadreServiceV7.ClassificationType
            {
                Category = new AcadreServiceV7.CategoryType[] {
                    new AcadreServiceV7.CategoryType(){ Principle="KL Koder", Literal = journalizingCode }
                       ,new AcadreServiceV7.CategoryType(){ Principle="Facetter", Literal = facet }
                   }
            };

            caseFile.Party = new AcadreServiceV7.PartyType[] { new AcadreServiceV7.PartyType() {
                CreationDate = DateTime.Now
                ,ContactReference = contactGUID
                ,PublicAccessLevelReference = "3"
                ,IsPrimary = true
            } };

            var userList = configurationService.GetUserList(
                new AcadreServiceV7.EmptyRequestType()).ToList();
            var user = userList.SingleOrDefault(u => u.Initials == caseResponsible);
            if (user != null)
            {
                caseFile.CaseFileManagerReference = user.Id;
            }

            createCaseRequest.CaseFile = caseFile;

            var createCaseResponse = caseService.CreateCase(createCaseRequest);
            // check for multicase (samlesag) response.
            if (createCaseResponse.CreateCaseAndAMCResult == AcadreServiceV7.CreateCaseAndAMCResultType.CaseNotCreatedAndListAMCReceived)
            {
                // create the case in all the multicases
                createCaseRequest.MultiCaseIdentifiers = createCaseResponse.MultiCaseIdentifiers;
                createCaseResponse = createCaseResponse = caseService.CreateCase(createCaseRequest);
            }

            return createCaseResponse.CaseFileIdentifier;
        }
        public static string CloseCase(
            /* Case identifier returned by CreateCase */
            string caseId)
        {
            var caseService = Acadre.AcadreServiceFactory.GetCaseService7();

            var acadreCase = caseService.GetCase(caseId);
            acadreCase.CaseFileStatusCode = "A";
            acadreCase.ClosedDate = DateTime.Now;
            return caseService.UpdateCase(acadreCase);
        }
        public static string GetCaseURL(
            /* Case identifier returned by CreateCase */
            string caseId)
        {
            return AcadrePWS.Properties.Settings.Default.AcadreFrontEndBaseURL + "/Case/Details?caseId=" + caseId;
        }
        public static string GetCaseNumber(
            /* Case identifier returned by CreateCase */
            string caseId)
        {
            var caseService = Acadre.AcadreServiceFactory.GetCaseService7();

            var acadreCase = caseService.GetCase(caseId);
            if (acadreCase != null)
            {
                return acadreCase.CaseFileNumberIdentifier;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Create a document in acadre 7
        /// </summary>
        /// <param name="documentCaseId"></param>
        /// <param name="recordStatusCode"></param>
        /// <param name="documentTypeCode"></param>
        /// <param name="documentDescriptionText"></param>
        /// <param name="documentAccessCode"></param>
        /// <param name="documentStatusCode"></param>
        /// <param name="documentTitleText"></param>
        /// <param name="documentCategoryCode"></param>
        /// <param name="recordPublicIndicator"></param>
        /// <param name="fileName"></param>
        /// <param name="fileBytes"></param>
        /// <returns></returns>
        public static string CreateDocumentService(
            string documentCaseId,
            string recordStatusCode,
            string documentTypeCode,
            string documentDescriptionText,
            string documentAccessCode,
            string documentStatusCode,
            string documentTitleText,
            string documentCategoryCode,
            string recordPublicIndicator,
            string fileName,
            byte[] fileBytes
            )
        {
            var doc = new AcadreServiceV7.CreateMainDocumentRequestType2();

            //Record Type
            var record = new AcadreServiceV7.RecordType2();
            doc.Record = record;
            record.CaseFileReference = documentCaseId;
            record.RecordStatusCode = recordStatusCode;
            record.DocumentTypeCode = documentTypeCode;
            record.RecordSerialNumber = "1";
            record.AccessCode = documentAccessCode;
            record.DescriptionText = documentDescriptionText;
            record.RecordPaperStorageIndicator = false;
            record.PublicationIndicator = recordPublicIndicator;

            //Document type
            var docType = new AcadreServiceV7.DocumentType();
            doc.Document = docType;
            docType.DocumentStatusCode = documentStatusCode;
            docType.DocumentCategoryCode = documentCategoryCode;
            docType.DocumentTitleText = documentTitleText;

            //Document link type
            var docLinkType = new AcadreServiceV7.DocumentLinkType();
            doc.DocumentLink = docLinkType;
            doc.FileName = fileName;
            doc.XMLBinary = fileBytes;

            var documentService = Acadre.AcadreServiceFactory.GetMainDocumentService7();
            var documentId = documentService.CreateMainDocument(doc);
            return documentId;
        }
    }
}
