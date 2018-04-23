Imports System
Imports DevExpress.ExpressApp
Imports DevExpress.Data.Filtering
Imports MainDemo.Module.BusinessObjects
Imports DevExpress.ExpressApp.SystemModule
Imports DevExpress.Persistent.Base.General
Imports DevExpress.ExpressApp.Security.Strategy

Namespace MainDemo.Module.DatabaseUpdate
	Public Class Updater
		Inherits DevExpress.ExpressApp.Updating.ModuleUpdater

		Public Sub New(ByVal objectSpace As IObjectSpace, ByVal currentDBVersion As Version)
			MyBase.New(objectSpace, currentDBVersion)
		End Sub
		Public Overrides Sub UpdateDatabaseAfterUpdateSchema()
			MyBase.UpdateDatabaseAfterUpdateSchema()
			'Create departments.
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
			'Create employees.
			'Admin is a god that can do everything.
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
			'Sam is a manager and he can do everything with his own department
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
			'John is an ordinary user within the Sam's department.
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
			'Mary is a manager of another department.  
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
			'Joe is an ordinary user within the Mary's department.
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
			'Create tasks for employees.
			If ObjectSpace.FindObject(Of EmployeeTask)(CriteriaOperator.Parse("Subject == 'Do homework'")) Is Nothing Then
				Dim task As EmployeeTask = ObjectSpace.CreateObject(Of EmployeeTask)()
				task.Subject = "Do homework"
				task.AssignedTo = managerSam
				task.DueDate = Date.Now
				task.Status = TaskStatus.NotStarted
				task.Description = "This is a task for Sam"
				task.Save()
			End If
			If ObjectSpace.FindObject(Of EmployeeTask)(CriteriaOperator.Parse("Subject == 'Prepare coffee for everyone'")) Is Nothing Then
				Dim task As EmployeeTask = ObjectSpace.CreateObject(Of EmployeeTask)()
				task.Subject = "Prepare coffee for everyone"
				task.AssignedTo = userJohn
				task.DueDate = Date.Now
				task.Status = TaskStatus.InProgress
				task.Description = "This is a task for John"
				task.Save()
			End If
			If ObjectSpace.FindObject(Of EmployeeTask)(CriteriaOperator.Parse("Subject == 'Read latest news'")) Is Nothing Then
				Dim task As EmployeeTask = ObjectSpace.CreateObject(Of EmployeeTask)()
				task.Subject = "Read latest news"
				task.AssignedTo = managerMary
				task.DueDate = Date.Now
				task.Status = TaskStatus.Completed
				task.Description = "This is a task for Mary"
				task.Save()
			End If
			If ObjectSpace.FindObject(Of EmployeeTask)(CriteriaOperator.Parse("Subject == 'Book tickets'")) Is Nothing Then
				Dim task As EmployeeTask = ObjectSpace.CreateObject(Of EmployeeTask)()
				task.Subject = "Book tickets"
				task.AssignedTo = userJoe
				task.DueDate = Date.Now
				task.Status = TaskStatus.Deferred
				task.Description = "This is a task for Joe"
				task.Save()
			End If
			ObjectSpace.CommitChanges()
		End Sub

		'Administrators can do everything within the application.
		Private Function GetAdministratorRole() As SecuritySystemRole
			Dim administratorRole As SecuritySystemRole = ObjectSpace.FindObject(Of SecuritySystemRole)(New BinaryOperator("Name", "Administrators"))
			If administratorRole Is Nothing Then
				administratorRole = ObjectSpace.CreateObject(Of SecuritySystemRole)()
				administratorRole.Name = "Administrators"
				'Can access everything.
				administratorRole.IsAdministrative = True
			End If
			Return administratorRole
		End Function
		'Users can access and partially edit data (no create and delete capabilities) from their own department.
		Private Function GetUserRole() As SecuritySystemRole
			Dim userRole As SecuritySystemRole = ObjectSpace.FindObject(Of SecuritySystemRole)(New BinaryOperator("Name", "Users"))
			If userRole Is Nothing Then
				userRole = ObjectSpace.CreateObject(Of SecuritySystemRole)()
				userRole.Name = "Users"

				Dim userTypePermission As SecuritySystemTypePermissionObject = ObjectSpace.CreateObject(Of SecuritySystemTypePermissionObject)()
				userTypePermission.TargetType = GetType(Employee)
				userRole.TypePermissions.Add(userTypePermission)

				Dim canViewEmployeesFromOwnDepartmentObjectPermission As SecuritySystemObjectPermissionsObject = ObjectSpace.CreateObject(Of SecuritySystemObjectPermissionsObject)()
				canViewEmployeesFromOwnDepartmentObjectPermission.Criteria = "Department.Employees[Oid = CurrentUserId()]"
				'canViewEmployeesFromOwnDepartmentObjectPermission.Criteria = new BinaryOperator(new OperandProperty("Department.Oid"), currentlyLoggedEmployeeDepartmemntOid, BinaryOperatorType.Equal).ToString();
				canViewEmployeesFromOwnDepartmentObjectPermission.AllowNavigate = True
				canViewEmployeesFromOwnDepartmentObjectPermission.AllowRead = True
				userTypePermission.ObjectPermissions.Add(canViewEmployeesFromOwnDepartmentObjectPermission)

				Dim canEditOwnUserMemberPermission As SecuritySystemMemberPermissionsObject = ObjectSpace.CreateObject(Of SecuritySystemMemberPermissionsObject)()
				canEditOwnUserMemberPermission.Members = "ChangePasswordOnFirstLogon; StoredPassword; FirstName; LastName;"
				canEditOwnUserMemberPermission.Criteria = "Oid=CurrentUserId()"
				canEditOwnUserMemberPermission.Criteria = (New OperandProperty("Oid") = New FunctionOperator(CurrentUserIdOperator.OperatorName)).ToString()
				canEditOwnUserMemberPermission.AllowWrite = True
				userTypePermission.MemberPermissions.Add(canEditOwnUserMemberPermission)

				Dim canEditUserAssociationsFromOwnDepartmentMemberPermission As SecuritySystemMemberPermissionsObject = ObjectSpace.CreateObject(Of SecuritySystemMemberPermissionsObject)()
				canEditUserAssociationsFromOwnDepartmentMemberPermission.Members = "Tasks; Department;"
				canEditUserAssociationsFromOwnDepartmentMemberPermission.Criteria = "Department.Employees[Oid = CurrentUserId()]"
				'canEditUserAssociationsFromOwnDepartmentMemberPermission.Criteria = new BinaryOperator(new OperandProperty("Department.Oid"), currentlyLoggedEmployeeDepartmemntOid, BinaryOperatorType.Equal).ToString();
				canEditUserAssociationsFromOwnDepartmentMemberPermission.AllowWrite = True
				userTypePermission.MemberPermissions.Add(canEditUserAssociationsFromOwnDepartmentMemberPermission)


				Dim roleTypePermission As SecuritySystemTypePermissionObject = ObjectSpace.CreateObject(Of SecuritySystemTypePermissionObject)()
				roleTypePermission.TargetType = GetType(SecuritySystemRole)
				roleTypePermission.AllowRead = True
				userRole.TypePermissions.Add(roleTypePermission)


				Dim taskTypePermission As SecuritySystemTypePermissionObject = ObjectSpace.CreateObject(Of SecuritySystemTypePermissionObject)()
				taskTypePermission.TargetType = GetType(EmployeeTask)
				taskTypePermission.AllowNavigate = True
				userRole.TypePermissions.Add(taskTypePermission)

				Dim canEditTaskAssociationsMemberPermission As SecuritySystemMemberPermissionsObject = ObjectSpace.CreateObject(Of SecuritySystemMemberPermissionsObject)()
				canEditTaskAssociationsMemberPermission.Members = "AssignedTo;"
				canEditTaskAssociationsMemberPermission.Criteria = "AssignedTo.Department.Oid=[<Employee>][Oid=CurrentUserId()].Single(Department.Oid)"
				canEditTaskAssociationsMemberPermission.Criteria = "AssignedTo.Department.Employees[Oid = CurrentUserId()]"
				'canEditTaskAssociationsMemberPermission.Criteria = new BinaryOperator(new OperandProperty("AssignedTo.Department.Oid"), currentlyLoggedEmployeeDepartmemntOid, BinaryOperatorType.Equal).ToString();
				canEditTaskAssociationsMemberPermission.AllowWrite = True
				taskTypePermission.MemberPermissions.Add(canEditTaskAssociationsMemberPermission)

				Dim canyEditTasksFromOwnDepartmentObjectPermission As SecuritySystemObjectPermissionsObject = ObjectSpace.CreateObject(Of SecuritySystemObjectPermissionsObject)()
				canyEditTasksFromOwnDepartmentObjectPermission.Criteria = "AssignedTo.Department.Oid=[<Employee>][Oid=CurrentUserId()].Single(Department.Oid)"
				canyEditTasksFromOwnDepartmentObjectPermission.Criteria = "AssignedTo.Department.Employees[Oid = CurrentUserId()]"
				'canyEditTasksFromOwnDepartmentObjectPermission.Criteria = new BinaryOperator(new OperandProperty("AssignedTo.Department.Oid"), currentlyLoggedEmployeeDepartmemntOid, BinaryOperatorType.Equal).ToString();
				canyEditTasksFromOwnDepartmentObjectPermission.AllowNavigate = True
				canyEditTasksFromOwnDepartmentObjectPermission.AllowWrite = True
				canyEditTasksFromOwnDepartmentObjectPermission.AllowRead = True
				taskTypePermission.ObjectPermissions.Add(canyEditTasksFromOwnDepartmentObjectPermission)

				Dim departmentTypePermission As SecuritySystemTypePermissionObject = ObjectSpace.CreateObject(Of SecuritySystemTypePermissionObject)()
				departmentTypePermission.TargetType = GetType(Department)
				userRole.TypePermissions.Add(departmentTypePermission)

				Dim canViewOwnDepartmentObjectPermission As SecuritySystemObjectPermissionsObject = ObjectSpace.CreateObject(Of SecuritySystemObjectPermissionsObject)()
				canViewOwnDepartmentObjectPermission.Criteria = "Oid=[<Employee>][Oid=CurrentUserId()].Single(Department.Oid)"
				canViewOwnDepartmentObjectPermission.Criteria = "Employees[Oid=CurrentUserId()]"
				'canViewOwnDepartmentObjectPermission.Criteria = new BinaryOperator(new OperandProperty("Oid"), currentlyLoggedEmployeeDepartmemntOid, BinaryOperatorType.Equal).ToString();
				canViewOwnDepartmentObjectPermission.AllowNavigate = True
				canViewOwnDepartmentObjectPermission.AllowRead = True
				canViewOwnDepartmentObjectPermission.Save()
				departmentTypePermission.ObjectPermissions.Add(canViewOwnDepartmentObjectPermission)

				Dim canEditAssociationsMemberPermission As SecuritySystemMemberPermissionsObject = ObjectSpace.CreateObject(Of SecuritySystemMemberPermissionsObject)()
				canEditAssociationsMemberPermission.Members = "Employees;"
				canEditAssociationsMemberPermission.Criteria = "Oid=[<Employee>][Oid=CurrentUserId()].Single(Department.Oid)"
				canEditAssociationsMemberPermission.Criteria = "Employees[Oid=CurrentUserId()]"
				'canEditAssociationsMemberPermission.Criteria = new BinaryOperator(new OperandProperty("Oid"), currentlyLoggedEmployeeDepartmemntOid, BinaryOperatorType.Equal).ToString();
				canEditAssociationsMemberPermission.AllowWrite = True
				departmentTypePermission.MemberPermissions.Add(canEditAssociationsMemberPermission)
			End If
			Return userRole
		End Function
		'Managers can access and fully edit (including create and delete capabilities) data from their own department. However, they cannot access data from other departments.
		Private Function GetManagerRole() As SecuritySystemRole
			Dim managerRole As SecuritySystemRole = ObjectSpace.FindObject(Of SecuritySystemRole)(New BinaryOperator("Name", "Managers"))
			If managerRole Is Nothing Then
				managerRole = ObjectSpace.CreateObject(Of SecuritySystemRole)()
				managerRole.Name = "Managers"
				managerRole.ChildRoles.Add(GetUserRole())


				Dim departmentTypePermission As SecuritySystemTypePermissionObject = ObjectSpace.CreateObject(Of SecuritySystemTypePermissionObject)()
				departmentTypePermission.TargetType = GetType(Department)
				departmentTypePermission.AllowNavigate = True
				managerRole.TypePermissions.Add(departmentTypePermission)

				Dim canEditOwnDepartmentObjectPermission As SecuritySystemObjectPermissionsObject = ObjectSpace.CreateObject(Of SecuritySystemObjectPermissionsObject)()
				canEditOwnDepartmentObjectPermission.Criteria = "Oid=[<Employee>][Oid=CurrentUserId()].Single(Department.Oid)"
				canEditOwnDepartmentObjectPermission.Criteria = "Employees[Oid=CurrentUserId()]"
				'canEditOwnDepartmentObjectPermission.Criteria = new BinaryOperator(new OperandProperty("Oid"), currentlyLoggedEmployeeDepartmemntOid, BinaryOperatorType.Equal).ToString();
				canEditOwnDepartmentObjectPermission.AllowNavigate = True
				canEditOwnDepartmentObjectPermission.AllowRead = True
				canEditOwnDepartmentObjectPermission.AllowWrite = True
				canEditOwnDepartmentObjectPermission.AllowDelete = True
				canEditOwnDepartmentObjectPermission.Save()
				departmentTypePermission.ObjectPermissions.Add(canEditOwnDepartmentObjectPermission)

				Dim employeeTypePermission As SecuritySystemTypePermissionObject = ObjectSpace.CreateObject(Of SecuritySystemTypePermissionObject)()
				employeeTypePermission.TargetType = GetType(Employee)
				employeeTypePermission.AllowNavigate = True
				employeeTypePermission.AllowCreate = True
				managerRole.TypePermissions.Add(employeeTypePermission)
				Dim canEditEmployeesFromOwnDepartmentObjectPermission As SecuritySystemObjectPermissionsObject = ObjectSpace.CreateObject(Of SecuritySystemObjectPermissionsObject)()
				canEditEmployeesFromOwnDepartmentObjectPermission.Criteria = "IsNull(Department) || Department.Oid=[<Employee>][Oid=CurrentUserId()].Single(Department.Oid)"
				canEditEmployeesFromOwnDepartmentObjectPermission.Criteria = "IsNull(Department) || Department.Employees[Oid=CurrentUserId()]"
				'canEditEmployeesFromOwnDepartmentObjectPermission.Criteria = (new NullOperator(new OperandProperty("Department")) | new BinaryOperator(new OperandProperty("Department.Oid"), currentlyLoggedEmployeeDepartmemntOid, BinaryOperatorType.Equal)).ToString();
				canEditEmployeesFromOwnDepartmentObjectPermission.AllowWrite = True
				canEditEmployeesFromOwnDepartmentObjectPermission.AllowDelete = True
				canEditEmployeesFromOwnDepartmentObjectPermission.AllowNavigate = True
				canEditEmployeesFromOwnDepartmentObjectPermission.AllowRead = True
				canEditEmployeesFromOwnDepartmentObjectPermission.Save()
				employeeTypePermission.ObjectPermissions.Add(canEditEmployeesFromOwnDepartmentObjectPermission)

				Dim taskTypePermission As SecuritySystemTypePermissionObject = ObjectSpace.CreateObject(Of SecuritySystemTypePermissionObject)()
				taskTypePermission.TargetType = GetType(EmployeeTask)
				taskTypePermission.AllowNavigate = True
				taskTypePermission.AllowCreate = True
				managerRole.TypePermissions.Add(taskTypePermission)
				Dim canEditTasksOnlyFromOwnDepartmentObjectPermission As SecuritySystemObjectPermissionsObject = ObjectSpace.CreateObject(Of SecuritySystemObjectPermissionsObject)()
				canEditTasksOnlyFromOwnDepartmentObjectPermission.Criteria = "IsNull(AssignedTo) || IsNull(AssignedTo.Department) || AssignedTo.Department.Oid=[<Employee>][Oid=CurrentUserId()].Single(Department.Oid)"
				canEditTasksOnlyFromOwnDepartmentObjectPermission.Criteria = "IsNull(AssignedTo) || IsNull(AssignedTo.Department) || AssignedTo.Department.Employees[Oid=CurrentUserId()]"
				'canEditTasksOnlyFromOwnDepartmentObjectPermission.Criteria = (new NullOperator(new OperandProperty("AssignedTo")) | new NullOperator(new OperandProperty("AssignedTo.Department")) | new BinaryOperator(new OperandProperty("AssignedTo.Department.Oid"), currentlyLoggedEmployeeDepartmemntOid, BinaryOperatorType.Equal)).ToString();
				canEditTasksOnlyFromOwnDepartmentObjectPermission.AllowNavigate = True
				canEditTasksOnlyFromOwnDepartmentObjectPermission.AllowRead = True
				canEditTasksOnlyFromOwnDepartmentObjectPermission.AllowWrite = True
				canEditTasksOnlyFromOwnDepartmentObjectPermission.AllowDelete = True
				canEditTasksOnlyFromOwnDepartmentObjectPermission.Save()
				taskTypePermission.ObjectPermissions.Add(canEditTasksOnlyFromOwnDepartmentObjectPermission)
			End If
			Return managerRole
		End Function
	End Class
End Namespace
