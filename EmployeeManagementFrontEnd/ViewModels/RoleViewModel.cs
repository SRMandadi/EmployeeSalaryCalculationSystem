using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EmployeeSalaryCalculationSystem.MVC.ViewModels
{
    public class RoleViewModel
    {
        [Required(ErrorMessage = "Please enter Role Id.")]
        [Display(Name = "Role Id")]
        public int RoleId { get; set; }

        [Required(ErrorMessage = "Please enter Role Name.")]
        [Display(Name = "Role Name")]
        public string RoleName { get; set; }

        [Display(Name = "Role Description")]
        public string RoleDescription { get; set; }

        [Required(ErrorMessage = "Please enter Rate Per Hour.")]
        [Display(Name = "Rate Per Hour")]
        public decimal? RatePerhour { get; set; }
        public virtual ICollection<EmployeeViewModel> Employee { get; set; }
    }
}
