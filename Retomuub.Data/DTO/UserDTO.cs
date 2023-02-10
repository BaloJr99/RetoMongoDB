using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Retomuub.Data.DTO
{
    public class UserDTO
    {
        [BsonId]
        [Required]
        public ObjectId Id { get; set; }
        [Required]
        public string Username { get; set; } = null!;
        [Required]
        public string Email { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!; 
        [Required]
        public string Rol { get; set; } = null!;
    }

    public class LoggedUserDTO
    {
        public ObjectId? Id { get; set; }
        [Required]
        public string Username { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!; 
    }
}