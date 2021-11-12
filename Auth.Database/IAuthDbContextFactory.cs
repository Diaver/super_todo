namespace Auth.Database
{
    public interface IAuthDbContextFactory
    {
        AuthDbContext Create();
    }
}