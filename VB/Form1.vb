Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Data
Imports DevExpress.Utils

Namespace ArrayOfCellStyles
	''' <summary>
	''' Summary description for Form1.
	''' </summary>
	Public Class Form1
		Inherits System.Windows.Forms.Form
		Private gridControl1 As DevExpress.XtraGrid.GridControl
		Private WithEvents gridView1 As DevExpress.XtraGrid.Views.Grid.GridView
		Private WithEvents radioGroup1 As DevExpress.XtraEditors.RadioGroup
		Private groupControl1 As DevExpress.XtraEditors.GroupControl
		''' <summary>
		''' Required designer variable.
		''' </summary>
		Private components As System.ComponentModel.Container = Nothing

		Public Sub New()
			'
			' Required for Windows Form Designer support
			'
			InitializeComponent()

			'
			' TODO: Add any constructor code after InitializeComponent call
			'
		End Sub

		''' <summary>
		''' Clean up any resources being used.
		''' </summary>
		Protected Overrides Overloads Sub Dispose(ByVal disposing As Boolean)
			If disposing Then
				If components IsNot Nothing Then
					components.Dispose()
				End If
			End If
			MyBase.Dispose(disposing)
		End Sub

		#Region "Windows Form Designer generated code"
		''' <summary>
		''' Required method for Designer support - do not modify
		''' the contents of this method with the code editor.
		''' </summary>
		Private Sub InitializeComponent()
			Me.gridControl1 = New DevExpress.XtraGrid.GridControl()
			Me.gridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
			Me.radioGroup1 = New DevExpress.XtraEditors.RadioGroup()
			Me.groupControl1 = New DevExpress.XtraEditors.GroupControl()
			CType(Me.gridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
			CType(Me.gridView1, System.ComponentModel.ISupportInitialize).BeginInit()
			CType(Me.radioGroup1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
			CType(Me.groupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
			Me.groupControl1.SuspendLayout()
			Me.SuspendLayout()
			' 
			' gridControl1
			' 
			Me.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill
			Me.gridControl1.EmbeddedNavigator.Name = ""
			Me.gridControl1.Location = New System.Drawing.Point(0, 0)
			Me.gridControl1.MainView = Me.gridView1
			Me.gridControl1.Name = "gridControl1"
			Me.gridControl1.Size = New System.Drawing.Size(446, 321)
			Me.gridControl1.TabIndex = 0
			Me.gridControl1.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() { Me.gridView1})
			' 
			' gridView1
			' 
			Me.gridView1.GridControl = Me.gridControl1
			Me.gridView1.Name = "gridView1"
'			Me.gridView1.RowCellStyle += New DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(Me.gridView1_RowCellStyle);
			' 
			' radioGroup1
			' 
			Me.radioGroup1.Dock = System.Windows.Forms.DockStyle.Top
			Me.radioGroup1.EditValue = 0
			Me.radioGroup1.Location = New System.Drawing.Point(2, 20)
			Me.radioGroup1.Name = "radioGroup1"
			Me.radioGroup1.Properties.Items.AddRange(New DevExpress.XtraEditors.Controls.RadioGroupItem() { New DevExpress.XtraEditors.Controls.RadioGroupItem(0, "Without styles"), New DevExpress.XtraEditors.Controls.RadioGroupItem(1, "Solution 1"), New DevExpress.XtraEditors.Controls.RadioGroupItem(2, "Solution 2 (recommended)")})
			Me.radioGroup1.Size = New System.Drawing.Size(156, 104)
			Me.radioGroup1.TabIndex = 1
'			Me.radioGroup1.EditValueChanged += New System.EventHandler(Me.radioGroup1_EditValueChanged);
			' 
			' groupControl1
			' 
			Me.groupControl1.Controls.Add(Me.radioGroup1)
			Me.groupControl1.Dock = System.Windows.Forms.DockStyle.Right
			Me.groupControl1.Location = New System.Drawing.Point(446, 0)
			Me.groupControl1.Name = "groupControl1"
			Me.groupControl1.Size = New System.Drawing.Size(160, 321)
			Me.groupControl1.TabIndex = 2
			Me.groupControl1.Text = "Available options"
			' 
			' Form1
			' 
			Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
			Me.ClientSize = New System.Drawing.Size(606, 321)
			Me.Controls.Add(Me.gridControl1)
			Me.Controls.Add(Me.groupControl1)
			Me.Name = "Form1"
			Me.Text = "How to apply custom styles to grid cells"
'			Me.Load += New System.EventHandler(Me.Form1_Load);
			CType(Me.gridControl1, System.ComponentModel.ISupportInitialize).EndInit()
			CType(Me.gridView1, System.ComponentModel.ISupportInitialize).EndInit()
			CType(Me.radioGroup1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
			CType(Me.groupControl1, System.ComponentModel.ISupportInitialize).EndInit()
			Me.groupControl1.ResumeLayout(False)
			Me.ResumeLayout(False)

		End Sub
		#End Region

		''' <summary>
		''' The main entry point for the application.
		''' </summary>
		<STAThread> _
		Shared Sub Main()
			Application.Run(New Form1())
		End Sub

		Private Sub Form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
'			#Region "Create Table"
			Const ColumnCount As Integer = 5
			Const RowCount As Integer = 20
			Dim table As New DataTable()
			For i As Integer = 0 To ColumnCount - 1
				table.Columns.Add()
			Next i
			Dim rnd As New Random()
			For j As Integer = 0 To RowCount - 1
				Dim row As DataRow = table.NewRow()
				For i As Integer = 0 To ColumnCount - 1
					row(i) = Convert.ToString(rnd.Next(90) + 10)
				Next i
				table.Rows.Add(row)
			Next j
'			#End Region

			gridControl1.DataSource = table
		End Sub

		Private cellStyles As Hashtable

		Private Sub SetCellStyles()
			cellStyles = New Hashtable()
			For j As Integer = 0 To gridView1.DataRowCount - 1
				For Each column As DevExpress.XtraGrid.Columns.GridColumn In gridView1.Columns
					Dim cellValue As String = gridView1.GetRowCellDisplayText(j, column)

					' some Rule
					If cellValue.IndexOf("1"c) > 0 Then
						Dim ap As New AppearanceObject()
						ap.BackColor = Color.Red
						cellStyles.Add(New Point(column.AbsoluteIndex, j), ap) ' Point is used here to identify a cell
					End If
				Next column
			Next j
		End Sub

		Private Sub gridView1_RowCellStyle(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles gridView1.RowCellStyle
			Select Case Convert.ToInt32(radioGroup1.EditValue)
				Case 1 ' solution 1
					If cellStyles IsNot Nothing Then
						Dim column As Integer = e.Column.AbsoluteIndex
						Dim row As Integer = gridView1.GetDataSourceRowIndex(e.RowHandle)
						Dim cellAppearance As AppearanceObject = TryCast(cellStyles(New Point(column, row)), AppearanceObject)
						If cellAppearance IsNot Nothing Then
							e.Appearance.Assign(cellAppearance)
						End If
					End If
				Case 2 ' solution 2
					Dim cellValue As String = gridView1.GetRowCellDisplayText(e.RowHandle, e.Column)
					If cellValue.IndexOf("1"c) > 0 Then
						e.Appearance.BackColor = Color.Red
					End If
			End Select
		End Sub

		Private Sub radioGroup1_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles radioGroup1.EditValueChanged
			If radioGroup1.EditValue.Equals(1) Then
				SetCellStyles()
			End If
			gridView1.LayoutChanged() ' force repainting
		End Sub
	End Class
End Namespace