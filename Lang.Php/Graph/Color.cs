namespace Lang.Php.Graph
{
    [Skip]
    public class Color
    {
        [UseBinaryExpression("!==", "false", "$0")]
        public static bool IsValid(Color image)
        {
            return image != null;
        }
    }
}
