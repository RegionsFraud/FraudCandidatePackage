using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroService.Models
{
    public class Request
    {
        public List<Check> Checks { get; set; } = new List<Check>();
    }
}
