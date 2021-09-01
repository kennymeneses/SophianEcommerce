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
        public int roleId { get; set; }

        [Column("nombre_rol")]
        public string roleName { get; set; }

        [Column("Eliminado")]
        public bool removed { get; set; }
    }
}
