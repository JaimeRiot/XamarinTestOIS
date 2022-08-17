using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinTest160822.Model
{
    public partial class Rating
    {
        [JsonProperty("rate")]
        public double Rate { get; set; }
        [JsonProperty("count")]
        public int Count { get; set; }
    }
}
