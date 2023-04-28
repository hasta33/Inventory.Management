namespace InventoryManagement;

public record DatabaseProviders(string Name, string Assembly)
{
    public static readonly DatabaseProviders SqlServer = new(nameof(SqlServer), typeof(SqlServer.Marker).Assembly.GetName().Name!);
    public static readonly DatabaseProviders Postgresql = new(nameof(Postgresql), typeof(Postgresql.Marker).Assembly.GetName().Name!);
}
