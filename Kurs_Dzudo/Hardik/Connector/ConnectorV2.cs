using System;
using System.Collections.Generic;
using Kurs_Dzudo.Hardik.Connector.Date;
using Npgsql;

namespace ukhasnikis_BD_Sec.Hardik.Connect
{
    public class DatabaseConnection : IDisposable
    {
        private readonly NpgsqlConnection _connection;

        public DatabaseConnection() // подключение к БД
        {
            string connectionString = "Server=45.67.56.214;Port=5421;Database=user16;User Id=user16;Password=dZ28IVE5;";
            _connection = new NpgsqlConnection(connectionString);
            _connection.Open();
        }

        public NpgsqlConnection GetConnection() => _connection;

        public List<UkhasnikiDao> GetAllUkhasnikis() // работа с участниками
        {
            var ukhasnikis = new List<UkhasnikiDao>();
            using (var cmd = new NpgsqlCommand("SELECT * FROM \"Sec\".ukhasniki", _connection))
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    ukhasnikis.Add(new UkhasnikiDao
                    {
                        Name = reader.GetString(reader.GetOrdinal("Name")),
                        SecName = reader.IsDBNull(reader.GetOrdinal("secname")) ? null : reader.GetString(reader.GetOrdinal("secname")),
                        DateSorevnovaniy = reader.IsDBNull(reader.GetOrdinal("datesorevnovaniy")) ? default : DateOnly.FromDateTime(reader.GetDateTime(reader.GetOrdinal("datesorevnovaniy"))),
                        Club = reader.IsDBNull(reader.GetOrdinal("club")) ? null : reader.GetString(reader.GetOrdinal("club")),
                        Adres = reader.IsDBNull(reader.GetOrdinal("adres")) ? null : reader.GetString(reader.GetOrdinal("adres")),
                        Ves = reader.IsDBNull(reader.GetOrdinal("ves")) ? 0 : reader.GetDecimal(reader.GetOrdinal("ves"))
                    });
                }
            }
            return ukhasnikis;
        }

        public List<OrganizatorDao> GetAllOrganizators()
        {
            var organizators = new List<OrganizatorDao>();
            using (var cmd = new NpgsqlCommand("SELECT * FROM \"Sec\".organizator", _connection))
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    organizators.Add(new OrganizatorDao
                    {
                        login = reader.GetString(reader.GetOrdinal("login")),
                        pass = reader.GetString(reader.GetOrdinal("pass")),
                        pozition = reader.GetString(reader.GetOrdinal("pozition"))
                    });
                }
            }
            return organizators;
        }

        public void Updateukhasniki(UkhasnikiDao ukhasniki)
        {
            using (var cmd = new NpgsqlCommand(
                "UPDATE \"Sec\".ukhasniki SET secname = @SecName, club = @Club, adres = @Adres, ves = @Ves " +
                "WHERE \"Name\" = @Name",
                _connection))
            {
                cmd.Parameters.AddWithValue("@Name", ukhasniki.Name);
                cmd.Parameters.AddWithValue("@SecName", ukhasniki.SecName ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Club", ukhasniki.Club ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Adres", ukhasniki.Adres ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Ves", ukhasniki.Ves);
                cmd.ExecuteNonQuery();
            }
        }

        public void AddUkhasniki(UkhasnikiDao ukhasniki)
        {
            using (var cmd = new NpgsqlCommand(
                "INSERT INTO \"Sec\".ukhasniki (\"Name\", secname, datesorevnovaniy, club, adres, ves) " +
                "VALUES (@Name, @SecName, @DateSorevnovaniy, @Club, @Adres, @Ves)",
                _connection))
            {
                cmd.Parameters.AddWithValue("@Name", ukhasniki.Name);
                cmd.Parameters.AddWithValue("@SecName", ukhasniki.SecName ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@DateSorevnovaniy", ukhasniki.DateSorevnovaniy.ToDateTime(TimeOnly.MinValue));
                cmd.Parameters.AddWithValue("@Club", ukhasniki.Club ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Adres", ukhasniki.Adres ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Ves", ukhasniki.Ves);
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteUkhasniki(string name)
        {
            using (var cmd = new NpgsqlCommand(
                "DELETE FROM \"Sec\".ukhasniki WHERE \"Name\" = @Name",
                _connection))
            {
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.ExecuteNonQuery();
            }
        }

        public void Dispose()
        {
            _connection?.Close();
            _connection?.Dispose();
        }
    }
}