using System;

namespace Lang.Php
{
    /// <summary>
    ///     Sugeruje zastąpienie (np. własności instancyjnej) operatorem (np.===)
    /// </summary>
    public class UseBinaryExpressionAttribute : Attribute
    {
        /// <summary>
        ///     Tworzy instancję obiektu
        ///     <param name="_operator"></param>
        ///     <param name="left"></param>
        ///     <param name="right"></param>
        /// </summary>
        public UseBinaryExpressionAttribute(string _operator, string left, string right)
        {
            Operator = _operator;
            Left     = left;
            Right    = right;
        }

        /// <summary>
        ///     Własność jest tylko do odczytu.
        /// </summary>
        public string Operator { get; } = string.Empty;

        /// <summary>
        ///     Własność jest tylko do odczytu.
        /// </summary>
        public string Left { get; } = string.Empty;

        /// <summary>
        ///     Własność jest tylko do odczytu.
        /// </summary>
        public string Right { get; } = string.Empty;
    }
}