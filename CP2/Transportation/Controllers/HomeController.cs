using Microsoft.AspNetCore.Mvc;
using Transportation.Interfaces;
using Transportation.Models;

namespace Transportation.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index([FromServices] IEnumerable<IAirplanes> airplanes)
    {
        using var db = new CarsContext();

        // ===============================
        //      CARRO DE MINNIE MOUSE
        // ===============================

        // 1. Cliente con apellido "Mouse"
        var customer = db.Customers.First(c => c.LastName == "Mouse");

        // 2. Ownership del cliente
        var ownership = db.CustomerOwnerships.First(o => o.CustomerId == customer.CustomerId);

        // 3. VIN asociado
        var vin = db.CarVins.First(v => v.Vin == ownership.Vin);

        // 4. Modelo (CarVin tiene ModelId)
        var model = db.Models.First(m => m.ModelId == vin.ModelId);

        // 5. Marca
        var brand = db.Brands.First(b => b.BrandId == model.BrandId);

        // Brand & Model para la vista
        ViewData["BrandModel"] = $"{brand.BrandName} - {model.ModelName}";

        // ===============================
        //      DEALER DE MINNIE MOUSE
        // ===============================

        // CustomerOwnership tiene DealerId
        var dealer = db.Dealers.First(d => d.DealerId == ownership.DealerId);

        // Dealer.cs: DealerName + DealerAddress
        ViewData["Dealer"] = $"{dealer.DealerName} - {dealer.DealerAddress}";

        // ===============================
        //      AIRPLANES (DI)
        // ===============================

        foreach (var a in airplanes)
        {
            // OJO: GetBrand y GetModels son PROPIEDADES, no métodos
            var brandName = a.GetBrand;
            var models = string.Join(" - ", a.GetModels);

            if (brandName == "Airbus")
            {
                ViewData["Airbus"] = $"{brandName}: {models}";
            }
            else if (brandName == "Boeing")
            {
                ViewData["Boeing"] = $"{brandName}: {models}";
            }
        }

        return View();
    }
}
