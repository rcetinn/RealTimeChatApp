using System;
using System.Collections.Generic;

#nullable disable

namespace RealTimeChatApp.Models
{
    public partial class Channel
    {
        public Channel()
        {
            MessageLogs = new HashSet<MessageLog>();
        }

        public int ChannelId { get; set; }
        public string ChannelName { get; set; }

        public virtual ICollection<MessageLog> MessageLogs { get; set; }
    }
}
