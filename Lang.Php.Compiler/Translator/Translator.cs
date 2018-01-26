using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Lang.Cs.Compiler;
using Lang.Php.Compiler.Source;

namespace Lang.Php.Compiler.Translator
{
    public class Translator
    {
        public Translator(TranslationState translationState)
        {
            if (translationState == null)
                throw new ArgumentNullException(nameof(translationState));
#if DEBUG
            if (translationState == null)
                throw new ArgumentNullException("translationState");

#endif
            _state = translationState; // ?? new TranslationState(new TranslationInfo());
            Info   = translationState.Principles;
        }

        // Private Methods 

        /*
                static IPhpStatement[] MkArray(IPhpStatement x)
                {
                    return new IPhpStatement[] { x };
                }
        */

        // Public Methods 


        public void Translate(AssemblySandbox sandbox)
        {
            var classes            = Info.GetClasses();
            var classesToTranslate = Info.ClassTranslations.Values
                .Where(u => u.Type.Assembly.FullName == Info.CurrentAssembly.FullName).ToArray();
            //            classesToTranslate = (from i in _info.ClassTranslations.Values
            //                                      where i.Type.Assembly.FullName == _info.CurrentAssembly.FullName
            //                                      select this.ge.ToArray();
            var interfaces = Info.GetInterfaces();
            //     var interfacesToTranslate = info.ClassTranslations.Values.Where(u => u.Type.Assembly == info.CurrentAssembly).ToArray();
            foreach (var classTranslationInfo in classesToTranslate)
            {
                if (classTranslationInfo.Skip)
                    Debug.Write("");
                PhpClassDefinition phpClass;
                var                phpModule = GetOrMakeModuleByName(classTranslationInfo.ModuleName);
                // var assemblyTI = _info.GetOrMakeTranslationInfo(_info.CurrentAssembly);

                {
                    PhpQualifiedName phpBaseClassName;
                    {
                        var netBaseType = classTranslationInfo.Type.BaseType;
                        if ((object)netBaseType == null || netBaseType == typeof(object))
                        {
                            phpBaseClassName = PhpQualifiedName.Empty;
                        }
                        else
                        {
                            // _state.Principles.CurrentTyp is null so we will obtain absolute name
                            phpBaseClassName =
                                _state.Principles.GetPhpType(netBaseType, true, null); // absolute name
                            var baseTypeTranslationInfo = _state.Principles.GetOrMakeTranslationInfo(netBaseType);
                            if (baseTypeTranslationInfo.Skip)
                                phpBaseClassName = PhpQualifiedName.Empty;
                        }
                    }
                    phpClass = phpModule.FindOrCreateClass(classTranslationInfo.ScriptName, phpBaseClassName);
                }
                _state.Principles.CurrentType     = classTranslationInfo.Type;
                _state.Principles.CurrentAssembly = _state.Principles.CurrentType.Assembly;
                Console.WriteLine(classTranslationInfo.ModuleName);

                IClassMember[] members;

                if (classTranslationInfo.Type.IsInterface)
                {
                    var sources = interfaces.Where(i => i.FullName == classTranslationInfo.Type.FullName).ToArray();
                    members     = (from i in sources
                        from j in i.ClassDeclaration.Members
                        select j).ToArray();
                    {
                        var fileNames = classTranslationInfo.Type.GetCustomAttributes<RequireOnceAttribute>()
                            .Select(i => i.Filename).Distinct().ToArray();
                        if (fileNames.Any())
                        {
                            var b = fileNames.Select(u => new PhpConstValue(u)).ToArray();
                            phpModule.RequiredFiles.AddRange(b);
                        }
                    }
                }
                else
                {
                    var sources = classes.Where(i => i.FullName == classTranslationInfo.Type.FullName).ToArray();
                    members     = (from i in sources
                        from j in i.ClassDeclaration.Members
                        select j).ToArray();
                    {
                        var fileNames = classTranslationInfo.Type.GetCustomAttributes<RequireOnceAttribute>()
                            .Select(i => i.Filename).Distinct().ToArray();
                        if (fileNames.Any())
                        {
                            var b = fileNames.Select(u => new PhpConstValue(u)).ToArray();
                            phpModule.RequiredFiles.AddRange(b);
                        }
                    }
                }

                {
                    var c = members.OfType<ConstructorDeclaration>().ToArray();
                    if (c.Length > 1)
                        throw new Exception("PHP supports only one constructor per class");
                    if (c.Any())
                        TranslateConstructor(phpClass, c.First());
                }

                {
                    foreach (var methodDeclaration in members.OfType<MethodDeclaration>())
                        TranslateMethod(phpClass, methodDeclaration);
                }

                {
                    foreach (var pDeclaration in members.OfType<CsharpPropertyDeclaration>())
                        TranslateProperty(phpClass, pDeclaration);
                }

                {
                    foreach (var constDeclaration in members.OfType<FieldDeclaration>())
                        TranslateField(phpModule, phpClass, constDeclaration);
                }

                _state.Principles.CurrentType = null;
                {
                    if (classTranslationInfo.IsPage)
                    {
                        {
                            var ati = Info.GetOrMakeTranslationInfo(Info.CurrentAssembly);
                            if (ati.DefaultTimezone.HasValue)
                            {
                                // date_default_timezone_set('America/Los_Angeles');
                                var a                      = new PhpValueTranslator(_state);
                                var aa                     = a.Visit(new ConstValue(ati.DefaultTimezone.Value));
                                var dateDefaultTimezoneSet =
                                    new PhpMethodCallExpression("date_default_timezone_set", aa);
                                phpModule.BottomCode.Statements.Add(new PhpExpressionStatement(dateDefaultTimezoneSet));
                            }
                        }

                        {
                            var mti = MethodTranslationInfo.FromMethodInfo(classTranslationInfo.PageMethod,
                                classTranslationInfo);
                            var callMain = new PhpMethodCallExpression(mti.ScriptName);
                            callMain.SetClassName(
                                classTranslationInfo.ScriptName,
                                mti
                            );
                            phpModule.BottomCode.Statements.Add(new PhpExpressionStatement(callMain));
                        }
                    }
                }

                {
                    var moduleCodeRequests = new List<ModuleCodeRequest>();
                    var codeRequests       = (phpModule as ICodeRelated).GetCodeRequests().ToArray();
                    {
                        var classCodeRequests = (from request in codeRequests.OfType<ClassCodeRequest>()
                                where request.ClassName != null
                                select request.ClassName.FullName)
                            .Distinct()
                            .ToArray();

                        foreach (var req in classCodeRequests)
                        {
                            var m = Info.ClassTranslations.Values.Where(i => i.ScriptName.FullName == req).ToArray();
                            if (m.Length != 1)
                                throw new NotSupportedException();
                            var mm = m[0];
                            if (mm.DontIncludeModuleForClassMembers)
                                continue;
                            var includeModule = mm.IncludeModule;
                            if (includeModule == null || mm.ModuleName == phpModule.Name)
                                continue;
                            var h = new ModuleCodeRequest(includeModule, "class request: " + req);
                            moduleCodeRequests.Add(h);
                        }
                    }
                    {
                        var moduleRequests = (from i in codeRequests.OfType<ModuleCodeRequest>()
                            where i.ModuleName != null
                            select i).Union(moduleCodeRequests).ToArray();
                        var moduleNames = (from mReq in moduleRequests
                            where mReq.ModuleName != phpModule.Name
                            let mName = mReq.ModuleName
                            where mName != null
                            select mName
                        ).Distinct().ToArray();
                        foreach (var i in moduleNames.Where(x => !PhpCodeModuleName.IsFrameworkName(x)))
                            AppendCodeReq(i, phpModule);
                    }
                }
            }

            {
                var emptyModules = Modules.Where(a => a.IsEmpty).ToArray();
                foreach (var module in Modules)
                {
                    // if (module.IsEmpty) 
                }
            }
        }

