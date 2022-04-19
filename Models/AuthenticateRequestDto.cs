using System.ComponentModel.DataAnnotations;

namespace TrackYourExpenseApi.Models
{
    public class AuthenticateRequestDto
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
