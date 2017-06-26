namespace Lang.Php
{
    public struct PregMatchResult
    {
        #region Fields

        private readonly int _value;

        #endregion Fields

        #region Properties


        internal static PregMatchResult Error
        {
            get { return new PregMatchResult(-1); }
        }

        internal static PregMatchResult Fail
        {
            get { return new PregMatchResult(0); }
        }


        [UseBinaryExpression("===", "this", "false")]
        public bool IsError
        {
            get { return _value == -1; }
        }

        [UseBinaryExpression("==", "this", "0")]
        public bool IsFail
        {
            get { return _value == 0; }
        }

        [UseBinaryExpression("==", "this", "1")]
        public bool IsSuccess
        {
            get { return _value == 1; }
        }

        internal static PregMatchResult Success
        {
            get { return new PregMatchResult(1); }
        }

        #endregion Properties

        #region Methods

        private PregMatchResult(int value)
        {
            _value = value;
        }

        #endregion Methods
    }
}
