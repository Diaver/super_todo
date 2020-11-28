namespace Users.Database
{
    public interface IUsersDbContextFactory
    {
        UsersDbContext Create();
    }
}