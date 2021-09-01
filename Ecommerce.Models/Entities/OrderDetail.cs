using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Models.Entities
{
    [Table("orden_detalles", Schema = "Dbo")]
    public class OrderDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id_reg_ord_detail")]
        public int orderDetailId { get; set; }

        [Column("id_orden")]
        public int orderId { get; set; }

        [Column("id_product")]
        public int productoId { get; set; }

        [Column("cantidad_producto")]
        public int amountProduct { get; set; }
    }
}
