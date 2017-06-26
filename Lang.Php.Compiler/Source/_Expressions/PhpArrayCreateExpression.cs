using System;
using System.Collections.Generic;

namespace Lang.Php.Compiler.Source
{

    /*
    smartClass
    option NoAdditionalFile
    implement Constructor
    
    property Initializers IPhpValue[] wartości inicjujące
    smartClassEnd
    */

    public partial class PhpArrayCreateExpression : IPhpValueBase, ICodeRelated
    {

        public static PhpArrayCreateExpression MakeKeyValue(params IPhpValue[] key_values)
        {
            if (key_values.Length % 2 == 1)
                throw new ArgumentException("key_values");
            var a = new List<IPhpValue>();
            for (var i = 1; i < key_values.Length; i += 2)
            {
                if (key_values[i] == null)
                    continue;
                var g = new PhpAssignExpression(key_values[i - 1], key_values[i]);
                a.Add(g);
            }
            return new PhpArrayCreateExpression(a.ToArray());
        }

        #region Constructors

        /// <summary>
        /// Tworzy instancję obiektu
        /// <param name="Initializers">wartości inicjujące</param>
        /// </summary>
        public PhpArrayCreateExpression(params IPhpValue[] Initializers)
        {
            this.Initializers = Initializers;
        }

        #endregion Constructors

        #region Methods

        // Public Methods 

        public override IEnumerable<ICodeRequest> GetCodeRequests()
        {
            if (initializers == null || initializers.Length == 0)
                return new ICodeRequest[0];
            var l = new List<ICodeRequest>();
            foreach (var i in initializers)
            {
                var ll = i.GetCodeRequests();
                l.AddRange(ll);
            }
            return l;
        }

        public override string GetPhpCode(PhpEmitStyle style)
        {
            if (initializers == null || initializers.Length == 0)
                return "array()";
            style = style ?? new PhpEmitStyle();
            var przecinek = style.Compression == EmitStyleCompression.Beauty ? ", " : ",";
            var www = style.Compression == EmitStyleCompression.Beauty ? " => " : "=>";


            var list = new List<string>();
            foreach (var initializeValue in initializers)
            {
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
                            continue;
                        }
                        else
                            throw new NotSupportedException();
                    }
                    else if (assignExpression.Left is PhpInstanceFieldAccessExpression)
                    {
                        var l1 = assignExpression.Left as PhpInstanceFieldAccessExpression;
                        var fn = new PhpConstValue(l1.FieldName);
                        var o = fn.GetPhpCode(style) + www + assignExpression.Right;
                        list.Add(o);
                        continue;
                    }
                    else
                    {
                        var o = assignExpression.Left.GetPhpCode(style) + www + assignExpression.Right;
                        list.Add(o);
                        continue;
                    }
                }
                else
                    list.Add(initializeValue.GetPhpCode(style));
            }
            return "array(" + string.Join(przecinek, list) + ")";
        }

        #endregion Methods
    }
}


// -----:::::##### smartClass embedded code begin #####:::::----- generated 2013-11-22 15:18
// File generated automatically ver 2013-07-10 08:43
// Smartclass.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0c4d5d36fb5eb4ac
namespace Lang.Php.Compiler.Source
{
    public partial class PhpArrayCreateExpression
    {
        /*
        /// <summary>
        /// Tworzy instancję obiektu
        /// </summary>
        public PhpArrayCreateExpression()
        {
        }

        Przykłady użycia

        implement INotifyPropertyChanged
        implement INotifyPropertyChanged_Passive
        implement ToString ##Initializers##
        implement ToString Initializers=##Initializers##
        implement equals Initializers
        implement equals *
        implement equals *, ~exclude1, ~exclude2
        */
        #region Constructors
        /// <summary>
        /// Tworzy instancję obiektu
        /// </summary>
        public PhpArrayCreateExpression()
        {
        }

        #endregion Constructors

        #region Constants
        /// <summary>
        /// Nazwa własności Initializers; wartości inicjujące
        /// </summary>
        public const string PROPERTYNAME_INITIALIZERS = "Initializers";
        #endregion Constants

        #region Methods
        #endregion Methods

        #region Properties
        /// <summary>
        /// wartości inicjujące
        /// </summary>
        public IPhpValue[] Initializers
        {
            get
            {
                return initializers;
            }
            set
            {
                initializers = value;
            }
        }
        private IPhpValue[] initializers;
        #endregion Properties

    }
}
