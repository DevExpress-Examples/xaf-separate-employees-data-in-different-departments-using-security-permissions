using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Security.Adapters;
using DevExpress.ExpressApp.Security.Xpo.Adapters;

namespace MainDemo.Module {
	public sealed partial class MainDemoModule : ModuleBase {
		public MainDemoModule() {
			InitializeComponent();
			IsGrantedAdapter.Enable(new XpoIntegratedCachedRequestSecurityAdapterProvider(), ReloadPermissionStrategy.CacheOnFirstAccess);
		}
	}
}
