using BrewUI.Items;
using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrewUI.Models
{
    public static class SQLiteAcces
    {
        #region Grains
        public static List<Grain> LoadGrains()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<Grain>("select * from Grains", new DynamicParameters());
                return output.ToList();
            }
        }

        public static void SaveGrain(Grain grain)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("insert into Grains (Name, Origin, Notes) values (@name, @origin, @notes)", grain);
            }
        }

        #endregion

        #region Hops
        public static List<Hops> LoadHops()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<Hops>("select * from Hops", new DynamicParameters());
                return output.ToList();
            }
        }

        public static void SaveHops(Hops hops)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("insert into Hops (Name, Origin, Alpha, Beta, Notes) values (@Name, @Origin, @Alpha, @Beta, @Notes)", hops);
            }
        }
        #endregion

        #region Yeasts
        public static List<Yeast> LoadYeasts()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<Yeast>("select * from Yeasts", new DynamicParameters());
                return output.ToList();
            }
        }

        public static void SaveYeast(Yeast yeast)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("insert into Yeasts (Name, Lab, BestFor, Notes) values (@name, @lab, @bestFor, @notes)", yeast);
            }
        }

        #endregion

        #region Styles
        public static List<BeerStyle> LoadStyles()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<BeerStyle>("select * from Styles", new DynamicParameters());
                return output.ToList();
            }
        }

        public static void SaveStyle(BeerStyle style)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("insert into Styles (Name, Category, Description, Profile, Ingredients) values (@Name, @Category, @Description, @Profile, @Ingredients)", style);
            }
        }

        #endregion

        #region General functions

        public static bool ItemExistsInDB(string tableName, string columnName, string searchText)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                string sqlQuery = "select * from " + tableName + " where "+ columnName + "='" + searchText + "'";
                var output = cnn.Query<Grain>(sqlQuery, new DynamicParameters());
                List<Grain> outputList = output.ToList();
                if(outputList.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        private static string LoadConnectionString()
        {
            ConnectionStringSettings connectionString = new ConnectionStringSettings();
            connectionString.ConnectionString = @"Data source = .\Database\RungeBreweryDatabase.db;Version=3;";
            connectionString.ProviderName = "System.Data.SqlClient";
            return connectionString.ToString();
        }

        #endregion
    }
}
