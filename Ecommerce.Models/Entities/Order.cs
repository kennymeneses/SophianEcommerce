using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Models.Entities
{
    [Table("ordenes", Schema = "Dbo")]
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id_main_order")]
        public int orderId { get; set; }

        [Column("orden_codigo")]
        public string orderCode { get; set; }

        [Column("orden_id_user")]
        public int userIdOrder { get; set; }

        [Column("orden_subtotal")]
        public double subtotalOrder { get; set; }

        [Column("orden_moneda")]
        public string currencyOrder { get; set; }

        [Column("orden_fecha")]
        public DateTimeOffset dateOrder { get; set; }

        [Column("eliminado")]
        public bool removed { get; set; }
    }
}
