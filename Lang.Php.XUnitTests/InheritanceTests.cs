using System;
using System.Collections.Generic;
using System.Linq;
using Lang.Php.Compiler;
using Xunit;

namespace Lang.Php.XUnitTests
{
    public class InheritanceTests
    {
        private class SampleIPhpStatement : IPhpStatement 
        {
            public IEnumerable<ICodeRequest> GetCodeRequests()
            {
                throw new NotImplementedException();
            }

            public void Emit(PhpSourceCodeEmiter emiter, PhpSourceCodeWriter writer, PhpEmitStyle style)
            {
                throw new NotImplementedException();
            }

            public StatementEmitInfo GetStatementEmitInfo(PhpEmitStyle style)
            {
                throw new NotImplementedException();
            }
        }

        [Fact]
        public static void TestICodeRelated()
        {
            var a = new SampleIPhpStatement();
            Assert.True(a is ICodeRelated);

            var types = from assembly in AppDomain.CurrentDomain.GetAssemblies()
                from type in assembly.GetTypes()
                select type;
            foreach (var type in types)
            {
                var g = type.GetInterfaces();
                if (g.FirstOrDefault(q => q == typeof (IPhpStatement)) != null)
                {
                    if (g.FirstOrDefault(q => q == typeof(ICodeRelated)) == null)
                    {
                        throw new Exception(string.Format("type {0} implements IPhpStatement but not ICodeRequest", type));
                    }
                }
            }

           
        }
    }
}
