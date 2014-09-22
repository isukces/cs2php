// ReSharper disable once CheckNamespace
namespace Lang.Php
{
    [Skip]
    public sealed class UnixTimestamp
    {
        #region Methods

        // Public Methods 

        [DirectCall(null, "this")]
        public override string ToString()
        {
            return base.ToString();
        }

        [DirectCall("date", "0,this")]
        public string ToString(string format)
        {
            return base.ToString();
        }

        #endregion Methods
    }
}