        public IPhpStatement[] TranslateStatement(IStatement x)
        {
            if (!(x is CSharpBase)) throw new Exception("Błąd translacji " + x.GetType().FullName);
            var op = new OptimizeOptions();

            var s      = new PhpStatementSimplifier(op);
            var a      = new PhpStatementTranslatorVisitor(_state);
            var tmp    = a.Visit(x as CSharpBase);
            var result = new List<IPhpStatement>(tmp.Length);
            result.AddRange(tmp.Select(i => s.Visit(i as PhpSourceBase)));
            return result.ToArray();
        }

        private void AppendCodeReq(PhpCodeModuleName req, PhpCodeModule current)
        {
            if (req == current.Name)
                return;
            if (req.Name == PhpCodeModuleName.CS2PHP_CONFIG_MODULE_NAME)
            {
                var phpModule = CurrentConfigModule();
                req           = phpModule.Name;
            }

            if (req.AssemblyInfo != null && !string.IsNullOrEmpty(req.AssemblyInfo.IncludePathConstOrVarName))
            {
                var isCurrentAssembly = Info.CurrentAssembly == req.AssemblyInfo.Assembly;
                if (!isCurrentAssembly)
                {
                    var tmp = req.AssemblyInfo.IncludePathConstOrVarName;
                    if (tmp.StartsWith("$"))
                        throw new NotSupportedException();
                    // leading slash is not necessary -> config is in global namespace
                    // but full name is a key in dictionary
                    var phpModule = CurrentConfigModule();
                    if (phpModule.DefinedConsts.All(i => i.Key != tmp))
                    {
                        KnownConstInfo value;
                        if (Info.KnownConstsValues.TryGetValue(tmp, out value))
                        {
                            if (!value.UseFixedValue)
                            {
                                var expression = PathUtil.MakePathValueRelatedToFile(value, Info);
                                phpModule.DefinedConsts.Add(new KeyValuePair<string, IPhpValue>(tmp, expression));
                            }
                            else
                            {
                                throw new NotImplementedException();
                            }
                        }
                        else
                        {
                            Info.Log(MessageLevels.Error,
                                string.Format("const {0} defined in {1} has no known value", tmp, phpModule.Name));
                            phpModule.DefinedConsts.Add(
                                new KeyValuePair<string, IPhpValue>(tmp, new PhpConstValue("UNKNOWN")));
                        }
                    }
                }
            }

            var fileNameExpression = req.MakeIncludePath(current.Name);
            if (fileNameExpression == null) return;
            if (current.RequiredFiles.Any())
            {
                var s    = new PhpEmitStyle();
                var code = fileNameExpression.GetPhpCode(s);
                var a    = current.RequiredFiles.Select(i => i.GetPhpCode(s)).ToArray();
                if (a.Any(i => i == code))
                    return;
            }

            // if (fileNameExpression1 !=null)
            {
                var fileNameExpressionICodeRelated = fileNameExpression as ICodeRelated;
                // scan nested requests
                var nestedCodeRequests = fileNameExpressionICodeRelated.GetCodeRequests().ToArray();
                if (nestedCodeRequests.Any())
                {
                    var nestedModuleCodeRequests = nestedCodeRequests.OfType<ModuleCodeRequest>();
                    foreach (var nested in nestedModuleCodeRequests)
                        AppendCodeReq(nested.ModuleName, current);
                }
            }
            current.RequiredFiles.Add(fileNameExpression);
        }

