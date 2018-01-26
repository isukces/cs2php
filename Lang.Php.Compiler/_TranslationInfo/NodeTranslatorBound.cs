using System.Reflection;

namespace Lang.Php.Compiler
{
    /// <summary>
    ///     Zawiera opakowanie dla jednego translatora i potrafi go wywołać
    /// </summary>
    public class NodeTranslatorBound
    {
        /// <summary>
        ///     Tworzy instancję obiektu
        ///     <param name="method"></param>
        ///     <param name="targetObject"></param>
        ///     <param name="gpMethod"></param>
        /// </summary>
        public NodeTranslatorBound(MethodInfo method, object targetObject, MethodInfo gpMethod)
        {
            Method       = method;
            TargetObject = targetObject;
            GpMethod     = gpMethod;
        }

        public override string ToString()
        {
            return string.Format("NodeTranslatorBound {0} {1} for {2}",
                Priority,
                TargetObject.GetType().ExcName(),
                Method.GetParameters()[1].ParameterType.ExcName());
        }

        public IPhpValue Translate(IExternalTranslationContext ctx, object node)
        {
            return Method.Invoke(TargetObject, new[] {ctx, node}) as IPhpValue;
        }

        private int GetPriority()
        {
            if (!_priority.HasValue) _priority = (int)GpMethod.Invoke(TargetObject, new object[0]);
            return _priority.Value;
            //   return method.Invoke(targetObject, new object[] { ctx, node }) as IPhpValue;
        }

        private int? _priority;

        /// <summary>
        /// </summary>
        public MethodInfo Method { get; set; }

        /// <summary>
        /// </summary>
        public MethodInfo GpMethod { get; set; }

        /// <summary>
        /// </summary>
        public object TargetObject { get; set; }

        /// <summary>
        ///     Własność jest tylko do odczytu.
        /// </summary>
        public int? Priority => GetPriority();
    }
}