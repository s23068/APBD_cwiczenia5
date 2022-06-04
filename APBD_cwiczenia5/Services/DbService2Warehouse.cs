using APBD_cwiczenia5.Models;
using System.Data.SqlClient;

namespace APBD_cwiczenia5.Services
{
    public class DbService2Warehouse : IDbService2Warehouse
    {
        private readonly string connection = "Data Source=db-mssql.pjwstk.edu.pl;Initial Catalog=2019SBD;Integrated Security=True";

        public async void PostWarehouse(Warehouse warehouse)
        {
            var procedure = "AddToWarehouse";

            using (var con = new SqlConnection(connection))
            {
                await con.OpenAsync();
                using (SqlTransaction transaction = con.BeginTransaction())
                {
                    using (var com = new SqlCommand())
                    {
                        com.Connection = con;
                        com.Transaction = transaction;

                        com.CommandText = procedure;
                        com.CommandType = System.Data.CommandType.StoredProcedure;

                        try
                        {
                            com.Parameters.Add("@IdProduct", System.Data.SqlDbType.Int).Value = warehouse.IdProduct;
                            com.Parameters.Add("@IdWarehouse", System.Data.SqlDbType.Int).Value = warehouse.IdWarehouse;
                            com.Parameters.Add("@Amount", System.Data.SqlDbType.Int).Value = warehouse.Amount;
                            com.Parameters.Add("@CreatedAt", System.Data.SqlDbType.DateTime).Value = DateTime.Parse(warehouse.CreatedAt);
                            await com.ExecuteNonQueryAsync();
                        }
                        catch (SqlException exception)
                        {
                            await transaction.RollbackAsync();
                        }
                    }

                    await transaction.CommitAsync();
                    await con.CloseAsync();
                }
            }
        }
    
}
}
