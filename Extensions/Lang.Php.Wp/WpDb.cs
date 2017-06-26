using System;
using System.Collections.Generic;

namespace Lang.Php.Wp
{
   [BuiltIn]
    public class WpDb
    {
        public List<string[]> queries;


        [DirectCall("prepare")]
        public string Prepare(string query, params object[] args)
        {
            throw new NotImplementedException();
        }

        [DirectCall("get_var")]
        public Nullable<T> GetVar<T>(string query) where T : struct
        {
            throw new NotImplementedException();
        }
        [DirectCall("get_var")]
        public T GetVarClass<T>(string query) where T : class
        {
            throw new NotImplementedException();
        }
    }
}
