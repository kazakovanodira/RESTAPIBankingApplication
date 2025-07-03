using System.ComponentModel.DataAnnotations;

namespace RESTAPIBankingApplication.Models.Requests;

public class CreateAccountRequest
{
    [Required(ErrorMessage = "Name cannot be empty.")]
    public string Name { get; set; }
}   