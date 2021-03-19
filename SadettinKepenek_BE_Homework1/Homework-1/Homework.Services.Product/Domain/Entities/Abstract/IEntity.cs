using System.ComponentModel.DataAnnotations;

namespace Homework.Services.Product.Domain.Entities.Abstract
{
    public interface IEntity<TKey>
    {
        [Key]
        public TKey Id { get; set; }
    }
}