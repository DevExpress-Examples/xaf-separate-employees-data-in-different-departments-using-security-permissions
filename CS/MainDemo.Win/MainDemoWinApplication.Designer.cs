namespace MainDemo.Win {
	partial class MainDemoWinApplication {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void DisposeCore() {
			if((components != null)) {
				components.Dispose();
			}
			base.DisposeCore();
		}

		#region Component Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
            this.systemModule1 = new DevExpress.ExpressApp.SystemModule.SystemModule();
            this.winSystemModule1 = new DevExpress.ExpressApp.Win.SystemModule.SystemWindowsFormsModule();
            this.validationModule1 = new DevExpress.ExpressApp.Validation.ValidationModule();
            this.securityModule1 = new DevExpress.ExpressApp.Security.SecurityModule();
            this.objectsModule1 = new DevExpress.ExpressApp.Objects.BusinessClassLibraryCustomizationModule();
            this.validationWinModule1 = new DevExpress.ExpressApp.Validation.Win.ValidationWindowsFormsModule();
            this.mainDemoModule1 = new MainDemo.Module.MainDemoModule();
            this.securityStrategyComplex1 = new DevExpress.ExpressApp.Security.SecurityStrategyComplex();
            this.authenticationStandard1 = new DevExpress.ExpressApp.Security.AuthenticationStandard();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // validationModule1
            // 
            this.validationModule1.AllowValidationDetailsAccess = true;
            // 
            // securityStrategyComplex1
            // 
            this.securityStrategyComplex1.Authentication = this.authenticationStandard1;
            this.securityStrategyComplex1.RoleType = typeof(DevExpress.Persistent.BaseImpl.PermissionPolicy.PermissionPolicyRole);
            this.securityStrategyComplex1.UserType = typeof(MainDemo.Module.BusinessObjects.Employee);
            this.securityStrategyComplex1.AssociationPermissionsMode = DevExpress.ExpressApp.Security.AssociationPermissionsMode.Manual;
            // 
            // authenticationStandard1
            // 
            this.authenticationStandard1.LogonParametersType = typeof(DevExpress.ExpressApp.Security.AuthenticationStandardLogonParameters);
            // 
            // MainDemoWinApplication
            // 
            this.ApplicationName = "MainDemo";
            this.Modules.Add(this.systemModule1);
            this.Modules.Add(this.winSystemModule1);
            this.Modules.Add(this.validationModule1);
            this.Modules.Add(this.securityModule1);
            this.Modules.Add(this.objectsModule1);
            this.Modules.Add(this.mainDemoModule1);
            this.Modules.Add(this.validationWinModule1);
            this.Security = this.securityStrategyComplex1;
            this.TablePrefixes = "DevExpress.ExpressApp.Updating=DX";
            this.LastLogonParametersRead += new System.EventHandler<DevExpress.ExpressApp.LastLogonParametersReadEventArgs>(this.MainDemoWebApplication_LastLogonParametersRead);
            this.DatabaseVersionMismatch += new System.EventHandler<DevExpress.ExpressApp.DatabaseVersionMismatchEventArgs>(this.MainDemoWinApplication_DatabaseVersionMismatch);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

		#endregion

		private DevExpress.ExpressApp.SystemModule.SystemModule systemModule1;
        private DevExpress.ExpressApp.Win.SystemModule.SystemWindowsFormsModule winSystemModule1;
		private DevExpress.ExpressApp.Validation.ValidationModule validationModule1;
		private DevExpress.ExpressApp.Security.SecurityModule securityModule1;
		private MainDemo.Module.MainDemoModule mainDemoModule1;
        private DevExpress.ExpressApp.Objects.BusinessClassLibraryCustomizationModule objectsModule1;
        private DevExpress.ExpressApp.Validation.Win.ValidationWindowsFormsModule validationWinModule1;
		private DevExpress.ExpressApp.Security.SecurityStrategyComplex securityStrategyComplex1;
        private DevExpress.ExpressApp.Security.AuthenticationStandard authenticationStandard1;
	}
}
