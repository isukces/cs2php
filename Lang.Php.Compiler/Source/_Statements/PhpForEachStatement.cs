using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lang.Php.Compiler.Source
{

    /*
    smartClass
    option NoAdditionalFile
    implement Constructor KeyVarname, ValueVarname, Collection, Statement
    implement Constructor ValueVarname, Collection, Statement
    
    property KeyVarname string 
    	preprocess value = ad(value);
    
    property ValueVarname string 
    	preprocess value = ad(value);
    
    property OneVariable bool 
    	read only string.IsNullOrEmpty(keyVarname)
    
    property Collection IPhpValue 
    
    property Statement IPhpStatement 
    smartClassEnd
    */

    public partial class PhpForEachStatement : IPhpStatementBase
    {

        public override IPhpStatement Simplify(IPhpSimplifier s)
        {
            var __collection = s.Simplify(collection);
            var __statement = s.Simplify(statement);
            if (__collection == collection && __statement == statement)
                return this;
            if (string.IsNullOrEmpty(keyVarname))
                return new PhpForEachStatement(valueVarname, __collection, __statement);
            return new PhpForEachStatement(keyVarname, valueVarname, __collection, __statement);
        }
        #region Static Methods

        // Private Methods 

        static string ad(string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new NotSupportedException();
            // return "";
            return value.StartsWith("$") ? value : "$" + value;
        }

        #endregion Static Methods

        #region Methods

        // Public Methods 

        //LangType ItemType, string VarName, IValue Collection, IStatement Statement
        public override void Emit(PhpSourceCodeEmiter emiter, PhpSourceCodeWriter writer, PhpEmitStyle style)
        {
            style = style ?? new PhpEmitStyle();
            var arrayOperator = style.Compression == EmitStyleCompression.Beauty ? " => " : "=>";
            string header = OneVariable ? "foreach({0} as {3})" : "foreach({0} as {1}{2}{3})";
            header = string.Format(header,
                collection.GetPhpCode(style),
                keyVarname, arrayOperator, valueVarname);
            EmitHeaderStatement(emiter, writer, style, header, statement);
        }

        public override IEnumerable<ICodeRequest> GetCodeRequests()
        {
            var t = Xxx(collection, statement).ToList();
            if (!string.IsNullOrEmpty(keyVarname))
            {
                var a = new LocalVariableRequest(keyVarname, false,
                    (nv) =>
                    {
                        keyVarname = nv;
                    });
                t.Add(a);
            }
            if (!string.IsNullOrEmpty(valueVarname))
            {
                var a = new LocalVariableRequest(valueVarname, false,
                    (nv) =>
                    {
                        valueVarname = nv;
                    });
                t.Add(a);
            }
            return t;
        }

        #endregion Methods
    }
}


// -----:::::##### smartClass embedded code begin #####:::::----- generated 2013-11-07 12:45
// File generated automatically ver 2013-07-10 08:43
// Smartclass.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0c4d5d36fb5eb4ac
namespace Lang.Php.Compiler.Source
{
    partial class PhpForEachStatement
    {
        /*
        /// <summary>
        /// Tworzy instancję obiektu
        /// </summary>
        public PhpForEachStatement()
        {
        }
        Przykłady użycia
        implement INotifyPropertyChanged
        implement INotifyPropertyChanged_Passive
        implement ToString ##KeyVarname## ##ValueVarname## ##OneVariable## ##Collection## ##Statement##
        implement ToString KeyVarname=##KeyVarname##, ValueVarname=##ValueVarname##, OneVariable=##OneVariable##, Collection=##Collection##, Statement=##Statement##
        implement equals KeyVarname, ValueVarname, OneVariable, Collection, Statement
        implement equals *
        implement equals *, ~exclude1, ~exclude2
        */


        #region Constructors
        /// <summary>
        /// Tworzy instancję obiektu
        /// <param name="KeyVarname"></param>
        /// <param name="ValueVarname"></param>
        /// <param name="Collection"></param>
        /// <param name="Statement"></param>
        /// </summary>
        public PhpForEachStatement(string KeyVarname, string ValueVarname, IPhpValue Collection, IPhpStatement Statement)
        {
            this.KeyVarname = KeyVarname;
            this.ValueVarname = ValueVarname;
            this.Collection = Collection;
            this.Statement = Statement;
        }

        /// <summary>
        /// Tworzy instancję obiektu
        /// <param name="ValueVarname"></param>
        /// <param name="Collection"></param>
        /// <param name="Statement"></param>
        /// </summary>
        public PhpForEachStatement(string ValueVarname, IPhpValue Collection, IPhpStatement Statement)
        {
            this.ValueVarname = ValueVarname;
            this.Collection = Collection;
            this.Statement = Statement;
        }

        #endregion Constructors


        #region Constants
        /// <summary>
        /// Nazwa własności KeyVarname; 
        /// </summary>
        public const string PROPERTYNAME_KEYVARNAME = "KeyVarname";
        /// <summary>
        /// Nazwa własności ValueVarname; 
        /// </summary>
        public const string PROPERTYNAME_VALUEVARNAME = "ValueVarname";
        /// <summary>
        /// Nazwa własności OneVariable; 
        /// </summary>
        public const string PROPERTYNAME_ONEVARIABLE = "OneVariable";
        /// <summary>
        /// Nazwa własności Collection; 
        /// </summary>
        public const string PROPERTYNAME_COLLECTION = "Collection";
        /// <summary>
        /// Nazwa własności Statement; 
        /// </summary>
        public const string PROPERTYNAME_STATEMENT = "Statement";
        #endregion Constants


        #region Methods
        #endregion Methods


        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public string KeyVarname
        {
            get
            {
                return keyVarname;
            }
            set
            {
                value = (value ?? String.Empty).Trim();
                value = ad(value);
                keyVarname = value;
            }
        }
        private string keyVarname = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        public string ValueVarname
        {
            get
            {
                return valueVarname;
            }
            set
            {
                value = (value ?? String.Empty).Trim();
                value = ad(value);
                valueVarname = value;
            }
        }
        private string valueVarname = string.Empty;
        /// <summary>
        /// Własność jest tylko do odczytu.
        /// </summary>
        public bool OneVariable
        {
            get
            {
                return string.IsNullOrEmpty(keyVarname);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public IPhpValue Collection
        {
            get
            {
                return collection;
            }
            set
            {
                collection = value;
            }
        }
        private IPhpValue collection;
        /// <summary>
        /// 
        /// </summary>
        public IPhpStatement Statement
        {
            get
            {
                return statement;
            }
            set
            {
                statement = value;
            }
        }
        private IPhpStatement statement;
        #endregion Properties
    }
}
