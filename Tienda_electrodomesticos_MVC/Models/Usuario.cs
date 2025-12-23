namespace Tienda_electrodomesticos_MVC.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = string.Empty;

        public string Apellido { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        // ⚠️ En el MVC normalmente NO se usa el hash,
        // pero lo dejamos porque la API lo envía
        public string PasswordHash { get; set; } = string.Empty;

        public string ProfileImage { get; set; } = string.Empty;

        public string Rol { get; set; } = "ROLE_USER";

        public bool IsEnable { get; set; }

        public bool CuentaNoBloqueada { get; set; }

        public int IntentoFallido { get; set; }

        public DateTime? LockTime { get; set; }

        public string? ResetToken { get; set; }
    }
}
