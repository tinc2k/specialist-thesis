using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wayfarer.Mvc.Repositories
{
    interface IChatRepository
    {
        void LogChatLine(string username, string channel, string line);
    }
}
