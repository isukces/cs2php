using System.Collections.Generic;
using System.Linq;
using Lang.Php.Compiler.Source;

namespace Lang.Php.Compiler
{
    public class PhpModuleNamespaceManager
    {
        // Public Methods 


        // Private Methods 

        private static Item GetItemForNamespace(List<Item> list, PhpNamespace name)
        {
            var item = list.Any() ? list.Last() : null;
            if (item == null || item.Name != name)
            {
                item = new Item(name);
                list.Add(item);
            }

            return item;
        }

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
                var tmp  = statement as PhpNamespaceStatement;
                var item = GetItemForNamespace(Container, tmp.Name);
                item.Items.AddRange(tmp.Code.Statements);
            }
            else if (statement is PhpClassDefinition)
            {
                var tmp = statement as PhpClassDefinition;
                if (tmp.IsEmpty)
                    return;
                var item = GetItemForNamespace(Container, tmp.Name.Namespace);
                item.Items.Add(statement);
            }
            else
            {
                var item = GetItemForNamespace(Container, PhpNamespace.Root);
                item.Items.Add(statement);
            }
        }

        /// <summary>
        ///     Własność jest tylko do odczytu.
        /// </summary>
        public bool OnlyOneRootStatement => Container.Count == 1 && Container[0].Name.IsRoot;

        /// <summary>
        /// </summary>
        public List<Item> Container { get; set; } = new List<Item>();


        public class Item
        {
            public Item(PhpNamespace Name)
            {
                this.Name = Name;
                Items     = new List<IEmitable>();
                ;
            }

            public List<IEmitable> Items { get; private set; }

            public PhpNamespace Name { get; private set; }
        }
    }
}