namespace API.DTOs;

public class CreateMessageDto
{
    [Required]
    public required string RecipientUsername { get; set; }

    [Required]
    public required string Content { get; set; }
}
