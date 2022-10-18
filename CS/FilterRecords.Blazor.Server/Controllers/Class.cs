using DevExpress.ExpressApp.Blazor;
using DevExpress.ExpressApp.SystemModule;

namespace FilterRecords.Blazor.Server.Controllers {
    public class BlazorDiagnosticInfoController : DiagnosticInfoController {
        protected override void OnActivated() {
            base.OnActivated();
            IConfiguration configuration = (IConfiguration)((BlazorApplication)Application).ServiceProvider.GetService(typeof(IConfiguration));
            DiagnosticInfo.Active.SetItemValue(EnableDiagnosticActionsActiveKey, true);
        }
    }
}
