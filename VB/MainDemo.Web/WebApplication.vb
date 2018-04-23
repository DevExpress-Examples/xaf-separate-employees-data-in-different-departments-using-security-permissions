Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports DevExpress.ExpressApp
Imports DevExpress.ExpressApp.Web
Imports DevExpress.ExpressApp.Xpo
Imports DevExpress.ExpressApp.Security.ClientServer
Imports DevExpress.ExpressApp.Security

Namespace MainDemo.Web
	Partial Public Class MainDemoAspNetApplication
		Inherits WebApplication

		Private module1 As DevExpress.ExpressApp.SystemModule.SystemModule
		Private module2 As DevExpress.ExpressApp.Web.SystemModule.SystemAspNetModule
		Private module3 As MainDemo.Module.MainDemoModule
		Private businessClassLibraryCustomizationModule1 As DevExpress.ExpressApp.Objects.BusinessClassLibraryCustomizationModule
		Private validationModule1 As DevExpress.ExpressApp.Validation.ValidationModule
		Private securityModule1 As DevExpress.ExpressApp.Security.SecurityModule
		Private securityStrategyComplex1 As DevExpress.ExpressApp.Security.SecurityStrategyComplex
		Private authenticationStandard1 As DevExpress.ExpressApp.Security.AuthenticationStandard

		Public Sub New()
			InitializeComponent()
		End Sub
		Protected Overrides Sub CreateDefaultObjectSpaceProvider(ByVal args As CreateCustomObjectSpaceProviderEventArgs)
			args.ObjectSpaceProvider = New SecuredObjectSpaceProvider(DirectCast(Security, ISelectDataSecurityProvider), args.ConnectionString, args.Connection)
		End Sub

		Private Sub MainDemoAspNetApplication_DatabaseVersionMismatch(ByVal sender As Object, ByVal e As DevExpress.ExpressApp.DatabaseVersionMismatchEventArgs) Handles MyBase.DatabaseVersionMismatch
	e.Updater.Update()
	e.Handled = True
		End Sub

		Private Sub InitializeComponent()
			Me.module1 = New DevExpress.ExpressApp.SystemModule.SystemModule()
			Me.module2 = New DevExpress.ExpressApp.Web.SystemModule.SystemAspNetModule()
			Me.module3 = New MainDemo.Module.MainDemoModule()
			Me.businessClassLibraryCustomizationModule1 = New DevExpress.ExpressApp.Objects.BusinessClassLibraryCustomizationModule()
			Me.validationModule1 = New DevExpress.ExpressApp.Validation.ValidationModule()
			Me.securityModule1 = New DevExpress.ExpressApp.Security.SecurityModule()
			Me.securityStrategyComplex1 = New DevExpress.ExpressApp.Security.SecurityStrategyComplex()
			Me.authenticationStandard1 = New DevExpress.ExpressApp.Security.AuthenticationStandard()
			DirectCast(Me, System.ComponentModel.ISupportInitialize).BeginInit()
			' 
			' validationModule1
			' 
			Me.validationModule1.AllowValidationDetailsAccess = True
			' 
			' securityStrategyComplex1
			' 
			Me.securityStrategyComplex1.Authentication = Me.authenticationStandard1
			Me.securityStrategyComplex1.RoleType = GetType(DevExpress.ExpressApp.Security.Strategy.SecuritySystemRole)
			Me.securityStrategyComplex1.UserType = GetType(MainDemo.Module.BusinessObjects.Employee)
			' 
			' authenticationStandard1
			' 
			Me.authenticationStandard1.LogonParametersType = GetType(DevExpress.ExpressApp.Security.AuthenticationStandardLogonParameters)
			' 
			' MainDemoAspNetApplication
			' 
			Me.ApplicationName = "MainDemo"
			Me.Modules.Add(Me.module1)
			Me.Modules.Add(Me.module2)
			Me.Modules.Add(Me.businessClassLibraryCustomizationModule1)
			Me.Modules.Add(Me.validationModule1)
			Me.Modules.Add(Me.securityModule1)
			Me.Modules.Add(Me.module3)
			Me.Security = Me.securityStrategyComplex1
'			Me.DatabaseVersionMismatch += New System.EventHandler(Of DevExpress.ExpressApp.DatabaseVersionMismatchEventArgs)(Me.MainDemoAspNetApplication_DatabaseVersionMismatch)
			DirectCast(Me, System.ComponentModel.ISupportInitialize).EndInit()

		End Sub
	End Class
End Namespace
