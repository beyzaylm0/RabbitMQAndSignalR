using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobConsumer

{
    public class Job
    {
        public int Kod { get; set; }
        public int Okuyucu { get; set; }
        public int Uzaklik { get; set; }
        public int VeriNo { get; set; }
        public DateTime Tarih { get; set; }
    }
}
