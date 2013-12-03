using Lang.Cs.Compiler;
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
    
    property Items Dictionary<Type, List<NodeTranslatorBound>> 
    	init #
    smartClassEnd
    */

    /// <summary>
    /// Przechowuje kolekcje translatorów dla różnych typów gałęzi
    /// </summary>
    public partial class NodeTranslatorsContainer
    {
        public void Add(Type t, NodeTranslatorBound b)
        {
            List<NodeTranslatorBound> x;
            if (!items.TryGetValue(t, out x))
                items[t] = x = new List<NodeTranslatorBound>();
            x.Add(b);
        }

        public IPhpValue Translate<T>(IExternalTranslationContext ctx, T node) where T : IValue
        {
            List<NodeTranslatorBound> x;
            if (!items.TryGetValue(typeof(T), out x))
                return null;
            var hh = x.OrderBy(i => i.Priority).ToArray();
            foreach (var i in hh)
            {
                var y = i.Translate(ctx, node);
                if (y != null)
                    return y;
            }
            return null;
        }
    }
}


// -----:::::##### smartClass embedded code begin #####:::::----- generated 2013-11-12 22:46
// File generated automatically ver 2013-07-10 08:43
// Smartclass.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0c4d5d36fb5eb4ac
namespace Lang.Php.Compiler
{
    public partial class NodeTranslatorsContainer
    {
        /*
        /// <summary>
        /// Tworzy instancję obiektu
        /// </summary>
        public NodeTranslatorsContainer()
        {
        }

        Przykłady użycia

        implement INotifyPropertyChanged
        implement INotifyPropertyChanged_Passive
        implement ToString ##Items##
        implement ToString Items=##Items##
        implement equals Items
        implement equals *
        implement equals *, ~exclude1, ~exclude2
        */
        #region Constants
        /// <summary>
        /// Nazwa własności Items; 
        /// </summary>
        public const string PROPERTYNAME_ITEMS = "Items";
        #endregion Constants

        #region Methods
        #endregion Methods

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public Dictionary<Type, List<NodeTranslatorBound>> Items
        {
            get
            {
                return items;
            }
            set
            {
                items = value;
            }
        }
        private Dictionary<Type, List<NodeTranslatorBound>> items = new Dictionary<Type, List<NodeTranslatorBound>>();
        #endregion Properties

    }
}
