using System.Collections.Generic;
using System.Linq;

namespace Lang.Php.Compiler.Source
{
    public class PhpForStatement : PhpStatementBase
    {
        /// <summary>
        ///     Tworzy instancję obiektu
        ///     <param name="initVariables"></param>
        ///     <param name="condition"></param>
        ///     <param name="statement"></param>
        ///     <param name="incrementors"></param>
        /// </summary>
        public PhpForStatement(PhpAssignExpression[] initVariables, IPhpValue condition, IPhpStatement statement,
            IPhpStatement[]                          incrementors)
        {
            InitVariables = initVariables;
            Condition     = condition;
            Statement     = statement;
            Incrementors  = incrementors;
        }

        // Private Methods 

        private static string Collect(PhpSourceCodeEmiter emiter, PhpEmitStyle style, IPhpStatement[] collection)
        {
            var list             = new List<string>();
            var xStyle           = PhpEmitStyle.xClone(style);
            xStyle.AsIncrementor = true;
            foreach (var item in collection)
            {
                var writer = new PhpSourceCodeWriter();
                writer.Clear();
                item.Emit(emiter, writer, xStyle);
                list.Add(writer.GetCode(true).Trim());
            }

            return string.Join(", ", list);
        }

        private static string Collect(PhpSourceCodeEmiter emiter, PhpEmitStyle style, IPhpValue[] collection)
        {
            var list             = new List<string>();
            var xStyle           = PhpEmitStyle.xClone(style);
            xStyle.AsIncrementor = true;
            foreach (var item in collection) list.Add(item.GetPhpCode(xStyle));
            return string.Join(", ", list);
        }

        // Public Methods 

        public override void Emit(PhpSourceCodeEmiter emiter, PhpSourceCodeWriter writer, PhpEmitStyle style)
        {
            var phpIncrementrors = Collect(emiter, style, Incrementors);
            var phpInitVariables = Collect(emiter, style, InitVariables);

            style = style ?? new PhpEmitStyle();

            var header =
                style.Compression == EmitStyleCompression.Beauty
                    ? "for({0}; {1}; {2})"
                    : "for({0};{1};{2})";
            header = string.Format(header, phpInitVariables, Condition.GetPhpCode(style), phpIncrementrors);

            EmitHeaderStatement(emiter, writer, style, header, Statement);
        }

        public override IEnumerable<ICodeRequest> GetCodeRequests()
        {
            var a = GetCodeRequests<PhpAssignExpression>(InitVariables);
            var b = GetCodeRequests<IPhpStatement>(Incrementors);
            var c = GetCodeRequests(Condition, Statement);
            return a.Union(b).Union(c);
        }

        public override IPhpStatement Simplify(IPhpSimplifier s)
        {
            var initVariables = InitVariables == null
                ? null
                : InitVariables.Select(u => s.Simplify(u)).Cast<PhpAssignExpression>().ToArray();
            var condition    = s.Simplify(Condition);
            var statement    = s.Simplify(Statement);
            var incrementors = Incrementors == null
                ? null
                : Incrementors.Select(u => s.Simplify(u)).ToArray();
            var theSame = EqualCode(condition, Condition) && EqualCode(statement, Statement) &&
                          EqualCode_Array(initVariables, InitVariables) &&
                          EqualCode_Array(incrementors,  Incrementors);
            if (theSame)
                return this;
            return new PhpForStatement(initVariables, condition, statement, incrementors);
        }


        /// <summary>
        /// </summary>
        public PhpAssignExpression[] InitVariables { get; set; }

        /// <summary>
        /// </summary>
        public IPhpValue Condition { get; set; }

        /// <summary>
        /// </summary>
        public IPhpStatement Statement { get; set; }

        /// <summary>
        /// </summary>
        public IPhpStatement[] Incrementors { get; set; }
    }
}