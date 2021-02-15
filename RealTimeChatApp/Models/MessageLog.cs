using System;
using System.Collections.Generic;

#nullable disable

namespace RealTimeChatApp.Models
{
    public partial class MessageLog
    {
        public int MessageLogId { get; set; }
        public int? ChannelId { get; set; }
        public string NickName { get; set; }
        public string Message { get; set; }

        public virtual Channel Channel { get; set; }
    }
}
