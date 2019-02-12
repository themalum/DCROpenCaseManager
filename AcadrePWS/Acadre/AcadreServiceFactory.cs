using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Services.Protocols;

namespace AcadrePWS.Acadre
{
    public static class AcadreServiceFactory
    {
        private static AcadreServiceV7.CaseService7 caseService7;
        private static AcadreServiceV7.ContactService7 contactService7;
		private static AcadreServiceV7.MainDocumentService7 mainDocumentService7;
		private static AcadreServiceV7.ConfigurationService7 configurationService7;

        public static AcadreServiceV7.CaseService7 GetCaseService7()
        {
            if (caseService7 == null)
            {
                caseService7 = new AcadreServiceV7.CaseService7(Properties.Settings.Default.AcadrePWS_AcadreServiceV7_AcadreServiceV7);
                ConfigureService(caseService7);
            }
            return caseService7;
        }

        public static AcadreServiceV7.ContactService7 GetContactService7()
        {
            if (contactService7 == null)
            {
                contactService7 = new AcadreServiceV7.ContactService7(Properties.Settings.Default.AcadrePWS_AcadreServiceV7_AcadreServiceV7);
                ConfigureService(contactService7);
            }
            return contactService7;
        }

        public static AcadreServiceV7.MainDocumentService7 GetMainDocumentService7()
        {
            if (mainDocumentService7 == null)
            {
                mainDocumentService7 = new AcadreServiceV7.MainDocumentService7(Properties.Settings.Default.AcadrePWS_AcadreServiceV7_AcadreServiceV7);
                ConfigureService(mainDocumentService7);
            }
            return mainDocumentService7;
        }

		public static AcadreServiceV7.ConfigurationService7 GetConfigurationService7()
        {
			if (configurationService7 == null)
            {
				configurationService7 = new AcadreServiceV7.ConfigurationService7(Properties.Settings.Default.AcadrePWS_AcadreServiceV7_AcadreServiceV7);
				ConfigureService(configurationService7);
            }
			return configurationService7;
        }

        private static void ConfigureService(SoapHttpClientProtocol service)
        {
			var credential = new System.Net.NetworkCredential(
				Properties.Settings.Default.PWSServiceUserName
				, Properties.Settings.Default.PWSServiceUserPassword
				, Properties.Settings.Default.PWSServiceUserDomain);
            service.Credentials = credential;
        }
    }
}
