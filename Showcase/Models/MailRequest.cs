using System.ComponentModel.DataAnnotations;

public class MailRequest
{
    [Required(ErrorMessage = "Voornaam is verplicht.")]
    [MaxLength(50, ErrorMessage = "Voornaam mag maximaal 50 tekens zijn.")]
    [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Voornaam mag alleen letters bevatten.")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "Achternaam is verplicht.")]
    [MaxLength(50, ErrorMessage = "Achternaam mag maximaal 50 tekens zijn.")]
    [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Achternaam mag alleen letters bevatten.")]
    public string LastName { get; set; }

    [Required(ErrorMessage = "Onderwerp is verplicht.")]
    [MaxLength(200, ErrorMessage = "Onderwerp mag maximaal 200 tekens zijn.")]
    public string Subject { get; set; }

    [Required(ErrorMessage = "E-mailadres is verplicht.")]
    [EmailAddress(ErrorMessage = "Voer een geldig e-mailadres in.")]
    public string FromEmail { get; set; }

    [Required(ErrorMessage = "Mobiele nummer is verplicht.")]
    [RegularExpression(@"^\d{10,20}$", ErrorMessage = "Mobiele nummer moet tussen de 10 en 20 cijfers bevatten.")]
    public string Mobile { get; set; }

    [Required(ErrorMessage = "Bericht is verplicht.")]
    [MaxLength(600, ErrorMessage = "Bericht mag maximaal 600 tekens zijn.")]
    public string Body { get; set; }
    public string ToEmail = "mailservertestdev@gmail.com";
}

