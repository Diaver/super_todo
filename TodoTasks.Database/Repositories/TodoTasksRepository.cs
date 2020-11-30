using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiService.Models.Api.TodoTasksApi.Response;
using Microsoft.EntityFrameworkCore;
using TodoTasks.Database.Base;
using TodoTasks.Database.Models;

namespace TodoTasks.Database.Repositories
{
    public interface ITodoTaskRepository : IRepository<TodoTask>
    {
        Task<IEnumerable<TodoTaskResponse>> GetByUserId(Guid userId);
    }

    public class TodoTaskRepository : BaseRepository<TodoTask>, ITodoTaskRepository
    {
        public TodoTaskRepository(ITodoTasksDbContextFactory contextManager)
            : base(contextManager)
        {
        }

        public async Task<IEnumerable<TodoTaskResponse>> GetByUserId(Guid userId)
        {
            await using TodoTasksDbContext dbContext = CreateDbContext();
            
            return await dbContext.TodoTasks.Where(c => c.UserId == userId && c.IsDeleted == false)
                .Select(c =>
                    new TodoTaskResponse
                    {
                        TodoTaskId = c.TodoTaskId,
                        Text = c.Text,
                        Status = c.TodoTaskStatus,
                        UserId = c.UserId
                    }).ToListAsync();
        }
    }
}