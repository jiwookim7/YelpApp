using static System.Windows.Forms.ListBox;

namespace YelpApp
{
    public partial class Form1 : Form
    {
        // Business Search ==========================================================
        private int priceLevel;

        private string SelectedState { get { return comboBoxState.SelectedItem != null ? comboBoxState.SelectedItem.ToString() : null; } }
        private string SelectedCity { get { return listBoxCity.SelectedItem != null ? listBoxCity.SelectedItem.ToString() : null; } }
        private string SelectedZipcode { get { return listBoxZipcode.SelectedItem != null ? listBoxZipcode.SelectedItem.ToString() : null; } }
        private string SelectedCategory { get { return listBoxCategory.SelectedItem != null ? listBoxCategory.SelectedItem.ToString() : null; } }
        private string SelectedCategoryFilter { get { return listBoxCategoryFilter.SelectedItem != null ? listBoxCategoryFilter.SelectedItem.ToString() : null; } }
        private DataGridViewCellCollection SelectedBusinessCells
        {
            get
            {
                if (dataGridViewBusiness.SelectedRows.Count > 0 && dataGridViewBusiness.SelectedRows[0].Cells.Count > 0)
                {
                    return dataGridViewBusiness.SelectedRows[0].Cells;
                }
                return null;
            }
        }
        private string SelectedBusinessId
        {
            get
            {
                if (SelectedBusinessCells != null && SelectedBusinessCells[0].Value != null)
                {
                    return SelectedBusinessCells[0].Value.ToString();
                }
                return null;
            }
        }
        private string SelectedBusinessName
        {
            get
            {
                if (SelectedBusinessCells != null && SelectedBusinessCells[1].Value != null)
                {
                    return SelectedBusinessCells[1].Value.ToString();
                }
                return null;
            }
        }
        private string SelectedBusinessAddress
        {
            get
            {
                if (SelectedBusinessCells[2].Value != null)
                {
                    return SelectedBusinessCells[2].Value.ToString();
                }
                return null;
            }
        }

        public Form1()
        {
            InitializeComponent();
            AppManager.Instance.UserId = "srnPSBxBl7ENG2CziRXk9A";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DBManager.Instance.Connect();

            UpdateStates();
            comboBoxSortResultsBy.SelectedIndex = 0;
        }

        // Business Search helpers ==========================================================================
        private void UpdateCities()
        {
            if (SelectedState == null)
                return;

            listBoxCity.Items.Clear();
            List<string> cities = DBManager.Instance.GetAllCities(SelectedState);

            for (int i = 0; i < cities.Count; i++)
            {
                listBoxCity.Items.Add(cities[i]);
            }
        }

        private void UpdateZipcodes()
        {
            if (SelectedState == null || SelectedCity == null)
                return;

            listBoxZipcode.Items.Clear();
            List<string> zipcodes = DBManager.Instance.GetAllZipcodes(SelectedState, SelectedCity);

            for (int i = 0; i < zipcodes.Count; i++)
            {
                listBoxZipcode.Items.Add(zipcodes[i]);
            }
        }

        private void UpdateCategories()
        {
            if (SelectedState == null || SelectedCity == null || SelectedZipcode == null)
                return;

            listBoxCategoryFilter.Items.Clear();
            listBoxCategory.Items.Clear();
            List<string> categories = DBManager.Instance.GetAllCategories(SelectedState, SelectedCity, SelectedZipcode);

            for (int i = 0; i < categories.Count; i++)
            {
                listBoxCategory.Items.Add(categories[i]);
            }
        }


        private void UpdateStates()
        {
            List<string> states = DBManager.Instance.GetAllStates();

            for (int i = 0; i < states.Count; i++)
            {
                comboBoxState.Items.Add(states[i]);
            }
        }

        private void UpdateListBoxCategoriesAndAttributes()
        {
            if (SelectedBusinessId != null)
            {
                listBoxCategoriesAndAttributes.Items.Clear();
                List<string> categories = DBManager.Instance.GetAllCategoriesByBusinessId(SelectedBusinessId);
                listBoxCategoriesAndAttributes.Items.Add("Categories");
                for (int i = 0; i < categories.Count; i++)
                {
                    listBoxCategoriesAndAttributes.Items.Add("  " + categories[i]);
                }

                List<AttributeModel> attributes = DBManager.Instance.GetAllAttributesByBusinessId(SelectedBusinessId);
                listBoxCategoriesAndAttributes.Items.Add("Attributes");
                for (int i = 0; i < attributes.Count; i++)
                {
                    if (attributes[i].value == "True")
                    {
                        listBoxCategoriesAndAttributes.Items.Add("  " + attributes[i].attrName);
                    }
                    else
                    {
                        listBoxCategoriesAndAttributes.Items.Add("  " + attributes[i].attrName + "(" + attributes[i].value + ")");
                    }
                }
            }
        }

