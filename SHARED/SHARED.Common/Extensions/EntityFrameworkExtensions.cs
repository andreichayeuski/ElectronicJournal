using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace SHARED.Common.Extensions
{
    public static class EntityFrameworkExtensions
    {
        /// <returns>true, если были изменения</returns>
        public static bool UpdateCollection<T, TKey>(this ICollection<T> existingItems, IEnumerable<T> newItems, Func<T, TKey> comparer, DbContext db) where T : class
        {
            var eItems = existingItems.ToList();
            var nItems = newItems.ToList();

            var addedItems = nItems.Except(eItems, comparer).ToList();

            var deletedItems = eItems.Except(nItems, comparer).ToList();

            var updatedItems = eItems.Except(deletedItems, comparer).ToList();

            addedItems.ForEach(existingItems.Add);
            //addedItems.ForEach(t => db.Entry(t).State = EntityState.Added);
            deletedItems.ForEach(t => db.Entry(t).State = EntityState.Deleted);
            updatedItems.ForEach(t => db.Entry(t).State = EntityState.Modified);

            return addedItems.Any() || deletedItems.Any();
        }

        /// <returns>true, если были изменения</returns>
        public static bool UpdateCollection<T>(this ICollection<T> existingItems, IEnumerable<T> newItems, IEqualityComparer<T> comparer, DbContext db) where T : class
        {
            var eItems = existingItems.ToList();
            var nItems = newItems.ToList();

            var addedItems = nItems.Except(eItems, comparer).ToList();

            var deletedItems = eItems.Except(nItems, comparer).ToList();

            var updatedItems = eItems.Except(deletedItems, comparer).ToList();

            addedItems.ForEach(existingItems.Add);
            //addedItems.ForEach(t => db.Entry(t).State = EntityState.Added);
            deletedItems.ForEach(t => db.Entry(t).State = EntityState.Deleted);
            updatedItems.ForEach(t => db.Entry(t).State = EntityState.Modified);

            return addedItems.Any() || deletedItems.Any();
        }

        //public static string DumpChanges(this DbContext dbContext)
        //{
        //    var context = ((IObjectContextAdapter) dbContext).ObjectContext;

        //    context.DetectChanges();

        //    var updateText = new StringBuilder();

        //    foreach (var added in context.ObjectStateManager.GetObjectStateEntries(EntityState.Added))
        //    {
        //        updateText.AppendLine(string.Format("<strong>Добавление данных</strong>, таблица - {0}, Id={1}<br/>", added.EntityKey.EntitySetName,added.EntityKey.EntityKeyValues!=null?added.EntityKey.EntityKeyValues[0].Value:""));
        //        for (var i = 0; i < added.CurrentValues.FieldCount; ++i)
        //        {
        //            updateText.AppendLine(
        //                string.Format("Свойство: [{0}]. Новое значение: [{1}]."
        //                    , added.Entity.GetPropertyInfos().FirstOrDefault(z => z.Name == added.CurrentValues.GetName(i)).GetAttributValue<DisplayAttribute, string>(d => d.Name)
        //                    , added.CurrentValues.GetValue(i)));
        //        }

        //    }

        //    foreach (var modified in context.ObjectStateManager.GetObjectStateEntries(EntityState.Modified))
        //    {
        //        // Put original field values into dictionary
        //        var originalValues = new Dictionary<string, int>();
        //        for (var i = 0; i < modified.OriginalValues.FieldCount; ++i)
        //        {
        //            originalValues.Add(modified.OriginalValues.GetName(i), i);
        //        }


        //        // Output each of the changed properties.
        //        foreach (var entry in modified.GetModifiedProperties())
        //        {
        //            var originalIdx = originalValues[entry];

        //            if ((modified.OriginalValues.GetValue(originalIdx) is int) && (modified.CurrentValues.GetValue(originalIdx) is int))
        //            {
        //                if ((int)modified.OriginalValues.GetValue(originalIdx) != (int)modified.CurrentValues.GetValue(originalIdx))
        //                {
        //                    updateText.AppendLine(string.Format("<strong>Изменение данных</strong>, таблица - {0}, Id={1}<br/>", modified.EntityKey.EntitySetName, modified.EntityKey.EntityKeyValues != null ? modified.EntityKey.EntityKeyValues[0].Value : ""));
        //                    updateText.AppendLine(
        //                        string.Format("Свойство: [{0}]. Старое значение: [{1}] -> Новое значение: [{2}].<br/>"
        //                        //, modified.OriginalValues.GetName(originalIdx)
        //                    , modified.Entity.GetPropertyInfos().FirstOrDefault(z => z.Name == modified.OriginalValues.GetName(originalIdx)).GetAttributValue<DisplayAttribute, string>(d => d.Name)
        //                    , modified.OriginalValues.GetValue(originalIdx),
        //                    modified.CurrentValues.GetValue(originalIdx)));
        //                }
        //            }
        //            else
        //            {
        //                updateText.AppendLine(string.Format("<strong>Изменение данных</strong>, таблица - {0}, Id={1}<br/>", modified.EntityKey.EntitySetName, modified.EntityKey.EntityKeyValues != null ? modified.EntityKey.EntityKeyValues[0].Value : ""));
        //                updateText.AppendLine(
        //                string.Format("Свойство: [{0}]. Старое значение: [{1}] -> Новое значение: [{2}].<br/>"
        //                    //, modified.OriginalValues.GetName(originalIdx)
        //                    , modified.Entity.GetPropertyInfos().FirstOrDefault(z => z.Name == modified.OriginalValues.GetName(originalIdx)).GetAttributValue<DisplayAttribute, string>(d => d.Name)
        //                    , modified.OriginalValues.GetValue(originalIdx),
        //                    modified.CurrentValues.GetValue(originalIdx)));
        //            }


        //        }

        //    }

        //    foreach (var deleted in context.ObjectStateManager.GetObjectStateEntries(EntityState.Deleted))
        //    {
        //        updateText.AppendLine(string.Format("<strong>Удаление</strong>, таблица - {0}, Id={1}<br/>", deleted.EntityKey.EntitySetName, deleted.EntityKey.EntityKeyValues!=null? deleted.EntityKey.EntityKeyValues[0].Value:""));

        //    }

        //    return updateText.ToString();
        //}
    }

    public static class GenericListDataReaderExtensions
    {
        public static GenericListDataReader<T> GetDataReader<T>(this IEnumerable<T> list)
        {
            return new GenericListDataReader<T>(list);
        }
    }

    public class GenericListDataReader<T> : IDataReader
    {
        private IEnumerator<T> list = null;
        private List<PropertyInfo> properties = new List<PropertyInfo>();
        private Dictionary<string, int> nameLookup = new Dictionary<string, int>();

        public GenericListDataReader(IEnumerable<T> list)
        {
            this.list = list.GetEnumerator();

            properties.AddRange(
                typeof(T)
                .GetProperties(
                    BindingFlags.GetProperty |
                    BindingFlags.Instance |
                    BindingFlags.Public |
                    BindingFlags.DeclaredOnly
                    ));

            for (int i = 0; i < properties.Count; i++)
            {
                nameLookup[properties[i].Name] = i;
            }
        }

        #region IDataReader Members

        public void Close()
        {
            list.Dispose();
        }

        public int Depth
        {
            get { throw new NotImplementedException(); }
        }

        public DataTable GetSchemaTable()
        {
            throw new NotImplementedException();
        }

        public bool IsClosed
        {
            get { throw new NotImplementedException(); }
        }

        public bool NextResult()
        {
            throw new NotImplementedException();
        }

        public bool Read()
        {
            return list.MoveNext();
        }

        public int RecordsAffected
        {
            get { throw new NotImplementedException(); }
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            Close();
        }

        #endregion

        #region IDataRecord Members

        public int FieldCount
        {
            get { return properties.Count; }
        }

        public bool GetBoolean(int i)
        {
            return (bool)GetValue(i);
        }

        public byte GetByte(int i)
        {
            return (byte)GetValue(i);
        }

        public long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length)
        {
            throw new NotImplementedException();
        }

        public char GetChar(int i)
        {
            return (char)GetValue(i);
        }

        public long GetChars(int i, long fieldoffset, char[] buffer, int bufferoffset, int length)
        {
            throw new NotImplementedException();
        }

        public IDataReader GetData(int i)
        {
            throw new NotImplementedException();
        }

        public string GetDataTypeName(int i)
        {
            throw new NotImplementedException();
        }

        public DateTime GetDateTime(int i)
        {
            return (DateTime)GetValue(i);
        }

        public decimal GetDecimal(int i)
        {
            return (decimal)GetValue(i);
        }

        public double GetDouble(int i)
        {
            return (double)GetValue(i);
        }

        public Type GetFieldType(int i)
        {
            return properties[i].PropertyType;
        }

        public float GetFloat(int i)
        {
            return (float)GetValue(i);
        }

        public Guid GetGuid(int i)
        {
            return (Guid)GetValue(i);
        }

        public short GetInt16(int i)
        {
            return (short)GetValue(i);
        }

        public int GetInt32(int i)
        {
            return (int)GetValue(i);
        }

        public long GetInt64(int i)
        {
            return (long)GetValue(i);
        }

        public string GetName(int i)
        {
            return properties[i].Name;
        }

        public int GetOrdinal(string name)
        {
            if (nameLookup.ContainsKey(name))
            {
                return nameLookup[name];
            }
            else
            {
                return -1;
            }
        }

        public string GetString(int i)
        {
            return (string)GetValue(i);
        }

        public object GetValue(int i)
        {
            return properties[i].GetValue(list.Current, null);
        }

        public int GetValues(object[] values)
        {
            int getValues = Math.Max(FieldCount, values.Length);

            for (int i = 0; i < getValues; i++)
            {
                values[i] = GetValue(i);
            }

            return getValues;
        }

        public bool IsDBNull(int i)
        {
            return GetValue(i) == null;
        }

        public object this[string name]
        {
            get
            {
                return GetValue(GetOrdinal(name));
            }
        }

        public object this[int i]
        {
            get
            {
                return GetValue(i);
            }
        }

        #endregion
    }
}
