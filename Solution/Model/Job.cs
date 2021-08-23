using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Solution.Model
{
    public class Job
    {
        public int Kod { get; set; }
        public int Okuyucu { get; set; }
        public int Uzaklık { get; set; }
        public int VeriNo { get; set; }
        public DateTime Tarih { get; set; }
    }
}
