using Homework_4.Jwt.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Homework_4.Jwt.API.Data
{
    public class HotelApiDbContext : DbContext
    {
        public HotelApiDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<RoomEntity> Rooms { get; set; }
    }
}