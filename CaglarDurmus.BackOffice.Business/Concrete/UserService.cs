using CaglarDurmus.BackOffice.Business.Abstract;
using CaglarDurmus.BackOffice.DataAccess.Abstract;
using CaglarDurmus.BackOffice.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaglarDurmus.BackOffice.Business.Concrete
{
    public class UserService : IUserService
    {
        private IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public User GetUser(string userName)
        {
            return _userRepository.Get(p => p.UserName == userName);
        }

        public User GetById(int userId)
        {
            return _userRepository.Get(p => p.Id == userId);
        }
    }
}
