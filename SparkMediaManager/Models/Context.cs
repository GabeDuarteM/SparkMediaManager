// Developed by: Gabriel Duarte
// 
// Created at: 14/05/2016 18:42

using System.Data.Common;
using System.Data.Entity;

namespace SparkMediaManager.Models
{
    public class Context : DbContext, IContext
    {
        public Context() : base(GetConnectionStringName())
        {
        }

        // Unit testing
        public Context(DbConnection connection)
            : base(connection, true)
        {
        }

        private static string GetConnectionStringName()
        {
#if DEBUG
            return "DB_Spark_Debug";
#else
            return "DB_Spark";
#endif
        }

        #region Implementation of IContext

        public DbSet<Feed> Feed { get; set; }

        public DbSet<Serie> Serie { get; set; }

        #endregion
    }
}
