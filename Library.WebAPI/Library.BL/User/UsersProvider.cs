using AutoMapper;
using Library.DataAccess.Entities;
using Library.DataAccess;
using Library.BL.User.Entites;

namespace Library.BL.User
{
    public class UsersProvider : IUsersProvider
    {
        private readonly IRepository<UserEntity> _userRepository;
        private readonly IMapper _mapper;

        public UsersProvider(IRepository<UserEntity> usersRepository, IMapper mapper)
        {
            _userRepository = usersRepository;
            _mapper = mapper;
        }

        public UserModel GetUser(Guid userId)
        {
            UserEntity? user = _userRepository.GetById(userId);

            if (user is null)
            {
                throw new ArgumentException("Нет пользователя по заданному id");
            }

            return _mapper.Map<UserModel>(user);
        }

        public IEnumerable<UserModel> GetAllUsers()
        {
            IEnumerable<UserEntity> users = _userRepository.GetAll();

            return _mapper.Map<IEnumerable<UserModel>>(users);
        }
    }
}
