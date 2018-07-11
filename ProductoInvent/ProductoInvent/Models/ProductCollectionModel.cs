using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProductoInvent.Models
{
    public class ProductCollectionModel
    {
        [DisplayName("Product Id")]
       [DisplayFormat(NullDisplayText ="NA")]
        public int ProductId { get; set; }

        [DisplayName("Product Name")]
        [DisplayFormat(NullDisplayText = "NA")]
        public string ProductName { get; set; }

        [DisplayName("Category Id")]
        [DisplayFormat(NullDisplayText = "NA")]
        public int ReferenceId { get; set; }

        [DisplayName("Category TypeId")]
        [DisplayFormat(NullDisplayText = "NA")]
        public int  ReferenceTypeId { get; set; }

        [DisplayName("Sell Start Date")]
        [DisplayFormat(NullDisplayText = "NA")]
        public DateTime CreatedDateTime { get; set; }

        [DisplayName("Modified Datetime")]
        [DisplayFormat(NullDisplayText = "NA")]
        public DateTime ModifiedDateTime { get; set; }

        [DisplayName("CreatedBy")]
        [DisplayFormat(NullDisplayText = "NA")]
        public string CreatedBy { get; set; }

        [DisplayName("ModifiedBy")]
        [DisplayFormat(NullDisplayText = "NA")]
        public string ModifiedBy{ get; set; }

        [DisplayName("Price")]
        [DisplayFormat(NullDisplayText = "0.00")]
        public decimal Price { get; set; }

        [DisplayName("Active")]
        [DisplayFormat(NullDisplayText = "NA")]
        public bool IsActive{ get; set; }

        [DisplayName("Image")]
        [DisplayFormat(NullDisplayText = "NA")]
        public byte[] ProductImage { get; set; }

        [DisplayName("")]
        [DisplayFormat(NullDisplayText = "NA")]
        public string ActiveClass { get;set;}

        [DisplayName("Product Number")]
        [DisplayFormat(NullDisplayText = "NA")]
        public string ProductNumber { get; set; }

        public string FileName { get; set; }

        [DisplayName("E-Mail")]
        [DisplayFormat(NullDisplayText = "NA")]
        public string CustomerEmail { get; set; }
    }
}
