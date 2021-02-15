using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using RealTimeChatApp.Models;
using RealTimeChatApp.signalR.Hubs;

namespace RealTimeChatApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHubContext<ChatHub> _chatHub;
        private CHATAPPDBContext _dbcontext;

        public HomeController(IHubContext<ChatHub> chatHub,CHATAPPDBContext dbcontext)
        {
            _chatHub = chatHub;
            _dbcontext = dbcontext;
        }

        public IActionResult Index()
         {
            var channelList = _dbcontext.Channels.ToList(); 
            return View(channelList);
        }


        public void SaveMessage(int channelId, string nickName ,string message)
        {
            _dbcontext.MessageLogs.Add(new MessageLog { ChannelId = channelId, NickName=nickName, Message = message });
            _dbcontext.SaveChanges();
        }

        public List<MessageLog> GetMessageHistoryByChannelId(int channelId) 
        {
          var list = _dbcontext.MessageLogs.Where(w=>w.ChannelId == channelId).ToList();
          return list;
        }

    }
}
