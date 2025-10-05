using QuizApp.Models;

namespace QuizApp.Services.Repositories;

public class UserRepository : Repository<User>, IRepository<User>
{
    public UserRepository(DatabaseContext context) : base(context)
    {
    }

    public new void Create(User item)
    {
        var hashedPassword = PasswordHasher.Hash(item.Password);
        item.Password = hashedPassword;
        base.Create(item);
    }
    
    public new User Get(int id)
    {
        return base.Get(id) ?? throw new KeyNotFoundException($"User with ID {id} not found");
    }

    public User? validateUser(string username, string password)
    {
        var user = base.GetAll()
                       .AsQueryable()
                       .FirstOrDefault(u => u.Username == username);

        if (user == null)
        {
            return null;
        }

        return PasswordHasher.Verify(password, user.Password) ? user : null;
    }
    
    public void Delete(int id)
    {
        var userToDelete = base.Get(id);
        if (userToDelete != null)
        {
            base.Delete(userToDelete);
        }
    }
}