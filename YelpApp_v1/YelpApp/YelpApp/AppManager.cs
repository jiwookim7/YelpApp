using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YelpApp
{
    public sealed class AppManager
    {
        private static AppManager? instance = null;
        private static readonly object padlock = new object();

        AppManager()
        {
        }

        public static AppManager Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new AppManager();
                    }
                    return instance;
                }
            }
        }

        public string UserId { get; set; }
    }
}
