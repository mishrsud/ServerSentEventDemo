using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SseServer.Model
{

    public class BulkRequest
    {
        public Item[] Items { get; set; }
    }

    public class Item
    {
        public long creationDate { get; set; }
        public User user { get; set; }
        public string kind { get; set; }
        public string key { get; set; }
        public string userKey { get; set; }
        public int variation { get; set; }
        public int version { get; set; }
        public bool value { get; set; }
        public bool _default { get; set; }
        public Features features { get; set; }
        public long startDate { get; set; }
        public long endDate { get; set; }
    }

    public class User
    {
        public string key { get; set; }
    }

    public class Features
    {
        public MySettingEnabled1 mysettingenabled { get; set; }
    }

    public class MySettingEnabled1
    {
        public bool _default { get; set; }
        public Counter[] counters { get; set; }
    }

    public class Counter
    {
        public int variation { get; set; }
        public bool value { get; set; }
        public int version { get; set; }
        public int count { get; set; }
    }

}
