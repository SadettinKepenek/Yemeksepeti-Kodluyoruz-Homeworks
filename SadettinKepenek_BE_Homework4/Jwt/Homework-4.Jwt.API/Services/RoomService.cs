using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Homework_4.Jwt.API.Data;
using Homework_4.Jwt.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Homework_4.Jwt.API.Services
{
    public class RoomService : IRoomService
    {
        private readonly HotelApiDbContext _dbContext;
        private readonly IMapper _mapper;
      
        public RoomService(HotelApiDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<List<RoomEntity>> GetRoomsAsync()
        {
            var roomEntities = await _dbContext.Rooms.ToListAsync();
            var result = roomEntities.Select(room => _mapper.Map<RoomEntity>(room))
                .ToList();

            return result;
        }
        public async Task<RoomEntity> GetRoomAsync(Guid id)
        {
            var roomEntity = await _dbContext.Rooms.SingleOrDefaultAsync(room => room.Id == id);
            if (roomEntity == null)
                return null;
            
            return _mapper.Map<RoomEntity>(roomEntity);


        }
    }
}