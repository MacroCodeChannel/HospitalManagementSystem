using System.ComponentModel.DataAnnotations;

namespace HospitalManagementSystem.Models
{
    public class SystemCode : UserCreateActivity
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Code is required")]
        public string Code { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Order No is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Order No must be greater than 0")]
        public int OrderNo { get; set; }
    }
}
