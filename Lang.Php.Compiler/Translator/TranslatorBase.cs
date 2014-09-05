using System;
using System.Reflection;

namespace Lang.Php.Compiler.Translator
{
    public abstract class TranslatorBase
    {

        public static string GetCompareName(MemberInfo mi)
        {
            if (mi is PropertyInfo || mi is FieldInfo)
                return GetCompareName(mi.DeclaringType) + "::" + mi.Name;
            if (mi is MethodInfo)
            {
                var mii = mi as MethodInfo;
                if (mii.IsGenericMethod && !mii.IsGenericMethodDefinition)
                    mii = mii.GetGenericMethodDefinition();
                return GetCompareName(mi.DeclaringType) + "::" + mii;
            }
            throw new NotSupportedException();
        }

        private static string GetCompareName(Type t)
        {
            t = GetGenericTypeDefinition(t);
            return t.FullName;
        }

        public static Type GetGenericTypeDefinition(Type t)
        {
            t = t.IsGenericType && !t.IsGenericTypeDefinition ? t.GetGenericTypeDefinition() : t;
            return t;
        }


        #region Constructors

        public TranslatorBase(TranslationState state)
        {
            if (state == null)
                throw new ArgumentNullException("state");
            this.state = state;
        }

        #endregion Constructors

        #region Methods

        // Protected Methods 
        protected ClassReplaceInfo GetClassReplacer(Type srcType)
        {
            var a = state.FindOneClassReplacer(srcType);
            if (a == null)
                throw new Exception(string.Format("wise gecko, Unable to find replacer for {0}", srcType.FullName));
            return a;
        }


        #endregion Methods

        #region Fields

        protected TranslationState state;

        #endregion Fields
    }
}
