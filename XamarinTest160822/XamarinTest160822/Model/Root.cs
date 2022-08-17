using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinTest160822.Model
{
    public class Root
    {
        [JsonProperty("Root")]
        public List<Product> products { get; set; }
    }
}
