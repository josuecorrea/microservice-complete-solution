using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraService.Message.Kafka.Models
{
    public class Message
    {
        public Message(Guid correlationId, string messageValue, long offset, int partition, string topic)
        {
            CorrelationId = correlationId;
            MessageValue = messageValue;
            Offset = offset;
            Partition = partition;
            Topic = topic;
        }

        public Guid CorrelationId { get; set; }
        public string MessageValue { get; set; }
        public long Offset { get; set; }
        public int Partition { get; set; }
        public string Topic { get; set; }

    }
}
