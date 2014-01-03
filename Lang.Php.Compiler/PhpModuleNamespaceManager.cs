using Lang.Php.Compiler.Source;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lang.Php.Compiler
{

    /*
    smartClass
    option NoAdditionalFile
    
    property OnlyOneRootStatement bool 
    	read only container.Count == 1 && container[0].Name.IsRoot
    
    property Container List<Item> 
    	init #
    smartClassEnd
    */

    public partial class PhpModuleNamespaceManager
    {
        #region Static Methods

        // Public Methods 


        // Private Methods 

        private static Item GetItemForNamespace(List<Item> list, PhpNamespace name)
        {
            Item item = list.Any() ? list.Last() : null;
            if (item == null || item.Name != name)
            {
                item = new Item(name);
                list.Add(item);
            }
            return item;
        }

        #endregion Static Methods

        #region Methods

        // Public Methods 

        public void Add(IEnumerable<IPhpStatement> statements)
        {

            foreach (var statement in statements)
                Add(statement);

        }

        public void Add(IEmitable statement)
        {
            if (statement is IPhpStatement && !PhpCodeBlock.HasAny(statement as IPhpStatement))
                return;
            if (statement is PhpNamespaceStatement)
            {
                var tmp = statement as PhpNamespaceStatement;
                Item item = GetItemForNamespace(container, tmp.Name);
                item.Items.AddRange(tmp.Code.Statements);
            }
            else if (statement is PhpClassDefinition)
            {
                var tmp = statement as PhpClassDefinition;
                if (tmp.IsEmpty)
                    return;
                Item item = GetItemForNamespace(container, tmp.Name.Namespace);
                item.Items.Add(statement);
            }
            else
            {
                Item item = GetItemForNamespace(container, PhpNamespace.Root);
                item.Items.Add(statement);
            }
        }



        #endregion Methods

        #region Nested Classes


        public class Item
        {
            #region Constructors

            public Item(PhpNamespace Name)
            {
                this.Name = Name;
                this.Items = new List<IEmitable>(); ;
            }

            #endregion Constructors

            #region Properties

            public List<IEmitable> Items { get; private set; }

            public PhpNamespace Name { get; private set; }

            #endregion Properties
        }
        #endregion Nested Classes
    }
}


// -----:::::##### smartClass embedded code begin #####:::::----- generated 2014-01-03 12:11
// File generated automatically ver 2013-07-10 08:43
// Smartclass.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0c4d5d36fb5eb4ac
namespace Lang.Php.Compiler
{
    public partial class PhpModuleNamespaceManager
    {
        /*
        /// <summary>
        /// Tworzy instancję obiektu
        /// </summary>
        public PhpModuleNamespaceManager()
        {
        }

        Przykłady użycia

        implement INotifyPropertyChanged
        implement INotifyPropertyChanged_Passive
        implement ToString ##OnlyOneRootStatement## ##Container##
        implement ToString OnlyOneRootStatement=##OnlyOneRootStatement##, Container=##Container##
        implement equals OnlyOneRootStatement, Container
        implement equals *
        implement equals *, ~exclude1, ~exclude2
        */
        #region Constants
        /// <summary>
        /// Nazwa własności OnlyOneRootStatement; 
        /// </summary>
        public const string PROPERTYNAME_ONLYONEROOTSTATEMENT = "OnlyOneRootStatement";
        /// <summary>
        /// Nazwa własności Container; 
        /// </summary>
        public const string PROPERTYNAME_CONTAINER = "Container";
        #endregion Constants

        #region Methods
        #endregion Methods

        #region Properties
        /// <summary>
        /// Własność jest tylko do odczytu.
        /// </summary>
        public bool OnlyOneRootStatement
        {
            get
            {
                return container.Count == 1 && container[0].Name.IsRoot;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public List<Item> Container
        {
            get
            {
                return container;
            }
            set
            {
                container = value;
            }
        }
        private List<Item> container = new List<Item>();
        #endregion Properties

    }
}
