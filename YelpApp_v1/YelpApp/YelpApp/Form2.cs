

namespace YelpApp
{
    public partial class NumsByTips : Form
    {
        private DataGridViewCellCollection SelectedTipCells
        {
            get
            {
                if (dataGridViewNumTips.SelectedRows.Count > 0 && dataGridViewNumTips.SelectedRows[0].Cells.Count > 0)
                {
                    return dataGridViewNumTips.SelectedRows[0].Cells;
                }
                return null;
            }
        }
        private string SelectedTipDate
        {
            get
            {
                if (SelectedTipCells != null && SelectedTipCells[0].Value != null)
                {
                    return SelectedTipCells[0].Value.ToString();
                }
                return null;
            }
        }
        private string SelectedTipUserId
        {
            get
            {
                if (SelectedTipCells != null && SelectedTipCells[4].Value != null)
                {
                    return SelectedTipCells[4].Value.ToString();
                }
                return null;
            }
        }

        private Form1 mainForm;
        private string businessId;

        public NumsByTips(Form1 mainForm, string businessId)
        {
            InitializeComponent();
            this.mainForm = mainForm;
            this.businessId = businessId;
        }

        private void NumsByTips_Load(object sender, EventArgs e)
        {
            UpdateTipSpreadsheet();
            UpdateFriendReviewSheet();
        }


        private void UpdateTipSpreadsheet()
        {
            dataGridViewNumTips.Rows.Clear();
            List<TipFetchedModel> tips = DBManager.Instance.GetTips(businessId);

            for (int i = 0; i < tips.Count(); i++)
            {
                DataGridViewRow row = (DataGridViewRow)dataGridViewNumTips.Rows[0].Clone();
                row.Cells[0].Value = tips[i].date;
                row.Cells[1].Value = tips[i].userName;
                row.Cells[2].Value = tips[i].likes;
                row.Cells[3].Value = tips[i].text;
                row.Cells[4].Value = tips[i].userId;

                dataGridViewNumTips.Rows.Add(row);
            }
        }

        private void UpdateFriendReviewSheet()
        {
            FriendReview.Rows.Clear();
            List<FriendReviews> reviews = DBManager.Instance.GetFriendReviews(businessId, AppManager.Instance.UserId);

            for(int i = 0; i <reviews.Count(); i++)
            {
                DataGridViewRow row = (DataGridViewRow)FriendReview.Rows[0].Clone();
                row.Cells[0].Value = reviews[i].userName;
                row.Cells[1].Value = reviews[i].date;
                row.Cells[2].Value = reviews[i].text;

                FriendReview.Rows.Add(row); 
            }
        }

        private void TipAddButton_Click(object sender, EventArgs e)
        {
            if (NumTipsTextBox.Text == String.Empty)
                return;

            DataGridViewRow row = (DataGridViewRow)dataGridViewNumTips.Rows[0].Clone();
            DBManager.Instance.InsertTip(businessId, NumTipsTextBox.Text);
            UpdateTipSpreadsheet();
            mainForm.UpdateBusinessSpreadsheet();
            NumTipsTextBox.Text = String.Empty;
        }

        private void Likes_Click(object sender, EventArgs e)
        {
            if (SelectedTipCells == null)
                return;

            DBManager.Instance.IncrementLikeOnTip(this.SelectedTipDate, this.SelectedTipUserId, this.businessId);
            UpdateTipSpreadsheet();
        }
    }
}
