#DropDB E4045_EasyTest
#Application MainDemoWeb
#Application MainDemoWin

*FillForm
 User Name = Joe
*Action Log On

#IfDef MainDemoWin
!Action New(Task)
!Action New(Employee)
!Action New(Department)
#EndIf
#IfDef MainDemoWeb
!Action New
!Action Quick Create
#EndIf


*CheckTable
 Columns = Subject
 RowCount = 2

*ProcessRecord
 Subject = Book tickets
*Sleep 500
!ActionAvailable Delete
*OptionalAction Edit
*Sleep 500
*FillForm
 Subject = changed
*Action Save
*FillForm
 Assigned To = Mary Tellinson
*Action Save and Close

#IfDef MainDemoWin
*Action Navigation(Default.My Details)
*CheckFieldValues
 Department = Technical Support
*OptionalAction Edit
*Sleep 500

*ExecuteEditorAction Department
*CheckTable
 Columns = Title
 RowCount = 1
*ProcessRecord
 Title = Technical Support
*Action Open Related Record
*OptionalAction Edit
*Sleep 500
!FillForm
 Title = changed

!ActionAvailable Delete
!ActionAvailable New(Department)

*CheckTable Employees
 Columns = First Name
 Row = Joe
 Row = Mary
 RowCount = 2
*Action Save and Close
#EndIf

*AutoTest