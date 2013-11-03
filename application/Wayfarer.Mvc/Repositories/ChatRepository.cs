using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Wayfarer.Mvc.Models;

namespace Wayfarer.Mvc.Repositories
{
    public class ChatRepository : IChatRepository
    {
        WayfarerContext _context;

        public ChatRepository()
        {
            _context = new WayfarerContext();
        }

        public void LogChatLine(string username, string channel, string line)
        {
            throw new NotImplementedException();
        }
    }
}