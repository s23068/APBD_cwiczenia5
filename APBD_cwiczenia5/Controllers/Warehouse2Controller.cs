using APBD_cwiczenia5.Models;
using APBD_cwiczenia5.Services;
using Microsoft.AspNetCore.Mvc;

namespace APBD_cwiczenia5.Controllers
{ 
    [ApiController]
    [Route("api/warehouse2")]
    public class Warehouse2Controller : ControllerBase
    {
            private readonly IDbService2Warehouse  _wearehouseDbService;

            public Warehouse2Controller(IDbService2Warehouse warehouseDbService)
            {
                _wearehouseDbService = warehouseDbService;
            }

            [HttpPost]
            public async Task<IActionResult> GetWarehouses(Warehouse warehouse)
            {
                _wearehouseDbService.PostWarehouse(warehouse);
                return Ok();
            }
     }
}

