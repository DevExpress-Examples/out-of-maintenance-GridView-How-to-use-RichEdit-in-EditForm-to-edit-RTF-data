using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.OleDb;

namespace WebApplicationCS.Models {
    public class OleDbDataProvider {
        private static string connectionString = ConfigurationManager.ConnectionStrings["CarsDbConnection"].ConnectionString;
        public static IEnumerable<Car> GetCars() {
            List<Car> cars = new List<Car>();
            using (OleDbConnection dbc = new OleDbConnection(connectionString)) {
                using (OleDbCommand cmd = dbc.CreateCommand()) {
                    dbc.Open();
                    cmd.CommandText = "SELECT [ID], [Trademark], [Model], [RtfContent] FROM [Cars]";
                    using (OleDbDataReader reader = cmd.ExecuteReader()) {
                        while (reader.Read()) {
                            cars.Add(new Car {
                                ID = Convert.ToInt32(reader["ID"]),
                                TradeMark = reader["TradeMark"].ToString(),
                                Model = reader["Model"].ToString(),
                                RtfContent = reader["RtfContent"].ToString()
                            });
                        }
                    }
                    dbc.Close();
                }
            }
            return cars;
        }
        public static void UpdateItem(Car item) {
            using (OleDbConnection dbc = new OleDbConnection(connectionString)) {
                using (OleDbCommand cmd = dbc.CreateCommand()) {
                    cmd.CommandText = "UPDATE [Cars] SET [Trademark] = @TradeMark, [Model] = @Model, [RtfContent] = @RtfContent WHERE [ID] = @ID";
                    cmd.Parameters.AddWithValue("@TradeMark", item.TradeMark);
                    cmd.Parameters.AddWithValue("@Model", item.Model);
                    cmd.Parameters.AddWithValue("@RtfContent", item.RtfContent);
                    cmd.Parameters.AddWithValue("@ID", item.ID);
                    dbc.Open();
                    cmd.ExecuteNonQuery();
                    dbc.Close();
                }
            }
        }
        public static void AddNewItem(Car item) {
            using (OleDbConnection dbc = new OleDbConnection(connectionString)) {
                using (OleDbCommand cmd = dbc.CreateCommand()) {
                    cmd.CommandText = "INSERT INTO [Cars] ([Trademark], [Model], [RtfContent]) VALUES (@Trademark, @Model, @RtfContent)";
                    cmd.Parameters.AddWithValue("@TradeMark", item.TradeMark);
                    cmd.Parameters.AddWithValue("@Model", item.Model);
                    cmd.Parameters.AddWithValue("@RtfContent", item.RtfContent);
                    dbc.Open();
                    cmd.ExecuteNonQuery();
                    dbc.Close();
                }
            }
        }
        public static void DeleteItem(int id) {
            using (OleDbConnection dbc = new OleDbConnection(connectionString)) {
                using (OleDbCommand cmd = dbc.CreateCommand()) {
                    cmd.CommandText = "DELETE * FROM [Cars] WHERE [ID] = @ID";
                    cmd.Parameters.AddWithValue("@ID", id);

                    dbc.Open();
                    cmd.ExecuteNonQuery();
                    dbc.Close();
                }
            }
        }
    }
}