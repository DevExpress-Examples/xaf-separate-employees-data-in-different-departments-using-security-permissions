Imports DevExpress.Data.Filtering
Imports DevExpress.ExpressApp
Imports DevExpress.ExpressApp.Security
Imports DevExpress.ExpressApp.SystemModule
Imports DevExpress.Persistent.Base
Imports DevExpress.Persistent.Base.General
Imports DevExpress.Persistent.BaseImpl.PermissionPolicy
Imports MainDemo.[Module].BusinessObjects
Imports System

Namespace MainDemo.[Module].DatabaseUpdate
    Public Class Updater
        Inherits DevExpress.ExpressApp.Updating.ModuleUpdater

        Public Sub New(ByVal objectSpace As IObjectSpace, ByVal currentDBVersion As Version)
            MyBase.New(objectSpace, currentDBVersion)
        End Sub

        Public Overrides Sub UpdateDatabaseAfterUpdateSchema()
            MyBase.UpdateDatabaseAfterUpdateSchema()
            Dim devDepartment As Department = ObjectSpace.FindObject(Of Department)(CriteriaOperator.Parse("Title == 'R&D'"))

            If devDepartment Is Nothing Then
                devDepartment = ObjectSpace.CreateObject(Of Department)()
                devDepartment.Title = "R&D"
                devDepartment.Office = "1"
                devDepartment.Save()
            End If

            Dim supDepartment As Department = ObjectSpace.FindObject(Of Department)(CriteriaOperator.Parse("Title == 'Technical Support'"))

            If supDepartment Is Nothing Then
                supDepartment = ObjectSpace.CreateObject(Of Department)()
                supDepartment.Title = "Technical Support"
                supDepartment.Office = "2"
                supDepartment.Save()
            End If

            Dim mngDepartment As Department = ObjectSpace.FindObject(Of Department)(CriteriaOperator.Parse("Title == 'Management'"))

            If mngDepartment Is Nothing Then
                mngDepartment = ObjectSpace.CreateObject(Of Department)()
                mngDepartment.Title = "Management"
                mngDepartment.Office = "3"
                mngDepartment.Save()
            End If

            Dim administrator As Employee = ObjectSpace.FindObject(Of Employee)(CriteriaOperator.Parse("UserName == 'Admin'"))

            If administrator Is Nothing Then
                administrator = ObjectSpace.CreateObject(Of Employee)()
                administrator.UserName = "Admin"
                administrator.FirstName = "Admin"
                administrator.LastName = "Admin"
                administrator.Department = mngDepartment
                administrator.IsActive = True
                administrator.SetPassword("")
                administrator.Roles.Add(GetAdministratorRole())
                administrator.Save()
            End If

            Dim managerSam As Employee = ObjectSpace.FindObject(Of Employee)(CriteriaOperator.Parse("UserName == 'Sam'"))

            If managerSam Is Nothing Then
                managerSam = ObjectSpace.CreateObject(Of Employee)()
                managerSam.UserName = "Sam"
                managerSam.FirstName = "Sam"
                managerSam.LastName = "Jackson"
                managerSam.IsActive = True
                managerSam.SetPassword("")
                managerSam.Department = devDepartment
                managerSam.Roles.Add(GetManagerRole())
                managerSam.Save()
            End If

            Dim userJohn As Employee = ObjectSpace.FindObject(Of Employee)(CriteriaOperator.Parse("UserName == 'John'"))

            If userJohn Is Nothing Then
                userJohn = ObjectSpace.CreateObject(Of Employee)()
                userJohn.UserName = "John"
                userJohn.FirstName = "John"
                userJohn.LastName = "Doe"
                userJohn.IsActive = True
                userJohn.SetPassword("")
                userJohn.Department = devDepartment
                userJohn.Roles.Add(GetUserRole())
                userJohn.Save()
            End If

            Dim managerMary As Employee = ObjectSpace.FindObject(Of Employee)(CriteriaOperator.Parse("UserName == 'Mary'"))

            If managerMary Is Nothing Then
                managerMary = ObjectSpace.CreateObject(Of Employee)()
                managerMary.UserName = "Mary"
                managerMary.FirstName = "Mary"
                managerMary.LastName = "Tellinson"
                managerMary.IsActive = True
                managerMary.SetPassword("")
                managerMary.Department = supDepartment
                managerMary.Roles.Add(GetManagerRole())
                managerMary.Save()
            End If

            Dim userJoe As Employee = ObjectSpace.FindObject(Of Employee)(CriteriaOperator.Parse("UserName == 'Joe'"))

            If userJoe Is Nothing Then
                userJoe = ObjectSpace.CreateObject(Of Employee)()
                userJoe.UserName = "Joe"
                userJoe.FirstName = "Joe"
                userJoe.LastName = "Pitt"
                userJoe.IsActive = True
                userJoe.SetPassword("")
                userJoe.Department = supDepartment
                userJoe.Roles.Add(GetUserRole())
                userJoe.Save()
            End If

            If ObjectSpace.FindObject(Of EmployeeTask)(CriteriaOperator.Parse("Subject == 'Do homework'")) Is Nothing Then
                Dim task As EmployeeTask = ObjectSpace.CreateObject(Of EmployeeTask)()
                task.Subject = "Do homework"
                task.AssignedTo = managerSam
                task.DueDate = DateTime.Now
                task.Status = TaskStatus.NotStarted
                task.Description = "This is a task for Sam"
                task.Save()
            End If

            If ObjectSpace.FindObject(Of EmployeeTask)(CriteriaOperator.Parse("Subject == 'Prepare coffee for everyone'")) Is Nothing Then
                Dim task As EmployeeTask = ObjectSpace.CreateObject(Of EmployeeTask)()
                task.Subject = "Prepare coffee for everyone"
                task.AssignedTo = userJohn
                task.DueDate = DateTime.Now
                task.Status = TaskStatus.InProgress
                task.Description = "This is a task for John"
                task.Save()
            End If

            If ObjectSpace.FindObject(Of EmployeeTask)(CriteriaOperator.Parse("Subject == 'Read latest news'")) Is Nothing Then
                Dim task As EmployeeTask = ObjectSpace.CreateObject(Of EmployeeTask)()
                task.Subject = "Read latest news"
                task.AssignedTo = managerMary
                task.DueDate = DateTime.Now
                task.Status = TaskStatus.Completed
                task.Description = "This is a task for Mary"
                task.Save()
            End If

            If ObjectSpace.FindObject(Of EmployeeTask)(CriteriaOperator.Parse("Subject == 'Book tickets'")) Is Nothing Then
                Dim task As EmployeeTask = ObjectSpace.CreateObject(Of EmployeeTask)()
                task.Subject = "Book tickets"
                task.AssignedTo = userJoe
                task.DueDate = DateTime.Now
                task.Status = TaskStatus.Deferred
                task.Description = "This is a task for Joe"
                task.Save()
            End If

            ObjectSpace.CommitChanges()
        End Sub

        Private Function GetAdministratorRole() As PermissionPolicyRole
            Dim administratorRole As PermissionPolicyRole = ObjectSpace.FindObject(Of PermissionPolicyRole)(New BinaryOperator("Name", "Administrators"))

            If administratorRole Is Nothing Then
                administratorRole = ObjectSpace.CreateObject(Of PermissionPolicyRole)()
                administratorRole.Name = "Administrators"
                administratorRole.IsAdministrative = True
            End If

            Return administratorRole
        End Function

        Private Function GetUserRole() As PermissionPolicyRole
            Dim userRole As PermissionPolicyRole = ObjectSpace.FindObject(Of PermissionPolicyRole)(New BinaryOperator("Name", "Users"))

            If userRole Is Nothing Then
                userRole = ObjectSpace.CreateObject(Of PermissionPolicyRole)()
                userRole.Name = "Users"
                userRole.AddNavigationPermission("Application/NavigationItems/Items/Default/Items/MyDetails", SecurityPermissionState.Allow)
                userRole.AddNavigationPermission("Application/NavigationItems/Items/Default/Items/Employee_ListView", SecurityPermissionState.Allow)
                userRole.AddNavigationPermission("Application/NavigationItems/Items/Default/Items/EmployeeTask_ListView", SecurityPermissionState.Allow)
                userRole.AddObjectPermission(Of Employee)(SecurityOperations.Read, "Department.Employees[Oid = CurrentUserId()]", SecurityPermissionState.Allow)
                userRole.AddMemberPermission(Of Employee)(SecurityOperations.Write, "ChangePasswordOnFirstLogon;StoredPassword;FirstName;LastName", "Oid=CurrentUserId()", SecurityPermissionState.Allow)
                userRole.AddMemberPermission(Of Employee)(SecurityOperations.Write, "Tasks", "Department.Employees[Oid = CurrentUserId()]", SecurityPermissionState.Allow)
                userRole.SetTypePermission(Of PermissionPolicyRole)(SecurityOperations.Read, SecurityPermissionState.Allow)
                userRole.AddObjectPermission(Of EmployeeTask)(SecurityOperations.ReadWriteAccess, "AssignedTo.Department.Employees[Oid = CurrentUserId()]", SecurityPermissionState.Allow)
                userRole.AddMemberPermission(Of EmployeeTask)(SecurityOperations.Write, "AssignedTo", "AssignedTo.Department.Employees[Oid = CurrentUserId()]", SecurityPermissionState.Allow)
                userRole.AddObjectPermission(Of Department)(SecurityOperations.Read, "Employees[Oid=CurrentUserId()]", SecurityPermissionState.Allow)
            End If

            Return userRole
        End Function

        Private Function GetManagerRole() As PermissionPolicyRole
            Dim managerRole As PermissionPolicyRole = ObjectSpace.FindObject(Of PermissionPolicyRole)(New BinaryOperator("Name", "Managers"))

            If managerRole Is Nothing Then
                managerRole = ObjectSpace.CreateObject(Of PermissionPolicyRole)()
                managerRole.Name = "Managers"
                managerRole.AddNavigationPermission("Application/NavigationItems/Items/Default/Items/MyDetails", SecurityPermissionState.Allow)
                managerRole.AddNavigationPermission("Application/NavigationItems/Items/Default/Items/Department_ListView", SecurityPermissionState.Allow)
                managerRole.AddNavigationPermission("Application/NavigationItems/Items/Default/Items/Employee_ListView", SecurityPermissionState.Allow)
                managerRole.AddNavigationPermission("Application/NavigationItems/Items/Default/Items/EmployeeTask_ListView", SecurityPermissionState.Allow)
                managerRole.AddObjectPermission(Of Department)(SecurityOperations.FullObjectAccess, "Employees[Oid=CurrentUserId()]", SecurityPermissionState.Allow)
                managerRole.SetTypePermission(Of Employee)(SecurityOperations.Create, SecurityPermissionState.Allow)
                managerRole.AddObjectPermission(Of Employee)(SecurityOperations.FullObjectAccess, "IsNull(Department) || Department.Employees[Oid=CurrentUserId()]", SecurityPermissionState.Allow)
                managerRole.SetTypePermission(Of EmployeeTask)(SecurityOperations.Create, SecurityPermissionState.Allow)
                managerRole.AddObjectPermission(Of EmployeeTask)(SecurityOperations.FullObjectAccess, "IsNull(AssignedTo) || IsNull(AssignedTo.Department) || AssignedTo.Department.Employees[Oid=CurrentUserId()]", SecurityPermissionState.Allow)
                managerRole.SetTypePermission(Of PermissionPolicyRole)(SecurityOperations.Read, SecurityPermissionState.Allow)
            End If

            Return managerRole
        End Function
    End Class
End Namespace
