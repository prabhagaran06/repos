using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace ProductoInvent
{
    public class BaseRepository
    {
        private SqlConnectionStringBuilder _sqlConnection;
        public SqlConnectionStringBuilder ADOSqlConnection
        {
            get
            {
                return _sqlConnection = GetConnectionStringBuilder();
            }           

        }
        public SqlConnectionStringBuilder GetConnectionStringBuilder()
        {
            var cb = new SqlConnectionStringBuilder();
            cb.DataSource = "productmanagement.database.windows.net";
            cb.UserID = "AdminUser";
            cb.Password = "allowmeAdmin!";
            cb.InitialCatalog = "ProductManagement";
            return cb;
        }
    }
}
