﻿using AutoMapper;
using Library.BL.User.Entites;
using Library.DataAccess.Entities;
using Library.DataAccess;

namespace Library.BL.User
{
    public class UsersManager : IUsersManager
    {
        private readonly IRepository<UserEntity> _userRepository;
        private readonly IMapper _mapper;

        public UsersManager(IRepository<UserEntity> userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public void DeleteUser(Guid id)
        {
            UserEntity? entity = _userRepository.GetById(id);

            if (entity == null)
            {
                throw new ArgumentException("Нет пользователя по заданному id");
            }

            _userRepository.Delete(entity);
        }

        public UserModel UpdateUser(Guid id, UpdateUserModel model)
        {
            UserEntity? entity = _userRepository.GetById(id);

            if (entity == null)
            {
                throw new ArgumentException("Нет пользователя по заданному id");
            }

            UserEntity newEntity = _mapper.Map<UserEntity>(model);
            newEntity.Id = entity.Id;
            newEntity.ExternalId = entity.ExternalId;
            newEntity.CreationTime = entity.CreationTime;
            newEntity.ModificationTime = entity.ModificationTime;

            _userRepository.Save(entity);

            return _mapper.Map<UserModel>(entity);
        }
    }
}
