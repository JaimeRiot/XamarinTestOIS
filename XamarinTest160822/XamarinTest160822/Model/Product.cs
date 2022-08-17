using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinTest160822.Model
{
    public class Product
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("price")]
        public double Price { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("category")]
        public string Category { get; set; }
        [JsonProperty("image")]
        public string Image { get; set; }
        [JsonProperty("rating")]
        [Ignore]
        public Rating Rating { get; set; }
    }
}
