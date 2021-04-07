using FluentAssertions;
using FluentAssertions.Primitives;
using Go_Rest_API_Automation.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Go_Rest_API_Automation.client
{
    public class User
    {
        [JsonProperty("code")]
        public int Code { get; set; }

        [JsonProperty("meta")]
        public string Meta { get; set; }

        [JsonProperty("data")]
        public Data Data { get; set; }

    }
}
