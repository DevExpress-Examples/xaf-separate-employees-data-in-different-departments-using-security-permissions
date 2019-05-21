Imports DevExpress.ExpressApp
Imports DevExpress.ExpressApp.Editors
Imports DevExpress.ExpressApp.ConditionalAppearance

Namespace MainDemo.[Module].Controllers
    Public Class HideProtectedContentController
        Inherits ViewController(Of ObjectView)

        Private appearanceController As AppearanceController

        Protected Overrides Sub OnActivated()
            MyBase.OnActivated()
            appearanceController = Frame.GetController(Of AppearanceController)()

            If appearanceController IsNot Nothing Then
                AddHandler appearanceController.CustomApplyAppearance, AddressOf appearanceController_CustomApplyAppearance
            End If
        End Sub

        Protected Overrides Sub OnDeactivated()
            If appearanceController IsNot Nothing Then
                RemoveHandler appearanceController.CustomApplyAppearance, AddressOf appearanceController_CustomApplyAppearance
            End If

            MyBase.OnDeactivated()
        End Sub

        Private Sub appearanceController_CustomApplyAppearance(ByVal sender As Object, ByVal e As ApplyAppearanceEventArgs)
            If TypeOf View Is ListView Then

                If TypeOf e.Item Is ColumnWrapper Then

                    If Not DataManipulationRight.CanRead(View.ObjectTypeInfo.Type, (CType(e.Item, ColumnWrapper)).PropertyName, Nothing, (CType(View, ListView)).CollectionSource, View.ObjectSpace) Then
                        e.AppearanceObject.Visibility = ViewItemVisibility.Hide
                    End If
                End If
            End If

            If TypeOf View Is DetailView Then

                If TypeOf e.Item Is PropertyEditor Then

                    If Not DataManipulationRight.CanRead(View.ObjectTypeInfo.Type, (CType(e.Item, PropertyEditor)).PropertyName, If(e.ContextObjects.Length > 0, e.ContextObjects(0), Nothing), Nothing, View.ObjectSpace) Then
                        e.AppearanceObject.Visibility = ViewItemVisibility.Hide
                    End If
                End If
            End If
        End Sub
    End Class
End Namespace
