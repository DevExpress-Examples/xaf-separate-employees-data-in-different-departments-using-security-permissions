Namespace MainDemo.Win
    Partial Class MainDemoWinApplication
        Private components As System.ComponentModel.IContainer = Nothing

        Protected Overrides Sub DisposeCore()
            If (components IsNot Nothing) Then
                components.Dispose()
            End If

            MyBase.DisposeCore()
        End Sub

        Private Sub InitializeComponent()
            Me.systemModule1 = New DevExpress.ExpressApp.SystemModule.SystemModule()
            Me.winSystemModule1 = New DevExpress.ExpressApp.Win.SystemModule.SystemWindowsFormsModule()
            Me.validationModule1 = New DevExpress.ExpressApp.Validation.ValidationModule()
            Me.securityModule1 = New DevExpress.ExpressApp.Security.SecurityModule()
            Me.objectsModule1 = New DevExpress.ExpressApp.Objects.BusinessClassLibraryCustomizationModule()
            Me.validationWinModule1 = New DevExpress.ExpressApp.Validation.Win.ValidationWindowsFormsModule()
            Me.mainDemoModule1 = New MainDemo.[Module].MainDemoModule()
            Me.securityStrategyComplex1 = New DevExpress.ExpressApp.Security.SecurityStrategyComplex()
            Me.authenticationStandard1 = New DevExpress.ExpressApp.Security.AuthenticationStandard()
            CType((Me), System.ComponentModel.ISupportInitialize).BeginInit()
            Me.validationModule1.AllowValidationDetailsAccess = True
            Me.securityStrategyComplex1.Authentication = Me.authenticationStandard1
            Me.securityStrategyComplex1.RoleType = GetType(DevExpress.Persistent.BaseImpl.PermissionPolicy.PermissionPolicyRole)
            Me.securityStrategyComplex1.UserType = GetType(MainDemo.[Module].BusinessObjects.Employee)
            Me.securityStrategyComplex1.AssociationPermissionsMode = DevExpress.ExpressApp.Security.AssociationPermissionsMode.Manual
            Me.authenticationStandard1.LogonParametersType = GetType(DevExpress.ExpressApp.Security.AuthenticationStandardLogonParameters)
            Me.ApplicationName = "MainDemo"
            Me.Modules.Add(Me.systemModule1)
            Me.Modules.Add(Me.winSystemModule1)
            Me.Modules.Add(Me.validationModule1)
            Me.Modules.Add(Me.securityModule1)
            Me.Modules.Add(Me.objectsModule1)
            Me.Modules.Add(Me.mainDemoModule1)
            Me.Modules.Add(Me.validationWinModule1)
            Me.Security = Me.securityStrategyComplex1
            Me.TablePrefixes = "DevExpress.ExpressApp.Updating=DX"
            AddHandler Me.LastLogonParametersRead, New System.EventHandler(Of DevExpress.ExpressApp.LastLogonParametersReadEventArgs)(AddressOf Me.MainDemoWebApplication_LastLogonParametersRead)
            AddHandler Me.DatabaseVersionMismatch, New System.EventHandler(Of DevExpress.ExpressApp.DatabaseVersionMismatchEventArgs)(AddressOf Me.MainDemoWinApplication_DatabaseVersionMismatch)
            CType((Me), System.ComponentModel.ISupportInitialize).EndInit()
        End Sub

        Private systemModule1 As DevExpress.ExpressApp.SystemModule.SystemModule
        Private winSystemModule1 As DevExpress.ExpressApp.Win.SystemModule.SystemWindowsFormsModule
        Private validationModule1 As DevExpress.ExpressApp.Validation.ValidationModule
        Private securityModule1 As DevExpress.ExpressApp.Security.SecurityModule
        Private mainDemoModule1 As MainDemo.[Module].MainDemoModule
        Private objectsModule1 As DevExpress.ExpressApp.Objects.BusinessClassLibraryCustomizationModule
        Private validationWinModule1 As DevExpress.ExpressApp.Validation.Win.ValidationWindowsFormsModule
        Private securityStrategyComplex1 As DevExpress.ExpressApp.Security.SecurityStrategyComplex
        Private authenticationStandard1 As DevExpress.ExpressApp.Security.AuthenticationStandard
    End Class
End Namespace
