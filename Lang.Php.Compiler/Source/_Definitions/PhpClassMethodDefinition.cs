using System.Linq;

namespace Lang.Php.Compiler.Source
{
    public class PhpClassMethodDefinition : PhpMethodDefinition, IPhpClassMember, IEmitable
    {
        public PhpClassMethodDefinition(string Name)
            : base(Name)
        {
        }

        // Public Methods 

        public override void Emit(PhpSourceCodeEmiter emiter, PhpSourceCodeWriter code, PhpEmitStyle style)
        {
            Refactor();
            base.Emit(emiter, code, style);
        }

        public void Refactor()
        {
            // return;
            var groups = GetCodeRequests()
                .OfType<LocalVariableRequest>()
                .Where(i => !string.IsNullOrEmpty(i.VariableName))
                .GroupBy(i => i.VariableName)
                .Select(i => i.ToArray())
                .ToArray();

            var ii = 0;
            foreach (var group in groups)
            {
                if (group.Where(i => i.IsArgument).Any())
                    continue;
                if (!group.First().VariableName.Contains("@"))
                    continue;
                string nn;
                if (ii < 26)
                    nn = ((char)(65 + ii)).ToString();
                else
                    nn = (char)(65 + ii / 26) + ((char)(65 + ii % 26)).ToString();

                nn =  "$" + nn;
                nn += "___" + group.First().VariableName.Replace("$", "").Replace("@", "_");

                // po prostu
                nn = group.First().VariableName.Replace("@", "__");
                foreach (var item in group)
                    item.ChangeNameAction(nn);
                ii++;
            }
        }
        // Protected Methods 

        protected override string GetAccessModifiers()
        {
            return PhpSourceCodeEmiter.GetAccessModifiers(this);
        }


        /// <summary>
        /// </summary>
        public bool IsStatic { get; set; }

        /// <summary>
        /// </summary>
        public Visibility Visibility { get; set; }
    }
}