using Newtonsoft.Json;
using System;

namespace AdpLabs.Infaestructure.interview.adpeai
{
    public class SubmitTaskRequestModel
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("result")]
        public Int64 Result{ get; set; }
    }
}
