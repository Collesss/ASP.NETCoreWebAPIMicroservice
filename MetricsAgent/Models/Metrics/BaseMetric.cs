using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent.Models
{
    public abstract class BaseMetric : IBaseMetric
    {
        //protected virtual string TableName => "";

        public int Id { get; set; }

        public int Value { get; set; }

        public TimeSpan Time { get; set; }
        /*
        void IBaseMetric.FillField(IDataRecord dataRecord)
        {
            Id = dataRecord.GetInt32(0);
            Value = dataRecord.GetInt32(1);
            Time = TimeSpan.FromSeconds(dataRecord.GetInt32(2));
        }

        void IBaseMetric.GetAllFill(IDbCommand dbCommand) =>
            dbCommand.CommandText = $"SELECT * FROM {DataBase};";

        void IBaseMetric.CreateFill(IDbCommand dbCommand) =>
            dbCommand.CommandText = $"INSERT INTO {DataBase}(value,time) VALUES({Value},{(int)Time.TotalSeconds});";

        void IBaseMetric.DeleteFill(IDbCommand dbCommand) =>
            dbCommand.CommandText = $"DELETE FROM {DataBase} WHERE id={Id}";

        void IBaseMetric.UpdateFill(IDbCommand dbCommand) =>
            dbCommand.CommandText = $"UPDATE {DataBase} SET value={Value}, time={(int)Time.TotalSeconds} WHERE id={Id};";

        string IBaseMetric.GetScheme() =>
            $"CREATE TABLE IF NOT EXISTS {DataBase}(id INTEGER PRIMARY KEY AUTOINCREMENT, value INT NOT NULL, time INT UNIQUE);";
        */
    }
}
