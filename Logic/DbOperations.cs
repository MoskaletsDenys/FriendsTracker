using FriendsTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FriendsTracker.Logic
{
    public class DbOperations
    {
        private readonly FriendsContext _friendsContext;
        public DbOperations(FriendsContext context)
        {
            _friendsContext = context;
        }
        public async void DeleteAll()
        {
            _friendsContext.Friends.RemoveRange(_friendsContext.Friends);
            await _friendsContext.SaveChangesAsync();
        }
    }
}
