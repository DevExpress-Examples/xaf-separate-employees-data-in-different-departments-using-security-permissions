using DevExpress.ExpressApp;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Updating;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.EF;
using DevExpress.Persistent.BaseImpl.EF;
using DevExpress.Persistent.BaseImpl.EF.PermissionPolicy;
using FilterRecords.Module.BusinessObjects;

namespace FilterRecords.Module.DatabaseUpdate;

// For more typical usage scenarios, be sure to check out https://docs.devexpress.com/eXpressAppFramework/DevExpress.ExpressApp.Updating.ModuleUpdater
public class Updater : ModuleUpdater {
    public Updater(IObjectSpace objectSpace, Version currentDBVersion) :
        base(objectSpace, currentDBVersion) {
    }
    public override void UpdateDatabaseAfterUpdateSchema() {
        base.UpdateDatabaseAfterUpdateSchema();
        //string name = "MyName";
        //EntityObject1 theObject = ObjectSpace.FirstOrDefault<EntityObject1>(u => u.Name == name);
        //if(theObject == null) {
        //    theObject = ObjectSpace.CreateObject<EntityObject1>();
        //    theObject.Name = name;
        //}
        ApplicationUser sampleUser = ObjectSpace.FirstOrDefault<ApplicationUser>(u => u.UserName == "User");
        if(sampleUser == null) {
            sampleUser = ObjectSpace.CreateObject<ApplicationUser>();
            sampleUser.UserName = "User";
            // Set a password if the standard authentication type is used
            sampleUser.SetPassword("");

            // The UserLoginInfo object requires a user object Id (Oid).
            // Commit the user object to the database before you create a UserLoginInfo object. This will correctly initialize the user key property.
            ObjectSpace.CommitChanges(); //This line persi sts created object(s).
            ((ISecurityUserWithLoginInfo)sampleUser).CreateUserLoginInfo(SecurityDefaults.PasswordAuthentication, ObjectSpace.GetKeyValueAsString(sampleUser));
        }
        PermissionPolicyRole defaultRole = CreateDefaultRole();
        sampleUser.Roles.Add(defaultRole);

        ApplicationUser userAdmin = ObjectSpace.FirstOrDefault<ApplicationUser>(u => u.UserName == "Admin");
        if(userAdmin == null) {
            userAdmin = ObjectSpace.CreateObject<ApplicationUser>();
            userAdmin.UserName = "Admin";
            // Set a password if the standard authentication type is used
            userAdmin.SetPassword("");

            // The UserLoginInfo object requires a user object Id (Oid).
            // Commit the user object to the database before you create a UserLoginInfo object. This will correctly initialize the user key property.
            ObjectSpace.CommitChanges(); //This line persists created object(s).
            ((ISecurityUserWithLoginInfo)userAdmin).CreateUserLoginInfo(SecurityDefaults.PasswordAuthentication, ObjectSpace.GetKeyValueAsString(userAdmin));
        }
        // If a role with the Administrators name doesn't exist in the database, create this role
        PermissionPolicyRole adminRole = ObjectSpace.FirstOrDefault<PermissionPolicyRole>(r => r.Name == "Administrators");
        if(adminRole == null) {
            adminRole = ObjectSpace.CreateObject<PermissionPolicyRole>();
            adminRole.Name = "Administrators";
        }
        adminRole.IsAdministrative = true;
        userAdmin.Roles.Add(adminRole);


        var departments = ObjectSpace.GetObjectsCount(typeof(Department), null);
        if(departments == 0) {
            var dep1 = ObjectSpace.CreateObject<Department>();
            dep1.DepartmentName = "Department1";

            var dep2 = ObjectSpace.CreateObject<Department>();
            dep2.DepartmentName = "Department2";

            var depGoal11 = ObjectSpace.CreateObject<DepartmentGoal>();
            depGoal11.GoalName = "Goal11";
            depGoal11.Department = dep1;

            var depGoal12 = ObjectSpace.CreateObject<DepartmentGoal>();
            depGoal12.GoalName = "Goal12";
            depGoal12.Department = dep1;

            var depGoal21 = ObjectSpace.CreateObject<DepartmentGoal>();
            depGoal21.GoalName = "Goal21";
            depGoal21.Department = dep2;

            var depGoal22 = ObjectSpace.CreateObject<DepartmentGoal>();
            depGoal22.GoalName = "Goal22";
            depGoal22.Department = dep2;

            var departmentRole = CreateDepartmentUserRole();

            var user1 = CreateApplicationUser("user1");
            user1.Department = dep1;
            user1.Roles.Add(departmentRole);
            user1.Roles.Add(defaultRole);

            var user12 = CreateApplicationUser("user12");
            user12.Department = dep1;
            user12.Roles.Add(departmentRole);
            user12.Roles.Add(defaultRole);

            var user2 = CreateApplicationUser("user2");
            user2.Department = dep2;
            user2.Roles.Add(departmentRole);
            user2.Roles.Add(defaultRole);

            var user22 = CreateApplicationUser("user22");
            user22.Department = dep2;
            user22.Roles.Add(departmentRole);
            user22.Roles.Add(defaultRole);

            var managerRole = CreateDepartmentManagerRole();

            var manager1 = CreateApplicationUser("manager1");
            manager1.Department = dep1;
            manager1.Roles.Add(managerRole);
            manager1.Roles.Add(defaultRole);

            var manager2 = CreateApplicationUser("manager2");
            manager2.Department = dep2;
            manager2.Roles.Add(managerRole);
            manager2.Roles.Add(defaultRole);

            var task11 = ObjectSpace.CreateObject<MyTask>();
            task11.Subject = "Task11";
            task11.AssignedUser = user1;

            var task12 = ObjectSpace.CreateObject<MyTask>();
            task12.Subject = "Task12";
            task12.AssignedUser = user1;

            var task1Shared = ObjectSpace.CreateObject<MyTask>();
            task1Shared.Subject = "Task1Shared";
            task1Shared.IsSharedTask = true;
            task1Shared.AssignedUser = user1;

            var task121 = ObjectSpace.CreateObject<MyTask>();
            task121.Subject = "task121";
            task121.AssignedUser = user12;

            var task122 = ObjectSpace.CreateObject<MyTask>();
            task122.Subject = "task122";
            task122.AssignedUser = user12;

            var task12Shared = ObjectSpace.CreateObject<MyTask>();
            task12Shared.Subject = "task12Shared";
            task12Shared.IsSharedTask = true;
            task12Shared.AssignedUser = user12;

            var task21 = ObjectSpace.CreateObject<MyTask>();
            task21.Subject = "Task21";
            task21.AssignedUser = user2;

            var task22 = ObjectSpace.CreateObject<MyTask>();
            task22.Subject = "Task22";
            task22.AssignedUser = user2;

            var task2Shared = ObjectSpace.CreateObject<MyTask>();
            task2Shared.Subject = "Task2Shared";
            task2Shared.IsSharedTask = true;
            task2Shared.AssignedUser = user2;

            var task221 = ObjectSpace.CreateObject<MyTask>();
            task221.Subject = "task221";
            task221.AssignedUser = user22;

            var task222 = ObjectSpace.CreateObject<MyTask>();
            task222.Subject = "task222";
            task222.AssignedUser = user22;

            var task22Shared = ObjectSpace.CreateObject<MyTask>();
            task22Shared.Subject = "task22Shared";
            task22Shared.IsSharedTask = true;
            task22Shared.AssignedUser = user22;
        }


        ObjectSpace.CommitChanges(); //This line persists created object(s).
    }
 

