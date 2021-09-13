using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent.Models
{

    public abstract class Repository<T, V> : IRepository<T> where T : BaseMetric, new() where V : IDbConnection, new()
    {
        protected virtual string ConnectionString => "Data Source=metrics.db;Version=3;Pooling=true;Max Pool Size=100;";
        protected abstract string TableName {get;}

        protected Repository()
        {
            using (V connection = GetConnection())
            {
                using (IDbCommand dbCommand = connection.CreateCommand())
                {
                    dbCommand.CommandText = $"DROP TABLE IF EXISTS {TableName};";
                    dbCommand.ExecuteNonQuery();

                    dbCommand.CommandText = $"CREATE TABLE {TableName}(id INTEGER PRIMARY KEY AUTOINCREMENT, value INT NOT NULL, time INT UNIQUE);";
                    dbCommand.ExecuteNonQuery();
                }
            }

            //AddTestData();
        }

        private void AddTestData()
        {
            using (V connection = GetConnection())
            {
                using (IDbCommand dbCommand = connection.CreateCommand())
                {
                    Random random = new Random();
                    int currentValue = 50;

                    Enumerable.Range(0, 100).ToList().ForEach(i => {
                        dbCommand.CommandText = $"INSERT INTO {TableName}(value,time) VALUES({currentValue = Math.Clamp(currentValue + random.Next(-20, 20), 0, 100)},{i});";
                        dbCommand.ExecuteNonQuery();
                    });
                }
            }
        }

        protected V GetConnection()
        {
            V connection = new V();
            connection.ConnectionString = ConnectionString;
            connection.Open();
            return connection;
        }

        IList<T> IRepository<T>.GetAll()
        {
            using (V connection = GetConnection())
            {
                using (IDbCommand dbCommand = connection.CreateCommand())
                {
                    dbCommand.CommandText = $"SELECT * FROM {TableName};";

                    IDataReader dataReader = dbCommand.ExecuteReader();

                    List<T> result = new List<T>(dataReader.FieldCount);

                    while (dataReader.Read())
                        result.Add(new T
                        {
                            Id = dataReader.GetInt32(0),
                            Value = dataReader.GetInt32(1),
                            Time = TimeSpan.FromSeconds(dataReader.GetInt32(2))
                        });

                    return result;
                }
            }
        }

        void IRepository<T>.Create(T item)
        {
            using (V connection = GetConnection())
            {
                using (IDbCommand dbCommand = connection.CreateCommand())
                {
                    dbCommand.CommandText = $"INSERT INTO {TableName}(value,time) VALUES({item.Value},{(int)item.Time.TotalSeconds});";

                    dbCommand.ExecuteNonQuery();
                }
            }
        }

        void IRepository<T>.Delete(int id)
        {
            using (V connection = GetConnection())
            {
                using (IDbCommand dbCommand = connection.CreateCommand())
                {
                    dbCommand.CommandText = $"DELETE FROM {TableName} WHERE id={id}";

                    dbCommand.ExecuteNonQuery();
                }
            }
        }

        T IRepository<T>.GetById(int id)
        {
            using (V connection = GetConnection())
            {
                using (IDbCommand dbCommand = connection.CreateCommand())
                {
                    dbCommand.CommandText = $"SELECT * FROM {TableName} WHERE Id={id};";

                    IDataReader dataReader = dbCommand.ExecuteReader();

                    return dataReader.Read() ? new T
                    {
                        Id = dataReader.GetInt32(0),
                        Value = dataReader.GetInt32(1),
                        Time = TimeSpan.FromSeconds(dataReader.GetInt32(2))
                    } : null;
                }
            }
        }

        void IRepository<T>.Update(T item)
        {
            using (V connection = GetConnection())
            {
                using (IDbCommand dbCommand = connection.CreateCommand())
                {
                    dbCommand.CommandText = $"UPDATE {TableName} SET value={item.Value}, time={(int)item.Time.TotalSeconds} WHERE id={item.Id};";

                    dbCommand.ExecuteNonQuery();
                }
            }
        }

    }
    
}
