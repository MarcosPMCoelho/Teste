using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GanhoCapital.Entity
{

    public class  Capitais
    {
        public List<Capital> capitals { get; set; }
    }
    public class Capital
    {
        public string? operation { get; set; }
        
        [JsonPropertyName("unit-cost")]
        public decimal unitcost { get; set; }
        public int quantity { get; set; }
        public decimal? tax { get; set; } = null;
    }
}
