using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Lang.Cs.Compiler
{

    /*
    smartClass
    option NoAdditionalFile
    implement ToString ##Summary##
    
    property Summary string 
    
    property Returns string 
    
    property Parameters Dictionary<string, string> 
    	init #
    smartClassEnd
    */

    public partial class DeclarationItemDescription
    {

        public static DeclarationItemDescription Parse(SyntaxNode node)
        {
            // wywalili  SyntaxKind.DocumentationCommentTrivia
            var documentationCommentTrivia =
                      node.GetLeadingTrivia()
                      .SingleOrDefault(t => t.Kind() == (SyntaxKind)0/* SyntaxKind.DocumentationCommentTrivia */);
            if (documentationCommentTrivia.Kind() == SyntaxKind.None)
                return null;
            var result = new DeclarationItemDescription();
            {
                var doc = (StructuredTriviaSyntax)documentationCommentTrivia.GetStructure();
                var docChilds = doc.ChildNodes().OfType<XmlElementSyntax>().ToArray();
                foreach (var child in docChilds)
                {
                    var xmlNameSyntax = child.StartTag.Name;
                    var text = xmlNameSyntax.GetText().ToString();
                    switch (text)
                    {
                        case "summary":
                            result.Summary = GetText(child);
                            break;
                        case "param":
                        {
                            var xmlAttributeSyntax = child.StartTag.Attributes.SingleOrDefault(i => i.Name.ToString() == "name");
                            if (xmlAttributeSyntax == null) continue;
                            var name = xmlAttributeSyntax.Name.ToString();
                            var p = GetText(child);
                            if (p != "")
                                result.Parameters[name] = p;
                        }
                            break;
                        case "returns":
                            result.Returns = GetText(child);
                            break;
                        default:
                            throw new NotSupportedException();
                    }
                }
            }
            return result;
        }

        private static string GetText(XmlElementSyntax child)
        {
            var lines = new List<string>();
            foreach (var textSyntax in child.Content.OfType<XmlTextSyntax>())
            {
                var lines2 = from textLine in textSyntax.GetText().Lines
                    let line = textLine.ToString().TrimStart()
                    let text = (line.StartsWith("///") ? line.Substring(3).TrimStart() : line).Trim()
                    where !string.IsNullOrEmpty(text)
                    select text;
                lines.AddRange(lines2);
            }
            var joined = string.Join("\r\n", lines);
            return joined.Trim();
        }
    }
}


// -----:::::##### smartClass embedded code begin #####:::::----- generated 2013-11-26 13:00
// File generated automatically ver 2013-07-10 08:43
// Smartclass.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0c4d5d36fb5eb4ac
namespace Lang.Cs.Compiler
{
    public partial class DeclarationItemDescription
    {
        /*
        /// <summary>
        /// Tworzy instancję obiektu
        /// </summary>
        public DeclarationItemDescription()
        {
        }

        Przykłady użycia

        implement INotifyPropertyChanged
        implement INotifyPropertyChanged_Passive
        implement ToString ##Summary## ##Returns## ##Parameters##
        implement ToString Summary=##Summary##, Returns=##Returns##, Parameters=##Parameters##
        implement equals Summary, Returns, Parameters
        implement equals *
        implement equals *, ~exclude1, ~exclude2
        */
        #region Constants
        /// <summary>
        /// Nazwa własności Summary; 
        /// </summary>
        public const string PROPERTYNAME_SUMMARY = "Summary";
        /// <summary>
        /// Nazwa własności Returns; 
        /// </summary>
        public const string PROPERTYNAME_RETURNS = "Returns";
        /// <summary>
        /// Nazwa własności Parameters; 
        /// </summary>
        public const string PROPERTYNAME_PARAMETERS = "Parameters";
        #endregion Constants

        #region Methods
        /// <summary>
        /// Zwraca tekstową reprezentację obiektu
        /// </summary>
        /// <returns>Tekstowa reprezentacja obiektu</returns>
        public override string ToString()
        {
            return string.Format("{0}", summary);
        }

        #endregion Methods

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public string Summary
        {
            get
            {
                return summary;
            }
            set
            {
                value = (value ?? String.Empty).Trim();
                summary = value;
            }
        }
        private string summary = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        public string Returns
        {
            get
            {
                return returns;
            }
            set
            {
                value = (value ?? String.Empty).Trim();
                returns = value;
            }
        }
        private string returns = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        public Dictionary<string, string> Parameters
        {
            get
            {
                return parameters;
            }
            set
            {
                parameters = value;
            }
        }
        private Dictionary<string, string> parameters = new Dictionary<string, string>();
        #endregion Properties

    }
}
