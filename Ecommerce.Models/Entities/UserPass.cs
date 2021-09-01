using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Models.Entities
{
    [Table("usuarios_passwords", Schema = "Dbo")]
    public class UserPass
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id_user_pass_reg")]
        public int userPassId { get; set; }

        [Column("id_user")]
        public int userId { get; set; }

        [Column("id_user_pass")]
        public string passwordHash { get; set; }

        [NotMapped]
        public User user { get; set; }
    }
}
