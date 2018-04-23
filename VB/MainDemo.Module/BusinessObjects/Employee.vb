Imports System
Imports DevExpress.Xpo
Imports System.ComponentModel
Imports DevExpress.Persistent.Base
Imports System.Collections.Generic
Imports DevExpress.ExpressApp.Security.Strategy
Imports DevExpress.Persistent.Validation

Namespace MainDemo.Module.BusinessObjects
	<DefaultClassOptions, DefaultProperty("FullName"), ImageName("BO_User")>
	Public Class Employee
		Inherits SecuritySystemUser

		Private _LastName As String
		Private _FirstName As String
        Private _department As Department
		Public Sub New(ByVal session As Session)
			MyBase.New(session)
		End Sub
		Public Property FirstName() As String
			Get
				Return _FirstName
			End Get
			Set(ByVal value As String)
				SetPropertyValue("FirstName", _FirstName, value)
			End Set
		End Property
		Public Property LastName() As String
			Get
				Return _LastName
			End Get
			Set(ByVal value As String)
				SetPropertyValue("LastName", _LastName, value)
			End Set
		End Property
		<PersistentAlias("concat(FirstName, ' ', LastName)")>
		Public ReadOnly Property FullName() As String
			Get
				Return Convert.ToString(EvaluateAlias("FullName"))
			End Get
		End Property
		<Association, RuleRequiredField>
		Public Property Department() As Department
			Get
                Return _department
			End Get
			Set(ByVal value As Department)
                SetPropertyValue("Department", _department, value)
			End Set
		End Property
		<Association>
		Public ReadOnly Property Tasks() As XPCollection(Of EmployeeTask)
			Get
				Return GetCollection(Of EmployeeTask)("Tasks")
			End Get
		End Property
	End Class
End Namespace
