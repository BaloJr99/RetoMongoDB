using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Retomuub.Bussiness.Interfaces;
using Retomuub.Bussiness.Repositories;
using Retomuub.Data.DTO;
using Retomuub.Data.Model;
using MongoDB.Driver;
using MongoDB.Bson;
using AutoMapper;
using System.Security.Cryptography;
using System.Text;

namespace Retomuub.Bussiness.Services
{
    public class UserService: IUserCollection
    {
        private readonly IMongoCollection<User> _user;
        private readonly IMapper _mapper;

        public UserService(IMongoCollection<User> user, IMapper mapper)
        {
            _user = user;
            _mapper = mapper;
        }

        public async Task InsertUser(UserDTO userDTO)
        {
            var salt = DateTime.Now.ToString();
            userDTO.Password = HashPassword($"{userDTO.Password}{salt}");
            User user = _mapper.Map<User>(userDTO);
            user.Salt = salt;
            await _user.InsertOneAsync(user);
        }

        private string HashPassword(string password){
            var sha = SHA256.Create();
            var passwordBytes = Encoding.Default.GetBytes(password);

            var hashedPassword = sha.ComputeHash(passwordBytes);

            return Convert.ToHexString(hashedPassword);
        }

        public async Task<UserDTO> LoginUser(LoggedUserDTO userDTO)
        {
            var filter = Builders<User>.Filter.Eq( s => s.Username, userDTO.Username);
            var loginUser = (await _user.FindAsync(filter)).ToList();
            if(loginUser != null){
                if(HashPassword($"{userDTO.Password}{loginUser.First().Salt}") == loginUser.First().Password){
                    Console.WriteLine("Logged");
                    return _mapper.Map<UserDTO>(loginUser.First());
                }
            }
            return null!;
        }

        public Task LogoutUser(UserDTO userDTO)
        {
            throw new NotImplementedException();
        }

        public async Task<UserDTO> GetUsuario(string id)
        {
            var filter = Builders<User>.Filter.Eq( s => s.Id, ObjectId.Parse(id));
            var loginUser = (await _user.FindAsync(filter)).ToList();
            var loginUserDTO = _mapper.Map<UserDTO>(loginUser.First());
            return loginUserDTO;
        }
    }
}