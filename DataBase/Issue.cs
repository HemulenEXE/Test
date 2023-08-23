namespace ToDo.DataBase
{

    
   public class Issue
    {

        public Issue(int id, string description)
        {
            Id = id;
            Description = description;
        }

        public int Id { get; set; }
        public string Description { get; set; } = ""; 
        public bool hasComplete { get; set; } = false; 
    }
}
