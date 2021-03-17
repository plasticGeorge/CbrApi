﻿using Newtonsoft.Json;
using System;

namespace CBRapi.Models
{
    [Serializable]
    public class Valute
    {
        [JsonProperty("ID")]
        public string ID { get; set; }

        [JsonProperty("NumCode")]
        public string NumCode { get; set; }

        [JsonProperty("CharCode")]
        public string CharCode { get; set; }

        [JsonProperty("Nominal")]
        public int Nominal { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Value")]
        public decimal Value { get; set; }

        [JsonProperty("Previous")]
        public decimal Previous { get; set; }
    }
}
