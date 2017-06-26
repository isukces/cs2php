using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Lang.Cs.Compiler
{
    partial class CompilationUnit
    {
        public string[] GetClasses()
        {
            var q = from ns in namespaceDeclarations
                    from cd in ns.Members.OfType<ClassDeclaration>()
                    select ns.Name + "." + cd.Name;
            return q.ToArray();
        }

    }

    sealed partial class ConstValue
    {
        public override string ToString()
        {
            if (myValue == null)
                return "null";
            if (myValue is string)
                return "\"" + myValue + "\"";
            return myValue.ToString();
        }
        Type IValue.ValueType
        {
            get
            {

                if (myValue == null)
                    return null;
                return  myValue.GetType();
            }
        }
    }
    sealed partial class FunctionArgument
    {
        public override string ToString()
        {
            return string.Format("{0} {1}", refOrOutKeyword, myValue).Trim();
        }
        Type IValue.ValueType
        {
            get { throw new NotImplementedException(); }
        }
    }
    partial class LangType
    {
        public override string ToString()
        {
            return (object)dotnetType == null ? "var or unknown" : dotnetType.FullName;
        }
    }

    sealed partial class InvocationExpression
    {
        public override string ToString()
        {
            return string.Format("{0}({1})", expression, string.Join(", ", arguments.Select(i => i.ToString())));
        }
        Type IValue.ValueType
        {
            get { throw new NotImplementedException(); }
        }
    }

    sealed partial class MemberAccessExpression
    {
        public override string ToString()
        {
            return string.Format("{0}.{1}", Expression, memberName);
        }

        Type IValue.ValueType
        {
            get
            {
                return TypesUtil.GetMemberType(expression, memberName);
            }
        }
    }

    partial class ClassFieldAccessExpression
    {
        public override string ToString()
        {
            return member.ToString();
        }

        Type IValue.ValueType
        {
            get { return  member.FieldType; }
        }
    }

    partial class ClassPropertyAccessExpression
    {
        public override string ToString()
        {
            return member.ToString();
        }

        Type IValue.ValueType
        {
            get
            {
                return member.PropertyType;
            }
        }
    }
    partial class InstanceFieldAccessExpression
    {
        public override string ToString()
        {
            return string.Format("{0}.{1}", targetObject, member.Name);
        }

        Type IValue.ValueType
        {
            get
            {
                return member.FieldType;
            }
        }
    }


    partial class StaticMemberAccessExpression
    {
        Type IValue.ValueType
        {
            get
            {
                return TypesUtil.GetMemberType(expression, memberName, true);
            }
        }
    }
    partial class ArgumentExpression
    {
        Type IValue.ValueType
        {
            get
            {
                return type.DotnetType;
            }
        }
        public override string ToString()
        {
            return name;
        }
    }

    partial class SimpleLambdaExpression
    {
        Type IValue.ValueType
        {
            get { throw new NotImplementedException(); }
        }
    }
    partial class BinaryOperatorExpression
    {
        Type IValue.ValueType
        {
            get
            {
                if ((object)forceType != null)
                    return forceType;
                throw new NotSupportedException();
            }
        }
        public override string ToString()
        {
            return string.Format("{0} {1} {2}", left, _operator, right);
        }
    }

    partial class LocalVariableExpression
    {
        Type IValue.ValueType
        {
            get { return type.DotnetType; }
        }
        public override string ToString()
        {
            return name;
        }
    }

    partial class CallConstructor
    {
        Type IValue.ValueType
        {
            get
            {
                return info.ReflectedType;
            }
        }
    }
    partial class Modifiers
    {
        public bool Has(string x)
        {
            return items.Where(u => u == x).Any();
        }
        public override string ToString()
        {
            return string.Join(" ", items);
        }
    }
    partial class TypeValue : IValue
    {

        Type IValue.ValueType
        {
            get
            {
                return dotnetType;
            }
        }
    }
    partial class TypeOfExpression
    {
        Type IValue.ValueType
        {
            get
            {
                return typeof(Type);
            }
        }
    }

    sealed partial class CsharpMethodCallExpression
    {
        Type IValue.ValueType
        {
            get
            {
                var rt = methodInfo.ReturnType;
                if (!methodInfo.IsGenericMethod)
                    return rt;
                bool isArray = rt.IsArray;
                int rank = 0;
                if (isArray)
                {
                    rank = rt.GetArrayRank();
                    rt = rt.GetElementType();
                }
                var b = methodInfo.GetGenericArguments();
                for (int i = 0; i < b.Length; i++)
                {
                    if (rt == b[i])
                    {
                        rt = genericTypes[i];
                        if (!isArray)
                            return rt;
                        return rt.MakeArrayType(rank);
                    }
                }
                throw new NotSupportedException();
            }
        }
        public override string ToString()
        {
            return string.Format("{0}.{1}({2})", methodInfo.DeclaringType.FullName, methodInfo.Name, string.Join(", ", arguments.Select(i => i.ToString())));
        }
    }

    partial class VariableDeclarator
    {
        public override string ToString()
        {
            return string.Format("{0} = {1}", name, _value);
        }
    }

    partial class VariableDeclaration
    {
        public override string ToString()
        {
            return string.Format("{0} {1};", type, string.Join(", ", declarators.Select(i => i.ToString())));
        }
    }

    partial class InstanceMemberAccessExpression
    {
        Type IValue.ValueType
        {
            get
            {
                throw new NotImplementedException();
            }
        }
    }

    partial class UnknownIdentifierValue
    {
        Type IValue.ValueType
        {
            get
            {
                throw new NotImplementedException();
            }
        }
        public override string ToString()
        {
            if (optionalGenericTypes == null || optionalGenericTypes.Length == 0)
                return identifier;
            return string.Format("{0}<{1}>", identifier, string.Join(",", optionalGenericTypes as IEnumerable<IValue>));
        }
    }

    partial class CsharpAssignExpression : IValue, IStatement
    {

        Type IValue.ValueType
        {
            get { return left.ValueType; }
        }
        public override string ToString()
        {
            return string.Format("{0} {1}= {2}", left, optionalOperator, right);
        }
    }


    partial class ParenthesizedExpression : IValue
    {

        Type IValue.ValueType
        {
            get { return expression.ValueType; }
        }
        public override string ToString()
        {
            return string.Format("({0})", expression);
        }
    }

    partial class IncrementDecrementExpression : IValue, IStatement
    {

        Type IValue.ValueType
        {
            get { return operand.ValueType; }
        }
    }
    partial class ElementAccessExpression : IValue
    {

        Type IValue.ValueType
        {
            get { return elementType; }
        }
    }
    partial class UnaryOperatorExpression : IValue
    {

        Type IValue.ValueType
        {
            get
            {
                Debug.Assert((object)forceType != null);
                return forceType;
            }
        }
    }

    partial class ThisExpression : IValue
    {

        Type IValue.ValueType
        {
            get { return objectType; }
        }
        public override string ToString()
        {
            return "this";
        }
    }

    partial class ConditionalExpression : IValue
    {

        Type IValue.ValueType
        {
            get { return resultType; }
        }
        public override string ToString()
        {
            return string.Format("{0} ? {1} : {2}", condition, whenTrue, whenFalse);
        }
    }
    public partial class CastExpression : IValue
    {

        public Type ValueType
        {
            get
            {
                return dotnetType;

            }
        }
    }


    public partial class IValueTable_PseudoValue : IValue
    {

        public Type ValueType
        {
            get { throw new NotImplementedException(); }
        }
    }
    public partial class IValueTable2_PseudoValue : IValue
    {

        public Type ValueType
        {
            get { throw new NotImplementedException(); }
        }
    }

    public partial class MethodExpression : IValue
    {

        public Type ValueType
        {
            get
            {
                throw new Exception();
                // return method.GetType();
            }
        }
    }
    public partial class ArrayCreateExpression : IValue
    {

        public Type ValueType
        {
            get { return arrayType; }
        }
    }
    public partial class LambdaExpression : IValue
    {

        public Type ValueType
        {
            get { return dotnetType; }
        }
        public override string ToString()
        {
            return string.Format("Lambda: ({0})", string.Join(", ", parameters.Select(i => i.ToString())));
        }
    }
    public partial class FunctionDeclarationParameter
    {
        public override string ToString()
        {
            return string.Format("{0} {1}", modifiers, name).Trim();
        }
    }

    public partial class CsharpInstancePropertyAccessExpression : IValue
    {
        public Type ValueType
        {
            get
            {
                return member.PropertyType;
            }
        }
        public override string ToString()
        {
            return targetObject.ToString() + "." + member.Name;
        }
    }

    public partial class CsharpSwichLabel : IValue
    {

        public Type ValueType
        {
            get
            {
                throw new NotSupportedException();
            }
        }
        public override string ToString()
        {
            if (isDefault)
                return "default";
            return expression.ToString();
        }
    }
}
