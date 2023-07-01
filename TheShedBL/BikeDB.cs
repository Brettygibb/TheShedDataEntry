using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using TheShedDL;
using TheShedBL;

namespace TheShedDB
{
    public static class BikeDB
    {
       public static Bikes GetBikeByID(int BikeID)
        {
            Bikes b = null;
            string sql = "SELECT BikeID,Manufacturer,Model,Type,Price,BikeImage FROM Bikes WHERE BikeID = @BikeID";
            SqlConnection conn = TheShedDBL.GetConnection();

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@BikeID", BikeID);
                SqlDataReader myReader = cmd.ExecuteReader(System.Data.CommandBehavior.SingleRow);

                if (myReader.Read())
                {
                    b = new Bikes();
                    b.id = Convert.ToInt32(myReader["BikeID"]);
                    b.Manufacturer = myReader[1].ToString();
                    b.Model = myReader[2].ToString();
                    b.Type = myReader[3].ToString();
                    b.Price = Convert.ToDecimal(myReader["Price"]);
                    b.BikeImage = myReader[4].ToString();
                    //add bike image
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally { conn.Close(); }

            return b;
        }
        public static int UpdateBikes(Bikes b)
        {
            int numRows = -1;
            SqlConnection connection = TheShedDBL.GetConnection();
            string sql = "UPDATE Bikes set " + "Manufacturer = @Manufacturer, " + "Model = @Model, " + "Type = @Type, " + "Price =@Price " + "WHERE BikeID =@BikeID";
            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(sql, connection);
                
                cmd.Parameters.AddWithValue("@Manufacturer", b.Manufacturer);
                cmd.Parameters.AddWithValue("@Model", b.Model);
                cmd.Parameters.AddWithValue("@Type", b.Type);
                cmd.Parameters.AddWithValue("@Price", b.Price);
                cmd.Parameters.AddWithValue("@BikeImage",b.BikeImage);
                //add bike image
                numRows = cmd.ExecuteNonQuery();
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { connection.Close(); }
            return numRows;
        }
        public static int DeleteBikebyID(string BikeID)
        {
            int numRows = -1;
            SqlConnection conn = TheShedDBL.GetConnection();
            string sql = "DELETE from Bikes WHERE BikeID = @BikeID";//it might be bikesid, check later
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@BikeID", BikeID);
                numRows = cmd.ExecuteNonQuery();
            }
            catch (Exception ex) { throw new Exception(ex.Message);
            }
            finally { conn.Close(); }
            return numRows;
        }
        public static int AddBike(Bikes bikes)
        {
            int BikeCode = -1;
            SqlConnection conn = TheShedDBL.GetConnection();

            try
            {
                conn.Open();
                string sql = "INSERT INTO [dbo].[Bikes]" + "([BikeID],[Manufacturer],[Model],[Type],[Price],[BikeImage])" + "VALUES (@BikeID,@Manufacturer,@Model,@Type,@Price,@BikeImage)";
                
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@BikeID", bikes.id);
                cmd.Parameters.AddWithValue("@Manufacturer", bikes.Manufacturer);
                cmd.Parameters.AddWithValue("@Model", bikes.Model);
                cmd.Parameters.AddWithValue("@Type", bikes.Type);
                cmd.Parameters.AddWithValue("@Price", bikes.Price);
                cmd.Parameters.AddWithValue("@BikeImage", bikes.BikeImage);
                //add bike image
                int rows = cmd.ExecuteNonQuery();

                if (rows > 0)
                {
                    sql = "SELECT IDENT_CURRENT('Bikes') FROM Bikes where BikeId = @BikeID";
                    cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@BikeID", bikes.id);
                    BikeCode = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { conn.Close(); }
            return BikeCode;
        }
    }
    
}
