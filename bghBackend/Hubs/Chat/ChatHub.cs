using bghBackend.Application.Common.DTOs;
using bghBackend.Domain.Models.Chat;
using bghBackend.Infra;
using bghBackend.Infra.Utilities;
using Microsoft.AspNetCore.SignalR;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Concurrent;

namespace bghBackend.Hubs.Chat
{
    public class ChatHub : Hub
    {
        
        private readonly SharedDb _sharedDb;
        public ChatHub()
        {
            _sharedDb = new();
        }


        /// whenever an admin or support user gets online it will be added to online support list
        public Task JoinToOnlineSupports(UserDTO joinedUser)
        {
            if(!joinedUser.Roles.IsNullOrEmpty() && (joinedUser.Roles!.Contains(SD.ROLE_ADMIN) || joinedUser.Roles.Contains(SD.ROLE_ADMIN)))
            {
                _sharedDb.OnlineSupportUsers.Add(joinedUser);

                return Task.CompletedTask;
            }
            throw new Exception("user can't join the list");
        }



      





        




        //public override Task OnConnectedAsync()
        //{
        //    return base.OnConnectedAsync();
        //}

        //public override Task OnDisconnectedAsync(Exception? exception)
        //{
        //    return base.OnDisconnectedAsync(exception);
        //}

    }
}
