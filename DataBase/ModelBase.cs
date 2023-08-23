using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using ToDo;
using static ToDo.DataBase.Issue;

namespace ToDo.DataBase
{
    public class ModelBase : DbContext
    {
        public DbSet<Issue> Tasks { get; set; } = null!;
        public ModelBase(DbContextOptions<ModelBase> options)
            : base(options)
        {
            Database.EnsureCreated();   // создаем базу данных при первом обращении
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Issue>().HasData(
        //            new Issue(1, "First"),
        //            new Issue(2, "Second"),
        //            new Issue(3, "Third")
        //    );
        //}

        //public void AddTask(Issue task)
        //{
        //    Tasks.Add(task);
        //}
        //public List<Issue>? GetTasks()
        //{
        //    return Tasks.ToList();
        //}

        //public Issue? GetTask(int id)
        //{
        //    return Tasks.SingleOrDefault(t => t.Id == id);
        //}

        //public IResult DeleteTask(int id)
        //{
        //    // получаем пользователя по id
        //    Issue? task =  Tasks.FirstOrDefault(u => u.Id == id);

        //    //// если не найден, отправляем статусный код и сообщение об ошибке
        //    if (task == null) return Results.NotFound(new { message = "Пользователь не найден" });

        //    // если пользователь найден, удаляем его
        //    Tasks.Remove(task);
        //    SaveChanges();
        //    return Results.Json(task);
        //}

        //public IResult UpdateTask(int id, string newDescription)
        //{
        //    // получаем пользователя по id
        //    Issue? task = Tasks.FirstOrDefault(u => u.Id == id);

        //    //// если не найден, отправляем статусный код и сообщение об ошибке
        //    if (task == null) return Results.NotFound(new { message = "Пользователь не найден" });

        //    // если пользователь найден, удаляем его
        //    task.Description = newDescription;
        //    SaveChanges();
        //    return Results.Json(task);
        //}

        //public void CompleteTask(int id)
        //{
        //    // получаем пользователя по id
        //    Issue? task = Tasks.FirstOrDefault(u => u.Id == id);
        //    task.hasComplete = true;
        //    SaveChanges();
        //}
    }
}
