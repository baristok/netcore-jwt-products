using System.ComponentModel.DataAnnotations;

namespace JwtApp.Front.Models;

public class CreateCategoryModel
{
    [Required(ErrorMessage = "Kategori Adı Gereklidir.")]
    public string? Definition { get; set; } = null!;
}