using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public class Post
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [Required(ErrorMessage = "EntryName is required")]
        public string EntryName { get; set; }
        [Required(ErrorMessage = "EntryText is required")]
        public string EntryText { get; set; }
        [Required(ErrorMessage = "CreatedDate is required")]
        public string CreatedDate { get; set; }
        [Required(ErrorMessage = "BlogId is required")]
        public string BlogId { get; set; }
    }
}
