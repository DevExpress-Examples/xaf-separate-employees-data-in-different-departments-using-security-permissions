Imports System
Imports DevExpress.ExpressApp
Imports DevExpress.ExpressApp.Win
Imports DevExpress.ExpressApp.Xpo
Imports System.Collections.Generic
Imports DevExpress.ExpressApp.Security
Imports DevExpress.ExpressApp.Security.ClientServer

Namespace MainDemo.Win
	Partial Public Class MainDemoWinApplication
		Inherits WinApplication

		Public Sub New()
			InitializeComponent()
			DelayedViewItemsInitialization = True
		End Sub
		Private Sub MainDemoWinApplication_DatabaseVersionMismatch(ByVal sender As Object, ByVal e As DatabaseVersionMismatchEventArgs) Handles MyBase.DatabaseVersionMismatch
			e.Updater.Update()
			e.Handled = True
		End Sub
		Private Sub MainDemoWebApplication_LastLogonParametersRead(ByVal sender As Object, ByVal e As LastLogonParametersReadEventArgs) Handles MyBase.LastLogonParametersRead
			' Just to read demo user name for logon.
			Dim logonParameters As AuthenticationStandardLogonParameters = TryCast(e.LogonObject, AuthenticationStandardLogonParameters)
			If logonParameters IsNot Nothing Then
				If String.IsNullOrEmpty(logonParameters.UserName) Then
					logonParameters.UserName = "Sam"
				End If
			End If
		End Sub
		Protected Overrides Sub CreateDefaultObjectSpaceProvider(ByVal args As CreateCustomObjectSpaceProviderEventArgs)
			args.ObjectSpaceProvider = New SecuredObjectSpaceProvider(DirectCast(Security, ISelectDataSecurityProvider), args.ConnectionString, args.Connection)
		End Sub
	End Class
End Namespace