        private PhpCodeModule CurrentConfigModule()
        {
            var assemblyTranslationInfo = Info.GetOrMakeTranslationInfo(Info.CurrentAssembly);
            var phpCodeModuleName       =
                new PhpCodeModuleName(assemblyTranslationInfo.ConfigModuleName, assemblyTranslationInfo);
            var phpModule = GetOrMakeModuleByName(phpCodeModuleName);
            return phpModule;
        }
        // Private Methods 

        /// <summary>
        ///     Gets existing or creates code module for given name
        /// </summary>
        /// <param name="requiredModuleName"></param>
        /// <returns></returns>
        private PhpCodeModule GetOrMakeModuleByName(PhpCodeModuleName requiredModuleName)
        {
            var mod = Modules.FirstOrDefault(i => i.Name == requiredModuleName);
            if (mod != null) return mod;
            mod = new PhpCodeModule(requiredModuleName);
            Modules.Add(mod);
            return mod;
        }

        private void Tranlate_MethodOrProperty(PhpClassDefinition phpClass, MethodInfo info, IStatement body,
            string                                                overrideName)
        {
            _state.Principles.CurrentMethod = info;
            try
            {
                var mti       = _state.Principles.GetOrMakeTranslationInfo(info);
                var phpMethod =
                    new PhpClassMethodDefinition(string.IsNullOrEmpty(overrideName) ? mti.ScriptName : overrideName);
                phpClass.Methods.Add(phpMethod);

                if (info.IsPublic)
                    phpMethod.Visibility = Visibility.Public;
                else if (info.IsPrivate)
                    phpMethod.Visibility = Visibility.Private;
                else
                    phpMethod.Visibility = Visibility.Protected;

                phpMethod.IsStatic = info.IsStatic;
                {
                    var declaredParameters = info.GetParameters();
                    foreach (var parameter in declaredParameters)
                    {
                        var phpParameter  = new PhpMethodArgument();
                        phpParameter.Name = parameter.Name;
                        phpMethod.Arguments.Add(phpParameter);
                        if (parameter.HasDefaultValue)
                            phpParameter.DefaultValue = new PhpConstValue(parameter.DefaultValue);
                    }
                }

                if (body != null)
                    phpMethod.Statements.AddRange(TranslateStatement(body));
            }
            finally
            {
                _state.Principles.CurrentMethod = null;
            }
        }

