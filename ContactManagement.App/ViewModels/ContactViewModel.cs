using System.ComponentModel.DataAnnotations;

namespace ContactManagement.App.ViewModels;

public class ContactViewModel
{
    [Key]
    public Guid ID { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 5)]
    public string Name { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [StringLength(9, ErrorMessage = "O campo {0} precisa ter {2} caracteres!", MinimumLength = 9)]
    public int EntityContact { get; set; }

    [EmailAddress]
    public string Email { get; set; }
}
