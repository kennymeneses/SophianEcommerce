using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Models.Entities
{
    [Table("roles", Schema = "Dbo")]
    public class Role
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id_rol")]
        public int RoleId { get; set; }

        [Column("nombre_rol")]
        public string RoleName { get; set; }

        [Column("Eliminado")]
        public bool Removed { get; set; }
    }
}
