using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Homework_4.Jwt.API.Entities;

namespace Homework_4.Jwt.API.Services
{
    public interface IRoomService
    {
        Task<List<RoomEntity>> GetRoomsAsync();

        Task<RoomEntity> GetRoomAsync(Guid id);   
    }
}