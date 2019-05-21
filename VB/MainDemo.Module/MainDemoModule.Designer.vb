Namespace MainDemo.[Module]
    Partial Class MainDemoModule
        Private components As System.ComponentModel.IContainer = Nothing

        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If disposing AndAlso (components IsNot Nothing) Then
                components.Dispose()
            End If

            MyBase.Dispose(disposing)
        End Sub

        Private Sub InitializeComponent()
            Me.AdditionalExportedTypes.Add(GetType(DevExpress.Persistent.BaseImpl.BaseObject))
            Me.AdditionalExportedTypes.Add(GetType(DevExpress.ExpressApp.Security.AuthenticationStandardLogonParameters))
            Me.Description = "MainDemo module"
            Me.RequiredModuleTypes.Add(GetType(DevExpress.ExpressApp.Objects.BusinessClassLibraryCustomizationModule))
            Me.RequiredModuleTypes.Add(GetType(DevExpress.ExpressApp.Validation.ValidationModule))
            Me.RequiredModuleTypes.Add(GetType(DevExpress.ExpressApp.SystemModule.SystemModule))
            Me.RequiredModuleTypes.Add(GetType(DevExpress.ExpressApp.Security.SecurityModule))
            Me.RequiredModuleTypes.Add(GetType(DevExpress.ExpressApp.ConditionalAppearance.ConditionalAppearanceModule))
        End Sub
    End Class
End Namespace
