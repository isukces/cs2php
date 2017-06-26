using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection;

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
            var fields = typeof(T).GetFields();
            var fieldMapping = new Dictionary<string, FieldInfo>();
            foreach (var f in fields)
            {
                var sn = f.Name;
                var scriptName = f.GetCustomAttributes(true).OfType<ScriptNameAttribute>().FirstOrDefault();
                if (scriptName != null)
                    sn = scriptName.Name;

                fieldMapping[sn] = f;
            }
            var values = _results[_cursor++];
            for (var nr = 0; nr < _fieldNames.Length; nr++)
            {
                try
                {
                    FieldInfo fieldInfo;
                    if (fieldMapping.TryGetValue(_fieldNames[nr], out fieldInfo))
                        fieldInfo.SetValue(row, values[nr]);
                }
                catch (Exception e)
                {
                    throw new PlatformImplementationException(GetType(), "FetchAssoc", e.Message);
                }
            }
            return true;
            // throw new NotSupportedException();
        }
        // Protected Methods 

        protected override int getNumRows()
        {
            return _ok ? _results.Count : 0;
        }

        // Internal Methods 

        internal void _SetFromDR(DbDataReader reader)
        {
            _ok = false;
            _fieldNames = Enumerable.Range(0, reader.FieldCount).Select(reader.GetName).ToArray();
            _types = Enumerable.Range(0, reader.FieldCount).Select(reader.GetDataTypeName).ToArray();
            while (reader.Read())
            {
                var values = new object[reader.FieldCount];
                reader.GetValues(values);
                _results.Add(values);
            }
            _ok = true;

        }

        #endregion Methods

        #region Fields

        int _cursor;
        private string[] _fieldNames;
        private List<object[]> _results = new List<object[]>();
        private string[] _types;

        #endregion Fields
    }
}
