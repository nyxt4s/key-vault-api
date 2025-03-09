namespace KeyVaultApi.Application.DTOs.Response;

public class UpdateBussinessResponse
{
    public string Name { get; set; } = string.Empty; // Obligatorio con valor por defecto
    public string? UserName { get; set; }           // Opcional
    public string Password { get; set; } = string.Empty;
    public string? Address { get; set; }
    public string Phone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public bool Active { get; set; }
}
