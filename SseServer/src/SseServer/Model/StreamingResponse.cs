using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SseServer.Model
{

    public class StreamingResponse
    {
        public string path { get; set; }
        public Data data { get; set; }
    }

    public class Data
    {
        public string key { get; set; }
        public int version { get; set; }
        public bool on { get; set; }
        public object[] prerequisites { get; set; }
        public string salt { get; set; }
        public string sel { get; set; }
        public object[] targets { get; set; }
        public object[] rules { get; set; }
        public Fallthrough fallthrough { get; set; }
        public int offVariation { get; set; }
        public bool[] variations { get; set; }
        public bool trackEvents { get; set; }
        public object debugEventsUntilDate { get; set; }
        public bool deleted { get; set; }
    }

    public class Fallthrough
    {
        public int variation { get; set; }
    }
}
