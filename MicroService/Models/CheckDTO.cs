using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroService.Models
{
    public class CheckDTO
    {
        public int Index { get; internal set; }
        public Check? Check { get; internal set; }
        public int EWSData {  get; internal set; }
        public int IIRTData { get; set; }
    }
}
