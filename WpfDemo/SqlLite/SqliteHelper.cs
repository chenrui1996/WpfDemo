using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using SQLitePCL;
using WpfDemo.ViewModels.PageViewModels;
using static WpfDemo.Attributes.CustomDataGridAttributes;

namespace WpfDemo.SqlLite
{
    public class SqliteHelper
    {
        private static readonly Lazy<SqliteHelper> _instance = new(() => new SqliteHelper());

        private readonly string _connectionString;

        private static readonly string _dbPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "WPFDemo",
            "mydb.sqlite"
        );

        //private static readonly string _dbPath = "mydb.sqlite";

        private SqliteHelper()
        {
            _connectionString = $"Data Source={_dbPath};Cache=Shared;";
            Batteries.Init();
            // 如果数据库文件不存在，可以在这里自动创建表
            InitTable();
        }

        /// <summary>
        /// 单例懒加载，切记connection不能复用，会出现脏读脏写
        /// </summary>
        public static SqliteHelper Instance => _instance.Value;

        /// <summary>
        /// 初始化应用程序的数据库白哦
        /// </summary>
        private void InitTable()
        {
            ExecuteNonQuery(
                @"CREATE TABLE IF NOT EXISTS Users (
                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                            Name TEXT NOT NULL,
                            Gender INTEGER NOT NULL,
                            Age INTEGER)"
            );
        }

        /// <summary>
        /// 执行非查询 SQL (增删改)
        /// </summary>
        public int ExecuteNonQuery(string sql, params SqliteParameter[] parameters)
        {
            using var conn = new SqliteConnection(_connectionString);
            conn.Open();

            using var tran = conn.BeginTransaction();
            try
            {
                using var cmd = new SqliteCommand(sql, conn, tran);
                if (parameters != null)
                    cmd.Parameters.AddRange(parameters);
                return cmd.ExecuteNonQuery();
            }
            catch
            {
                tran.Rollback();
                throw;
            }
        }

        /// <summary>
        /// 执行查询 SQL，返回 DataTable
        /// </summary>
        public DataTable ExecuteQuery(string sql, params SqliteParameter[] parameters)
        {
            var dt = new DataTable();

            using var conn = new SqliteConnection(_connectionString);
            conn.Open();

            using var cmd = new SqliteCommand(sql, conn);
            if (parameters != null)
                cmd.Parameters.AddRange(parameters);

            using var reader = cmd.ExecuteReader();
            dt.Load(reader);
            return dt;
        }

        /// <summary>
        /// 执行查询 SQL，返回单个值
        /// </summary>
        public object ExecuteScalar(string sql, params SqliteParameter[] parameters)
        {
            using var conn = new SqliteConnection(_connectionString);
            conn.Open();
            using var cmd = new SqliteCommand(sql, conn);
            if (parameters != null)
                cmd.Parameters.AddRange(parameters);
            return cmd.ExecuteScalar()!;
        }
    }

    public static class DataTableExtensions
    {
        public static List<T> ToEntityList<T>(this DataTable table)
            where T : new()
        {
            var properties = typeof(T).GetProperties();
            var list = new List<T>();

            foreach (DataRow row in table.Rows)
            {
                var entity = new T();

                foreach (var prop in properties)
                {
                    if (table.Columns.Contains(prop.Name) && row[prop.Name] != DBNull.Value)
                    {
                        var attr = prop.GetCustomAttribute<GridColumnAttribute>();
                        if (attr?.Type != null)
                        {
                            prop.SetValue(entity, Enum.ToObject(attr.Type, row[prop.Name]));
                            continue;
                        }
                        prop.SetValue(
                            entity,
                            Convert.ChangeType(row[prop.Name], prop.PropertyType)
                        );
                    }
                }

                list.Add(entity);
            }

            return list;
        }
    }

    public static class CollectionExtensions
    {
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> source)
        {
            return new ObservableCollection<T>(source);
        }
    }
}
