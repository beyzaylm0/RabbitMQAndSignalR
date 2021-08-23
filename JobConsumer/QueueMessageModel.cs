using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobConsumer
{
    public class QueueMessageModel
    {
        public Job JobModel { get; set; }
        public string Frekans { get; set; }
    }
}
