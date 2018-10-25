using System;
using System.Configuration;

using DevExpress.ExpressApp;
using DevExpress.Persistent.AuditTrail;
using System.Globalization;
using DevExpress.ExpressApp.Win.EasyTest;

namespace MainDemo.Win {
	public class Program {
		static private void winApplication_CustomizeFormattingCulture(object sender, CustomizeFormattingCultureEventArgs e) {
			e.FormattingCulture = CultureInfo.GetCultureInfo("en-US");
		}
		[STAThread]
		public static void Main(string[] arguments) {
			MainDemoWinApplication winApplication = new MainDemoWinApplication();
#if DEBUG
			EasyTestRemotingRegistration.Register();
#endif
			winApplication.CustomizeFormattingCulture += new EventHandler<CustomizeFormattingCultureEventArgs>(winApplication_CustomizeFormattingCulture);
			try {
				AuditTrailService.Instance.QueryCurrentUserName += new QueryCurrentUserNameEventHandler(Instance_QueryCurrentUserName);
				winApplication.LastLogonParametersReading += new EventHandler<LastLogonParametersReadingEventArgs>(winApplication_LastLogonParametersReading);
				if(ConfigurationManager.ConnectionStrings["ConnectionString"] != null) {
					winApplication.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
				}
				DevExpress.ExpressApp.Xpo.InMemoryDataStoreProvider.Register();
				winApplication.ConnectionString = DevExpress.ExpressApp.Xpo.InMemoryDataStoreProvider.ConnectionString;
				winApplication.Setup();
				winApplication.Start();
			} catch(Exception e) {
				winApplication.HandleException(e);
			}
		}
		static void winApplication_LastLogonParametersReading(object sender, LastLogonParametersReadingEventArgs e) {
			if(string.IsNullOrEmpty(e.SettingsStorage.LoadOption("", "UserName"))) {
				e.SettingsStorage.SaveOption("", "UserName", "Sam");
			}
		}
		static void Instance_QueryCurrentUserName(object sender, QueryCurrentUserNameEventArgs e) {
			e.CurrentUserName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
		}
	}
}
