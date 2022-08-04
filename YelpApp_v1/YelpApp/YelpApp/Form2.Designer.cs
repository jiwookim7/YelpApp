namespace YelpApp
{
    partial class NumsByTips
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
            this.Date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewNumTips = new System.Windows.Forms.DataGridView();
            this.NumTipsTextBox = new System.Windows.Forms.TextBox();
            this.TipAddButton = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.FriendReview = new System.Windows.Forms.DataGridView();
            this.name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tipdate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UserName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.likes = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Text = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UserId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewNumTips)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FriendReview)).BeginInit();
            this.SuspendLayout();
            // 
            // Date
            // 
            this.Date.HeaderText = "Date";
            this.Date.MinimumWidth = 6;
            this.Date.Name = "Date";
            this.Date.Width = 125;
            // 
            // dataGridViewNumTips
            // 
            this.dataGridViewNumTips.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewNumTips.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.UserName,
            this.likes,
            this.Text,
            this.UserId});
            this.dataGridViewNumTips.Location = new System.Drawing.Point(0, 142);
            this.dataGridViewNumTips.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dataGridViewNumTips.Name = "dataGridViewNumTips";
            this.dataGridViewNumTips.RowHeadersWidth = 51;
            this.dataGridViewNumTips.RowTemplate.Height = 29;
            this.dataGridViewNumTips.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewNumTips.Size = new System.Drawing.Size(895, 285);
            this.dataGridViewNumTips.TabIndex = 0;
            // 
            // NumTipsTextBox
            // 
            this.NumTipsTextBox.Location = new System.Drawing.Point(10, 9);
            this.NumTipsTextBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.NumTipsTextBox.Multiline = true;
            this.NumTipsTextBox.Name = "NumTipsTextBox";
            this.NumTipsTextBox.Size = new System.Drawing.Size(664, 95);
            this.NumTipsTextBox.TabIndex = 1;
            // 
            // TipAddButton
            // 
            this.TipAddButton.Location = new System.Drawing.Point(698, 1);
            this.TipAddButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.TipAddButton.Name = "TipAddButton";
            this.TipAddButton.Size = new System.Drawing.Size(133, 52);
            this.TipAddButton.TabIndex = 2;
            this.TipAddButton.Text = "Add Tip";
            this.TipAddButton.UseVisualStyleBackColor = true;
            this.TipAddButton.Click += new System.EventHandler(this.TipAddButton_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(698, 64);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(133, 46);
            this.button1.TabIndex = 3;
            this.button1.Text = "Like";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Likes_Click);
            // 
            // FriendReview
            // 
            this.FriendReview.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.FriendReview.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.name,
            this.tipdate,
            this.dataGridViewTextBoxColumn2});
            this.FriendReview.Location = new System.Drawing.Point(0, 471);
            this.FriendReview.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.FriendReview.Name = "FriendReview";
            this.FriendReview.RowHeadersWidth = 51;
            this.FriendReview.RowTemplate.Height = 29;
            this.FriendReview.Size = new System.Drawing.Size(895, 251);
            this.FriendReview.TabIndex = 4;
            // 
            // name
            // 
            this.name.HeaderText = "User Name";
            this.name.MinimumWidth = 6;
            this.name.Name = "name";
            this.name.Width = 125;
            // 
            // tipdate
            // 
            this.tipdate.HeaderText = "Date";
            this.tipdate.MinimumWidth = 6;
            this.tipdate.Name = "tipdate";
            this.tipdate.Width = 125;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "Text";
            this.dataGridViewTextBoxColumn2.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.Width = 720;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(0, 454);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(191, 15);
            this.label1.TabIndex = 5;
            this.label1.Text = "Friends who reviewed this business";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(0, 124);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 15);
            this.label2.TabIndex = 6;
            this.label2.Text = "Business Tips";
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Date";
            this.dataGridViewTextBoxColumn1.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.Width = 125;
            // 
            // UserName
            // 
            this.UserName.HeaderText = "UserName";
            this.UserName.MinimumWidth = 6;
            this.UserName.Name = "UserName";
            this.UserName.Width = 125;
            // 
            // likes
            // 
            this.likes.HeaderText = "likes";
            this.likes.MinimumWidth = 6;
            this.likes.Name = "likes";
            this.likes.Width = 125;
            // 
            // Text
            // 
            this.Text.HeaderText = "Text";
            this.Text.MinimumWidth = 6;
            this.Text.Name = "Text";
            this.Text.Width = 700;
            // 
            // UserId
            // 
            this.UserId.HeaderText = "UserId";
            this.UserId.Name = "UserId";
            this.UserId.Visible = false;
            // 
            // NumsByTips
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(894, 733);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.FriendReview);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.TipAddButton);
            this.Controls.Add(this.NumTipsTextBox);
            this.Controls.Add(this.dataGridViewNumTips);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.Name = "NumsByTips";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.NumsByTips_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewNumTips)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FriendReview)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DataGridViewTextBoxColumn Date;
        private TextBox NumTipsTextBox;
        private Button TipAddButton;
        private Button button1;
        private DataGridView FriendReview;
        private Label label1;
        private Label label2;
        private DataGridViewTextBoxColumn name;
        private DataGridViewTextBoxColumn tipdate;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        public DataGridView dataGridViewNumTips;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn UserName;
        private DataGridViewTextBoxColumn likes;
        private DataGridViewTextBoxColumn Text;
        private DataGridViewTextBoxColumn UserId;
    }
}