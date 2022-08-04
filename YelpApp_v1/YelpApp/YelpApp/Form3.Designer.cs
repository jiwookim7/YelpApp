namespace YelpApp
{
    partial class DisplayCheckinsGraph
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.CheckinButton = new System.Windows.Forms.Button();
            this.CheckinsGrid = new System.Windows.Forms.DataGridView();
            this.ColumnMonths = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.CheckinsGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // CheckinButton
            // 
            this.CheckinButton.Location = new System.Drawing.Point(314, 12);
            this.CheckinButton.Name = "CheckinButton";
            this.CheckinButton.Size = new System.Drawing.Size(85, 87);
            this.CheckinButton.TabIndex = 0;
            this.CheckinButton.Text = "Check In";
            this.CheckinButton.UseVisualStyleBackColor = true;
            this.CheckinButton.Click += new System.EventHandler(this.CheckinButton_Click);
            // 
            // CheckinsGrid
            // 
            this.CheckinsGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.CheckinsGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnMonths,
            this.ColumnCount});
            this.CheckinsGrid.Location = new System.Drawing.Point(27, 12);
            this.CheckinsGrid.Name = "CheckinsGrid";
            this.CheckinsGrid.RowTemplate.Height = 25;
            this.CheckinsGrid.Size = new System.Drawing.Size(243, 426);
            this.CheckinsGrid.TabIndex = 1;
            // 
            // ColumnMonths
            // 
            this.ColumnMonths.HeaderText = "Month";
            this.ColumnMonths.Name = "ColumnMonths";
            // 
            // ColumnCount
            // 
            this.ColumnCount.HeaderText = "Number of checkins";
            this.ColumnCount.Name = "ColumnCount";
            // 
            // DisplayCheckinsGraph
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(447, 450);
            this.Controls.Add(this.CheckinsGrid);
            this.Controls.Add(this.CheckinButton);
            this.Name = "DisplayCheckinsGraph";
            this.Text = "Number of checkins per month";
            this.Load += new System.EventHandler(this.DisplayCheckinsGraph_Load);
            ((System.ComponentModel.ISupportInitialize)(this.CheckinsGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Button CheckinButton;
        private DataGridView CheckinsGrid;
        private DataGridViewTextBoxColumn ColumnMonths;
        private DataGridViewTextBoxColumn ColumnCount;
    }
}