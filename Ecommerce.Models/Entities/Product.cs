using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Models.Entities
{
    [Table("productos", Schema = "Dbo")]
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id_producto_main")]
        public int productId { get; set; }

        [Column("producto_sku")]
        public string skuProduct { get; set; }

        [Column("producto_nombre")]
        public string nameProduct { get; set; }

        [Column("producto_id_subcat")]
        public string subcatIdProduct { get; set; }

        [Column("producto_marca")]
        public string brandProduct { get; set; }

        [Column("producto_precio")]
        public double priceProduct { get; set; }

        [Column("producto_img_url")]
        public string urlImgProduct { get; set; }

        [Column("producto_short_descript")]
        public string shortDescriptProduct { get; set; }

        [Column("producto_long_descript")]
        public string  longDescriptProduct { get; set; }

        [Column("producto_stock")]
        public int stockProduct { get; set; }

        [Column("producto_moneda")]
        public string currencyProduct { get; set; }

        [Column("producto_distribuidor_id")]
        public int supplierIdProduct { get; set; }

        [Column("FechaCreacion")]
        public DateTimeOffset dateCreated { get; set; }

        [Column("Eliminado")]
        public bool removed { get; set; }
    }
}
