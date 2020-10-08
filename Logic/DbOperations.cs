using FriendsTracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        public async Task<List<Friend>> GetAllFriends()
        {
            return await _friendsContext.Friends.ToListAsync();
        }

        public int GetFriendsCount()
        {
            return _friendsContext.Friends.Count();
        }

        public async Task<Friend> GetFriendById(int? id)
        {
            return await _friendsContext.Friends.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task AddListOfFriends(List<Friend> friendsList)
        {
            _friendsContext.Friends.AddRange(friendsList);
            await _friendsContext.SaveChangesAsync();
        }

        public async Task AddFriend(Friend friend)
        {
            _friendsContext.Friends.Add(friend);
            await _friendsContext.SaveChangesAsync();
        }
        public async Task DeleteFriend(Friend friend)
        {
            _friendsContext.Friends.Remove(friend);
            await _friendsContext.SaveChangesAsync();
        }

        public async Task DeleteAllFriends()
        {
            _friendsContext.Friends.RemoveRange(_friendsContext.Friends);
            await _friendsContext.SaveChangesAsync();
        }

        public async Task UpdateFriend(Friend friend)
        {
            _friendsContext.Friends.Update(friend);
            await _friendsContext.SaveChangesAsync();
        }
    }   
}
