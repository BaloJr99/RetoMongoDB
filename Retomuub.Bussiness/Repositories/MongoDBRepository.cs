using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace Retomuub.Bussiness.Repositories
{
    public class MongoDBRepository
    {
        public MongoClient client;

        public IMongoDatabase db;

        public MongoDBRepository()
        {
            client = new MongoClient("mongodb+srv://testUser:zNW3wMQsbUWXc0Nk@retomuub.psqtodl.mongodb.net/?retryWrites=true&w=majority");
            db = client.GetDatabase("retomuub");
        }
    }
}