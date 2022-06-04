using APBD_cwiczenia5.Models;
using APBD_cwiczenia5.Services;
using Microsoft.AspNetCore.Mvc;

namespace APBD_cwiczenia5.Controllers
{ 
    [ApiController]
    [Route("api/warehouse")]
    public class WarehouseController : Controller
    {
            private readonly IDbServiceWarehouse _wearehouseDbService;

            public WarehouseController(IDbServiceWarehouse warehouseDbService)
            {
                _wearehouseDbService = warehouseDbService;
            }

            [HttpPost]
            public async Task<IActionResult> GetWarehouses(Warehouse warehouse)
            {
                var result = _wearehouseDbService.PostWarehouse(warehouse);

                return Ok(result);
            }
        
    }
}
