using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Lang.Php.Runtime
{
    internal class RuntimeMysqlResult : MysqlResult
    {
        #region Methods

        // Public Methods 

        public override bool FetchAssoc<T>(out T row)
        {
            if (_phpLevelDisposed || !_ok || _cursor >= _results.Count)
            {
                row = default(T);
                return false;
            }

            row = Activator.CreateInstance<T>();
            FieldInfo[] fields = typeof(T).GetFields();
            Dictionary<string, FieldInfo> fieldMapping = new Dictionary<string, FieldInfo>();
            foreach (var f in fields)
            {
                string sn = f.Name;
                var _scriptName = f.GetCustomAttributes(true).OfType<Lang.Php.ScriptNameAttribute>().FirstOrDefault();
                if (_scriptName != null)
                    sn = _scriptName.Name;

                fieldMapping[sn] = f;
            }
            var _ROW = _results[_cursor++];
            for (int nr = 0; nr < _fieldNames.Length; nr++)
            {
                FieldInfo ii;
                try
                {
                    if (fieldMapping.TryGetValue(_fieldNames[nr], out ii))
                        ii.SetValue(row, _ROW[nr]);
                }
                catch (Exception e)
                {
                    throw new PlatformImplementationException(this.GetType(), "FetchAssoc", e.Message);
                }
            }
            return true;
            // throw new NotSupportedException();
        }
        // Protected Methods 

        protected override int getNumRows()
        {
            if (_ok)
                return _results.Count;
            return 0;
        }
        // Internal Methods 

        internal void _SetFromDR(DbDataReader reader)
        {
            _ok = false;
            _fieldNames = Enumerable.Range(0, reader.FieldCount).Select(i => reader.GetName(i)).ToArray();
            _types = Enumerable.Range(0, reader.FieldCount).Select(i => reader.GetDataTypeName(i)).ToArray();
            while (reader.Read())
            {
                object[] values = new object[reader.FieldCount];
                reader.GetValues(values);
                _results.Add(values);
            }
            _ok = true;

        }

        #endregion Methods

        #region Fields

        int _cursor = 0;
        private string[] _fieldNames;
        private List<object[]> _results = new List<object[]>();
        private string[] _types;

        #endregion Fields
    }
}
