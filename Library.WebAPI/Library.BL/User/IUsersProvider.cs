using Library.BL.User.Entites;

namespace Library.BL.User
{
    public interface IUsersProvider
    {
        IEnumerable<UserModel> GetAllUsers();
        UserModel GetUser(Guid userId);
    }
}
