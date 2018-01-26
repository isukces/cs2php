using System;
using System.Collections.Generic;
using System.Linq;

namespace Lang.Php.Compiler.Source
{
    public class PhpForEachStatement : PhpStatementBase
    {
        /// <summary>
        ///     Tworzy instancję obiektu
        ///     <param name="keyVarname"></param>
        ///     <param name="valueVarname"></param>
        ///     <param name="collection"></param>
        ///     <param name="statement"></param>
        /// </summary>
        public PhpForEachStatement(string keyVarname, string valueVarname, IPhpValue collection,
            IPhpStatement                 statement)
        {
            KeyVarname   = keyVarname;
            ValueVarname = valueVarname;
            Collection   = collection;
            Statement    = statement;
        }

        /// <summary>
        ///     Tworzy instancję obiektu
        ///     <param name="valueVarname"></param>
        ///     <param name="collection"></param>
        ///     <param name="statement"></param>
        /// </summary>
        public PhpForEachStatement(string valueVarname, IPhpValue collection, IPhpStatement statement)
        {
            ValueVarname = valueVarname;
            Collection   = collection;
            Statement    = statement;
        }

        // Private Methods 

        private static string Ad(string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new NotSupportedException();
            // return "";
            return value.StartsWith("$") ? value : "$" + value;
        }

        // Public Methods 

        //LangType ItemType, string VarName, IValue Collection, IStatement Statement
        public override void Emit(PhpSourceCodeEmiter emiter, PhpSourceCodeWriter writer, PhpEmitStyle style)
        {
            style             = style ?? new PhpEmitStyle();
            var arrayOperator = style.Compression == EmitStyleCompression.Beauty ? " => " : "=>";
            var header        = OneVariable ? "foreach({0} as {3})" : "foreach({0} as {1}{2}{3})";
            header            = string.Format(header,
                Collection.GetPhpCode(style),
                _keyVarname, arrayOperator, _valueVarname);
            EmitHeaderStatement(emiter, writer, style, header, Statement);
        }

        public override IEnumerable<ICodeRequest> GetCodeRequests()
        {
            var t = GetCodeRequests(Collection, Statement).ToList();
            if (!string.IsNullOrEmpty(_keyVarname))
            {
                var a                   = new LocalVariableRequest(_keyVarname, false,
                    nv => { _keyVarname = nv; });
                t.Add(a);
            }

            if (!string.IsNullOrEmpty(_valueVarname))
            {
                var a                     = new LocalVariableRequest(_valueVarname, false,
                    nv => { _valueVarname = nv; });
                t.Add(a);
            }

            return t;
        }

        public override IPhpStatement Simplify(IPhpSimplifier s)
        {
            var collection = s.Simplify(Collection);
            var statement  = s.Simplify(Statement);
            if (collection == Collection && statement == Statement)
                return this;
            if (string.IsNullOrEmpty(_keyVarname))
                return new PhpForEachStatement(_valueVarname, collection,    statement);
            return new PhpForEachStatement(_keyVarname,       _valueVarname, collection, statement);
        }


        /// <summary>
        /// </summary>
        public string KeyVarname
        {
            get => _keyVarname;
            set
            {
                value       = (value ?? string.Empty).Trim();
                value       = Ad(value);
                _keyVarname = value;
            }
        }

        /// <summary>
        /// </summary>
        public string ValueVarname
        {
            get => _valueVarname;
            set
            {
                value         = (value ?? string.Empty).Trim();
                value         = Ad(value);
                _valueVarname = value;
            }
        }

        /// <summary>
        ///     Własność jest tylko do odczytu.
        /// </summary>
        public bool OneVariable => string.IsNullOrEmpty(_keyVarname);

        /// <summary>
        /// </summary>
        public IPhpValue Collection { get; set; }

        /// <summary>
        /// </summary>
        public IPhpStatement Statement { get; set; }

        private string _keyVarname   = string.Empty;
        private string _valueVarname = string.Empty;
    }
}