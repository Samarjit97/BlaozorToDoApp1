using Domain.Contracts;
using Domain.Models;

namespace FileData.DataAccess;

public class TodoFileDao:IToDoHome
{
    private FileContent fileContent;

    public TodoFileDao(FileContent fileContent)
    {
        this.fileContent = fileContent;
    }

    public async Task<ICollection<Todo>> GetAsync()
    {
        ICollection<Todo> todos = fileContent.Todos;
        return todos;
    }

    public async Task<Todo> GetById(int id)
    {
        return fileContent.Todos.First(t => t.Id == id);
    }

    public async Task<Todo> AddAsync(Todo todo)
    {
        int largestId = fileContent.Todos.Max(t => t.Id);
        int nextId = largestId + 1;
        todo.Id = nextId;
        fileContent.Todos.Add(todo);
        fileContent.SaveChanges();
        return todo;
    }

    public async Task DeleteAsync(int id)
    {
        Todo toDelete = fileContent.Todos.First(t => t.Id == id);
        fileContent.Todos.Remove(toDelete);
        fileContent.SaveChanges();
    }

    public async Task UpdateAsync(Todo todo)
    {
        Todo toUpdate = fileContent.Todos.First(t => t.Id == todo.Id);
        toUpdate.IsCompleted =todo.IsCompleted;
        toUpdate.OwnerId = todo.OwnerId;
        fileContent.SaveChanges();
    }
}