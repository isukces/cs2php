using System.IO;
using Lang.Cs.Compiler.Visitors;
using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Lang.Cs.Compiler
{

    /*
    smartClass
    option NoAdditionalFile
    
    property ReferencedAssemblies List<Assembly> 
    	init #
    
    property Sources List<string> 
    	init #
    smartClassEnd
    */

    public partial class Compiler
    {
		#region Constructors 

        public Compiler(bool addCommonAssemblies)
        {
            if (!addCommonAssemblies) return;
            referencedAssemblies.Add(typeof(System.String).Assembly);
            referencedAssemblies.Add(typeof(List<string>).Assembly);
        }

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

		#region Methods 

		// Public Methods 

        public CompileResult[] Compile()
        {
            var compileResults = new List<CompileResult>(sources.Count);
            foreach (var i in sources)
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
            compileState.Context.KnownTypes = CompileState.GetAllTypes(referencedAssemblies);
            SyntaxTree tree = SyntaxFactory.ParseSyntaxTree(File.ReadAllText(filename));
            // .ParseFile(filename);
            var root = (CompilationUnitSyntax)tree.GetRoot();
            var x = langVisitor.Visit(root) as CompilationUnit;
            return new CompileResult(filename, x);

        }

		#endregion Methods 

		#region Static Fields 

        public static readonly bool[] _false_true_ = new bool[] { false, true };

		#endregion Static Fields 
    }
}


// -----:::::##### smartClass embedded code begin #####:::::----- generated 2013-11-05 11:04
// File generated automatically ver 2013-07-10 08:43
// Smartclass.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0c4d5d36fb5eb4ac
namespace Lang.Cs.Compiler
{
    public partial class Compiler
    {
        /*
        /// <summary>
        /// Tworzy instancję obiektu
        /// </summary>
        public Compiler()
        {
        }
        Przykłady użycia
        implement INotifyPropertyChanged
        implement INotifyPropertyChanged_Passive
        implement ToString ##ReferencedAssemblies## ##Sources##
        implement ToString ReferencedAssemblies=##ReferencedAssemblies##, Sources=##Sources##
        implement equals ReferencedAssemblies, Sources
        implement equals *
        implement equals *, ~exclude1, ~exclude2
        */


        #region Constants
        /// <summary>
        /// Nazwa własności ReferencedAssemblies; 
        /// </summary>
        public const string PROPERTYNAME_REFERENCEDASSEMBLIES = "ReferencedAssemblies";
        /// <summary>
        /// Nazwa własności Sources; 
        /// </summary>
        public const string PROPERTYNAME_SOURCES = "Sources";
        #endregion Constants


        #region Methods
        #endregion Methods


        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public List<Assembly> ReferencedAssemblies
        {
            get
            {
                return referencedAssemblies;
            }
            set
            {
                referencedAssemblies = value;
            }
        }
        private List<Assembly> referencedAssemblies = new List<Assembly>();
        /// <summary>
        /// 
        /// </summary>
        public List<string> Sources
        {
            get
            {
                return sources;
            }
            set
            {
                sources = value;
            }
        }
        private List<string> sources = new List<string>();
        #endregion Properties
    }
}
