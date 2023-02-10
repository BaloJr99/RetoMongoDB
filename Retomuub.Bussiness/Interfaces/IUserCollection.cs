using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Retomuub.Data.DTO;

namespace Retomuub.Bussiness.Interfaces
{
    public interface IUserCollection
    {
        Task InsertUser(UserDTO userDTO);
        Task<bool> LoginUser(LoggedUserDTO userDTO);
        Task LogoutUser(UserDTO userDTO);
    }
}