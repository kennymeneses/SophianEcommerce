using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Ecommerce.Models.Entities
{
    [Table("roles", Schema = "Dbo")]
    public class UserPass
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id_user_pass_reg")]
        public int UserPassId { get; set; }

        [Column("id_user")]
        public int UserId { get; set; }

        [Column("id_user_pass")]
        public string PasswordHash { get; set; }
    }
}
