using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Lang.Cs.Compiler
{
    public class DeclarationItemDescription
    {
        public static DeclarationItemDescription Parse(SyntaxNode node)
        {
            // wywalili  SyntaxKind.DocumentationCommentTrivia
            var documentationCommentTrivia =
                node.GetLeadingTrivia()
                    .SingleOrDefault(t => t.Kind() == (SyntaxKind)0 /* SyntaxKind.DocumentationCommentTrivia */);
            if (documentationCommentTrivia.Kind() == SyntaxKind.None)
                return null;
            var result = new DeclarationItemDescription();
            {
                var doc       = (StructuredTriviaSyntax)documentationCommentTrivia.GetStructure();
                var docChilds = doc.ChildNodes().OfType<XmlElementSyntax>().ToArray();
                foreach (var child in docChilds)
                {
                    var xmlNameSyntax = child.StartTag.Name;
                    var text          = xmlNameSyntax.GetText().ToString();
                    switch (text)
                    {
                        case "summary":
                            result.Summary = GetText(child);
                            break;
                        case "param":
                        {
                            var xmlAttributeSyntax =
                                child.StartTag.Attributes.SingleOrDefault(i => i.Name.ToString() == "name");
                            if (xmlAttributeSyntax == null) continue;
                            var name = xmlAttributeSyntax.Name.ToString();
                            var p    = GetText(child);
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


        /// <summary>
        ///     Zwraca tekstową reprezentację obiektu
        /// </summary>
        /// <returns>Tekstowa reprezentacja obiektu</returns>
        public override string ToString()
        {
            return string.Format("{0}", _summary);
        }

        /// <summary>
        /// </summary>
        public string Summary
        {
            get => _summary;
            set => _summary = (value ?? string.Empty).Trim();
        }

        /// <summary>
        /// </summary>
        public string Returns
        {
            get => _returns;
            set => _returns = (value ?? string.Empty).Trim();
        }

        /// <summary>
        /// </summary>
        public Dictionary<string, string> Parameters { get; set; } = new Dictionary<string, string>();

        private string _summary = string.Empty;
        private string _returns = string.Empty;
    }
}