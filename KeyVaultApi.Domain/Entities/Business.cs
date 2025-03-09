using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyVaultApi.Domain.Entities
{
    public class Business
    {
        public int? BusinessID { get; set; }
        public string Name { get; set; } = string.Empty; // Obligatorio con valor por defecto
        public string? UserName { get; set; }           // Opcional
        public string Password { get; set; } = string.Empty;
        public string? Address { get; set; }
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public bool Active { get; set; }
    }
}
