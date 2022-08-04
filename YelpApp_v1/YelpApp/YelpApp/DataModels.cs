using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YelpApp
{
    public class BusinessModel
    {
        public string businessId;
        public string name;
        public string address;
        public string city;
        public string state;
        public string distance;
        public string stars;
        public string numtips;
        public string numcheckins;
    }

    public class UserModel
    {
        public string userId;
        public string name;
        public string averageStars;
        public string fans;
        public string cool;
        public string tipcount;
        public string funny;
        public string useful;
        public string yelpingSince;
        public string totallikes;
        public string latitude;
        public string longitude;
    }

    public class AttributeModel
    {
        public string attrName;
        public string value;
    }

    public class TipFetchedModel
    {
        public string userId;
        public string userName;
        public string date;
        public string text;
        public string likes;
    }

    public class TipInsertingModel
    {
        public string userName;
        public string date;
        public string text;
        public string likes;
        public string userId;
        public string businessId;
    }


    public class FriendReviews
    {
        public string userName;
        public string date;
        public string text;
    }
    public class TipModel
    {
        public string userName;
        public string business;
        public string city;
        public string text;
        public string date;
    }

    public class HoursModel
    {
        public string dayofweek;
        public string close;
        public string open;
        public string businessId;

    }
}
