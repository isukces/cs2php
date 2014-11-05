using Lang.Cs.Compiler;
using System;
using System.Collections.Generic;
using System.Linq;

// ReSharper disable once CheckNamespace
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
    public class NodeTranslatorsContainer
    {
        #region Methods

        // Public Methods 

        public void Add(Type t, NodeTranslatorBound b)
        {
            List<NodeTranslatorBound> x;
            if (!_items.TryGetValue(t, out x))
                _items[t] = x = new List<NodeTranslatorBound>();
            x.Add(b);
        }

        public IPhpValue Translate<T>(IExternalTranslationContext ctx, T node) where T : IValue
        {
            List<NodeTranslatorBound> x;
            if (!_items.TryGetValue(typeof(T), out x))
                return null;
            var hh = x.OrderBy(GetNodeTranslatorBoundPriority).ToArray();
            // ReSharper disable once LoopCanBeConvertedToQuery
            foreach (var i in hh)
            {
                var y = i.Translate(ctx, node);
                if (y != null)
                    return y;
            }
            return null;
        }

        private static int? GetNodeTranslatorBoundPriority(NodeTranslatorBound i)
        {
            // use this named method insead of lambda for performace reasons
            return i.Priority;
        }

        #endregion Methods

        #region Fields

        private readonly Dictionary<Type, List<NodeTranslatorBound>> _items = new Dictionary<Type, List<NodeTranslatorBound>>();

        #endregion Fields
    }
}

