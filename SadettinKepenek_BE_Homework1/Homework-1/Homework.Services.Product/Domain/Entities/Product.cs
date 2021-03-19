using System.ComponentModel.DataAnnotations;
using Homework.Services.Product.Domain.Entities.Abstract;

namespace Homework.Services.Product.Domain.Entities
{
    public class Product:IEntity<int> 
    {
        
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        
    }
}