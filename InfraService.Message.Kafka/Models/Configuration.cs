using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraService.Message.Kafka.Models
{
    public class Configuration
    {
        public ConfigServerProperties Properties { get; set; }
        public bool AcksEnabled { get; set; }
        public bool CommitOnConsume { get; set; }
    }
}
