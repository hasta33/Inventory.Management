namespace InventoryManagement.Core.Models
{
    public class Inventory : BaseEntity
    {
        public int CompanyId { get; set; }
        public Company Company { get; set; } = default!;


        public int CategoryId { get; set; }
        //public Category Category { get; set; } = default!;



        public int CategorySubId { get; set; }
       // public CategorySub CategorySub { get; set; } = default!;



        public int BrandId { get; set; }
       // public Brand Brand { get; set; } = default!;



        public int ModelId { get; set; }
       // public Model Model { get; set; } = default!;




        public string Name { get; set; } = string.Empty;
        public DateTime InventoryDate { get; set; }
        public DateTime InvoiceDate { get; set; }
        public int Barcode { get; set; }
        public string SerialNumber { get; set; } = string.Empty;
        public int Imei { get; set; }
        public string Mac { get; set; } = string.Empty; 
        public string Status { get; set; } = string.Empty;
        public string Responsible { get; set; } = string.Empty;
    }
}
