namespace InventoryManagement;

public record Provider(string Name, string Assembly)
{
    public static readonly Provider SqlServer = new(nameof(SqlServer), typeof(SqlServer.Marker).Assembly.GetName().Name!);
    public static readonly Provider Postgresql = new(nameof(Postgresql), typeof(Postgresql.Marker).Assembly.GetName().Name!);
}
