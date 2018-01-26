using System;
using System.Collections.Generic;

namespace Lang.Php.Compiler.Source
{
    public class PhpArrayCreateExpression : PhpValueBase, ICodeRelated
    {
        /// <summary>
        ///     Tworzy instancję obiektu
        ///     <param name="initializers">wartości inicjujące</param>
        /// </summary>
        public PhpArrayCreateExpression(params IPhpValue[] initializers)
        {
            Initializers = initializers;
        }


        /// <summary>
        ///     Tworzy instancję obiektu
        /// </summary>
        public PhpArrayCreateExpression()
        {
        }

        public static PhpArrayCreateExpression MakeKeyValue(params IPhpValue[] keyValues)
        {
            if (keyValues.Length % 2 == 1)
                throw new ArgumentException("key_values");
            var a = new List<IPhpValue>();
            for (var i = 1; i < keyValues.Length; i += 2)
            {
                if (keyValues[i] == null)
                    continue;
                var g = new PhpAssignExpression(keyValues[i - 1], keyValues[i]);
                a.Add(g);
            }

            return new PhpArrayCreateExpression(a.ToArray());
        }

        // Public Methods 

        public override IEnumerable<ICodeRequest> GetCodeRequests()
        {
            if (Initializers == null || Initializers.Length == 0)
                return new ICodeRequest[0];
            var l = new List<ICodeRequest>();
            foreach (var i in Initializers)
            {
                var ll = i.GetCodeRequests();
                l.AddRange(ll);
            }

            return l;
        }

        public override string GetPhpCode(PhpEmitStyle style)
        {
            if (Initializers == null || Initializers.Length == 0)
                return "array()";
            style         = style ?? new PhpEmitStyle();
            var przecinek = style.Compression == EmitStyleCompression.Beauty ? ", " : ",";
            var www       = style.Compression == EmitStyleCompression.Beauty ? " => " : "=>";

            var list = new List<string>();
            foreach (var initializeValue in Initializers)
                if (initializeValue is PhpAssignExpression)
                {
                    var assignExpression = initializeValue as PhpAssignExpression;
                    if (!string.IsNullOrEmpty(assignExpression.OptionalOperator))
                        throw new NotSupportedException();

                    if (assignExpression.Left is PhpArrayAccessExpression)
                    {
                        var left = assignExpression.Left as PhpArrayAccessExpression;
                        if (left.PhpArray is PhpThisExpression)
                        {
                            var o = left.Index + www + assignExpression.Right;
                            list.Add(o);
                        }
                        else
                        {
                            throw new NotSupportedException();
                        }
                    }
                    else if (assignExpression.Left is PhpInstanceFieldAccessExpression)
                    {
                        var l1 = assignExpression.Left as PhpInstanceFieldAccessExpression;
                        var fn = new PhpConstValue(l1.FieldName);
                        var o  = fn.GetPhpCode(style) + www + assignExpression.Right;
                        list.Add(o);
                    }
                    else
                    {
                        var o = assignExpression.Left.GetPhpCode(style) + www + assignExpression.Right;
                        list.Add(o);
                    }
                }
                else
                {
                    list.Add(initializeValue.GetPhpCode(style));
                }

            return "array(" + string.Join(przecinek, list) + ")";
        }


        /// <summary>
        ///     wartości inicjujące
        /// </summary>
        public IPhpValue[] Initializers { get; set; }
    }
}