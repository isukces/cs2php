
using System;
using System.Reflection;
using Lang.Cs.Compiler.Sandbox;
    
namespace Lang.Cs.Compiler
{
    public interface IStatement {
    } // end of IStatement
    public interface IClassMember {
    } // end of IClassMember
    public interface IValue {
      Type ValueType { get; }
    } // end of IValue
    public interface INamespaceMember {
    } // end of INamespaceMember
    public sealed partial class NameType : CSharpBase {
      public NameType(string Name, LangType Type){
        this.name = Name;
        this.type = Type;
      }
      public string Name {
        get {
          return name;
        }
      }
      private string name;
      public LangType Type {
        get {
          return type;
        }
      }
      private LangType type;
    } // end of NameType
    public sealed partial class CompilationUnit : CSharpBase {
      public CompilationUnit(ImportNamespace[] ImportNamespaces, NamespaceDeclaration[] NamespaceDeclarations){
        this.importNamespaces = ImportNamespaces;
        this.namespaceDeclarations = NamespaceDeclarations;
      }
      public ImportNamespace[] ImportNamespaces {
        get {
          return importNamespaces;
        }
      }
      private ImportNamespace[] importNamespaces;
      public NamespaceDeclaration[] NamespaceDeclarations {
        get {
          return namespaceDeclarations;
        }
      }
      private NamespaceDeclaration[] namespaceDeclarations;
    } // end of CompilationUnit
    public sealed partial class ImportNamespaceCollection : CSharpBase, IStatement {
      public ImportNamespaceCollection(ImportNamespace[] Items){
        this.items = Items;
      }
      /// <summary>
      /// Lista definicji
      /// </summary>
      public ImportNamespace[] Items {
        get {
          return items;
        }
      }
      private ImportNamespace[] items;
    } // end of ImportNamespaceCollection
    public sealed partial class ImportNamespace : CSharpBase, IStatement {
      public ImportNamespace(string Name, string Alias){
        this.name = Name;
        this.alias = Alias;
      }
      /// <summary>
      /// Nazwa przestrzeni nazw
      /// </summary>
      public string Name {
        get {
          return name;
        }
      }
      private string name;
      /// <summary>
      /// Opcjonalny alias dla przestrzeni nazw lub typu
      /// </summary>
      public string Alias {
        get {
          return alias;
        }
      }
      private string alias;
    } // end of ImportNamespace
    public sealed partial class NamespaceDeclaration : CSharpBase {
      public NamespaceDeclaration(string Name, INamespaceMember[] Members){
        this.name = Name;
        this.members = Members;
      }
      /// <summary>
      /// Nazwa przestrzeni
      /// </summary>
      public string Name {
        get {
          return name;
        }
      }
      private string name;
      /// <summary>
      /// Elementy
      /// </summary>
      public INamespaceMember[] Members {
        get {
          return members;
        }
      }
      private INamespaceMember[] members;
    } // end of NamespaceDeclaration
    public sealed partial class ClassDeclaration : CSharpBase, INamespaceMember,IClassMember {
      public ClassDeclaration(string Name, IClassMember[] Members){
        this.name = Name;
        this.members = Members;
      }
      /// <summary>
      /// Nazwa klasy
      /// </summary>
      public string Name {
        get {
          return name;
        }
      }
      private string name;
      /// <summary>
      /// Elementy
      /// </summary>
      public IClassMember[] Members {
        get {
          return members;
        }
      }
      private IClassMember[] members;
    } // end of ClassDeclaration
    public sealed partial class InterfaceDeclaration : CSharpBase, INamespaceMember,IClassMember {
      public InterfaceDeclaration(string Name, IClassMember[] Members){
        this.name = Name;
        this.members = Members;
      }
      /// <summary>
      /// Nazwa interfejsu
      /// </summary>
      public string Name {
        get {
          return name;
        }
      }
      private string name;
      /// <summary>
      /// Elementy
      /// </summary>
      public IClassMember[] Members {
        get {
          return members;
        }
      }
      private IClassMember[] members;
    } // end of InterfaceDeclaration
    public sealed partial class FieldDeclaration : CSharpBase, IClassMember {
      public FieldDeclaration(LangType Type, VariableDeclarator[] Items, Modifiers Modifiers){
        this.type = Type;
        this.items = Items;
        this.modifiers = Modifiers;
      }
      public LangType Type {
        get {
          return type;
        }
      }
      private LangType type;
      public VariableDeclarator[] Items {
        get {
          return items;
        }
      }
      private VariableDeclarator[] items;
      public Modifiers Modifiers {
        get {
          return modifiers;
        }
      }
      private Modifiers modifiers;
    } // end of FieldDeclaration
    public sealed partial class VariableDeclarator : CSharpBase {
      public VariableDeclarator(string Name, IValue Value, FieldInfo OptionalFieldInfo){
        this.name = Name;
        this._value = Value;
        this.optionalFieldInfo = OptionalFieldInfo;
      }
      public string Name {
        get {
          return name;
        }
      }
      private string name;
      public IValue Value {
        get {
          return _value;
        }
      }
      private IValue _value;
      public FieldInfo OptionalFieldInfo {
        get {
          return optionalFieldInfo;
        }
      }
      private FieldInfo optionalFieldInfo;
    } // end of VariableDeclarator
    public sealed partial class CsharpPropertyDeclaration : CSharpBase, IClassMember {
      public CsharpPropertyDeclaration(string PropertyName, LangType Type, CsharpPropertyDeclarationAccessor[] Accessors, Modifiers Modifiers, DeclarationItemDescription Description){
        this.propertyName = PropertyName;
        this.type = Type;
        this.accessors = Accessors;
        this.modifiers = Modifiers;
        this.description = Description;
      }
      public string PropertyName {
        get {
          return propertyName;
        }
      }
      private string propertyName;
      public LangType Type {
        get {
          return type;
        }
      }
      private LangType type;
      public CsharpPropertyDeclarationAccessor[] Accessors {
        get {
          return accessors;
        }
      }
      private CsharpPropertyDeclarationAccessor[] accessors;
      public Modifiers Modifiers {
        get {
          return modifiers;
        }
      }
      private Modifiers modifiers;
      public DeclarationItemDescription Description {
        get {
          return description;
        }
      }
      private DeclarationItemDescription description;
    } // end of CsharpPropertyDeclaration
    public sealed partial class EnumDeclaration : CSharpBase, INamespaceMember,IClassMember {
      public EnumDeclaration(string Name){
        this.name = Name;
      }
      public string Name {
        get {
          return name;
        }
      }
      private string name;
    } // end of EnumDeclaration
    public sealed partial class CsharpPropertyDeclarationAccessor : CSharpBase {
      public CsharpPropertyDeclarationAccessor(string Name, Modifiers Modifiers, IStatement Statement){
        this.name = Name;
        this.modifiers = Modifiers;
        this.statement = Statement;
      }
      public string Name {
        get {
          return name;
        }
      }
      private string name;
      public Modifiers Modifiers {
        get {
          return modifiers;
        }
      }
      private Modifiers modifiers;
      public IStatement Statement {
        get {
          return statement;
        }
      }
      private IStatement statement;
    } // end of CsharpPropertyDeclarationAccessor
    public sealed partial class ConstValue : CSharpBase, IValue {
      public ConstValue(object MyValue){
        this.myValue = MyValue;
      }
      /// <summary>
      /// Wartość stała
      /// </summary>
      public object MyValue {
        get {
          return myValue;
        }
      }
      private object myValue;
    } // end of ConstValue
    public sealed partial class TypeValue : CSharpBase, IValue {
      public TypeValue(Type DotnetType){
        this.dotnetType = DotnetType;
      }
      public Type DotnetType {
        get {
          return dotnetType;
        }
      }
      private Type dotnetType;
    } // end of TypeValue
    public sealed partial class TypeOfExpression : CSharpBase, IValue {
      public TypeOfExpression(Type DotnetType){
        this.dotnetType = DotnetType;
      }
      public Type DotnetType {
        get {
          return dotnetType;
        }
      }
      private Type dotnetType;
    } // end of TypeOfExpression
    public sealed partial class InvocationExpression : CSharpBase, IValue {
      public InvocationExpression(IValue Expression, FunctionArgument[] Arguments){
        this.expression = Expression;
        this.arguments = Arguments;
      }
      public IValue Expression {
        get {
          return expression;
        }
      }
      private IValue expression;
      public FunctionArgument[] Arguments {
        get {
          return arguments;
        }
      }
      private FunctionArgument[] arguments;
    } // end of InvocationExpression
    public sealed partial class LocalVariableExpression : CSharpBase, IValue {
      public LocalVariableExpression(string Name, LangType Type){
        this.name = Name;
        this.type = Type;
      }
      public string Name {
        get {
          return name;
        }
      }
      private string name;
      public LangType Type {
        get {
          return type;
        }
      }
      private LangType type;
    } // end of LocalVariableExpression
    public sealed partial class ArgumentExpression : CSharpBase, IValue {
      public ArgumentExpression(string Name, LangType Type){
        this.name = Name;
        this.type = Type;
      }
      public string Name {
        get {
          return name;
        }
      }
      private string name;
      public LangType Type {
        get {
          return type;
        }
      }
      private LangType type;
    } // end of ArgumentExpression
    public sealed partial class SimpleLambdaExpression : CSharpBase, IValue {
      public SimpleLambdaExpression(FunctionDeclarationParameter Parameter, IValue Expression){
        this.parameter = Parameter;
        this.expression = Expression;
      }
      public FunctionDeclarationParameter Parameter {
        get {
          return parameter;
        }
      }
      private FunctionDeclarationParameter parameter;
      public IValue Expression {
        get {
          return expression;
        }
      }
      private IValue expression;
    } // end of SimpleLambdaExpression
    public sealed partial class CastExpression : CSharpBase, IValue {
      public CastExpression(IValue Expression, Type DotnetType){
        this.expression = Expression;
        this.dotnetType = DotnetType;
      }
      public IValue Expression {
        get {
          return expression;
        }
      }
      private IValue expression;
      public Type DotnetType {
        get {
          return dotnetType;
        }
      }
      private Type dotnetType;
    } // end of CastExpression
    public sealed partial class VariableDeclaration : CSharpBase, IStatement {
      public VariableDeclaration(LangType Type, VariableDeclarator[] Declarators){
        this.type = Type;
        this.declarators = Declarators;
      }
      public LangType Type {
        get {
          return type;
        }
      }
      private LangType type;
      public VariableDeclarator[] Declarators {
        get {
          return declarators;
        }
      }
      private VariableDeclarator[] declarators;
    } // end of VariableDeclaration
    public sealed partial class LangType : CSharpBase {
      public LangType(Type DotnetType){
        this.dotnetType = DotnetType;
      }
      /// <summary>
      /// Typ .NET
      /// </summary>
      public Type DotnetType {
        get {
          return dotnetType;
        }
      }
      private Type dotnetType;
    } // end of LangType
    public sealed partial class CallConstructor : CSharpBase, IValue {
      public CallConstructor(ConstructorInfo Info, FunctionArgument[] Arguments, IValue[] Initializers){
        this.info = Info;
        this.arguments = Arguments;
        this.initializers = Initializers;
      }
      public ConstructorInfo Info {
        get {
          return info;
        }
      }
      private ConstructorInfo info;
      public FunctionArgument[] Arguments {
        get {
          return arguments;
        }
      }
      private FunctionArgument[] arguments;
      public IValue[] Initializers {
        get {
          return initializers;
        }
      }
      private IValue[] initializers;
    } // end of CallConstructor
    public sealed partial class FunctionArgument : CSharpBase, IValue {
      public FunctionArgument(string RefOrOutKeyword, IValue MyValue){
        this.refOrOutKeyword = RefOrOutKeyword;
        this.myValue = MyValue;
      }
      public string RefOrOutKeyword {
        get {
          return refOrOutKeyword;
        }
      }
      private string refOrOutKeyword;
      public IValue MyValue {
        get {
          return myValue;
        }
      }
      private IValue myValue;
    } // end of FunctionArgument
    public sealed partial class Modifiers : CSharpBase {
      public Modifiers(string[] Items){
        this.items = Items;
      }
      public string[] Items {
        get {
          return items;
        }
      }
      private string[] items;
    } // end of Modifiers
    public sealed partial class FunctionDeclarationParameter : CSharpBase {
      public FunctionDeclarationParameter(string Name, Modifiers Modifiers, LangType Type, IValue Initial){
        this.name = Name;
        this.modifiers = Modifiers;
        this.type = Type;
        this.initial = Initial;
      }
      public string Name {
        get {
          return name;
        }
      }
      private string name;
      public Modifiers Modifiers {
        get {
          return modifiers;
        }
      }
      private Modifiers modifiers;
      public LangType Type {
        get {
          return type;
        }
      }
      private LangType type;
      public IValue Initial {
        get {
          return initial;
        }
      }
      private IValue initial;
    } // end of FunctionDeclarationParameter
    public sealed partial class CodeBlock : CSharpBase, IStatement {
      public CodeBlock(IStatement[] Items){
        this.items = Items;
      }
      public IStatement[] Items {
        get {
          return items;
        }
      }
      private IStatement[] items;
    } // end of CodeBlock
    public sealed partial class ReturnStatement : CSharpBase, IStatement {
      public ReturnStatement(IValue ReturnValue){
        this.returnValue = ReturnValue;
      }
      public IValue ReturnValue {
        get {
          return returnValue;
        }
      }
      private IValue returnValue;
    } // end of ReturnStatement
    public sealed partial class MethodDeclaration : CSharpBase, IClassMember {
      public MethodDeclaration(MethodInfo Info, IStatement Body){
        this.info = Info;
        this.body = Body;
      }
      public MethodInfo Info {
        get {
          return info;
        }
      }
      private MethodInfo info;
      public IStatement Body {
        get {
          return body;
        }
      }
      private IStatement body;
    } // end of MethodDeclaration
    public sealed partial class ConstructorDeclaration : CSharpBase, IClassMember {
      public ConstructorDeclaration(ConstructorInfo Info, IStatement Body){
        this.info = Info;
        this.body = Body;
      }
      public ConstructorInfo Info {
        get {
          return info;
        }
      }
      private ConstructorInfo info;
      public IStatement Body {
        get {
          return body;
        }
      }
      private IStatement body;
    } // end of ConstructorDeclaration
    public sealed partial class LocalDeclarationStatement : CSharpBase, IStatement {
      public LocalDeclarationStatement(bool IsConst, bool IsFixed, VariableDeclaration Declaration){
        this.isConst = IsConst;
        this.isFixed = IsFixed;
        this.declaration = Declaration;
      }
      public bool IsConst {
        get {
          return isConst;
        }
      }
      private bool isConst;
      public bool IsFixed {
        get {
          return isFixed;
        }
      }
      private bool isFixed;
      public VariableDeclaration Declaration {
        get {
          return declaration;
        }
      }
      private VariableDeclaration declaration;
    } // end of LocalDeclarationStatement
    public sealed partial class ThisExpression : CSharpBase, IValue {
      public ThisExpression(Type ObjectType){
        this.objectType = ObjectType;
      }
      public Type ObjectType {
        get {
          return objectType;
        }
      }
      private Type objectType;
    } // end of ThisExpression
    public sealed partial class MemberAccessExpression : CSharpBase, IValue {
      public MemberAccessExpression(string MemberName, IValue Expression){
        this.memberName = MemberName;
        this.expression = Expression;
      }
      public string MemberName {
        get {
          return memberName;
        }
      }
      private string memberName;
      public IValue Expression {
        get {
          return expression;
        }
      }
      private IValue expression;
    } // end of MemberAccessExpression
    public sealed partial class ClassFieldAccessExpression : CSharpBase, IValue {
      public ClassFieldAccessExpression(FieldInfo Member, bool IsStatic){
        this.member = Member;
        this.isStatic = IsStatic;
      }
      public FieldInfo Member {
        get {
          return member;
        }
      }
      private FieldInfo member;
      public bool IsStatic {
        get {
          return isStatic;
        }
      }
      private bool isStatic;
    } // end of ClassFieldAccessExpression
    public sealed partial class ClassPropertyAccessExpression : CSharpBase, IValue {
      public ClassPropertyAccessExpression(PropertyInfo Member){
        this.member = Member;
      }
      public PropertyInfo Member {
        get {
          return member;
        }
      }
      private PropertyInfo member;
    } // end of ClassPropertyAccessExpression
    public sealed partial class InstanceFieldAccessExpression : CSharpBase, IValue {
      public InstanceFieldAccessExpression(FieldInfo Member, IValue TargetObject){
        this.member = Member;
        this.targetObject = TargetObject;
      }
      public FieldInfo Member {
        get {
          return member;
        }
      }
      private FieldInfo member;
      public IValue TargetObject {
        get {
          return targetObject;
        }
      }
      private IValue targetObject;
    } // end of InstanceFieldAccessExpression
    public sealed partial class ArrayCreateExpression : CSharpBase, IValue {
      public ArrayCreateExpression(Type ArrayType, IValue[] Initializers){
        this.arrayType = ArrayType;
        this.initializers = Initializers;
      }
      public Type ArrayType {
        get {
          return arrayType;
        }
      }
      private Type arrayType;
      public IValue[] Initializers {
        get {
          return initializers;
        }
      }
      private IValue[] initializers;
    } // end of ArrayCreateExpression
    public sealed partial class StaticMemberAccessExpression : CSharpBase, IValue {
      public StaticMemberAccessExpression(string MemberName, LangType Expression){
        this.memberName = MemberName;
        this.expression = Expression;
      }
      public string MemberName {
        get {
          return memberName;
        }
      }
      private string memberName;
      public LangType Expression {
        get {
          return expression;
        }
      }
      private LangType expression;
    } // end of StaticMemberAccessExpression
    public sealed partial class InstanceMemberAccessExpression : CSharpBase, IValue {
      public InstanceMemberAccessExpression(string MemberName, IValue Expression, MemberInfo Member){
        this.memberName = MemberName;
        this.expression = Expression;
        this.member = Member;
      }
      public string MemberName {
        get {
          return memberName;
        }
      }
      private string memberName;
      public IValue Expression {
        get {
          return expression;
        }
      }
      private IValue expression;
      public MemberInfo Member {
        get {
          return member;
        }
      }
      private MemberInfo member;
    } // end of InstanceMemberAccessExpression
    public sealed partial class CsharpInstancePropertyAccessExpression : CSharpBase, IValue {
      public CsharpInstancePropertyAccessExpression(PropertyInfo Member, IValue TargetObject){
        this.member = Member;
        this.targetObject = TargetObject;
      }
      public PropertyInfo Member {
        get {
          return member;
        }
      }
      private PropertyInfo member;
      public IValue TargetObject {
        get {
          return targetObject;
        }
      }
      private IValue targetObject;
    } // end of CsharpInstancePropertyAccessExpression
    public sealed partial class CsharpMethodCallExpression : CSharpBase, IValue, IStatement {
      public CsharpMethodCallExpression(MethodInfo MethodInfo, IValue TargetObject, FunctionArgument[] Arguments, Type[] GenericTypes, bool IsDelegate){
        this.methodInfo = MethodInfo;
        this.targetObject = TargetObject;
        this.arguments = Arguments;
        this.genericTypes = GenericTypes;
        this.isDelegate = IsDelegate;
      }
      public MethodInfo MethodInfo {
        get {
          return methodInfo;
        }
      }
      private MethodInfo methodInfo;
      public IValue TargetObject {
        get {
          return targetObject;
        }
      }
      private IValue targetObject;
      public FunctionArgument[] Arguments {
        get {
          return arguments;
        }
      }
      private FunctionArgument[] arguments;
      public Type[] GenericTypes {
        get {
          return genericTypes;
        }
      }
      private Type[] genericTypes;
      public bool IsDelegate {
        get {
          return isDelegate;
        }
      }
      private bool isDelegate;
    } // end of CsharpMethodCallExpression
    public sealed partial class IfStatement : CSharpBase, IStatement {
      public IfStatement(IValue Condition, IStatement IfTrue, IStatement IfFalse){
        this.condition = Condition;
        this.ifTrue = IfTrue;
        this.ifFalse = IfFalse;
      }
      public IValue Condition {
        get {
          return condition;
        }
      }
      private IValue condition;
      public IStatement IfTrue {
        get {
          return ifTrue;
        }
      }
      private IStatement ifTrue;
      public IStatement IfFalse {
        get {
          return ifFalse;
        }
      }
      private IStatement ifFalse;
    } // end of IfStatement
    public sealed partial class WhileStatement : CSharpBase, IStatement {
      public WhileStatement(IValue Condition, IStatement Statement){
        this.condition = Condition;
        this.statement = Statement;
      }
      public IValue Condition {
        get {
          return condition;
        }
      }
      private IValue condition;
      public IStatement Statement {
        get {
          return statement;
        }
      }
      private IStatement statement;
    } // end of WhileStatement
    public sealed partial class ForStatement : CSharpBase, IStatement {
      public ForStatement(VariableDeclaration Declaration, IValue Condition, IStatement Statement, IStatement[] Incrementors){
        this.declaration = Declaration;
        this.condition = Condition;
        this.statement = Statement;
        this.incrementors = Incrementors;
      }
      public VariableDeclaration Declaration {
        get {
          return declaration;
        }
      }
      private VariableDeclaration declaration;
      public IValue Condition {
        get {
          return condition;
        }
      }
      private IValue condition;
      public IStatement Statement {
        get {
          return statement;
        }
      }
      private IStatement statement;
      public IStatement[] Incrementors {
        get {
          return incrementors;
        }
      }
      private IStatement[] incrementors;
    } // end of ForStatement
    public sealed partial class ForEachStatement : CSharpBase, IStatement {
      public ForEachStatement(LangType ItemType, string VarName, IValue Collection, IStatement Statement){
        this.itemType = ItemType;
        this.varName = VarName;
        this.collection = Collection;
        this.statement = Statement;
      }
      public LangType ItemType {
        get {
          return itemType;
        }
      }
      private LangType itemType;
      public string VarName {
        get {
          return varName;
        }
      }
      private string varName;
      public IValue Collection {
        get {
          return collection;
        }
      }
      private IValue collection;
      public IStatement Statement {
        get {
          return statement;
        }
      }
      private IStatement statement;
    } // end of ForEachStatement
    public sealed partial class BreakStatement : CSharpBase, IStatement {
      public BreakStatement(){
      }
    } // end of BreakStatement
    public sealed partial class ContinueStatement : CSharpBase, IStatement {
      public ContinueStatement(){
      }
    } // end of ContinueStatement
    public sealed partial class BinaryOperatorExpression : CSharpBase, IValue {
      public BinaryOperatorExpression(IValue Left, IValue Right, string Operator, Type ForceType, MethodInfo OperatorMethod){
        this.left = Left;
        this.right = Right;
        this._operator = Operator;
        this.forceType = ForceType;
        this.operatorMethod = OperatorMethod;
      }
      public IValue Left {
        get {
          return left;
        }
      }
      private IValue left;
      public IValue Right {
        get {
          return right;
        }
      }
      private IValue right;
      public string Operator {
        get {
          return _operator;
        }
      }
      private string _operator;
      /// <summary>
      /// Typ jeśli znany
      /// </summary>
      public Type ForceType {
        get {
          return forceType;
        }
      }
      private Type forceType;
      /// <summary>
      /// operator jeśli użyty
      /// </summary>
      public MethodInfo OperatorMethod {
        get {
          return operatorMethod;
        }
      }
      private MethodInfo operatorMethod;
    } // end of BinaryOperatorExpression
    public sealed partial class UnaryOperatorExpression : CSharpBase, IValue {
      public UnaryOperatorExpression(IValue Operand, string Operator, Type ForceType){
        this.operand = Operand;
        this._operator = Operator;
        this.forceType = ForceType;
      }
      public IValue Operand {
        get {
          return operand;
        }
      }
      private IValue operand;
      public string Operator {
        get {
          return _operator;
        }
      }
      private string _operator;
      /// <summary>
      /// Typ jeśli znany
      /// </summary>
      public Type ForceType {
        get {
          return forceType;
        }
      }
      private Type forceType;
    } // end of UnaryOperatorExpression
    public sealed partial class CsharpAssignExpression : CSharpBase, IValue, IStatement {
      public CsharpAssignExpression(IValue Left, IValue Right, string OptionalOperator){
        this.left = Left;
        this.right = Right;
        this.optionalOperator = OptionalOperator;
      }
      public IValue Left {
        get {
          return left;
        }
      }
      private IValue left;
      public IValue Right {
        get {
          return right;
        }
      }
      private IValue right;
      public string OptionalOperator {
        get {
          return optionalOperator;
        }
      }
      private string optionalOperator;
    } // end of CsharpAssignExpression
    public sealed partial class IncrementDecrementExpression : CSharpBase, IValue, IStatement {
      public IncrementDecrementExpression(IValue Operand, bool Increment, bool Pre){
        this.operand = Operand;
        this.increment = Increment;
        this.pre = Pre;
      }
      public IValue Operand {
        get {
          return operand;
        }
      }
      private IValue operand;
      public bool Increment {
        get {
          return increment;
        }
      }
      private bool increment;
      public bool Pre {
        get {
          return pre;
        }
      }
      private bool pre;
    } // end of IncrementDecrementExpression
    public sealed partial class ElementAccessExpression : CSharpBase, IValue {
      public ElementAccessExpression(IValue Expression, FunctionArgument[] Arguments, Type ElementType){
        this.expression = Expression;
        this.arguments = Arguments;
        this.elementType = ElementType;
      }
      public IValue Expression {
        get {
          return expression;
        }
      }
      private IValue expression;
      public FunctionArgument[] Arguments {
        get {
          return arguments;
        }
      }
      private FunctionArgument[] arguments;
      public Type ElementType {
        get {
          return elementType;
        }
      }
      private Type elementType;
    } // end of ElementAccessExpression
    public sealed partial class ConditionalExpression : CSharpBase, IValue {
      public ConditionalExpression(IValue Condition, IValue WhenTrue, IValue WhenFalse, Type ResultType){
        this.condition = Condition;
        this.whenTrue = WhenTrue;
        this.whenFalse = WhenFalse;
        this.resultType = ResultType;
      }
      public IValue Condition {
        get {
          return condition;
        }
      }
      private IValue condition;
      public IValue WhenTrue {
        get {
          return whenTrue;
        }
      }
      private IValue whenTrue;
      public IValue WhenFalse {
        get {
          return whenFalse;
        }
      }
      private IValue whenFalse;
      public Type ResultType {
        get {
          return resultType;
        }
      }
      private Type resultType;
    } // end of ConditionalExpression
    public sealed partial class CompileResult : CSharpBase {
      public CompileResult(string Source, CompilationUnit Compiled){
        this.source = Source;
        this.compiled = Compiled;
      }
      public string Source {
        get {
          return source;
        }
      }
      private string source;
      public CompilationUnit Compiled {
        get {
          return compiled;
        }
      }
      private CompilationUnit compiled;
    } // end of CompileResult
    public sealed partial class ParenthesizedExpression : CSharpBase, IValue {
      public ParenthesizedExpression(IValue Expression){
        this.expression = Expression;
      }
      public IValue Expression {
        get {
          return expression;
        }
      }
      private IValue expression;
    } // end of ParenthesizedExpression
    public sealed partial class UnknownIdentifierValue : CSharpBase, IValue {
      public UnknownIdentifierValue(string Identifier, IValue[] OptionalGenericTypes){
        this.identifier = Identifier;
        this.optionalGenericTypes = OptionalGenericTypes;
      }
      public string Identifier {
        get {
          return identifier;
        }
      }
      private string identifier;
      public IValue[] OptionalGenericTypes {
        get {
          return optionalGenericTypes;
        }
      }
      private IValue[] optionalGenericTypes;
    } // end of UnknownIdentifierValue
    public sealed partial class IValueTable_PseudoValue : CSharpBase, IValue {
      public IValueTable_PseudoValue(IValue[] Items){
        this.items = Items;
      }
      public IValue[] Items {
        get {
          return items;
        }
      }
      private IValue[] items;
    } // end of IValueTable_PseudoValue
    public sealed partial class IValueTable2_PseudoValue : CSharpBase, IValue {
      public IValueTable2_PseudoValue(IValueTable_PseudoValue[] Items){
        this.items = Items;
      }
      public IValueTable_PseudoValue[] Items {
        get {
          return items;
        }
      }
      private IValueTable_PseudoValue[] items;
    } // end of IValueTable2_PseudoValue
    public sealed partial class MethodExpression : CSharpBase, IValue {
      public MethodExpression(MethodInfo Method){
        this.method = Method;
      }
      public MethodInfo Method {
        get {
          return method;
        }
      }
      private MethodInfo method;
    } // end of MethodExpression
    public sealed partial class LambdaExpression : CSharpBase, IValue {
      public LambdaExpression(Type DotnetType, FunctionDeclarationParameter[] Parameters, IStatement Body){
        this.dotnetType = DotnetType;
        this.parameters = Parameters;
        this.body = Body;
      }
      public Type DotnetType {
        get {
          return dotnetType;
        }
      }
      private Type dotnetType;
      public FunctionDeclarationParameter[] Parameters {
        get {
          return parameters;
        }
      }
      private FunctionDeclarationParameter[] parameters;
      public IStatement Body {
        get {
          return body;
        }
      }
      private IStatement body;
    } // end of LambdaExpression
    public sealed partial class CsharpSwitchStatement : CSharpBase, IStatement {
      public CsharpSwitchStatement(IValue Expression, CsharpSwichSection[] Sections){
        this.expression = Expression;
        this.sections = Sections;
      }
      public IValue Expression {
        get {
          return expression;
        }
      }
      private IValue expression;
      public CsharpSwichSection[] Sections {
        get {
          return sections;
        }
      }
      private CsharpSwichSection[] sections;
    } // end of CsharpSwitchStatement
    public sealed partial class CsharpSwichSection : CSharpBase {
      public CsharpSwichSection(CsharpSwichLabel[] Labels, IStatement[] Statements){
        this.labels = Labels;
        this.statements = Statements;
      }
      public CsharpSwichLabel[] Labels {
        get {
          return labels;
        }
      }
      private CsharpSwichLabel[] labels;
      public IStatement[] Statements {
        get {
          return statements;
        }
      }
      private IStatement[] statements;
    } // end of CsharpSwichSection
    public sealed partial class CsharpSwichLabel : CSharpBase, IValue {
      public CsharpSwichLabel(IValue Expression, bool IsDefault){
        this.expression = Expression;
        this.isDefault = IsDefault;
      }
      public IValue Expression {
        get {
          return expression;
        }
      }
      private IValue expression;
      public bool IsDefault {
        get {
          return isDefault;
        }
      }
      private bool isDefault;
    } // end of CsharpSwichLabel
public class CSharpBase {
    public CSharpBaseKinds TokenKind
    {
        get
        {
            if (this is NameType) return CSharpBaseKinds.NameTypeKind;
            if (this is CompilationUnit) return CSharpBaseKinds.CompilationUnitKind;
            if (this is ImportNamespaceCollection) return CSharpBaseKinds.ImportNamespaceCollectionKind;
            if (this is ImportNamespace) return CSharpBaseKinds.ImportNamespaceKind;
            if (this is NamespaceDeclaration) return CSharpBaseKinds.NamespaceDeclarationKind;
            if (this is ClassDeclaration) return CSharpBaseKinds.ClassDeclarationKind;
            if (this is InterfaceDeclaration) return CSharpBaseKinds.InterfaceDeclarationKind;
            if (this is FieldDeclaration) return CSharpBaseKinds.FieldDeclarationKind;
            if (this is VariableDeclarator) return CSharpBaseKinds.VariableDeclaratorKind;
            if (this is CsharpPropertyDeclaration) return CSharpBaseKinds.PropertyDeclarationKind;
            if (this is EnumDeclaration) return CSharpBaseKinds.EnumDeclarationKind;
            if (this is CsharpPropertyDeclarationAccessor) return CSharpBaseKinds.PropertyDeclarationAccessorKind;
            if (this is ConstValue) return CSharpBaseKinds.ConstValueKind;
            if (this is TypeValue) return CSharpBaseKinds.TypeValueKind;
            if (this is TypeOfExpression) return CSharpBaseKinds.TypeOfExpressionKind;
            if (this is InvocationExpression) return CSharpBaseKinds.InvocationExpressionKind;
            if (this is LocalVariableExpression) return CSharpBaseKinds.LocalVariableExpressionKind;
            if (this is ArgumentExpression) return CSharpBaseKinds.ArgumentExpressionKind;
            if (this is SimpleLambdaExpression) return CSharpBaseKinds.SimpleLambdaExpressionKind;
            if (this is CastExpression) return CSharpBaseKinds.CastExpressionKind;
            if (this is VariableDeclaration) return CSharpBaseKinds.VariableDeclarationKind;
            if (this is LangType) return CSharpBaseKinds.LangTypeKind;
            if (this is CallConstructor) return CSharpBaseKinds.CallConstructorKind;
            if (this is FunctionArgument) return CSharpBaseKinds.FunctionArgumentKind;
            if (this is Modifiers) return CSharpBaseKinds.ModifiersKind;
            if (this is FunctionDeclarationParameter) return CSharpBaseKinds.FunctionDeclarationParameterKind;
            if (this is CodeBlock) return CSharpBaseKinds.CodeBlockKind;
            if (this is ReturnStatement) return CSharpBaseKinds.ReturnStatementKind;
            if (this is MethodDeclaration) return CSharpBaseKinds.MethodDeclarationKind;
            if (this is ConstructorDeclaration) return CSharpBaseKinds.ConstructorDeclarationKind;
            if (this is LocalDeclarationStatement) return CSharpBaseKinds.LocalDeclarationStatementKind;
            if (this is ThisExpression) return CSharpBaseKinds.ThisExpressionKind;
            if (this is MemberAccessExpression) return CSharpBaseKinds.MemberAccessExpressionKind;
            if (this is ClassFieldAccessExpression) return CSharpBaseKinds.ClassFieldAccessExpressionKind;
            if (this is ClassPropertyAccessExpression) return CSharpBaseKinds.ClassPropertyAccessExpressionKind;
            if (this is InstanceFieldAccessExpression) return CSharpBaseKinds.InstanceFieldAccessExpressionKind;
            if (this is ArrayCreateExpression) return CSharpBaseKinds.ArrayCreateExpressionKind;
            if (this is StaticMemberAccessExpression) return CSharpBaseKinds.StaticMemberAccessExpressionKind;
            if (this is InstanceMemberAccessExpression) return CSharpBaseKinds.InstanceMemberAccessExpressionKind;
            if (this is CsharpInstancePropertyAccessExpression) return CSharpBaseKinds.InstancePropertyAccessExpressionKind;
            if (this is CsharpMethodCallExpression) return CSharpBaseKinds.MethodCallExpressionKind;
            if (this is IfStatement) return CSharpBaseKinds.IfStatementKind;
            if (this is WhileStatement) return CSharpBaseKinds.WhileStatementKind;
            if (this is ForStatement) return CSharpBaseKinds.ForStatementKind;
            if (this is ForEachStatement) return CSharpBaseKinds.ForEachStatementKind;
            if (this is BreakStatement) return CSharpBaseKinds.BreakStatementKind;
            if (this is ContinueStatement) return CSharpBaseKinds.ContinueStatementKind;
            if (this is BinaryOperatorExpression) return CSharpBaseKinds.BinaryOperatorExpressionKind;
            if (this is UnaryOperatorExpression) return CSharpBaseKinds.UnaryOperatorExpressionKind;
            if (this is CsharpAssignExpression) return CSharpBaseKinds.AssignExpressionKind;
            if (this is IncrementDecrementExpression) return CSharpBaseKinds.IncrementDecrementExpressionKind;
            if (this is ElementAccessExpression) return CSharpBaseKinds.ElementAccessExpressionKind;
            if (this is ConditionalExpression) return CSharpBaseKinds.ConditionalExpressionKind;
            if (this is CompileResult) return CSharpBaseKinds.CompileResultKind;
            if (this is ParenthesizedExpression) return CSharpBaseKinds.ParenthesizedExpressionKind;
            if (this is UnknownIdentifierValue) return CSharpBaseKinds.UnknownIdentifierValueKind;
            if (this is IValueTable_PseudoValue) return CSharpBaseKinds.IValueTable_PseudoValueKind;
            if (this is IValueTable2_PseudoValue) return CSharpBaseKinds.IValueTable2_PseudoValueKind;
            if (this is MethodExpression) return CSharpBaseKinds.MethodExpressionKind;
            if (this is LambdaExpression) return CSharpBaseKinds.LambdaExpressionKind;
            if (this is CsharpSwitchStatement) return CSharpBaseKinds.SwitchStatementKind;
            if (this is CsharpSwichSection) return CSharpBaseKinds.SwichSectionKind;
            if (this is CsharpSwichLabel) return CSharpBaseKinds.SwichLabelKind;
        throw new NotSupportedException();
        }
    }
}
public enum CSharpBaseKinds {
    NameTypeKind,
    CompilationUnitKind,
    ImportNamespaceCollectionKind,
    ImportNamespaceKind,
    NamespaceDeclarationKind,
    ClassDeclarationKind,
    InterfaceDeclarationKind,
    FieldDeclarationKind,
    VariableDeclaratorKind,
    PropertyDeclarationKind,
    EnumDeclarationKind,
    PropertyDeclarationAccessorKind,
    ConstValueKind,
    TypeValueKind,
    TypeOfExpressionKind,
    InvocationExpressionKind,
    LocalVariableExpressionKind,
    ArgumentExpressionKind,
    SimpleLambdaExpressionKind,
    CastExpressionKind,
    VariableDeclarationKind,
    LangTypeKind,
    CallConstructorKind,
    FunctionArgumentKind,
    ModifiersKind,
    FunctionDeclarationParameterKind,
    CodeBlockKind,
    ReturnStatementKind,
    MethodDeclarationKind,
    ConstructorDeclarationKind,
    LocalDeclarationStatementKind,
    ThisExpressionKind,
    MemberAccessExpressionKind,
    ClassFieldAccessExpressionKind,
    ClassPropertyAccessExpressionKind,
    InstanceFieldAccessExpressionKind,
    ArrayCreateExpressionKind,
    StaticMemberAccessExpressionKind,
    InstanceMemberAccessExpressionKind,
    InstancePropertyAccessExpressionKind,
    MethodCallExpressionKind,
    IfStatementKind,
    WhileStatementKind,
    ForStatementKind,
    ForEachStatementKind,
    BreakStatementKind,
    ContinueStatementKind,
    BinaryOperatorExpressionKind,
    UnaryOperatorExpressionKind,
    AssignExpressionKind,
    IncrementDecrementExpressionKind,
    ElementAccessExpressionKind,
    ConditionalExpressionKind,
    CompileResultKind,
    ParenthesizedExpressionKind,
    UnknownIdentifierValueKind,
    IValueTable_PseudoValueKind,
    IValueTable2_PseudoValueKind,
    MethodExpressionKind,
    LambdaExpressionKind,
    SwitchStatementKind,
    SwichSectionKind,
    SwichLabelKind,
}
public class CSharpBaseVisitor<T> {
        public T Visit(CSharpBase a)
        {
            switch (a.TokenKind)
            {
                case CSharpBaseKinds.NameTypeKind:
                    return VisitNameType(a as NameType);
                case CSharpBaseKinds.CompilationUnitKind:
                    return VisitCompilationUnit(a as CompilationUnit);
                case CSharpBaseKinds.ImportNamespaceCollectionKind:
                    return VisitImportNamespaceCollection(a as ImportNamespaceCollection);
                case CSharpBaseKinds.ImportNamespaceKind:
                    return VisitImportNamespace(a as ImportNamespace);
                case CSharpBaseKinds.NamespaceDeclarationKind:
                    return VisitNamespaceDeclaration(a as NamespaceDeclaration);
                case CSharpBaseKinds.ClassDeclarationKind:
                    return VisitClassDeclaration(a as ClassDeclaration);
                case CSharpBaseKinds.InterfaceDeclarationKind:
                    return VisitInterfaceDeclaration(a as InterfaceDeclaration);
                case CSharpBaseKinds.FieldDeclarationKind:
                    return VisitFieldDeclaration(a as FieldDeclaration);
                case CSharpBaseKinds.VariableDeclaratorKind:
                    return VisitVariableDeclarator(a as VariableDeclarator);
                case CSharpBaseKinds.PropertyDeclarationKind:
                    return VisitPropertyDeclaration(a as CsharpPropertyDeclaration);
                case CSharpBaseKinds.EnumDeclarationKind:
                    return VisitEnumDeclaration(a as EnumDeclaration);
                case CSharpBaseKinds.PropertyDeclarationAccessorKind:
                    return VisitPropertyDeclarationAccessor(a as CsharpPropertyDeclarationAccessor);
                case CSharpBaseKinds.ConstValueKind:
                    return VisitConstValue(a as ConstValue);
                case CSharpBaseKinds.TypeValueKind:
                    return VisitTypeValue(a as TypeValue);
                case CSharpBaseKinds.TypeOfExpressionKind:
                    return VisitTypeOfExpression(a as TypeOfExpression);
                case CSharpBaseKinds.InvocationExpressionKind:
                    return VisitInvocationExpression(a as InvocationExpression);
                case CSharpBaseKinds.LocalVariableExpressionKind:
                    return VisitLocalVariableExpression(a as LocalVariableExpression);
                case CSharpBaseKinds.ArgumentExpressionKind:
                    return VisitArgumentExpression(a as ArgumentExpression);
                case CSharpBaseKinds.SimpleLambdaExpressionKind:
                    return VisitSimpleLambdaExpression(a as SimpleLambdaExpression);
                case CSharpBaseKinds.CastExpressionKind:
                    return VisitCastExpression(a as CastExpression);
                case CSharpBaseKinds.VariableDeclarationKind:
                    return VisitVariableDeclaration(a as VariableDeclaration);
                case CSharpBaseKinds.LangTypeKind:
                    return VisitLangType(a as LangType);
                case CSharpBaseKinds.CallConstructorKind:
                    return VisitCallConstructor(a as CallConstructor);
                case CSharpBaseKinds.FunctionArgumentKind:
                    return VisitFunctionArgument(a as FunctionArgument);
                case CSharpBaseKinds.ModifiersKind:
                    return VisitModifiers(a as Modifiers);
                case CSharpBaseKinds.FunctionDeclarationParameterKind:
                    return VisitFunctionDeclarationParameter(a as FunctionDeclarationParameter);
                case CSharpBaseKinds.CodeBlockKind:
                    return VisitCodeBlock(a as CodeBlock);
                case CSharpBaseKinds.ReturnStatementKind:
                    return VisitReturnStatement(a as ReturnStatement);
                case CSharpBaseKinds.MethodDeclarationKind:
                    return VisitMethodDeclaration(a as MethodDeclaration);
                case CSharpBaseKinds.ConstructorDeclarationKind:
                    return VisitConstructorDeclaration(a as ConstructorDeclaration);
                case CSharpBaseKinds.LocalDeclarationStatementKind:
                    return VisitLocalDeclarationStatement(a as LocalDeclarationStatement);
                case CSharpBaseKinds.ThisExpressionKind:
                    return VisitThisExpression(a as ThisExpression);
                case CSharpBaseKinds.MemberAccessExpressionKind:
                    return VisitMemberAccessExpression(a as MemberAccessExpression);
                case CSharpBaseKinds.ClassFieldAccessExpressionKind:
                    return VisitClassFieldAccessExpression(a as ClassFieldAccessExpression);
                case CSharpBaseKinds.ClassPropertyAccessExpressionKind:
                    return VisitClassPropertyAccessExpression(a as ClassPropertyAccessExpression);
                case CSharpBaseKinds.InstanceFieldAccessExpressionKind:
                    return VisitInstanceFieldAccessExpression(a as InstanceFieldAccessExpression);
                case CSharpBaseKinds.ArrayCreateExpressionKind:
                    return VisitArrayCreateExpression(a as ArrayCreateExpression);
                case CSharpBaseKinds.StaticMemberAccessExpressionKind:
                    return VisitStaticMemberAccessExpression(a as StaticMemberAccessExpression);
                case CSharpBaseKinds.InstanceMemberAccessExpressionKind:
                    return VisitInstanceMemberAccessExpression(a as InstanceMemberAccessExpression);
                case CSharpBaseKinds.InstancePropertyAccessExpressionKind:
                    return VisitInstancePropertyAccessExpression(a as CsharpInstancePropertyAccessExpression);
                case CSharpBaseKinds.MethodCallExpressionKind:
                    return VisitMethodCallExpression(a as CsharpMethodCallExpression);
                case CSharpBaseKinds.IfStatementKind:
                    return VisitIfStatement(a as IfStatement);
                case CSharpBaseKinds.WhileStatementKind:
                    return VisitWhileStatement(a as WhileStatement);
                case CSharpBaseKinds.ForStatementKind:
                    return VisitForStatement(a as ForStatement);
                case CSharpBaseKinds.ForEachStatementKind:
                    return VisitForEachStatement(a as ForEachStatement);
                case CSharpBaseKinds.BreakStatementKind:
                    return VisitBreakStatement(a as BreakStatement);
                case CSharpBaseKinds.ContinueStatementKind:
                    return VisitContinueStatement(a as ContinueStatement);
                case CSharpBaseKinds.BinaryOperatorExpressionKind:
                    return VisitBinaryOperatorExpression(a as BinaryOperatorExpression);
                case CSharpBaseKinds.UnaryOperatorExpressionKind:
                    return VisitUnaryOperatorExpression(a as UnaryOperatorExpression);
                case CSharpBaseKinds.AssignExpressionKind:
                    return VisitAssignExpression(a as CsharpAssignExpression);
                case CSharpBaseKinds.IncrementDecrementExpressionKind:
                    return VisitIncrementDecrementExpression(a as IncrementDecrementExpression);
                case CSharpBaseKinds.ElementAccessExpressionKind:
                    return VisitElementAccessExpression(a as ElementAccessExpression);
                case CSharpBaseKinds.ConditionalExpressionKind:
                    return VisitConditionalExpression(a as ConditionalExpression);
                case CSharpBaseKinds.CompileResultKind:
                    return VisitCompileResult(a as CompileResult);
                case CSharpBaseKinds.ParenthesizedExpressionKind:
                    return VisitParenthesizedExpression(a as ParenthesizedExpression);
                case CSharpBaseKinds.UnknownIdentifierValueKind:
                    return VisitUnknownIdentifierValue(a as UnknownIdentifierValue);
                case CSharpBaseKinds.IValueTable_PseudoValueKind:
                    return VisitIValueTable_PseudoValue(a as IValueTable_PseudoValue);
                case CSharpBaseKinds.IValueTable2_PseudoValueKind:
                    return VisitIValueTable2_PseudoValue(a as IValueTable2_PseudoValue);
                case CSharpBaseKinds.MethodExpressionKind:
                    return VisitMethodExpression(a as MethodExpression);
                case CSharpBaseKinds.LambdaExpressionKind:
                    return VisitLambdaExpression(a as LambdaExpression);
                case CSharpBaseKinds.SwitchStatementKind:
                    return VisitSwitchStatement(a as CsharpSwitchStatement);
                case CSharpBaseKinds.SwichSectionKind:
                    return VisitSwichSection(a as CsharpSwichSection);
                case CSharpBaseKinds.SwichLabelKind:
                    return VisitSwichLabel(a as CsharpSwichLabel);
            }
            throw new NotSupportedException();
        }
    protected virtual T VisitNameType(NameType src) {
        throw new NotSupportedException();
    }
    protected virtual T VisitCompilationUnit(CompilationUnit src) {
        throw new NotSupportedException();
    }
    protected virtual T VisitImportNamespaceCollection(ImportNamespaceCollection src) {
        throw new NotSupportedException();
    }
    protected virtual T VisitImportNamespace(ImportNamespace src) {
        throw new NotSupportedException();
    }
    protected virtual T VisitNamespaceDeclaration(NamespaceDeclaration src) {
        throw new NotSupportedException();
    }
    protected virtual T VisitClassDeclaration(ClassDeclaration src) {
        throw new NotSupportedException();
    }
    protected virtual T VisitInterfaceDeclaration(InterfaceDeclaration src) {
        throw new NotSupportedException();
    }
    protected virtual T VisitFieldDeclaration(FieldDeclaration src) {
        throw new NotSupportedException();
    }
    protected virtual T VisitVariableDeclarator(VariableDeclarator src) {
        throw new NotSupportedException();
    }
    protected virtual T VisitPropertyDeclaration(CsharpPropertyDeclaration src) {
        throw new NotSupportedException();
    }
    protected virtual T VisitEnumDeclaration(EnumDeclaration src) {
        throw new NotSupportedException();
    }
    protected virtual T VisitPropertyDeclarationAccessor(CsharpPropertyDeclarationAccessor src) {
        throw new NotSupportedException();
    }
    protected virtual T VisitConstValue(ConstValue src) {
        throw new NotSupportedException();
    }
    protected virtual T VisitTypeValue(TypeValue src) {
        throw new NotSupportedException();
    }
    protected virtual T VisitTypeOfExpression(TypeOfExpression src) {
        throw new NotSupportedException();
    }
    protected virtual T VisitInvocationExpression(InvocationExpression src) {
        throw new NotSupportedException();
    }
    protected virtual T VisitLocalVariableExpression(LocalVariableExpression src) {
        throw new NotSupportedException();
    }
    protected virtual T VisitArgumentExpression(ArgumentExpression src) {
        throw new NotSupportedException();
    }
    protected virtual T VisitSimpleLambdaExpression(SimpleLambdaExpression src) {
        throw new NotSupportedException();
    }
    protected virtual T VisitCastExpression(CastExpression src) {
        throw new NotSupportedException();
    }
    protected virtual T VisitVariableDeclaration(VariableDeclaration src) {
        throw new NotSupportedException();
    }
    protected virtual T VisitLangType(LangType src) {
        throw new NotSupportedException();
    }
    protected virtual T VisitCallConstructor(CallConstructor src) {
        throw new NotSupportedException();
    }
    protected virtual T VisitFunctionArgument(FunctionArgument src) {
        throw new NotSupportedException();
    }
    protected virtual T VisitModifiers(Modifiers src) {
        throw new NotSupportedException();
    }
    protected virtual T VisitFunctionDeclarationParameter(FunctionDeclarationParameter src) {
        throw new NotSupportedException();
    }
    protected virtual T VisitCodeBlock(CodeBlock src) {
        throw new NotSupportedException();
    }
    protected virtual T VisitReturnStatement(ReturnStatement src) {
        throw new NotSupportedException();
    }
    protected virtual T VisitMethodDeclaration(MethodDeclaration src) {
        throw new NotSupportedException();
    }
    protected virtual T VisitConstructorDeclaration(ConstructorDeclaration src) {
        throw new NotSupportedException();
    }
    protected virtual T VisitLocalDeclarationStatement(LocalDeclarationStatement src) {
        throw new NotSupportedException();
    }
    protected virtual T VisitThisExpression(ThisExpression src) {
        throw new NotSupportedException();
    }
    protected virtual T VisitMemberAccessExpression(MemberAccessExpression src) {
        throw new NotSupportedException();
    }
    protected virtual T VisitClassFieldAccessExpression(ClassFieldAccessExpression src) {
        throw new NotSupportedException();
    }
    protected virtual T VisitClassPropertyAccessExpression(ClassPropertyAccessExpression src) {
        throw new NotSupportedException();
    }
    protected virtual T VisitInstanceFieldAccessExpression(InstanceFieldAccessExpression src) {
        throw new NotSupportedException();
    }
    protected virtual T VisitArrayCreateExpression(ArrayCreateExpression src) {
        throw new NotSupportedException();
    }
    protected virtual T VisitStaticMemberAccessExpression(StaticMemberAccessExpression src) {
        throw new NotSupportedException();
    }
    protected virtual T VisitInstanceMemberAccessExpression(InstanceMemberAccessExpression src) {
        throw new NotSupportedException();
    }
    protected virtual T VisitInstancePropertyAccessExpression(CsharpInstancePropertyAccessExpression src) {
        throw new NotSupportedException();
    }
    protected virtual T VisitMethodCallExpression(CsharpMethodCallExpression src) {
        throw new NotSupportedException();
    }
    protected virtual T VisitIfStatement(IfStatement src) {
        throw new NotSupportedException();
    }
    protected virtual T VisitWhileStatement(WhileStatement src) {
        throw new NotSupportedException();
    }
    protected virtual T VisitForStatement(ForStatement src) {
        throw new NotSupportedException();
    }
    protected virtual T VisitForEachStatement(ForEachStatement src) {
        throw new NotSupportedException();
    }
    protected virtual T VisitBreakStatement(BreakStatement src) {
        throw new NotSupportedException();
    }
    protected virtual T VisitContinueStatement(ContinueStatement src) {
        throw new NotSupportedException();
    }
    protected virtual T VisitBinaryOperatorExpression(BinaryOperatorExpression src) {
        throw new NotSupportedException();
    }
    protected virtual T VisitUnaryOperatorExpression(UnaryOperatorExpression src) {
        throw new NotSupportedException();
    }
    protected virtual T VisitAssignExpression(CsharpAssignExpression src) {
        throw new NotSupportedException();
    }
    protected virtual T VisitIncrementDecrementExpression(IncrementDecrementExpression src) {
        throw new NotSupportedException();
    }
    protected virtual T VisitElementAccessExpression(ElementAccessExpression src) {
        throw new NotSupportedException();
    }
    protected virtual T VisitConditionalExpression(ConditionalExpression src) {
        throw new NotSupportedException();
    }
    protected virtual T VisitCompileResult(CompileResult src) {
        throw new NotSupportedException();
    }
    protected virtual T VisitParenthesizedExpression(ParenthesizedExpression src) {
        throw new NotSupportedException();
    }
    protected virtual T VisitUnknownIdentifierValue(UnknownIdentifierValue src) {
        throw new NotSupportedException();
    }
    protected virtual T VisitIValueTable_PseudoValue(IValueTable_PseudoValue src) {
        throw new NotSupportedException();
    }
    protected virtual T VisitIValueTable2_PseudoValue(IValueTable2_PseudoValue src) {
        throw new NotSupportedException();
    }
    protected virtual T VisitMethodExpression(MethodExpression src) {
        throw new NotSupportedException();
    }
    protected virtual T VisitLambdaExpression(LambdaExpression src) {
        throw new NotSupportedException();
    }
    protected virtual T VisitSwitchStatement(CsharpSwitchStatement src) {
        throw new NotSupportedException();
    }
    protected virtual T VisitSwichSection(CsharpSwichSection src) {
        throw new NotSupportedException();
    }
    protected virtual T VisitSwichLabel(CsharpSwichLabel src) {
        throw new NotSupportedException();
    }
}
}
