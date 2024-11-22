using System.ComponentModel.DataAnnotations;


namespace Task_Managment_System.Model.Entities
{
    public class Registration
    {
        [Key]
        public int UserId { get; set; }
        public string ? Name { get; set; }
        public required string Username { get; set; }
        public required string PasswordHash { get; set; }
     
    }   

}
