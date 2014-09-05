using System;
using System.Reflection;

namespace Lang.Cs.Compiler
{

    /*
    smartClass
    option NoAdditionalFile
    
    property ReferencedAssemblies List<AssemblyWrapper> 
    	init #
    
    property Sources List<string> 
    	init #
    smartClassEnd
    */

    internal static class Compiler
    {
		#region Constructors 
#if OLD
        public Compiler(bool addCommonAssemblies)
        {
            if (!addCommonAssemblies) return;
            _referencedAssemblies.Add(typeof(System.String).Assembly);
            _referencedAssemblies.Add(typeof(List<string>).Assembly);
        }
#endif
		#endregion Constructors 

        #region Static Methods 

		// Public Methods 

        /// <summary>
        /// Sprawdza czy sekwencja dostępnych argumentów pasuje do parametrów metody
        /// </summary>
        /// <param name="givenArgumentsTypes">tablica typów dostępnych wartości</param>
        /// <param name="parameters">parametry metody</param>
        /// <param name="isExtensionMethod">czy metoda jest extension - wtedy pomijamy sprawdzanie pierwszego parametru i sprawdzamy pierwszy argument z drugim parametrem, 2 z 3, 3 z 4 itp</param>
        /// <param name="lastParameterAsSequence">jeśli <c>true</c> wtedy traktujemy ostatni parametr jako tablicę, która akceptuje zmienną ilość parametrów</param>
        /// <param name="useImplicitOperator">jeśli true to przy sprawdzaniu czy argumenty pasują prubujemy je także rzutować przy pomocy niejawnego rzutowania</param>
        /// <returns>true jeśli da się dopasować</returns>
        public static bool CheckParametesType(Type[] givenArgumentsTypes, ParameterInfo[] parameters, bool isExtensionMethod, bool lastParameterAsSequence, bool useImplicitOperator)
        {
            var availableParameterCount = givenArgumentsTypes.Length;
            var lastParamIdx = parameters.Length - 1;
            for (var idx = 0; idx < availableParameterCount; idx++)
            {
                Type given, required;
                {
                    given = givenArgumentsTypes[idx];
                    var paramIdx = Math.Min(isExtensionMethod ? idx + 1 : idx, lastParamIdx);
                    required = parameters[paramIdx].ParameterType;
                    if (lastParameterAsSequence && paramIdx == lastParamIdx)
                    {
                        if (required.IsArray)
                            required = required.GetElementType();
                        else
                            throw new Exception("dirty boa");
                    }
                }
                if (!TypesUtil.IsAssignableFrom(required, given, useImplicitOperator))
                    return false;
            }
            return true;
        }

        #endregion Static Methods 
#if OLD
        #region Methods 

		// Public Methods 

        public CompileResult[] Compile()
        {
            var compileResults = new List<CompileResult>(_sources.Count);
            foreach (var i in _sources)
                compileResults.Add(Compile(i));
            return compileResults.ToArray();
        }
 
		// Private Methods 

        CompileResult Compile(string filename)
        {
            var compileState = new CompileState();
            var langVisitor = new LangVisitor(compileState)
            {
                throwNotImplementedException = true
            };
            compileState.Context.KnownTypes = CompileState.GetAllTypes(_referencedAssemblies);
            SyntaxTree tree = SyntaxFactory.ParseSyntaxTree(File.ReadAllText(filename));
            // .ParseFile(filename);
            var root = (CompilationUnitSyntax)tree.GetRoot();
            var x = langVisitor.Visit(root) as CompilationUnit;
            return new CompileResult(filename, x);

        }

        #endregion Methods
#endif
        #region Static Fields

        public static readonly bool[] FalseTrueArray = { false, true };

		#endregion Static Fields 
    }
}
