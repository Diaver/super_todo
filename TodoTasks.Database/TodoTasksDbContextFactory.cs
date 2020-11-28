using Services;

namespace TodoTasks.Database
{
    public class TodoTasksDbContextFactory : ITodoTasksDbContextFactory
    {
        private readonly IAppConfigurationProvider _appConfigurationProvider;

        public TodoTasksDbContextFactory(IAppConfigurationProvider appConfigurationProvider)
        {
            _appConfigurationProvider = appConfigurationProvider;
        }

        public TodoTasksDbContext Create()
        {
            return new TodoTasksDbContext(_appConfigurationProvider);
        }
    }
}