        private void TranslateConstructor(PhpClassDefinition phpClass, ConstructorDeclaration md)
        {
            //   state.Principles.CurrentMethod = md.Info;
            try
            {
                // MethodTranslationInfo mti = MethodTranslationInfo.FromMethodInfo(md.Info);
                // state.Principles.CurrentMethod = 
                var phpMethod = new PhpClassMethodDefinition("__construct");
                phpClass.Methods.Add(phpMethod);

                if (md.Info.IsPublic)
                    phpMethod.Visibility = Visibility.Public;
                else if (md.Info.IsPrivate)
                    phpMethod.Visibility = Visibility.Private;
                else
                    phpMethod.Visibility = Visibility.Protected;

                phpMethod.IsStatic = md.Info.IsStatic;
                {
                    var declaredParameters = md.Info.GetParameters();
                    foreach (var parameter in declaredParameters)
                    {
                        var phpParameter  = new PhpMethodArgument();
                        phpParameter.Name = parameter.Name;
                        phpMethod.Arguments.Add(phpParameter);
                        if (parameter.HasDefaultValue)
                            phpParameter.DefaultValue = new PhpConstValue(parameter.DefaultValue);
                    }
                }

                if (md.Body != null)
                    phpMethod.Statements.AddRange(TranslateStatement(md.Body));
            }
            finally
            {
                _state.Principles.CurrentMethod = null;
            }
        }

