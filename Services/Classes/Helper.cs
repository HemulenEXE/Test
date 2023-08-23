using Microsoft.AspNetCore.Http;
using ToDo.DataBase;

namespace ToDo.Services.Classes
{
    public static class Helper
    {
        public static List<Issue> FilterTasks(ModelBase db, bool queryHasCompleted)
        {
            return db.Tasks.Where(task => task.hasComplete == queryHasCompleted).ToList();
        }
    }
}
