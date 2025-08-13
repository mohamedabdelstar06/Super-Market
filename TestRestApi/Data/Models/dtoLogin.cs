using System.ComponentModel.DataAnnotations;

namespace TestRestApi.Data.Models;

public class dtoLogin
{
    [Required]
    public string userName { get; set; }
    [Required]
    public string Password { get; set; }

}
