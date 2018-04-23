Imports DevExpress.ExpressApp
Imports DevExpress.ExpressApp.Security.Adapters
Imports DevExpress.ExpressApp.Security.Xpo.Adapters

Namespace MainDemo.Module
	Public NotInheritable Partial Class MainDemoModule
		Inherits ModuleBase

		Public Sub New()
			InitializeComponent()
			'DevExpress.ExpressApp.Security.Strategy.SecuritySystemRoleBase.AutoAssociationPermissions = false;
			IsGrantedAdapter.Enable(New XpoIntegratedCachedRequestSecurityAdapterProvider(), ReloadPermissionStrategy.CacheOnFirstAccess)
		End Sub
	End Class
End Namespace
