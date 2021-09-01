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
        public int userId { get; set; }

        [Column("user_nickname")]
        public string nickUser { get; set; }

        [Column("user_nombres")]
        public string namesUser { get; set; }

        [Column("user_apellidos")]
        public string surnamesUser { get; set; }

        [Column("user_dni")]
        public string dniUser { get; set; }

        [Column("user_rol_id")]
        public int roleId { get; set; }

        [NotMapped]
        public virtual ICollection<UserRole> userRoles { get; set; }

        [NotMapped]
        public UserPass passUser { get; set; }

        [Column("user_email")]
        public string emailUser { get; set; }

        [Column("user_edad")]
        public int ageUser { get; set; }

        [Column("user_genero")]
        public string genderUser { get; set; }

        [Column("FechaCreacion")]
        public DateTime? dataCreated { get; set; }

        [NotMapped]
        public string creationDate { get { return String.Format("{0:dddd, MMMM d, yyyy}", dataCreated); } }

        [Column("Eliminado")]
        public bool removed { get; set; }
    }
}
