using System.ComponentModel.DataAnnotations;

namespace Showcase.Models
{
    public class CreateRoleViewModel
    {
        [Required]
        public string RoleName { get; set; }
    }
}
