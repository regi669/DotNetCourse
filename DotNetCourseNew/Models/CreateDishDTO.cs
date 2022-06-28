using System.ComponentModel.DataAnnotations;

namespace DotNetCourseNew.Models;

public class CreateDishDTO
{
    [Required]
    public string Name { get; set; }

    public string Description { get; set; }

    public decimal Price { get; set; }
}