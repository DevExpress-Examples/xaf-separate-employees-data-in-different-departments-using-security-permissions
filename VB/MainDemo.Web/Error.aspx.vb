Imports System
Imports System.Web.UI
Imports DevExpress.ExpressApp.Web
Imports DevExpress.ExpressApp.Web.SystemModule
Imports DevExpress.ExpressApp.Web.TestScripts

Public Partial Class ErrorPage
    Inherits System.Web.UI.Page

    Protected Overrides Sub InitializeCulture()
        If WebApplication.Instance IsNot Nothing Then WebApplication.Instance.InitializeCulture()
    End Sub

    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim testScriptsManager As TestScriptsManager = New TestScriptsManager(Page)
        testScriptsManager.RegisterControl(JSLabelTestControl.ClassName, "FormCaption", TestControlType.Field, "FormCaption")
        testScriptsManager.RegisterControl(JSLabelTestControl.ClassName, "DescriptionTextBox", TestControlType.Field, "Description")
        testScriptsManager.RegisterControl(JSDefaultTestControl.ClassName, "ReportButton", TestControlType.Action, "Report")
        testScriptsManager.AllControlRegistered()

        If WebApplication.Instance IsNot Nothing Then
            ApplicationTitle.Text = WebApplication.Instance.Title
        Else
            ApplicationTitle.Text = "No application"
        End If

        Header.Title = "Application Error - " & ApplicationTitle.Text
        Dim errorInfo As ErrorInfo = ErrorHandling.GetApplicationError()

        If errorInfo IsNot Nothing Then

            If ErrorHandling.CanShowDetailedInformation Then
                DetailsText.Text = errorInfo.GetTextualPresentation(True)
            Else
                Details.Visible = False
            End If

            ReportResult.Visible = False
            ReportForm.Visible = ErrorHandling.CanSendAlertToAdmin
        Else
            ErrorPanel.Visible = False
        End If
    End Sub

    Overrides Protected Sub OnInit(ByVal e As EventArgs)
        InitializeComponent()
        MyBase.OnInit(e)
    End Sub

    Private Sub InitializeComponent()
        AddHandler Me.Load, New System.EventHandler(AddressOf Me.Page_Load)
        AddHandler Me.PreRender, New EventHandler(AddressOf ErrorPage_PreRender)
    End Sub

    Private Sub ErrorPage_PreRender(ByVal sender As Object, ByVal e As EventArgs)
        RegisterThemeAssemblyController.RegisterThemeResources(CType(sender, Page))
    End Sub

    Protected Sub ReportButton_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim errorInfo As ErrorInfo = ErrorHandling.GetApplicationError()

        If errorInfo IsNot Nothing Then
            ErrorHandling.SendAlertToAdmin(errorInfo.Id, DescriptionTextBox.Text, errorInfo.Exception.Message)
            ErrorHandling.ClearApplicationError()
            ApologizeMessage.Visible = False
            ReportForm.Visible = False
            Details.Visible = False
            ReportResult.Visible = True
        End If
    End Sub

    Protected Sub NavigateToStart_Click(ByVal sender As Object, ByVal e As EventArgs)
        WebApplication.Instance.LogOff()
    End Sub
End Class
