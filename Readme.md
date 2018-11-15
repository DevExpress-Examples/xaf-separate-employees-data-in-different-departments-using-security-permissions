<!-- default file list -->
*Files to look at*:

* [Department.cs](./CS/MainDemo.Module/BusinessObjects/Department.cs) (VB: [Department.vb](./VB/MainDemo.Module/BusinessObjects/Department.vb))
* [Employee.cs](./CS/MainDemo.Module/BusinessObjects/Employee.cs) (VB: [EmployeeTask.vb](./VB/MainDemo.Module/BusinessObjects/EmployeeTask.vb))
* [EmployeeTask.cs](./CS/MainDemo.Module/BusinessObjects/EmployeeTask.cs) (VB: [EmployeeTask.vb](./VB/MainDemo.Module/BusinessObjects/EmployeeTask.vb))
* **[Updater.cs](./CS/MainDemo.Module/DatabaseUpdate/Updater.cs) (VB: [Updater.vb](./VB/MainDemo.Module/DatabaseUpdate/Updater.vb))**
* [MainDemoModule.cs](./CS/MainDemo.Module/MainDemoModule.cs) (VB: [MainDemoModule.vb](./VB/MainDemo.Module/MainDemoModule.vb))
* [WebApplication.cs](./CS/MainDemo.Web/WebApplication.cs) (VB: [WebApplication.vb](./VB/MainDemo.Web/WebApplication.vb))
* [MainDemoWinApplication.cs](./CS/MainDemo.Win/MainDemoWinApplication.cs) (VB: [MainDemoWinApplication.vb](./VB/MainDemo.Win/MainDemoWinApplication.vb))
<!-- default file list end -->
# How to separate employees data in different departments using security permissions in XPO


<p><strong>Scenario</strong><strong><br> </strong>This example demonstrates how to use <a href="http://documentation.devexpress.com/xaf/CustomDocument3361.aspx"><u>the new security system</u></a> to implement the following security roles:</p>
<p>- Users (<strong>Joe, John</strong>) can view and edit tasks from their own department, but cannot delete them or create new ones. They also have readonly access to employees and other data of their own department.</p>
<p>- Managers (<strong>Sam, Mary</strong>) can fully manage (CRUD) their own department, its employees and tasks. However, they cannot access data from other departments.</p>
<p>- Administrators (<strong>Admin</strong>) can do everything within the application.<br>All users have empty passwords by default.</p>
<p><br> <strong>Steps to implement</strong></p>
<p><strong>1.</strong> <a href="http://documentation.devexpress.com/#Xaf/CustomDocument3436"><u>Permissions at the type, object and member level (with a criteria)</u></a> are configured in the <em>MainDemo.Module/DatabaseUpdate/Updater</em> file. Take special note that for building a complex criteria against associated objects, the <a href="https://documentation.devexpress.com/#CoreLibraries/clsDevExpressDataFilteringContainsOperatortopic">ContainsOperator</a> together with <a href="http://documentation.devexpress.com/#xaf/CustomDocument3307"><u>the built-in CurrentUserId and IsCurrentUserInRole criteria functions</u></a>. For greater convenience, strongly typed criteria for permissions are accompanied with their string representation.<br><br><strong>2.</strong> The <a href="http://documentation.devexpress.com/#Xaf/CustomDocument3437"><u>SecuredObjectSpaceProvider</u></a> is used in the <em>CreateDefaultObjectSpaceProvider</em> method of the <em>XafApplication </em>descendants located in the WinForms and ASP.NET projects.<br><br><strong>3. </strong>Permission requests caching is enabled via the <em>IsGrantedAdapter.Enable</em> method in the <em>MainDemo.Module\MainDemoModule.xx</em> file (see the <a href="https://www.devexpress.com/Support/Center/p/T241873">T241873</a> ticket for more details).<br><br><strong>4.</strong> The <em>Department, Employee</em> and <em>EmployeeTask</em> classes are implemented in the <em>MainDemo.Module/BusinessObjects</em> folder. To quickly understand relationships between involved business classes, their class diagram is attached.</p>
<p><img src="https://raw.githubusercontent.com/DevExpress-Examples/how-to-separate-employees-data-in-different-departments-using-security-permissions-in-xpo-e4045/15.2.5+/media/4b846044-f185-4283-9a91-771163abd5f1.png"><br> <strong>IMPORTANT NOTES<br>1. </strong>See also the <a href="http://documentation.devexpress.com/#Xaf/CustomDocument3206"><u>functional tests</u></a> in the <em>MainDemo.EasyTests </em>folder for more details on the tested scenarios.<strong><br>2. </strong>For versions <strong>older than v15.2.5</strong>, be aware of the issue described in the <a href="https://www.devexpress.com/Support/Center/p/Q287727">Security - The "Entering state 'GetObjectsNonReenterant'" error may occur while saving data if a permission criteria involves a collection property</a> thread.<br><strong>3.</strong> <a href="https://www.devexpress.com/Support/Center/p/KA18785">The State of the New Security System</a></p>

<br/>


