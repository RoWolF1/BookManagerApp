using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookManagerApp.Models
{
    [Table("Books")]
    public class Book
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên sách!")]
        [StringLength(150, ErrorMessage = "Tên sách không vượt quá 150 ký tự")]
        [Display(Name = "Tên sách")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vui lòng nhập tên tác giả!")]
        [StringLength(100, ErrorMessage = "Tên tác giả không vượt quá 100 ký tự")]
        [Display(Name = "Tác giả")]
        public string Author { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vui lòng nhập thể loại!")]
        [StringLength(50, ErrorMessage = "Thể loại không vượt quá 50 ký tự")]
        [Display(Name = "Thể loại")]
        public string Genre { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vui lòng nhập giá bán!")]
        [Range(1000, 100000000, ErrorMessage = "Giá bán phải từ 1,000 VNĐ")]
        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "Giá bán (VNĐ)")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn ngày phát hành!")]
        [DataType(DataType.Date)]
        [Display(Name = "Ngày phát hành")]
        public DateTime PublishedDate { get; set; } = DateTime.Today;

        // Trường mới: Link ảnh bìa sách
        [Display(Name = "Ảnh bìa sách (URL)")]
        public string? ImageUrl { get; set; }
    }
}