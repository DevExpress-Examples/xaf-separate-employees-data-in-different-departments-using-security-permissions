#DropDB E4045_EasyTest
#Application MainDemoWeb
#Application MainDemoWin

*FillForm
 User Name = Mary
*Action Log On

*Action Navigation(Default.Department)
#IfDef MainDemoWin
!ActionAvailable New(Department)
#EndIf
#IfDef MainDemoWeb
!ActionAvailable New
!ActionAvailable Quick Create(Department)
#EndIf

*CheckTable
 Columns = Title
 Row = Technical Support
 RowCount = 1

*ProcessRecord
 Title = Technical Support
*ActionAvailable Delete

*OptionalAction Edit
*Sleep 500
*FillForm
 Title = changed
*Action Save and Close

*Action Navigation(Default.Employee)
#IfDef MainDemoWin
*ActionAvailable New(Employee)
#EndIf
#IfDef MainDemoWeb
*ActionAvailable New
#EndIf

*CheckTable
 Columns = Full Name
 Row = Joe Pitt
 Row = Mary Tellinson
 RowCount = 2

*ProcessRecord
  Full Name = Joe Pitt
*Sleep 500
*ActionAvailable Delete

*OptionalAction Edit
*Sleep 500
*FillForm
 First Name = changed
*Action Save and Close

*Action New
*FieldVisible First Name

*Action Navigation(Default.Task)
#IfDef MainDemoWin
*ActionAvailable New(Task)
#EndIf
#IfDef MainDemoWeb
*ActionAvailable New
#EndIf

*CheckTable
 RowCount = 2

*SelectRecords
 Columns = Subject
 Row = Read latest news
*Sleep 500
*ActionAvailable Delete

*ProcessRecord
 Subject = Book tickets
*ActionAvailable Delete

*OptionalAction Edit
*Sleep 500
*FillForm
 Subject = changed

*FillForm
 Assigned To = Mary Tellinson
*Action Save and Close

*Action Navigation(Default.Employee)
*Action New
*FieldVisible First Name

*AutoTest
