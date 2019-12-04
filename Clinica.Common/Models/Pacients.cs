using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Clinica.Common.Models
{
    public partial class Pacients
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("pictureUrl")]
        public object PictureUrl { get; set; }

        [JsonProperty("birthDate")]
        public object BirthDate { get; set; }

        [JsonProperty("user")]
        public User User { get; set; }

        [JsonProperty("imageFullPath")]
        public object ImageFullPath { get; set; }
    }
}
