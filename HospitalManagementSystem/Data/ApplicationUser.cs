using HospitalManagementSystem.Models;
using Microsoft.AspNetCore.Identity;

namespace HospitalManagementSystem.Data;

// Add profile data for application users by adding properties to the ApplicationUser class
public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public string FullName => $"{FirstName} {LastName}";
    public string Address { get; set; }
    public string EmergencyPhoneNumber { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string MotherName { get; set; }
    public string FatherName { get; set; }
    public string NationalIdNumber { get; set; }
    public int? GenderId { get; set; }
    public SystemCodeDetail Gender { get; set; }
    public int? BloodGroupId { get; set; }
    public SystemCodeDetail BloodGroup { get; set; }
    public int? MaritalStatusId { get; set; }
    public SystemCodeDetail MaritalStatus { get; set; }


}

