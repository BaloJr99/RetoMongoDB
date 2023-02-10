using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Retomuub.Data.Model
{
    public class User
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!; 
        public string Rol { get; set; } = null!;
        public string Salt { get; set; } = null!;
    }
}