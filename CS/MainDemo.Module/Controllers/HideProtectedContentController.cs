using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.ConditionalAppearance;

namespace MainDemo.Module.Controllers {
	public class HideProtectedContentController : ViewController<ObjectView> {
		private AppearanceController appearanceController;
		protected override void OnActivated() {
			base.OnActivated();
			appearanceController = Frame.GetController<AppearanceController>();
			if(appearanceController != null) {
				appearanceController.CustomApplyAppearance += appearanceController_CustomApplyAppearance;
			}
		}
		protected override void OnDeactivated() {
			if(appearanceController != null) {
				appearanceController.CustomApplyAppearance -= appearanceController_CustomApplyAppearance;
			}
			base.OnDeactivated();
		}
		void appearanceController_CustomApplyAppearance(object sender, ApplyAppearanceEventArgs e) {
			if(View is ListView) {
				if(e.Item is ColumnWrapper) {
					if(!DataManipulationRight.CanRead(View.ObjectTypeInfo.Type,
						((ColumnWrapper)e.Item).PropertyName, null,
						((ListView)View).CollectionSource, View.ObjectSpace)) {
						e.AppearanceObject.Visibility = ViewItemVisibility.Hide;
					}
				}
			}
			if(View is DetailView) {
				if(e.Item is PropertyEditor) {
					if(!DataManipulationRight.CanRead(View.ObjectTypeInfo.Type,
						((PropertyEditor)e.Item).PropertyName,
						e.ContextObjects.Length > 0 ? e.ContextObjects[0] : null, null,
						View.ObjectSpace)) {
						e.AppearanceObject.Visibility = ViewItemVisibility.Hide;
					}
				}
			}
		}
	}
}
