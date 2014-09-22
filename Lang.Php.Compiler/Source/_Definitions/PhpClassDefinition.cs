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
    implement Constructor Name
    implement Constructor Name, BaseTypeName
    
    property Name PhpClassName Nazwa klasy
    	read only
    
    property BaseTypeName PhpClassName Nazwa klasy
    	read only
    
    property Methods List<PhpClassMethodDefinition> 
    	init #
    
    property Fields List<PhpClassFieldDefinition> 
    	init #
    smartClassEnd
    */

    public partial class PhpClassDefinition : ICodeRelated, IEmitable
    {
        #region Static Methods

        // Private Methods 

        static int FieldOrderGroup(PhpClassFieldDefinition a)
        {
            if (a.IsConst) return 0;
            if (a.IsStatic) return 1;
            return 2;
        }

        #endregion Static Methods

        #region Methods

        // Public Methods 

        public void Emit(PhpSourceCodeEmiter emiter, PhpSourceCodeWriter writer, PhpEmitStyle style)
        {
            var saveStyleCurrentClass = style.CurrentClass;
            var saveStyleCurrentNamespace = style.CurrentNamespace;
            try
            {
                if (IsEmpty)
                    return;
                if (style.CurrentNamespace == null)
                    style.CurrentNamespace = PhpNamespace.Root;
                if (style.CurrentNamespace != name.Namespace)
                    throw new Exception("Unable to emit class into different namespace");
                string e = "";
                if (!baseTypeName.IsEmpty)
                    e = " extends " + baseTypeName.NameForEmit(style);
                writer.OpenLnF("class {0}{1} {{", Name.ShortName, e);
                style.CurrentClass = name; // do not move this before "class XXX" is emited
                for (int orderGroup = 0; orderGroup < 3; orderGroup++)
                    foreach (var field in fields.Where(_ => FieldOrderGroup(_) == orderGroup))
                        field.Emit(emiter, writer, style);
                foreach (var me in Methods)
                {
                    me.Emit(emiter, writer, style);
                }
                writer.CloseLn("}");
            }
            finally
            {
                style.CurrentClass = saveStyleCurrentClass;
                style.CurrentNamespace = saveStyleCurrentNamespace;
            }
        }

        public IEnumerable<ICodeRequest> GetCodeRequests()
        {
            var a = IPhpStatementBase.GetCodeRequests(Name, BaseTypeName);
            var b = IPhpStatementBase.GetCodeRequests<PhpClassFieldDefinition>(fields);
            var c = IPhpStatementBase.GetCodeRequests<PhpClassMethodDefinition>(methods);
            return a.Union(b).Union(c);
        }

        #endregion Methods

        #region Properties

        public bool IsEmpty
        {
            get
            {
                return methods.Count == 0 && fields.Count == 0;
            }
        }

        #endregion Properties
    }
}


// -----:::::##### smartClass embedded code begin #####:::::----- generated 2013-11-10 18:17
// File generated automatically ver 2013-07-10 08:43
// Smartclass.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0c4d5d36fb5eb4ac
namespace Lang.Php.Compiler.Source
{
    public partial class PhpClassDefinition
    {
        /*
        /// <summary>
        /// Tworzy instancję obiektu
        /// </summary>
        public PhpClassDefinition()
        {
        }
        Przykłady użycia
        implement INotifyPropertyChanged
        implement INotifyPropertyChanged_Passive
        implement ToString ##Name## ##BaseTypeName## ##Methods## ##Fields##
        implement ToString Name=##Name##, BaseTypeName=##BaseTypeName##, Methods=##Methods##, Fields=##Fields##
        implement equals Name, BaseTypeName, Methods, Fields
        implement equals *
        implement equals *, ~exclude1, ~exclude2
        */


        #region Constructors
        /// <summary>
        /// Tworzy instancję obiektu
        /// <param name="Name">Nazwa klasy</param>
        /// </summary>
        public PhpClassDefinition(PhpQualifiedName Name)
        {
            this.name = Name;
        }

        /// <summary>
        /// Tworzy instancję obiektu
        /// <param name="Name">Nazwa klasy</param>
        /// <param name="BaseTypeName">Nazwa klasy</param>
        /// </summary>
        public PhpClassDefinition(PhpQualifiedName Name, PhpQualifiedName BaseTypeName)
        {
            this.name = Name;
            this.baseTypeName = BaseTypeName;
        }

        #endregion Constructors


        #region Constants
        /// <summary>
        /// Nazwa własności Name; Nazwa klasy
        /// </summary>
        public const string PROPERTYNAME_NAME = "Name";
        /// <summary>
        /// Nazwa własności BaseTypeName; Nazwa klasy
        /// </summary>
        public const string PROPERTYNAME_BASETYPENAME = "BaseTypeName";
        /// <summary>
        /// Nazwa własności Methods; 
        /// </summary>
        public const string PROPERTYNAME_METHODS = "Methods";
        /// <summary>
        /// Nazwa własności Fields; 
        /// </summary>
        public const string PROPERTYNAME_FIELDS = "Fields";
        #endregion Constants


        #region Methods
        #endregion Methods


        #region Properties
        /// <summary>
        /// Nazwa klasy; własność jest tylko do odczytu.
        /// </summary>
        public PhpQualifiedName Name
        {
            get
            {
                return name;
            }
        }
        private PhpQualifiedName name;
        /// <summary>
        /// Nazwa klasy; własność jest tylko do odczytu.
        /// </summary>
        public PhpQualifiedName BaseTypeName
        {
            get
            {
                return baseTypeName;
            }
        }
        private PhpQualifiedName baseTypeName;
        /// <summary>
        /// 
        /// </summary>
        public List<PhpClassMethodDefinition> Methods
        {
            get
            {
                return methods;
            }
            set
            {
                methods = value;
            }
        }
        private List<PhpClassMethodDefinition> methods = new List<PhpClassMethodDefinition>();
        /// <summary>
        /// 
        /// </summary>
        public List<PhpClassFieldDefinition> Fields
        {
            get
            {
                return fields;
            }
            set
            {
                fields = value;
            }
        }
        private List<PhpClassFieldDefinition> fields = new List<PhpClassFieldDefinition>();
        #endregion Properties
    }
}
