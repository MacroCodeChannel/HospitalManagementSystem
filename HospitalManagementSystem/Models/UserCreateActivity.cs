using HospitalManagementSystem.Data;

namespace HospitalManagementSystem.Models
{
    public class UserCreateActivity : UserModifiedActivity
    {
        public string CreatedById { get; set; }
        public ApplicationUser CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
    }
    public class UserModifiedActivity
    {
        public string? ModifiedById { get; set; }
        public ApplicationUser ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
