using Microsoft.Data.Sqlite;
using QueryBuilder.Models;
using System.Reflection;
using System.Text;

namespace QueryBuilder
{
    public class QueryBuilder : IDisposable
    {
        private SqliteConnection Connection;
        public QueryBuilder(string databaseLocation)
        {
            Connection = new SqliteConnection("Data Source=" + databaseLocation);
            Connection.Open();
        }

        public T Read<T>(int Id) where T : IClassModel, new()
        {
            var command = Connection.CreateCommand();
            command.CommandText = $"SELECT * FROM {typeof(T).Name} WHERE Id= ({Id})";
            var reader = command.ExecuteReader();
            T result = new T();
           
            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    if (typeof(T).GetProperty(reader.GetName(i)).PropertyType == typeof(int))
                        typeof(T).GetProperty(reader.GetName(i)).SetValue(result, Convert.ToInt32(reader.GetValue(i)));
                    else
                        typeof(T).GetProperty(reader.GetName(i)).SetValue(result, reader.GetValue(i));
                }   
            }
            return result;
        } 
        public List<T> ReadAll<T>() where T : IClassModel, new()
        {
            var command = Connection.CreateCommand();
            command.CommandText = $"SELECT * FROM {typeof(T).Name}";
            var reader = command.ExecuteReader();
            T data;
            var datas = new List<T>();
            while (reader.Read())
            {
                data = new T();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    if (typeof(T).GetProperty(reader.GetName(i)).PropertyType == typeof(int))
                        typeof(T).GetProperty(reader.GetName(i)).SetValue(data, Convert.ToInt32(reader.GetValue(i)));
                    else
                        typeof(T).GetProperty(reader.GetName(i)).SetValue(data, reader.GetValue(i));
                }
                datas.Add(data);
            }
            return datas;
        }

        public void Create<T>(T obj)
        {
            PropertyInfo[] properties = typeof(T).GetProperties();

            List<string> values = new List<string>();
            List<string> names = new List<string>();
            
            foreach (PropertyInfo property in properties)
            {
                if (property.PropertyType == typeof(string))
                {
                    values.Add("\"" + property.GetValue(obj) + "\"");
                }
                else
                    values.Add(property.GetValue(obj).ToString());

                names.Add(property.Name);
            }  

            StringBuilder sbValues = new StringBuilder();
            StringBuilder sbNames = new StringBuilder();
            
            for(int i = 0; i < values.Count; i++)
            {
                if(i == values.Count - 1)
                {
                    sbValues.Append($"{values[i]}");
                    sbNames.Append($"{names[i]}");
                }
                else
                {
                    sbValues.Append($"{values[i]},");
                    sbNames.Append($"{names[i]},");
                }
            }

            var command = Connection.CreateCommand();
            command.CommandText = $"INSERT INTO {typeof(T).Name} ({sbNames}) VALUES ({sbValues})";

            var insert = command.ExecuteNonQuery();
        }

        public void Update<T>(T obj) where T: IClassModel
        {
            PropertyInfo[] properties = typeof(T).GetProperties();

            List<string> values = new List<string>();
            List<string> names = new List<string>();

            foreach (PropertyInfo property in properties)
            {
                if (property.PropertyType == typeof(string))
                {
                    values.Add("\"" + property.GetValue(obj) + "\"");
                }
                else
                    values.Add(property.GetValue(obj).ToString());

                names.Add(property.Name);
            }

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < values.Count; i++)
            {
                if (i == values.Count - 1)
                {
                    sb.Append($"{names[i]} = {values[i]}");
                }
                else
                {
                    sb.Append($"{names[i]} = {values[i]},");
                }
            }

            var command = Connection.CreateCommand();
            command.CommandText = $"UPDATE {typeof(T).Name} SET {sb} WHERE ID = {obj.ID};";

            var insert = command.ExecuteNonQuery();
        }

        public void Delete<T>(T obj) where T : IClassModel
        {
            var command = Connection.CreateCommand();
            command.CommandText = $"DELETE FROM {typeof(T).Name} WHERE ID= ({obj.ID})";
        }

        public void Dispose()
        {
            Connection.Dispose();
        }
    }
}
