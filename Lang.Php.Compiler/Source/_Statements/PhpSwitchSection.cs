using System.Collections.Generic;

namespace Lang.Php.Compiler.Source
{
    public class PhpSwitchSection : ICodeRelated
    {
        public IEnumerable<ICodeRequest> GetCodeRequests()
        {
            var result = new List<ICodeRequest>();
            if (Labels != null)
                foreach (var _label in Labels)
                    result.AddRange(_label.GetCodeRequests());
            if (Statement != null)
                result.AddRange(Statement.GetCodeRequests());
            return result;
        }

        public PhpSwitchSection Simplify(IPhpSimplifier s, out bool wasChanged)
        {
            wasChanged  = false;
            var nLabels = new List<PhpSwitchLabel>();
            foreach (var lab in Labels)
            {
                bool labelWasChanged;
                nLabels.Add(lab.Simplify(s, out labelWasChanged));
                if (labelWasChanged) wasChanged = true;
            }

            var nStatement = s.Simplify(Statement);
            if (!PhpSourceBase.EqualCode(nStatement, Statement))
                wasChanged = true;
            if (!wasChanged)
                return this;
            return new PhpSwitchSection
            {
                Labels    = nLabels.ToArray(),
                Statement = nStatement
            };
        }

        /// <summary>
        /// </summary>
        public PhpSwitchLabel[] Labels { get; set; }

        /// <summary>
        /// </summary>
        public IPhpStatement Statement { get; set; }
    }
}