        public void UpdateBusinessSpreadsheet()
        {
            dataGridViewBusiness.Rows.Clear();
            List<BusinessModel> businesses = DBManager.Instance.GetBusinesses(SelectedState,
                SelectedCity,
                SelectedZipcode,
                AppManager.Instance.UserId,
                ConvertListBoxItemsToStrings(listBoxCategoryFilter.Items),
                GetAttributes(),
                comboBoxSortResultsBy.SelectedIndex);

            for (int i = 0; i < businesses.Count; i++)
            {
                DataGridViewRow row = (DataGridViewRow)dataGridViewBusiness.Rows[0].Clone();
                row.Cells[0].Value = businesses[i].businessId;
                row.Cells[1].Value = businesses[i].name;
                row.Cells[2].Value = businesses[i].address;
                row.Cells[3].Value = businesses[i].city;
                row.Cells[4].Value = businesses[i].state;
                row.Cells[5].Value = businesses[i].distance;
                row.Cells[6].Value = businesses[i].stars;
                row.Cells[7].Value = businesses[i].numtips;
                row.Cells[8].Value = businesses[i].numcheckins;
                dataGridViewBusiness.Rows.Add(row);
            }
            labelNumBusiness.Text = "# of business " + businesses.Count;
        }

        private void UpdatePriceLevelFilter()
        {
            priceLevel = 0;
            if (priceRadioButton1.Checked == true)
            {
                priceLevel = 1;
            }
            else if (priceRadioButton2.Checked == true)
            {
                priceLevel = 2;
            }
            else if (priceRadioButton3.Checked == true)
            {
                priceLevel = 3;
            }
            else if (priceRadioButton4.Checked == true)
            {
                priceLevel = 4;
            }
        }

        private List<KeyValuePair<string, string>> UpdateAttributeFilter(List<KeyValuePair<string, string>> pairs)
        {
            if (creditCardsCheckBox.Checked == true)
            {
                pairs.Add(new KeyValuePair<string, string>("BusinessAcceptsCreditCards", "True"));   
            }
            if (reservationsCheckBox.Checked == true)
            {
                pairs.Add(new KeyValuePair<string, string>("RestaurantsReservations", "True"));
            }
            if (wheelchairCheckBox.Checked == true)
            {
                pairs.Add(new KeyValuePair<string, string>("WheelchairAccessible", "True"));
            }
            if (outdoorCheckBox.Checked == true)
            {
                pairs.Add(new KeyValuePair<string, string>("OutdoorSeating", "True"));
            }
            if (kidsCheckBox.Checked == true)
            {
                pairs.Add(new KeyValuePair<string, string>("GoodForKids", "True"));
            }
            if (groupsCheckBox.Checked == true)
            {
                pairs.Add(new KeyValuePair<string, string>("RestaurantsGoodForGroups", "True"));
            }
            if (deliveryCheckBox.Checked == true)
            {
                pairs.Add(new KeyValuePair<string, string>("RestaurantsDelivery", "True"));
            }
            if (takeOutCheckBox.Checked == true)
            {
                pairs.Add(new KeyValuePair<string, string>("RestaurantsTakeOut", "True"));
            }
            if (wifiCheckBox.Checked == true)
            {
                pairs.Add(new KeyValuePair<string, string>("WiFi", "free"));
            }
            if (bikeCheckBox.Checked == true)
            {
                pairs.Add(new KeyValuePair<string, string>("BikeParking", "True"));
            }
            return pairs;
        }

        private List<KeyValuePair<string, string>> UpdateMealFilter(List<KeyValuePair<string, string>> pairs)
        {
            if(breakfastCheckBox.Checked == true)
            {
                pairs.Add(new KeyValuePair<string, string>("breakfast", "True"));
            }
            if (lunchCheckBox.Checked == true)
            {
                pairs.Add(new KeyValuePair<string, string>("lunch", "True"));
            }
            if (brunchCheckBox.Checked == true)
            {
                pairs.Add(new KeyValuePair<string, string>("brunch", "True"));
            }
            if (dinnerCheckBox.Checked == true)
            {
                pairs.Add(new KeyValuePair<string, string>("dinner", "True"));
            }
            if (dessertCheckBox.Checked == true)
            {
                pairs.Add(new KeyValuePair<string, string>("dessert", "True"));
            }
            if (latenightCheckBox.Checked == true)
            {
                pairs.Add(new KeyValuePair<string, string>("latenight", "True"));
            }
            return pairs;
        }

