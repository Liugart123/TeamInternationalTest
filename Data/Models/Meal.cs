using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public class Meal
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Origin is required")]
        public string Origin { get; set; }
        [Required(ErrorMessage = "Price is required")]
        public string Price { get; set; }
        [Required(ErrorMessage = "Decorations are required")]
        public string Decorations { get; set; }
    }
}
