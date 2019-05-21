Imports System
Imports DevExpress.Xpo
Imports System.ComponentModel
Imports System.Collections.Generic
Imports DevExpress.Persistent.Base
Imports DevExpress.Persistent.BaseImpl

Namespace MainDemo.[Module].BusinessObjects
    <DefaultClassOptions>
    <ImageName("BO_Department")>
    <DefaultProperty("Title")>
    Public Class Department
        Inherits BaseObject

        Private _title As String
        Private _office As String

        Public Sub New(ByVal session As Session)
            MyBase.New(session)
        End Sub

        Public Property Title As String
            Get
                Return _title
            End Get
            Set(ByVal value As String)
                SetPropertyValue("Title", _title, value)
            End Set
        End Property

        Public Property Office As String
            Get
                Return _office
            End Get
            Set(ByVal value As String)
                SetPropertyValue("Office", _office, value)
            End Set
        End Property

        <Association>
        Public ReadOnly Property Employees As XPCollection(Of Employee)
            Get
                Return GetCollection(Of Employee)("Employees")
            End Get
        End Property
    End Class
End Namespace