    private ApplicationUser CreateApplicationUser(string _userName) {
        var appUser = ObjectSpace.CreateObject<ApplicationUser>();
        appUser.UserName = _userName;
        ObjectSpace.CommitChanges(); //This line persists created object(s).
        ((ISecurityUserWithLoginInfo)appUser).CreateUserLoginInfo(SecurityDefaults.PasswordAuthentication, ObjectSpace.GetKeyValueAsString(appUser));
        return appUser;
    }

    private PermissionPolicyRole CreateDepartmentManagerRole() {
        var role = ObjectSpace.CreateObject<PermissionPolicyRole>();
        role.Name = "DepartmentManagerRole";

        role.AddNavigationPermission("Application/NavigationItems/Items/Default/Items/MyDetails", SecurityPermissionState.Allow);
        role.AddNavigationPermission("Application/NavigationItems/Items/Default/Items/Department_ListView", SecurityPermissionState.Allow);
        role.AddNavigationPermission("Application/NavigationItems/Items/Default/Items/DepartmentGoal_ListView", SecurityPermissionState.Allow);
        role.AddNavigationPermission("Application/NavigationItems/Items/Default/Items/ApplicationUser_ListView", SecurityPermissionState.Allow);
        role.AddNavigationPermission("Application/NavigationItems/Items/Default/Items/MyTask_ListView", SecurityPermissionState.Allow);

        //Department
        role.AddObjectPermissionFromLambda<Department>(SecurityOperations.FullObjectAccess, d => d.ApplicationUsers.Any(au => au.ID == (int)CurrentUserIdOperator.CurrentUserId()), SecurityPermissionState.Allow);
        role.AddMemberPermissionFromLambda<Department>(SecurityOperations.ReadWriteAccess, nameof(Department.ApplicationUsers), d => d.ApplicationUsers.Any(au => au.ID == (int)CurrentUserIdOperator.CurrentUserId()), SecurityPermissionState.Allow);

        //ApplicationUser
        role.AddObjectPermissionFromLambda<ApplicationUser>(SecurityOperations.FullObjectAccess, au => au.ID == (int)CurrentUserIdOperator.CurrentUserId(), SecurityPermissionState.Allow);
        role.AddObjectPermissionFromLambda<ApplicationUser>(SecurityOperations.ReadWriteAccess, au => au.Department.ApplicationUsers.Any(au2 => au2.ID == (int)CurrentUserIdOperator.CurrentUserId()), SecurityPermissionState.Allow);

        //MyTask
        role.AddObjectPermissionFromLambda<MyTask>(SecurityOperations.FullObjectAccess, t => t.AssignedUser.Department.ApplicationUsers.Any(au2 => au2.ID == (int)CurrentUserIdOperator.CurrentUserId()), SecurityPermissionState.Allow);

        //DepartmentGoal
        role.AddObjectPermissionFromLambda<DepartmentGoal>(SecurityOperations.FullObjectAccess, dr => dr.Department.ApplicationUsers.Any(au => au.ID == (int)CurrentUserIdOperator.CurrentUserId()), SecurityPermissionState.Allow);

        return role;
    }

