using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using Npgsql;

namespace YelpApp
{
    public sealed class DBManager
    {
        private static DBManager? instance = null;
        private static readonly object padlock = new object();

        DBManager()
        {
        }

        public static DBManager Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new DBManager();
                    }
                    return instance;
                }
            }
        }

        private NpgsqlConnection con;

        public void Connect()
        {
            // TODO: set your PW 
            var cs = "Host=localhost;Username=postgres;Password=011643030;Database=tempyelp";

            con = new NpgsqlConnection(cs);
            con.Open();

            //var sql = "SELECT version()";

            //var cmd = new NpgsqlCommand(sql, con);
            //var version = cmd.ExecuteScalar().ToString();
            //return $"PostgreSQL version: {version}";
        }

        public List<string> GetAllStates()
        {
            NpgsqlCommand command = new NpgsqlCommand("SELECT DISTINCT state FROM business", con);
            NpgsqlDataReader reader = command.ExecuteReader();
            List<string> states = new List<string>();
            while (reader.Read())
            {
                states.Add(string.Format("{0}", reader[0]));
            }
            reader.Close();
            return states;
        }

        public List<TipFetchedModel> GetTips(string businessId)
        {
            NpgsqlCommand command = new NpgsqlCommand(string.Format("SELECT tipdate, users.name, tip.likes, tiptext, tip.user_id FROM tip, users, business WHERE tip.user_id = users.user_id AND tip.business_id = business.business_id AND business.business_id = '{0}' ORDER BY tipdate DESC", businessId), con);
            NpgsqlDataReader reader = command.ExecuteReader();

            List<TipFetchedModel> tips = new List<TipFetchedModel>();

            while(reader.Read())
            {
                TipFetchedModel numTips = new TipFetchedModel();
                numTips.date = string.Format("{0}", reader["tipdate"]);
                numTips.userName = string.Format("{0}", reader["name"]);
                numTips.likes = string.Format("{0}", reader["likes"]);
                numTips.text = string.Format("{0}", reader["tiptext"]);
                numTips.userId = string.Format("{0}", reader["user_id"]);

                tips.Add(numTips);
            }

            reader.Close();

            return tips;

        }

        public List<FriendReviews> GetFriendReviews(string bussinessId, string userId)
        {
            NpgsqlCommand command = new NpgsqlCommand(string.Format("SELECT users.name, tipdate, tiptext FROM business, tip, users WHERE"
                                                                 + " business.business_id = '{0}' AND"
                                                                 + " business.business_id = tip.business_id AND"
                                                                 + " tip.user_id IN(SELECT user_id FROM friend, users WHERE friend_of = '{1}' AND friend.friend_for = users.user_id) AND"
                                                                 + " users.user_id = tip.user_id", bussinessId, userId), con);
            NpgsqlDataReader reader = command.ExecuteReader();

            List<FriendReviews> tips = new List<FriendReviews>();

            while(reader.Read())
            {
                FriendReviews numReviews = new FriendReviews();
                numReviews.userName = String.Format("{0}", reader["name"]);
                numReviews.date = String.Format("{0}", reader["tipdate"]);
                numReviews.text = String.Format("{0}", reader["tiptext"]);

                tips.Add(numReviews);
            }

            reader.Close();

            return tips;
        }

        public List<string> GetAllCities(string state)
        {
            NpgsqlCommand cmd = new NpgsqlCommand(string.Format("SELECT DISTINCT city FROM business WHERE state = '{0}'", state), con);
            //NpgsqlCommand command = new NpgsqlCommand(cmd, con);
            NpgsqlDataReader reader = cmd.ExecuteReader();
            List<string> cities = new List<string>();
            while (reader.Read())
            {
                cities.Add(string.Format("{0}", reader[0]));
            }
            reader.Close();
            return cities;
        }

        public List<string> GetAllZipcodes(string state, string city)
        {
            string cmd = string.Format("SELECT DISTINCT zipcode FROM BUSINESS WHERE state = '{0}' AND city = '{1}'", state, city);
            NpgsqlCommand command = new NpgsqlCommand(cmd, con);
            NpgsqlDataReader reader = command.ExecuteReader();
            List<string> zipcodes = new List<string>();
            while (reader.Read())
            {
                zipcodes.Add(string.Format("{0}", reader[0]));
            }
            reader.Close();
            return zipcodes;
        }

        public List<string> GetAllCategories(string state, string city, string zipcode)
        {
            string cmd = string.Format("SELECT DISTINCT business_id FROM business WHERE state = '{0}' AND city = '{1}' AND zipcode = '{2}'", state, city, zipcode);
            NpgsqlCommand command = new NpgsqlCommand(cmd, con);
            NpgsqlDataReader reader = command.ExecuteReader();
            List<string> businesses = new List<string>();
            while (reader.Read())
            {
                businesses.Add(string.Format("{0}", reader[0]));
            }
            reader.Close();

            StringBuilder sb = new StringBuilder("SELECT DISTINCT category_name FROM categories WHERE");
            for (int i = 0; i < businesses.Count; i++)
            {
                if (i > 0)
                {
                    sb.Append(string.Format(" OR"));
                }
                sb.Append(string.Format(" business_id = '{0}'", businesses[i]));
            }

            cmd = sb.ToString();
            command = new NpgsqlCommand(cmd, con);
            reader = command.ExecuteReader();
            List<string> categories = new List<string>();
            while (reader.Read())
            {
                categories.Add(string.Format("{0}", reader[0]));
            }
            reader.Close();

            return categories;
        }

        public HoursModel GetHours(string businessId, string dayofweek)
        {
            string cmd = string.Format("SELECT * FROM hours WHERE business_id = '{0}' and dayofweek = '{1}'", businessId, dayofweek);
            NpgsqlCommand command = new NpgsqlCommand(cmd, con);
            NpgsqlDataReader reader = command.ExecuteReader();
            HoursModel hours = null;
            if (reader.Read())
            {
                hours = new HoursModel();
                hours.dayofweek = string.Format("{0}", reader["dayofweek"]);
                hours.close = string.Format("{0}", reader["close"]);
                hours.open = string.Format("{0}", reader["open"]);
                hours.businessId = string.Format("{0}", reader["business_id"]);
            }
            reader.Close();

            return hours;
        }

        public int[] getCheckinsPerMonth(string businessId)
        {
            StringBuilder sb = new StringBuilder(string.Format(
                  "SELECT count(*)"
                + " FROM Checkins as C"
                + " WHERE C.business_id = '{0}'"
                + " Group By C.month", businessId));
            string cmd = sb.ToString();
            NpgsqlCommand command = new NpgsqlCommand(cmd, con);
            NpgsqlDataReader reader = command.ExecuteReader();
            int[] checkins = new int[12];
            int i = 0;
            while (reader.Read())
            {
                checkins[i] = int.Parse(string.Format("{0}", reader[0]));
                i++;
            }
            reader.Close();

            return checkins;
        }

        public List<BusinessModel> GetBusinesses(string state, string city, string zipcode, string userId, List<string> categories = null, List<KeyValuePair<string, string>> attributes = null, int sortedBy = 0)
        {
            StringBuilder sb = new StringBuilder(string.Format("SELECT business.business_id, business.name, business.address, business.city, business.state,"
                    + " asin("
                    + "   sqrt("
                    + "     sin(radians(business.latitude - users.lat) / 2) ^ 2 +"
                    + "     sin(radians(business.longitude - users.long) / 2) ^ 2 *"
                    + "     cos(radians(users.lat)) *"
                    + "     cos(radians(business.latitude))"
                    + "   )"
                    + " ) * 7926.3352 AS distance,"
                    + " stars,"
                    + " numtips,"
                    + " numcheckins"
                    + " FROM business, users"
                    + " WHERE users.user_id = '{0}'"
                    + " AND business.state = '{1}'"
                    + " AND business.city = '{2}'"
                    + " AND business.zipcode = '{3}'", userId, state, city, zipcode));

            if (categories != null && categories.Count > 0)
            {
                sb.Append(" AND");
                for (int idx = 0; idx < categories.Count; idx++)
                {
                    if (idx > 0)
                    {
                        sb.Append(string.Format(" AND"));
                    }
                    sb.Append(string.Format(" business_id IN (SELECT business_id FROM categories WHERE category_name = '{0}')", categories[idx]));
                }
            }

            if (attributes != null && attributes.Count > 0)
            {
                sb.Append(" AND");
                for (int idx = 0; idx < attributes.Count; idx++)
                {
                    if (idx > 0)
                    {
                        sb.Append(string.Format(" AND"));
                    }
                    sb.Append(string.Format(" business_id IN (SELECT business_id FROM attributes WHERE attr_name = '{0}' AND value = '{1}')", attributes[idx].Key, attributes[idx].Value));
                }
            }

            if (sortedBy == 0)
            {
                sb.Append(" ORDER BY business.name");
            }
            else if (sortedBy == 1)
            {
                sb.Append(" ORDER BY stars DESC");
            }
            else if (sortedBy == 2)
            {
                sb.Append(" ORDER BY numtips DESC");
            }
            else if (sortedBy == 3)
            {
                sb.Append(" ORDER BY numcheckins DESC");
            }
            else if (sortedBy == 4)
            {
                sb.Append(" ORDER BY distance");
            }

            string cmd = sb.ToString();
            NpgsqlCommand command = new NpgsqlCommand(cmd, con);
            NpgsqlDataReader reader = command.ExecuteReader();
            List<BusinessModel> businesses = new List<BusinessModel>();
            int i = 0;
            while (reader.Read())
            {
                BusinessModel business = new BusinessModel();
                business.businessId = string.Format("{0}", reader["business_id"]);
                business.name = string.Format("{0}", reader["name"]);
                business.address = string.Format("{0}", reader["address"]);
                business.city = string.Format("{0}", reader["city"]);
                business.state = string.Format("{0}", reader["state"]);
                business.distance = string.Format("{0}", reader["distance"]);
                business.stars = string.Format("{0}", reader["stars"]);
                business.numtips = string.Format("{0}", reader["numtips"]);
                business.numcheckins = string.Format("{0}", reader["numcheckins"]);
                businesses.Add(business);
                i++;
            }
            reader.Close();

            return businesses;
        }

        public List<string> GetUserIds(string userName)
        {
            string cmd = string.Format("SELECT * FROM users WHERE name LIKE '{0}%'", userName);
            NpgsqlCommand command = new NpgsqlCommand(cmd, con);
            NpgsqlDataReader reader = command.ExecuteReader();
            List<string> userIds = new List<string>();
            while (reader.Read())
            {
                userIds.Add(string.Format("{0}", reader["user_id"]));
            }
            reader.Close();

            return userIds;
        }

        public UserModel GetUser(string userId)
        {
            string cmd = string.Format("SELECT * FROM users WHERE user_id = '{0}'", userId);
            NpgsqlCommand command = new NpgsqlCommand(cmd, con);
            NpgsqlDataReader reader = command.ExecuteReader();
            UserModel user = null;
            if (reader.Read())
            {
                user = new UserModel();
                user.userId = string.Format("{0}", reader["user_id"]);
                user.name = string.Format("{0}", reader["name"]);
                user.averageStars = string.Format("{0}", reader["average_stars"]);
                user.fans = string.Format("{0}", reader["fans"]);
                user.cool = string.Format("{0}", reader["cool"]);
                user.tipcount = string.Format("{0}", reader["tipcount"]);
                user.funny = string.Format("{0}", reader["funny"]);
                user.useful = string.Format("{0}", reader["useful"]);
                user.yelpingSince = string.Format("{0}", reader["yelping_since"]);
                user.totallikes = string.Format("{0}", reader["totallikes"]);
                user.latitude = string.Format("{0}", reader["lat"]);
                user.longitude = string.Format("{0}", reader["long"]);
            }
            reader.Close();

            return user;
        }

        public List<UserModel> GetFriends(string userId)
        {
            string cmd = string.Format("SELECT * FROM friend, users WHERE friend_of = '{0}' AND friend.friend_for = users.user_id"
                , userId);

            NpgsqlCommand command = new NpgsqlCommand(cmd, con);
            NpgsqlDataReader reader = command.ExecuteReader();
            List<UserModel> users = new List<UserModel>();
            while (reader.Read())
            {
                UserModel user = new UserModel();
                user.userId = string.Format("{0}", reader["user_id"]);
                user.name = string.Format("{0}", reader["name"]);
                user.averageStars = string.Format("{0}", reader["average_stars"]);
                user.fans = string.Format("{0}", reader["fans"]);
                user.cool = string.Format("{0}", reader["cool"]);
                user.tipcount = string.Format("{0}", reader["tipcount"]);
                user.funny = string.Format("{0}", reader["funny"]);
                user.useful = string.Format("{0}", reader["useful"]);
                user.yelpingSince = string.Format("{0}", reader["yelping_since"]);
                user.totallikes = string.Format("{0}", reader["totallikes"]);
                user.latitude = string.Format("{0}", reader["lat"]);
                user.longitude = string.Format("{0}", reader["long"]);
                users.Add(user);
            }
            reader.Close();

            return users;
        }

        public List<TipModel> GetFriendsTips(string userId)
        {
            string cmd = string.Format("SELECT users.name as userName, business.name as business, business.city, FRIENDTIPS.tiptext, FRIENDTIPS.tipdate FROM"
                                    + " (SELECT* FROM tip WHERE tip.user_id IN"
                                    + "     (SELECT user_id FROM friend, users"
                                    + "         WHERE friend_of = '{0}' AND friend.friend_for = users.user_id)) AS FRIENDTIPS,"
                                    + " (SELECT MAX(tipdate) as tipdate, user_id FROM tip GROUP BY user_id) AS LATESTTIP,"
                                    + " business, users"
                                    + " WHERE FRIENDTIPS.user_id = LATESTTIP.user_id"
                                    + "     AND FRIENDTIPS.tipdate = LATESTTIP.tipdate"
                                    + "     AND FRIENDTIPS.business_id = business.business_id"
                                    + "     AND FRIENDTIPS.user_id = users.user_id"
                , userId);

            NpgsqlCommand command = new NpgsqlCommand(cmd, con);
            NpgsqlDataReader reader = command.ExecuteReader();
            List<TipModel> tips = new List<TipModel>();
            while (reader.Read())
            {
                TipModel tip = new TipModel();
                tip.userName = string.Format("{0}", reader["username"]);
                tip.business = string.Format("{0}", reader["business"]);
                tip.city = string.Format("{0}", reader["city"]);
                tip.text = string.Format("{0}", reader["tiptext"]);
                tip.date = string.Format("{0}", reader["tipdate"]);
                tips.Add(tip);
            }
            reader.Close();
            return tips;
        }



        public void UpdateUserLatAndLong(string userId, string latitude, string longitude)
        {
            if (!float.TryParse(latitude, out float fLat))
            {
                latitude = "null";
            }
            if (!float.TryParse(longitude, out float fLong))
            {
                longitude = "null";
            }

            string cmd = string.Format("UPDATE users SET lat = {0}, long = {1} WHERE user_id = '{2}'",
                latitude,
                longitude,
                userId);
            NpgsqlCommand command = new NpgsqlCommand(cmd, con);
            command.ExecuteNonQuery();
        }

        public void IncrementLikeOnTip(string date, string userId, string businessId)
        {
            DateTime dateTime = Convert.ToDateTime(date);
            string cmd = string.Format("UPDATE tip SET likes = likes + 1" +
                                        " WHERE tipdate = (TO_TIMESTAMP('{0}', 'YYYY-MM-DD HH24:MI:SS')) AND user_id = '{1}' AND business_id = '{2}'",
                dateTime.ToString("yyyy-MM-dd H:mm:ss"),
                userId,
                businessId);
            NpgsqlCommand command = new NpgsqlCommand(cmd, con);
            command.ExecuteNonQuery();
        }

        public void InsertTip(string businessId, string text)// TO_TIMESTAMP('2017-02-31 30:8:00', 'YYYY-MM-DD HH24:MI:SS');
        {
            string cmd = string.Format("INSERT INTO Tip (tipdate, tiptext, likes, user_id, business_id) VALUES (TO_TIMESTAMP('{0}', 'YYYY-MM-DD HH24:MI:SS') , '{1}', {2}, '{3}', '{4}')",
                DateTime.Now.ToString("yyyy-MM-dd H:mm:ss"),
                text,
                0,
                AppManager.Instance.UserId,
                businessId
                );
            NpgsqlCommand command = new NpgsqlCommand(cmd, con);
            command.ExecuteNonQuery();
        }

        public void InsertCheckin(string businessId)
        {
            string cmd = string.Format("INSERT INTO Checkins (year, month, day, time, business_id) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}')",
                DateTime.Now.ToString("yyyy"),
                DateTime.Now.ToString("MM"),
                DateTime.Now.ToString("dd"),
                DateTime.Now.ToString("t"),
                businessId
                );
            NpgsqlCommand command = new NpgsqlCommand(cmd, con);
            command.ExecuteNonQuery();
        }

        public List<string> GetAllCategoriesByBusinessId(string businessId)
        {
            string cmd = string.Format("SELECT category_name FROM categories WHERE business_id = '{0}'", businessId);

            NpgsqlCommand command = new NpgsqlCommand(cmd, con);
            NpgsqlDataReader reader = command.ExecuteReader();
            List<string> categories = new List<string>();
            while (reader.Read())
            {
                categories.Add(string.Format("{0}", reader["category_name"]));
            }
            reader.Close();

            return categories;
        }

        public List<AttributeModel> GetAllAttributesByBusinessId(string businessId)
        {
            string cmd = string.Format("SELECT attr_name, value FROM attributes WHERE value <> 'False' AND business_id = '{0}'", businessId);

            NpgsqlCommand command = new NpgsqlCommand(cmd, con);
            NpgsqlDataReader reader = command.ExecuteReader();
            List<AttributeModel> attributes = new List<AttributeModel>();
            while (reader.Read())
            {
                AttributeModel model = new AttributeModel();
                model.attrName = string.Format("{0}", reader["attr_name"]);
                model.value = string.Format("{0}", reader["value"]);
                attributes.Add(model);
            }
            reader.Close();

            return attributes;
        }
    }
}
