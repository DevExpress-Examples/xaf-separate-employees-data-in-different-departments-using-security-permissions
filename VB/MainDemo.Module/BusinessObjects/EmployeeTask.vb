Imports System
Imports DevExpress.Xpo
Imports System.ComponentModel
Imports DevExpress.Persistent.Base
Imports DevExpress.ExpressApp.Model
Imports DevExpress.Persistent.BaseImpl
Imports DevExpress.Persistent.Base.General
Imports DevExpress.Persistent.Validation

Namespace MainDemo.Module.BusinessObjects
	<DefaultClassOptions, ImageName("BO_Task"), DefaultProperty("Subject"), ModelDefault("Caption", "Task")>
	Public Class EmployeeTask
		Inherits BaseObject

		Private _Description As String
		Private _Subject As String
		Private _DueDate As Date
		Private _Status As TaskStatus
		Private _AssignedTo As Employee
		Public Sub New(ByVal session As Session)
			MyBase.New(session)
		End Sub
		<Size(255)>
		Public Property Subject() As String
			Get
				Return _Subject
			End Get
			Set(ByVal value As String)
				SetPropertyValue("Subject", _Subject, value)
			End Set
		End Property
		Public Property Status() As TaskStatus
			Get
				Return _Status
			End Get
			Set(ByVal value As TaskStatus)
				SetPropertyValue("Status", _Status, value)
			End Set
		End Property
		<Size(SizeAttribute.Unlimited)>
		Public Property Description() As String
			Get
				Return _Description
			End Get
			Set(ByVal value As String)
				SetPropertyValue("Description", _Description, value)
			End Set
		End Property
		Public Property DueDate() As Date
			Get
				Return _DueDate
			End Get
			Set(ByVal value As Date)
				SetPropertyValue("DueDate", _DueDate, value)
			End Set
		End Property
		<Action(ToolTip := "Postpone the task to the next day", ImageName := "State_Task_Deferred")>
		Public Sub Postpone()
			If DueDate = Date.MinValue Then
				DueDate = Date.Now
			End If
			DueDate = DueDate.Add(TimeSpan.FromDays(1))
		End Sub
		<Association, RuleRequiredField>
		Public Property AssignedTo() As Employee
			Get
				Return _AssignedTo
			End Get
			Set(ByVal value As Employee)
				SetPropertyValue("AssignedTo", _AssignedTo, value)
			End Set
		End Property
	End Class
End Namespace
