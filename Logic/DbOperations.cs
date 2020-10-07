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
        public async void DeleteAllFriends()
        {
            _friendsContext.Friends.RemoveRange(_friendsContext.Friends);
            await _friendsContext.SaveChangesAsync();
        }
        public int GetFriendsCount()
        {
            return _friendsContext.Friends.Count();
        }
        public async void DeleteFriend(Friend friend)
        {
            _friendsContext.Friends.Remove(friend);
            await _friendsContext.SaveChangesAsync();
        }
        public async Task<Friend> GetFriendById(int? id)
        {
            return await _friendsContext.Friends.FirstOrDefaultAsync(p => p.Id == id);
        }
        public async void UpdateFriend(Friend friend)
        {
            _friendsContext.Friends.Update(friend);
            await _friendsContext.SaveChangesAsync();
        }
        public async void AddListOfFriends(List<Friend> friendsList)
        {
            _friendsContext.Friends.AddRange(friendsList);
            await _friendsContext.SaveChangesAsync();
        }
        public async void AddFriend(Friend friend)
        {
            _friendsContext.Friends.Add(friend);
            await _friendsContext.SaveChangesAsync();
        }
        public async Task<List<Friend>> GetFriends()
        {
            return await _friendsContext.Friends.ToListAsync();
        }
    }   
}
