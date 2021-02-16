using System.ComponentModel.DataAnnotations;

namespace PicPayChallenge.Core.DTOs
{
    public class UserRequestDTO
    {
        [Required]
        public string Term { get; set; }
        public int PageNumber { get; set; } = 1;
        public int RowsOfPage { get; set; } = 15;
    }
}
