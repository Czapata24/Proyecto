using System;
using System.Collections.Generic;
using HankoSpa.Models;
using System.ComponentModel.DataAnnotations;

namespace HankoSpa.DTOs
{
    public class CustomRolDTO
    {
        [Key]
        public int CustomRolId { get; set; }

        [Required(ErrorMessage = "El nombre del rol es obligatorio")]
        [StringLength(50)]
        public string NombreRol { get; set; } = null!;

        // Para vistas con checkboxes
        public bool Assigned { get; set; }

        //navegador de usuarios
        public virtual ICollection<User> Usuarios { get; set; } = new HashSet<User>();

        public virtual ICollection<RolPermission> RolPermissions { get; set; } = new HashSet<RolPermission>();
    }
}
