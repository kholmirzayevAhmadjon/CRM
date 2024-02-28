using CRM_system_for_training_centers.Enums;
using CRM_system_for_training_centers.Extensions;
using CRM_system_for_training_centers.Helps;
using CRM_system_for_training_centers.Interfaces;
using CRM_system_for_training_centers.Models.Users;

namespace CRM_system_for_training_centers.Services;

public class UserService : IUserService
{
    private List<User> users;

    public async ValueTask<UserViewModel> CreateAsync(UserCreationModel user)
    {
        users = await FileIO.ReadAsync<User>(Constants.User_Path);

        var existUser = users.FirstOrDefault(u => u.Email == user.Email);
        //If model is exist and was deleted with this email , update this model
        if (existUser is not null)
        {
            if (existUser.IsDeleted)
            {
                // Recover deleted model with this email
                return await UpdateAsync(existUser.Id, user.MapTo<UserUpdateModel>(), true);
            }
            throw new Exception($"This user is already exist with this email={user.Email}");
        }

        var createUser = users.Create(user.MapTo<User>());
        await FileIO.WriteAsync(Constants.User_Path, users);

        return createUser.MapTo<UserViewModel>();

    }


    public async ValueTask<bool> DeleteAsync(long id)
    {
        users = await FileIO.ReadAsync<User>(Constants.User_Path);
        var existUser = users.FirstOrDefault(u => u.Id == id && !u.IsDeleted)
            ?? throw new Exception($"This user is not found with ID = {id}");

        existUser.IsDeleted = true;
        existUser.DeletedAt = DateTime.UtcNow;
        await FileIO.WriteAsync(Constants.User_Path, users);
        return true;
    }

    public async ValueTask<IEnumerable<UserViewModel>> GetAllAsync()
    {
        users = await FileIO.ReadAsync<User>(Constants.User_Path);
        return users.Where(u => !u.IsDeleted).ToList().MapTo<UserViewModel>().ToList();
    }

    public async ValueTask<UserViewModel> GetByIdAsync(long id)
    {
        users = await FileIO.ReadAsync<User>(Constants.User_Path);
        var existUser = users.FirstOrDefault(u => u.Id == id && !u.IsDeleted)
            ?? throw new Exception($"This user is not found with ID = {id}");

        return existUser.MapTo<UserViewModel>();
    }

    public async ValueTask<UserViewModel> GetByEmailPassword(string email, string password)
    {
        users = await FileIO.ReadAsync<User>(Constants.User_Path);
        var existUser = users.FirstOrDefault(u => u.Email == email && u.Password == password && !u.IsDeleted)
            ?? throw new Exception("Login or password error" );

        return existUser.MapTo<UserViewModel>();
    }
    public async ValueTask<UserViewModel> UpdateAsync(long id, UserUpdateModel user, bool isUsedDeleted = false)
    {
        users = await FileIO.ReadAsync<User>(Constants.User_Path);
        var existUser = new User();

        if (isUsedDeleted)
        {
            existUser = users.FirstOrDefault(users => users.Id == id);
            existUser.IsDeleted = false;
        }
        else
        {
            existUser = users.FirstOrDefault(u => u.Id == id && !u.IsDeleted)
                ?? throw new Exception($"This user is not found with ID = {id}");
        }
        existUser.Id = id;
        existUser.FirstName = user.FirstName;
        existUser.LastName = user.LastName;
        existUser.Email = user.Email;
        existUser.Password = user.Password;
        existUser.Role = user.Role;
        existUser.UpdatedAt = DateTime.UtcNow;

        await FileIO.WriteAsync(Constants.User_Path, users);

        return existUser.MapTo<UserViewModel>();
    }

}
