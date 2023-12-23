using Library.BL.User.Entites;

namespace Library.BL.User
{
    public interface IUsersManager
    {
        void DeleteUser(Guid id);
        UserModel UpdateUser(Guid id, UpdateUserModel model);
    }
}
