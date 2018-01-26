using System;
using System.Collections.Generic;
using System.Linq;

namespace Lang.Php.Compiler.Source
{
    public class PhpClassDefinition : ICodeRelated, IEmitable
    {
        /// <summary>
        ///     Tworzy instancję obiektu
        ///     <param name="name">Nazwa klasy</param>
        /// </summary>
        public PhpClassDefinition(PhpQualifiedName name)
        {
            Name = name;
        }

        /// <summary>
        ///     Tworzy instancję obiektu
        ///     <param name="name">Nazwa klasy</param>
        ///     <param name="baseTypeName">Nazwa klasy</param>
        /// </summary>
        public PhpClassDefinition(PhpQualifiedName name, PhpQualifiedName baseTypeName)
        {
            Name          = name;
            _baseTypeName = baseTypeName;
        }
        // Private Methods 

        private static int FieldOrderGroup(PhpClassFieldDefinition fieldDefinition)
        {
            return fieldDefinition.IsConst ? 0 : (fieldDefinition.IsStatic ? 1 : 2);
        }

        // Public Methods 

        public void Emit(PhpSourceCodeEmiter emiter, PhpSourceCodeWriter writer, PhpEmitStyle style)
        {
            var saveStyleCurrentClass     = style.CurrentClass;
            var saveStyleCurrentNamespace = style.CurrentNamespace;
            try
            {
                if (IsEmpty)
                    return;
                if (style.CurrentNamespace == null)
                    style.CurrentNamespace = PhpNamespace.Root;
                if (style.CurrentNamespace != Name.Namespace)
                    throw new Exception("Unable to emit class into different namespace");
                var e = "";
                if (!_baseTypeName.IsEmpty)
                    e = " extends " + _baseTypeName.NameForEmit(style);
                writer.OpenLnF("class {0}{1} {{", Name.ShortName, e);
                style.CurrentClass = Name; // do not move this before "class XXX" is emited
                for (var orderGroup = 0; orderGroup < 3; orderGroup++)
                    foreach (var field in Fields.Where(_ => FieldOrderGroup(_) == orderGroup))
                        field.Emit(emiter, writer, style);
                foreach (var me in Methods) me.Emit(emiter, writer, style);
                writer.CloseLn("}");
            }
            finally
            {
                style.CurrentClass     = saveStyleCurrentClass;
                style.CurrentNamespace = saveStyleCurrentNamespace;
            }
        }

        public IEnumerable<ICodeRequest> GetCodeRequests()
        {
            var a = PhpStatementBase.GetCodeRequests(Name, BaseTypeName);
            var b = PhpStatementBase.GetCodeRequests(Fields);
            var c = PhpStatementBase.GetCodeRequests(Methods);
            return a.Union(b).Union(c);
        }

        public bool IsEmpty => Methods.Count == 0 && Fields.Count == 0;

        /// <summary>
        ///     Nazwa klasy; własność jest tylko do odczytu.
        /// </summary>
        public PhpQualifiedName Name { get; }

        /// <summary>
        ///     Nazwa klasy; własność jest tylko do odczytu.
        /// </summary>
        public PhpQualifiedName BaseTypeName => _baseTypeName;

        /// <summary>
        /// </summary>
        public List<PhpClassMethodDefinition> Methods { get; set; } = new List<PhpClassMethodDefinition>();

        /// <summary>
        /// </summary>
        public List<PhpClassFieldDefinition> Fields { get; set; } = new List<PhpClassFieldDefinition>();

        private PhpQualifiedName _baseTypeName;
    }
}