using System.ComponentModel.DataAnnotations;
using TrackYourExpenseApi.Entities;

namespace TrackYourExpenseApi.Models
{
    public class AuthenticateResponseDto
    {

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }

        public AuthenticateResponseDto(User user, string token)
        {
            Id = user.Id;
            FirstName = user.FirstName;
            LastName = user.LastName;
            UserName = user.UserName;
            Email = user.Email;
            Token = token;
        }

    }
}
