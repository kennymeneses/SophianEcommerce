using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Models.Entities
{
    [Table("usuarios", Schema = "Dbo")]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id_user_main")]
        public int Id_user_main { get; set; }

        [Column("user_nickname")]
        public string User_nick { get; set; }

        [Column("user_nombres")]
        public string User_nombres { get; set; }

        [Column("user_apellidos")]
        public string User_apellidos { get; set; }

        [Column("user_dni")]
        public string Dni { get; set; }

        [Column("user_rol_id")]
        public int Rol_id { get; set; }

        [NotMapped]
        public virtual ICollection<UserRole> UsuarioRol { get; set; }

        [NotMapped]
        public UserPass UsuarioPass { get; set; }

        [Column("user_email")]
        public string Correo { get; set; }

        [Column("user_edad")]
        public int Edad { get; set; }

        [Column("user_genero")]
        public string Genero { get; set; }

        [Column("FechaCreacion")]
        public DateTime? FechaCreacion { get; set; }

        [NotMapped]
        public string FechaAlta { get { return String.Format("{0:dddd, MMMM d, yyyy}", FechaCreacion); } }

        [Column("Eliminado")]
        public bool Eliminado { get; set; }
    }
}
