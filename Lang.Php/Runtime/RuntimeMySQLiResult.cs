using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection;

namespace Lang.Php.Runtime
{
    public class RuntimeMySQLiResult : MySQLiResult
    {
        #region Constructors

        public RuntimeMySQLiResult(MySQLi owner, MySqlDataReader reader)
        {
            this._owner = owner;
            this._reader = reader;
            _SetFromDR(reader);
            owner.OpenResult = _results.Count == 0 ? null : this;
        }

        #endregion Constructors

        #region Methods

        // Public Methods 

        public override bool Fetch<T>(out T row)
        {
            if (_phpLevelDisposed || _cursor >= _results.Count || _owner.OpenResult != this)
            {
                row = default(T);
                return false;
            }

            row = Activator.CreateInstance<T>();
            Dictionary<string, object> fieldMapping = new Dictionary<string, object>();
            {

                FieldInfo[] fields = typeof(T).GetFields();
                foreach (var f in fields)
                {
                    string sn = f.Name;
                    var _scriptName = f.GetCustomAttributes(true).OfType<ScriptNameAttribute>().FirstOrDefault();
                    if (_scriptName != null)
                        sn = _scriptName.Name;

                    fieldMapping[sn] = f;
                }
            }
            {
                PropertyInfo[] fields = typeof(T).GetProperties();
                foreach (var f in fields)
                {
                    string sn = f.Name;
                    var _scriptName = f.GetCustomAttributes(true).OfType<ScriptNameAttribute>().FirstOrDefault();
                    if (_scriptName != null)
                        sn = _scriptName.Name;

                    fieldMapping[sn] = f;
                }
            }
            var _ROW = _results[_cursor++];
            for (int nr = 0; nr < _fieldNames.Length; nr++)
            {
                object ii;
                try
                {
                    if (fieldMapping.TryGetValue(_fieldNames[nr], out ii))
                    {
                        if (ii is FieldInfo)
                            (ii as FieldInfo).SetValue(row, _ROW[nr]);
                        else if (ii is PropertyInfo)
                            (ii as PropertyInfo).SetValue(row, _ROW[nr], null);
                    }
                }
                catch (Exception e)
                {
                    throw new PlatformImplementationException(GetType(), "FetchAssoc", e.Message);
                }
            }
            if (_cursor >= _results.Count)
                Free();
            return true;
        }

        public override void Free()
        {
            if (_owner.OpenResult == this)
                _owner.OpenResult = null;
            _phpLevelDisposed = true;
        }
        // Internal Methods 

        internal void _SetFromDR(DbDataReader reader)
        {
            // _ok = false;
            _fieldNames = Enumerable.Range(0, reader.FieldCount).Select(i => reader.GetName(i)).ToArray();
            _types = Enumerable.Range(0, reader.FieldCount).Select(i => reader.GetDataTypeName(i)).ToArray();
            while (reader.Read())
            {
                object[] values = new object[reader.FieldCount];
                reader.GetValues(values);
                _results.Add(values);
            }
            // _ok = true;

        }

        #endregion Methods

        #region Fields

        int _cursor = 0;
        private string[] _fieldNames;
        bool _phpLevelDisposed;
        private readonly List<object[]> _results = new List<object[]>();
        private string[] _types;
        readonly MySQLi _owner;
        MySqlDataReader _reader;

        #endregion Fields

        #region Properties

        public override int NumRows
        {
            get
            {
                return _results.Count;
            }
        }

        #endregion Properties
    }
}
