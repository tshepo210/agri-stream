using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace agristeam.function.Models
{
    public class Irrigation
    {
        public int IrrigationId { get; set; }

        public string Description { get; set; } = string.Empty;

        public DateTime ProcessedDate { get; set; }
            = DateTime.UtcNow;
    }

}
