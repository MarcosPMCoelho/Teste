using Newtonsoft.Json;


namespace GanhoCapital.Entity
{

    public class Capital
    {
        [JsonProperty("operation")]
        public string operation { get; set; }
        
        [JsonProperty("unit-cost")]
        public decimal unitcost { get; set; }

        [JsonProperty("quantity")]
        public int quantity { get; set; }
    }
}
