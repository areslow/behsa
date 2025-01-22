//using bghBackend.Domain.Models;
using bghBackend.Application.Common.DTOs;
using bghBackend.Domain.Models.Chat;
using System.Collections.Concurrent;

namespace bghBackend.Infra
{
    public class SharedDb
    {
        private readonly ConcurrentDictionary<string, UserConnection> _connection = new();
        private readonly ICollection<UserDTO> _onlineSupportUsers = [];
        private readonly ICollection<UserDTO> _availableSupportUsers = [];

        public ConcurrentDictionary<string, UserConnection> Connection => _connection;
        public ICollection<UserDTO> OnlineSupportUsers => _onlineSupportUsers;
        public ICollection<UserDTO> AvailableSupportUser => _availableSupportUsers;
    }
}