        private void TranslateField(PhpCodeModule module, PhpClassDefinition phpClass, FieldDeclaration field)
        {
            PhpValueTranslator phpValueTranslator = null;
            foreach (var item in field.Items)
            {
                if (item.OptionalFieldInfo == null) continue;
                var fti = Info.GetOrMakeTranslationInfo(item.OptionalFieldInfo);
                switch (fti.Destination)
                {
                    case FieldTranslationDestionations.DefinedConst:
                        if (item.Value == null)
                            throw new NotSupportedException();
                        if (phpValueTranslator == null)
                            phpValueTranslator = new PhpValueTranslator(_state);
                        var definedValue       = phpValueTranslator.TransValue(item.Value);
                    {
                        if (fti.IncludeModule != module.Name) module = GetOrMakeModuleByName(fti.IncludeModule);
                    }
                        module.DefinedConsts.Add(new KeyValuePair<string, IPhpValue>(fti.ScriptName, definedValue));
                        break;
                    case FieldTranslationDestionations.GlobalVariable:
                        if (item.Value != null)
                        {
                            IPhpValue value;
                            // muszę na chwilę wyłączyć current type, bo to jes poza klasą generowane
                            {
                                var saveCurrentType           = _state.Principles.CurrentType;
                                _state.Principles.CurrentType = null;
                                try
                                {
                                    if (phpValueTranslator == null)
                                        phpValueTranslator = new PhpValueTranslator(_state);
                                    value                  = phpValueTranslator.TransValue(item.Value);
                                }
                                finally
                                {
                                    _state.Principles.CurrentType = saveCurrentType;
                                }
                            }

                            var assign = new PhpAssignExpression(PhpVariableExpression.MakeGlobal(fti.ScriptName),
                                value);
                            module.TopCode.Statements.Add(new PhpExpressionStatement(assign));
                        }

                        break;
                    case FieldTranslationDestionations.JustValue:
                        continue; // don't define
                    case FieldTranslationDestionations.NormalField:
                    case FieldTranslationDestionations.ClassConst:
                    {
                        var def = new PhpClassFieldDefinition();
                        var cti = _state.Principles.GetTi(_state.Principles.CurrentType, true);
                        if (cti.IsArray)
                            continue;
                        if (field.Modifiers.Has("const") ^
                            (fti.Destination == FieldTranslationDestionations.ClassConst))
                            throw new Exception("beige lion");

                        def.IsConst =
                            fti.Destination ==
                            FieldTranslationDestionations.ClassConst; // field.Modifiers.Has("const");
                        def.Name = fti.ScriptName;

                        def.IsStatic = def.IsConst || field.Modifiers.Has("static");
                        if (field.Modifiers.Has("public"))
                            def.Visibility = Visibility.Public;
                        else if (field.Modifiers.Has("protected"))
                            def.Visibility = Visibility.Protected;
                        else
                            def.Visibility = Visibility.Private;

                        if (item.Value != null)
                        {
                            if (phpValueTranslator == null)
                                phpValueTranslator = new PhpValueTranslator(_state);
                            def.ConstValue         = phpValueTranslator.TransValue(item.Value);
                        }

                        phpClass.Fields.Add(def);
                        break;
                    }
                    default:
                        throw new NotSupportedException();
                }
            }
        }

        private void TranslateMethod(PhpClassDefinition phpClass, MethodDeclaration md)
        {
            Tranlate_MethodOrProperty(phpClass, md.Info, md.Body, null);
        }

        private void TranslateProperty(PhpClassDefinition phpClassDefinition,
            CsharpPropertyDeclaration                     propertyDeclaration)
        {
            var pi  = _state.Principles.CurrentType.GetProperty(propertyDeclaration.PropertyName);
            var pti = PropertyTranslationInfo.FromPropertyInfo(pi);
            if (pti.GetSetByMethod)
            {
                CsharpPropertyDeclarationAccessor accessor;
                if (!string.IsNullOrEmpty(pti.GetMethodName))
                {
                    accessor = propertyDeclaration.Accessors.FirstOrDefault(u => u.Name == "get");
                    if (accessor != null)
                        Tranlate_MethodOrProperty(phpClassDefinition, pi.GetGetMethod(), accessor.Statement,
                            pti.GetMethodName);
                }

                if (string.IsNullOrEmpty(pti.SetMethodName)) return;
                accessor = propertyDeclaration.Accessors.FirstOrDefault(u => u.Name == "set");
                if (accessor != null)
                    Tranlate_MethodOrProperty(phpClassDefinition, pi.GetSetMethod(), accessor.Statement,
                        pti.SetMethodName);
            }
            else
            {
                phpClassDefinition.Fields.Add(new PhpClassFieldDefinition
                {
                    Name     = pti.FieldScriptName,
                    IsStatic = pti.IsStatic
                });
            }
        }

        /// <summary>
        ///     Własność jest tylko do odczytu.
        /// </summary>
        public TranslationInfo Info { get; }

        /// <summary>
        ///     Własność jest tylko do odczytu.
        /// </summary>
        public List<PhpCodeModule> Modules { get; } = new List<PhpCodeModule>();

        private readonly TranslationState _state;
    }
}