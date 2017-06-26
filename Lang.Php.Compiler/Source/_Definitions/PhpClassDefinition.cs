using System;
using System.Collections.Generic;
using System.Linq;

namespace Lang.Php.Compiler.Source
{

    /*
    smartClass
    option NoAdditionalFile
    implement Constructor Name
    implement Constructor Name, BaseTypeName
    
    property Name PhpQualifiedName Nazwa klasy
    	read only
    
    property BaseTypeName PhpQualifiedName Nazwa klasy
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

        static int FieldOrderGroup(PhpClassFieldDefinition fieldDefinition)
        {
            return fieldDefinition.IsConst ? 0 : (fieldDefinition.IsStatic ? 1 : 2);
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
                if (style.CurrentNamespace != _name.Namespace)
                    throw new Exception("Unable to emit class into different namespace");
                var e = "";
                if (!_baseTypeName.IsEmpty)
                    e = " extends " + _baseTypeName.NameForEmit(style);
                writer.OpenLnF("class {0}{1} {{", Name.ShortName, e);
                style.CurrentClass = _name; // do not move this before "class XXX" is emited
                for (var orderGroup = 0; orderGroup < 3; orderGroup++)
                    foreach (var field in _fields.Where(_ => FieldOrderGroup(_) == orderGroup))
                    {
                        field.Emit(emiter, writer, style);
                    }
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
            var b = IPhpStatementBase.GetCodeRequests(_fields);
            var c = IPhpStatementBase.GetCodeRequests(_methods);
            return a.Union(b).Union(c);
        }

        #endregion Methods

        #region Properties

        public bool IsEmpty
        {
            get
            {
                return _methods.Count == 0 && _fields.Count == 0;
            }
        }

        #endregion Properties
    }
}


// -----:::::##### smartClass embedded code begin #####:::::----- generated 2014-09-26 09:51
// File generated automatically ver 2014-09-01 19:00
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
        /// <param name="name">Nazwa klasy</param>
        /// </summary>
        public PhpClassDefinition(PhpQualifiedName name)
        {
            _name = name;
        }

        /// <summary>
        /// Tworzy instancję obiektu
        /// <param name="name">Nazwa klasy</param>
        /// <param name="baseTypeName">Nazwa klasy</param>
        /// </summary>
        public PhpClassDefinition(PhpQualifiedName name, PhpQualifiedName baseTypeName)
        {
            _name = name;
            _baseTypeName = baseTypeName;
        }

        #endregion Constructors

        #region Constants
        /// <summary>
        /// Nazwa własności Name; Nazwa klasy
        /// </summary>
        public const string PropertyNameName = "Name";
        /// <summary>
        /// Nazwa własności BaseTypeName; Nazwa klasy
        /// </summary>
        public const string PropertyNameBaseTypeName = "BaseTypeName";
        /// <summary>
        /// Nazwa własności Methods; 
        /// </summary>
        public const string PropertyNameMethods = "Methods";
        /// <summary>
        /// Nazwa własności Fields; 
        /// </summary>
        public const string PropertyNameFields = "Fields";
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
                return _name;
            }
        }
        private PhpQualifiedName _name;
        /// <summary>
        /// Nazwa klasy; własność jest tylko do odczytu.
        /// </summary>
        public PhpQualifiedName BaseTypeName
        {
            get
            {
                return _baseTypeName;
            }
        }
        private PhpQualifiedName _baseTypeName;
        /// <summary>
        /// 
        /// </summary>
        public List<PhpClassMethodDefinition> Methods
        {
            get
            {
                return _methods;
            }
            set
            {
                _methods = value;
            }
        }
        private List<PhpClassMethodDefinition> _methods = new List<PhpClassMethodDefinition>();
        /// <summary>
        /// 
        /// </summary>
        public List<PhpClassFieldDefinition> Fields
        {
            get
            {
                return _fields;
            }
            set
            {
                _fields = value;
            }
        }
        private List<PhpClassFieldDefinition> _fields = new List<PhpClassFieldDefinition>();
        #endregion Properties

    }
}