        private List<KeyValuePair<string,string>> GetAttributes()
        {
            List<KeyValuePair<string,string>> pairs = new List<KeyValuePair<string, string>>();
            UpdatePriceLevelFilter();
            UpdateAttributeFilter(pairs);
            UpdateMealFilter(pairs);

            if (priceLevel > 0)
            {
                pairs.Add(new KeyValuePair<string, string>("RestaurantsPriceRange2", priceLevel.ToString()));
            }
            return pairs;
        }

        private List<string> ConvertListBoxItemsToStrings(ObjectCollection items)
        {
            List<string> strings = new List<string>();
            for (int i = 0; i < items.Count; i++)
            {
                strings.Add(items[i].ToString());
            }

            return strings;
        }

        private void UpdateSelectedBusinessView()
        {
            if (SelectedBusinessId != null)
            {
                labelSelectedBusinessName.Text = SelectedBusinessName;
                labelSelectedBusinessAddress.Text = SelectedBusinessAddress;
                HoursModel hours = DBManager.Instance.GetHours(SelectedBusinessId, DateTime.Today.DayOfWeek.ToString());
                if (hours != null)
                {
                    labelSelectedBusinessHours.Text = string.Format("Today ({0}): Opens: {1} Closes: {2}", hours.dayofweek, hours.open, hours.close);
                }
                else
                {
                    labelSelectedBusinessHours.Text = string.Format("Today ({0}): Closed", DateTime.Today.DayOfWeek.ToString());
                }
            }
        }

        // Business Search UI ==========================================================================
        private void comboBoxState_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateCities();
        }

