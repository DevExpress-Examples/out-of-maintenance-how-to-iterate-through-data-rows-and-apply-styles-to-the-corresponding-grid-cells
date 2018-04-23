using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using DevExpress.Utils;

namespace ArrayOfCellStyles {
    /// <summary>
    /// Summary description for Form1.
    /// </summary>
    public class Form1 : System.Windows.Forms.Form {
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.RadioGroup radioGroup1;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;

        public Form1() {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing) {
            if(disposing) {
                if(components != null) {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.radioGroup1 = new DevExpress.XtraEditors.RadioGroup();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.EmbeddedNavigator.Name = "";
            this.gridControl1.Location = new System.Drawing.Point(0, 0);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(446, 321);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.gridView1_RowCellStyle);
            // 
            // radioGroup1
            // 
            this.radioGroup1.Dock = System.Windows.Forms.DockStyle.Top;
            this.radioGroup1.EditValue = 0;
            this.radioGroup1.Location = new System.Drawing.Point(2, 20);
            this.radioGroup1.Name = "radioGroup1";
            this.radioGroup1.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(0, "Without styles"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(1, "Solution 1"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(2, "Solution 2 (recommended)")});
            this.radioGroup1.Size = new System.Drawing.Size(156, 104);
            this.radioGroup1.TabIndex = 1;
            this.radioGroup1.EditValueChanged += new System.EventHandler(this.radioGroup1_EditValueChanged);
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.radioGroup1);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Right;
            this.groupControl1.Location = new System.Drawing.Point(446, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(160, 321);
            this.groupControl1.TabIndex = 2;
            this.groupControl1.Text = "Available options";
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(606, 321);
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.groupControl1);
            this.Name = "Form1";
            this.Text = "How to apply custom styles to grid cells";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            Application.Run(new Form1());
        }

        private void Form1_Load(object sender, System.EventArgs e) {
            #region Create Table
            const int ColumnCount = 5;
            const int RowCount = 20;
            DataTable table = new DataTable();
            for(int i = 0; i < ColumnCount; i++)
                table.Columns.Add();
            Random rnd = new Random();
            for(int j = 0; j < RowCount; j++) {
                DataRow row = table.NewRow();
                for(int i = 0; i < ColumnCount; i++)
                    row[i] = Convert.ToString(rnd.Next(90) + 10);
                table.Rows.Add(row);
            }
            #endregion

            gridControl1.DataSource = table;
        }

        Hashtable cellStyles;

        private void SetCellStyles() {
            cellStyles = new Hashtable();
            for(int j = 0; j < gridView1.DataRowCount; j++)
                foreach(DevExpress.XtraGrid.Columns.GridColumn column in gridView1.Columns) {
                    string cellValue = gridView1.GetRowCellDisplayText(j, column);

                    // some Rule
                    if(cellValue.IndexOf('1') > 0) {
                        AppearanceObject ap = new AppearanceObject();
                        ap.BackColor = Color.Red;
                        cellStyles.Add(new Point(column.AbsoluteIndex, j), ap); // Point is used here to identify a cell
                    }
                }
        }

        private void gridView1_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e) {
            switch(Convert.ToInt32(radioGroup1.EditValue)) {
                case 1: // solution 1
                    if(cellStyles != null) {
                        int column = e.Column.AbsoluteIndex;
                        int row = gridView1.GetDataSourceRowIndex(e.RowHandle);
                        AppearanceObject cellAppearance = cellStyles[new Point(column, row)] as AppearanceObject;
                        if(cellAppearance != null)
                            e.Appearance.Assign(cellAppearance);
                    }
                    break;
                case 2: // solution 2
                    string cellValue = gridView1.GetRowCellDisplayText(e.RowHandle, e.Column);
                    if(cellValue.IndexOf('1') > 0)
                        e.Appearance.BackColor = Color.Red;
                    break;
            }
        }

        private void radioGroup1_EditValueChanged(object sender, System.EventArgs e) {
            if(radioGroup1.EditValue.Equals(1)) {
                SetCellStyles();
            }
            gridView1.LayoutChanged(); // force repainting
        }
    }
}