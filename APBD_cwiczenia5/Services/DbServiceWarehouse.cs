using APBD_cwiczenia5.Models;
using System.Data.SqlClient;

namespace APBD_cwiczenia5.Services
{
    public class DbServiceWarehouse
    {
        private readonly string conection = "Data Source=db-mssql.pjwstk.edu.pl;Initial Catalog=2019SBD;Integrated Security=True";

        public async Task<int> PostWarehouse(Warehouse warehouse)
        {
            if (warehouse.IdProduct == 0 || warehouse.IdWarehouse == 0 || warehouse.Amount <= 0 || string.IsNullOrEmpty(warehouse.CreatedAt))
            {
                return 1;
            }
            var warehouseId = 0;
            var order = 0;
            var price = 0;
            using (var con = new SqlConnection(conection))
            {
                con.Open();

                using (SqlTransaction transaction = con.BeginTransaction())
                {
                    using (var com = new SqlCommand())
                    {
                        com.Connection = con;

                        com.Transaction = transaction;

                        com.CommandText = "select * from warehouse where idWarehouse = " + warehouse.IdProduct;

                        using (SqlDataReader dr = await com.ExecuteReaderAsync())
                        {
                            if (dr.HasRows)
                            {
                                warehouseId = int.Parse(dr["IdWarehouse"].ToString());
                            }
                        }
                    }

                    using (var comm = new SqlCommand())
                    {

                        comm.CommandText = "select * from order where idProduct = " + warehouse.IdProduct + " and amount = " + warehouse.Amount;

                        using (SqlDataReader dr = await comm.ExecuteReaderAsync())
                        {
                            if (dr.HasRows)
                            {
                                order = int.Parse(dr["IdOrder"].ToString());
                            }
                        }
                    }

                    using (var comm = new SqlCommand())
                    {

                        if (order != 0)
                        {

                            comm.CommandText = "select * from product_warehouse where idOrder = " + order;

                            int rows = comm.ExecuteNonQuery();
                            if (rows != 0)
                            {

                            }
                        }
                    }

                    using (var comm = new SqlCommand())
                    {
                        comm.CommandText = "update order set FulfilledAt = " + DateTime.Now + " where idorder = " + order;
                        comm.ExecuteNonQuery();
                    }

                    using (var comm = new SqlCommand())
                    {
                        comm.CommandText = "Select price from product where idproduct = " + warehouse.IdProduct;
                        await comm.ExecuteNonQueryAsync();
                        SqlDataReader dr = await comm.ExecuteReaderAsync();
                        if (dr.HasRows)
                        {
                            price = int.Parse(dr["price"].ToString());
                        }
                        comm.CommandText = $"insert into Product_Warehouse(IdWarehouse, IdProduct, IdOrder, Amount, Price, CreatedAt) values( {warehouse.IdWarehouse}, {warehouse.IdProduct}, {order}, {warehouse.Amount}, {warehouse.Amount * price}, {warehouse.CreatedAt} );";
                        await comm.ExecuteNonQueryAsync();
                    }

                    await transaction.CommitAsync();
                    await con.CloseAsync();

                }
            }

            return 1;
        }
    }
}

