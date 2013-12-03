
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Lang.Cs.Compiler;

namespace Lang.Php.Compiler
{
    public partial class FullClassDeclaration {
      public FullClassDeclaration(string FullName, ClassDeclaration ClassDeclaration, NamespaceDeclaration NamespaceDeclaration){
        this.fullName = FullName;
        this.classDeclaration = ClassDeclaration;
        this.namespaceDeclaration = NamespaceDeclaration;
      }
      public string FullName {
        get {
          return fullName;
        }
      }
      private string fullName;
      public ClassDeclaration ClassDeclaration {
        get {
          return classDeclaration;
        }
      }
      private ClassDeclaration classDeclaration;
      public NamespaceDeclaration NamespaceDeclaration {
        get {
          return namespaceDeclaration;
        }
      }
      private NamespaceDeclaration namespaceDeclaration;
    } // end of FullClassDeclaration
}
