namespace SharedModels
{
    public interface CreatedInventory
    {
        int Id { get; set; }
        string Name { get; set; }
        string Barcode { get; set; }
        string SerialNumber { get; set; }
    }
}
