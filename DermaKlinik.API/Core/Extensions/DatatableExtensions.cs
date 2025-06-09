using System.Data;
using System.Dynamic;
using System.Reflection;

namespace DermaKlinik.API.Core.Extensions
{
    public static class DatatableExtensions
    {
        public static IEnumerable<object> DynamicEnumerable(this DataTable table) => table.AsEnumerable().Select(row => new DynamicRow(row));

        public static List<T> ConvertDataTable<T>(this DataTable dt)
        {
            List<T> objList = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T obj = GetItem<T>(row);
                objList.Add(obj);
            }
            return objList;
        }

        private static T GetItem<T>(DataRow dr)
        {
            Type type = typeof(T);
            T instance = Activator.CreateInstance<T>();
            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo property in type.GetProperties())
                {
                    object obj = dr[column.ColumnName];
                    if (obj != null && obj != DBNull.Value && property.Name == column.ColumnName)
                        property.SetValue(instance, dr[column.ColumnName], (object[])null);
                }
            }
            return instance;
        }

        private sealed class DynamicRow : DynamicObject
        {
            private readonly DataRow _row;

            internal DynamicRow(DataRow row) => _row = row;

            public override bool TryGetMember(GetMemberBinder binder, out object result)
            {
                bool member = _row.Table.Columns.Contains(binder.Name);
                result = member ? _row[binder.Name] : null;
                return member;
            }
        }
    }
}