        private PermissionPolicyRole CreateDepartmentUserRole() {
        var role = ObjectSpace.CreateObject<PermissionPolicyRole>();
        role.Name = "DepartmentUserRole";
        role.AddNavigationPermission("Application/NavigationItems/Items/Default/Items/MyDetails", SecurityPermissionState.Allow);
        role.AddNavigationPermission("Application/NavigationItems/Items/Default/Items/Department_ListView", SecurityPermissionState.Allow);
        role.AddNavigationPermission("Application/NavigationItems/Items/Default/Items/DepartmentGoal_ListView", SecurityPermissionState.Allow);
        role.AddNavigationPermission("Application/NavigationItems/Items/Default/Items/ApplicationUser_ListView", SecurityPermissionState.Allow);
        role.AddNavigationPermission("Application/NavigationItems/Items/Default/Items/MyTask_ListView", SecurityPermissionState.Allow);

        //Department
        role.AddObjectPermissionFromLambda<Department>(SecurityOperations.Read, d => d.ApplicationUsers.Any(au => au.ID == (int)CurrentUserIdOperator.CurrentUserId()), SecurityPermissionState.Allow);

        //ApplicationUser
        role.AddObjectPermissionFromLambda<ApplicationUser>(SecurityOperations.Read, au => au.ID == (int)CurrentUserIdOperator.CurrentUserId(), SecurityPermissionState.Allow);
        role.AddObjectPermissionFromLambda<ApplicationUser>(SecurityOperations.Read, au => au.Department.ApplicationUsers.Any(au2 => au2.ID == (int)CurrentUserIdOperator.CurrentUserId()), SecurityPermissionState.Allow);

        //MyTask
        role.AddObjectPermissionFromLambda<MyTask>(SecurityOperations.Read, t => t.AssignedUser.ID == (int)CurrentUserIdOperator.CurrentUserId(), SecurityPermissionState.Allow);
        role.AddObjectPermissionFromLambda<MyTask>(SecurityOperations.Read, t => t.AssignedUser.Department.ApplicationUsers.Any(au => au.ID == (int)CurrentUserIdOperator.CurrentUserId()) && t.IsSharedTask, SecurityPermissionState.Allow);

        //DepartmentGoal
        role.AddObjectPermissionFromLambda<DepartmentGoal>(SecurityOperations.Read, dr => dr.Department.ApplicationUsers.Any(au => au.ID == (int)CurrentUserIdOperator.CurrentUserId()), SecurityPermissionState.Allow);

        return role;
    }
    private PermissionPolicyRole CreateDefaultRole() {
        PermissionPolicyRole defaultRole = ObjectSpace.FirstOrDefault<PermissionPolicyRole>(role => role.Name == "Default");
        if(defaultRole == null) {
            defaultRole = ObjectSpace.CreateObject<PermissionPolicyRole>();
            defaultRole.Name = "Default";

            defaultRole.AddObjectPermissionFromLambda<ApplicationUser>(SecurityOperations.Read, cm => cm.ID == (int)CurrentUserIdOperator.CurrentUserId(), SecurityPermissionState.Allow);
            defaultRole.AddNavigationPermission(@"Application/NavigationItems/Items/Default/Items/MyDetails", SecurityPermissionState.Allow);
            defaultRole.AddMemberPermissionFromLambda<ApplicationUser>(SecurityOperations.Write, "ChangePasswordOnFirstLogon", cm => cm.ID == (int)CurrentUserIdOperator.CurrentUserId(), SecurityPermissionState.Allow);
            defaultRole.AddMemberPermissionFromLambda<ApplicationUser>(SecurityOperations.Write, "StoredPassword", cm => cm.ID == (int)CurrentUserIdOperator.CurrentUserId(), SecurityPermissionState.Allow);
            defaultRole.AddTypePermissionsRecursively<PermissionPolicyRole>(SecurityOperations.Read, SecurityPermissionState.Deny);
            defaultRole.AddTypePermissionsRecursively<ModelDifference>(SecurityOperations.ReadWriteAccess, SecurityPermissionState.Allow);
            defaultRole.AddTypePermissionsRecursively<ModelDifferenceAspect>(SecurityOperations.ReadWriteAccess, SecurityPermissionState.Allow);
            defaultRole.AddTypePermissionsRecursively<ModelDifference>(SecurityOperations.Create, SecurityPermissionState.Allow);
            defaultRole.AddTypePermissionsRecursively<ModelDifferenceAspect>(SecurityOperations.Create, SecurityPermissionState.Allow);
        }
        return defaultRole;
    }
}
