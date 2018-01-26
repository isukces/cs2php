using System.Collections.Generic;

namespace Lang.Php.Compiler.Source
{
    public class PhpSwitchLabel : ICodeRelated
    {
        /// <summary>
        ///     Tworzy instancję obiektu
        /// </summary>
        public PhpSwitchLabel()
        {
        }

        /// <summary>
        ///     Tworzy instancję obiektu
        ///     <param name="value"></param>
        /// </summary>
        public PhpSwitchLabel(IPhpValue value)
        {
            Value = value;
        }
        // Public Methods 

        public IEnumerable<ICodeRequest> GetCodeRequests()
        {
            if (Value != null)
                return Value.GetCodeRequests();
            return new ICodeRequest[0];
        }

        public PhpSwitchLabel Simplify(IPhpSimplifier s, out bool wasChanged)
        {
            wasChanged = false;
            if (IsDefault)
                return this;
            var e1     = s.Simplify(Value);
            wasChanged = !PhpSourceBase.EqualCode(e1, Value);
            if (wasChanged)
                return new PhpSwitchLabel(e1);
            return this;
        }


        /// <summary>
        /// </summary>
        public IPhpValue Value { get; set; }

        /// <summary>
        /// </summary>
        public bool IsDefault { get; set; }
    }
}