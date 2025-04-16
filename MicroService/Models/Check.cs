using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroService.Models
{
    public class Check
    {
        public Guid CheckId { get; set; }
        public string? RoutingNumber { get; set; }
        public string? AccountNumber { get; set; }
        public decimal Amount { get; set; }
    }
}