        private void listBoxCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateZipcodes();
        }

        private void listBoxZipcode_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateCategories();
            UpdateBusinessSpreadsheet();
        }

        private void NumTipsButton_Click(object sender, EventArgs e)
        {
            if (SelectedBusinessId != null)
            {
                NumsByTips numTips = new NumsByTips(this, SelectedBusinessId);

                numTips.ShowDialog();
            }
        }
        private void priceCheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (priceRadioButton1.Checked)
            {
                UpdateBusinessSpreadsheet();
            }
        }
        private void priceCheckBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (priceRadioButton2.Checked)
            {
                UpdateBusinessSpreadsheet();
            }
        }
        private void priceCheckBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (priceRadioButton3.Checked)
            {
                UpdateBusinessSpreadsheet();
            }
        }
        private void priceCheckBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (priceRadioButton4.Checked)
            {
                UpdateBusinessSpreadsheet();
            }
        }

        private void attributesCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            UpdateBusinessSpreadsheet();
        }

        private void mealCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            UpdateBusinessSpreadsheet();
        }

        private void showCheckinsButton_Click(object sender, EventArgs e)
        {
            if (SelectedBusinessId != null)
            {
                DisplayCheckinsGraph displayCheckins = new DisplayCheckinsGraph(this, SelectedBusinessId);
                displayCheckins.ShowDialog();
            }
        }

        private void buttonAddCategoryFilter_Click(object sender, EventArgs e)
        {
            if (SelectedCategory != null)
            {
                listBoxCategoryFilter.Items.Add(SelectedCategory);
                listBoxCategory.Items.Remove(SelectedCategory);
            }
        }

        private void buttonRemoveCategoryFilter_Click(object sender, EventArgs e)
        {
            if (SelectedCategoryFilter != null)
            {
                listBoxCategory.Items.Add(SelectedCategoryFilter);
                listBoxCategoryFilter.Items.Remove(SelectedCategoryFilter);
            }
        }

        private void buttonSearchBusiness_Click(object sender, EventArgs e)
        {
            UpdateBusinessSpreadsheet();
        }

        private void dataGridViewBusiness_SelectionChanged(object sender, EventArgs e)
        {
            UpdateSelectedBusinessView();
            UpdateListBoxCategoriesAndAttributes();
        }


        // User Information =================================
        private string SelectedUserId { get { return listBoxUserId.SelectedItem != null ? listBoxUserId.SelectedItem.ToString() : null; } }

        // User Information logic ==========================================================================
        private void UpdateUserIds()
        {
            List<string> userIds = DBManager.Instance.GetUserIds(textSetUserName.Text);

            if (!string.IsNullOrEmpty(textSetUserName.Text) && textSetUserName.Text.Length >= 2)
            {
                listBoxUserId.Items.Clear();
                for (int i = 0; i < userIds.Count; i++)
                {
                    listBoxUserId.Items.Add(userIds[i]);
                }
            }
        }

        private void UpdateUserInformationView()
        {
            UserModel user = DBManager.Instance.GetUser(SelectedUserId);
            textBoxViewName.Text = user.name;
            textBoxViewTipCount.Text = user.tipcount;
            textBoxViewTotalTipLikes.Text = user.totallikes;
            textBoxViewUseful.Text = user.useful;
            textBoxViewYelpingSince.Text = user.yelpingSince;
            textBoxViewStars.Text = user.averageStars;
            textBoxViewCool.Text = user.cool;
            textBoxViewFunny.Text = user.funny;
            textBoxViewLat.Text = user.latitude;
            textBoxViewLong.Text = user.longitude;
        }

        public void UpdateFriendsSpreadsheet()
        {
            dataGridViewFriends.Rows.Clear();
            List<UserModel> users = DBManager.Instance.GetFriends(SelectedUserId);

            for (int i = 0; i < users.Count; i++)
            {
                DataGridViewRow row = (DataGridViewRow)dataGridViewFriends.Rows[0].Clone();
                row.Cells[0].Value = users[i].name;
                row.Cells[1].Value = users[i].totallikes;
                row.Cells[2].Value = users[i].averageStars;
                row.Cells[3].Value = users[i].yelpingSince;
                row.Cells[4].Value = users[i].fans;
                row.Cells[5].Value = users[i].cool;
                row.Cells[6].Value = users[i].tipcount;
                row.Cells[7].Value = users[i].funny;
                row.Cells[8].Value = users[i].useful;
                dataGridViewFriends.Rows.Add(row);
            }
        }

        public void UpdateFriendsTipsSpreadsheet()
        {
            dataGridViewFriendTips.Rows.Clear();
            List<TipModel> users = DBManager.Instance.GetFriendsTips(SelectedUserId);

            for (int i = 0; i < users.Count; i++)
            {
                DataGridViewRow row = (DataGridViewRow)dataGridViewFriendTips.Rows[0].Clone();
                row.Cells[0].Value = users[i].userName;
                row.Cells[1].Value = users[i].business;
                row.Cells[2].Value = users[i].city;
                row.Cells[3].Value = users[i].text;
                row.Cells[4].Value = users[i].date;
                dataGridViewFriendTips.Rows.Add(row);
            }
        }

        // User Information UI ==========================================================================
        private void textSetUserName_TextChanged(object sender, EventArgs e)
        {
            UpdateUserIds();
        }

        private void listBoxUserId_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateUserInformationView();
            UpdateFriendsSpreadsheet();
            UpdateFriendsTipsSpreadsheet();

            buttonEditUserInformation.Enabled = true;
            buttonUpdateUserInformation.Enabled = false;

            textBoxViewLat.Enabled = false;
            textBoxViewLong.Enabled = false;

            AppManager.Instance.UserId = SelectedUserId;
        }

        private void buttonEditUserInformation_Click(object sender, EventArgs e)
        {
            buttonEditUserInformation.Enabled = false;
            buttonUpdateUserInformation.Enabled = true;

            textBoxViewLat.Enabled = true;
            textBoxViewLong.Enabled = true;
        }

        private void buttonUpdateUserInformation_Click(object sender, EventArgs e)
        {
            buttonEditUserInformation.Enabled = true;
            buttonUpdateUserInformation.Enabled = false;

            textBoxViewLat.Enabled = false;
            textBoxViewLong.Enabled = false;

            DBManager.Instance.UpdateUserLatAndLong(
                SelectedUserId,
                textBoxViewLat.Text,
                textBoxViewLong.Text);
        }

        private void buttonResetFilterByPrice_Click(object sender, EventArgs e)
        {
            priceRadioButton1.Checked = false;
            priceRadioButton2.Checked = false;
            priceRadioButton3.Checked = false;
            priceRadioButton4.Checked = false;
            UpdateBusinessSpreadsheet();
        }

        private void comboBoxSortResultsBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateBusinessSpreadsheet();
        }
    }
}
