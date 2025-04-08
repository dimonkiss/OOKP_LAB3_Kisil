// Controllers/WarehouseController.cs

using Microsoft.AspNetCore.Mvc;

public class WarehouseController : Controller
{
    private static readonly WarehouseFacade _warehouse = new WarehouseFacade();

    public IActionResult Index(string searchTerm, string searchField)
    {
        var model = new WarehouseViewModel
        {
            Products = string.IsNullOrEmpty(searchTerm) ? 
                _warehouse.GetAllProducts() : 
                _warehouse.SearchProducts(searchTerm, searchField),
            CountByName = _warehouse.CountByName()
        };
        return View(model);
    }

    [HttpPost]
    public IActionResult Add(Product product)
    {
        if (ModelState.IsValid)
        {
            _warehouse.AddProduct(product);
        }
        return RedirectToAction("Index");
    }

    public IActionResult Remove(int id)
    {
        _warehouse.RemoveProduct(id);
        return RedirectToAction("Index");
    }
}

public class WarehouseViewModel
{
    public List<Product> Products { get; set; }
    public Dictionary<string, int> CountByName { get; set; }
}