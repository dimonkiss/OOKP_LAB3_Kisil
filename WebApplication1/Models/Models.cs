// Models/WarehouseModels.cs
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Cost { get; set; }
    public DateTime ReceiptDate { get; set; }
    public double Weight { get; set; }
    public double Width { get; set; }
    public double Depth { get; set; }
    public double Height { get; set; }
    public double? StorageTemperature { get; set; }
}

// Фасад для роботи зі складом
public class WarehouseFacade
{
    private List<Product> _products = new List<Product>();
    private int _nextId = 1;

    public List<Product> GetAllProducts() => _products;

    public void AddProduct(Product product)
    {
        product.Id = _nextId++;
        _products.Add(product);
    }

    public void RemoveProduct(int id)
    {
        var product = _products.FirstOrDefault(p => p.Id == id);
        if (product != null)
        {
            _products.Remove(product);
        }
    }

    public List<Product> SearchProducts(string searchTerm, string searchField)
    {
        var products = _products.AsQueryable();
        if (!string.IsNullOrEmpty(searchTerm))
        {
            switch (searchField)
            {
                case "Name":
                    return products.Where(p => p.Name.Contains(searchTerm)).ToList();
                case "Cost":
                    if (decimal.TryParse(searchTerm, out decimal cost))
                        return products.Where(p => p.Cost == cost).ToList();
                    break;
            }
        }
        return products.ToList();
    }

    public Dictionary<string, int> CountByName() =>
        _products
            .GroupBy(p => p.Name)
            .ToDictionary(g => g.Key, g => g.Count());
}