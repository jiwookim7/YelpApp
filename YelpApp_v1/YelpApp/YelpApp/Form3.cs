using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YelpApp
{
    public partial class DisplayCheckinsGraph : Form
    {
        private Form1 mainForm;
        private string businessId;
        public DisplayCheckinsGraph(Form1 mainForm, string businessId)
        {
            InitializeComponent();
            this.mainForm = mainForm;
            this.businessId = businessId;
            updateCheckinsGrid();
        }

        private void DisplayCheckinsGraph_Load(object sender, EventArgs e)
        {

        }

        private void updateCheckinsGrid()
        {
            CheckinsGrid.Rows.Clear();
            string[] months = new string[12] { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
            int[] months2 = new int[12] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
            int[] checkins = new int[12];

            checkins = DBManager.Instance.getCheckinsPerMonth(businessId);


            for (int i = 0; i < 12; i++)
            {
                DataGridViewRow row = (DataGridViewRow)CheckinsGrid.Rows[0].Clone();
                row.Cells[0].Value = months[i];
                row.Cells[1].Value = checkins[i];

                CheckinsGrid.Rows.Add(row);
            }


        }

        private void CheckinButton_Click(object sender, EventArgs e)
        {
            DBManager.Instance.InsertCheckin(businessId);
            mainForm.UpdateBusinessSpreadsheet();
            Button whatWasClicked = sender as Button;
            if (whatWasClicked != null)
            {
                this.Controls.Remove(whatWasClicked);
            }
            updateCheckinsGrid();
        }
    }
}
