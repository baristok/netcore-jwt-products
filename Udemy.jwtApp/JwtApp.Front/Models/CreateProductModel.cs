using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace JwtApp.Front.Models;

public class CreateProductModel
{
    [Required(ErrorMessage = "Ürün adı boş geçilemez.")]
    public string? Name { get; set; }

    [Required(ErrorMessage = "Stok boş geçilemez.")]
    public int Stock { get; set; }

    [Required(ErrorMessage = "Fiyat boş geçilemez.")]
    public decimal Price { get; set; }

    [Required(ErrorMessage = "Lütfen Kategori Seçiniz.")]
    public int CategoryId { get; set; }

    public SelectList? Categories { get; set; }
}