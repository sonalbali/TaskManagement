using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Task_Managment_System.Model.Entities; 

namespace Task_Managment_System.Model.Entities
{
    public class TaskItem
    {
        [Key]
        public required int Task_Id { get; set; }          
        public required string Title { get; set; }
        public string? Description { get; set; }
        public string? Status { get; set; }  // "To Do", "In Progress", "Completed"
        public DateTime? DueDate { get; set; }
        public string? Assign_To { get; set; }
  
        [ForeignKey("UserId")]
        public int UserId { get; set; }

    }
}
