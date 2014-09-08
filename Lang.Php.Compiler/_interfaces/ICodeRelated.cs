using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Lang.Php.Compiler
{

    /// <summary>
    /// Interfejs obiektu, który może żądać dostępu do innych obiektów, np. include modułu
    /// </summary>
    public interface ICodeRelated
    {
        /// <summary>
        /// Zwraca listę żądań o dostęp do innych elementów kodu
        /// </summary>
        /// <returns></returns>
        IEnumerable<ICodeRequest> GetCodeRequests();
    }
}
