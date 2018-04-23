Imports System
Imports System.Configuration

Imports DevExpress.ExpressApp
Imports DevExpress.Persistent.AuditTrail
Imports System.Globalization
Imports DevExpress.ExpressApp.Win.EasyTest

Namespace MainDemo.Win
	Public Class Program
		Private Shared Sub winApplication_CustomizeFormattingCulture(ByVal sender As Object, ByVal e As CustomizeFormattingCultureEventArgs)
			e.FormattingCulture = CultureInfo.GetCultureInfo("en-US")
		End Sub
		<STAThread>
		Public Shared Sub Main(ByVal arguments() As String)
			Dim winApplication As New MainDemoWinApplication()
#If DEBUG Then
			EasyTestRemotingRegistration.Register()
#End If
			AddHandler winApplication.CustomizeFormattingCulture, AddressOf winApplication_CustomizeFormattingCulture
			Try
				AddHandler AuditTrailService.Instance.QueryCurrentUserName, AddressOf Instance_QueryCurrentUserName
				AddHandler winApplication.LastLogonParametersReading, AddressOf winApplication_LastLogonParametersReading
				If ConfigurationManager.ConnectionStrings("ConnectionString") IsNot Nothing Then
					winApplication.ConnectionString = ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString
				End If
				DevExpress.ExpressApp.Xpo.InMemoryDataStoreProvider.Register()
				winApplication.ConnectionString = DevExpress.ExpressApp.Xpo.InMemoryDataStoreProvider.ConnectionString
				winApplication.Setup()
				winApplication.Start()
			Catch e As Exception
				winApplication.HandleException(e)
			End Try
		End Sub
		Private Shared Sub winApplication_LastLogonParametersReading(ByVal sender As Object, ByVal e As LastLogonParametersReadingEventArgs)
			If String.IsNullOrEmpty(e.SettingsStorage.LoadOption("", "UserName")) Then
				e.SettingsStorage.SaveOption("", "UserName", "Sam")
			End If
		End Sub
		Private Shared Sub Instance_QueryCurrentUserName(ByVal sender As Object, ByVal e As QueryCurrentUserNameEventArgs)
			e.CurrentUserName = System.Security.Principal.WindowsIdentity.GetCurrent().Name
		End Sub
	End Class
End Namespace
