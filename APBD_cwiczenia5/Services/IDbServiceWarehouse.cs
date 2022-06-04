using APBD_cwiczenia5.Models;

namespace APBD_cwiczenia5.Services
{
    public interface IDbServiceWarehouse
    {
        public Task<int> PostWarehouse(Warehouse warehouse);
    }
}
