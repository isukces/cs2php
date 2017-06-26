namespace Lang.Php.Examples.BasicFeaturesExample
{

    [Module("class.multiplication-table")]
    public class MultiplicationTable : PhpDummy
    {
        [ScriptName("show_multiplication_table")]
        public static void ShowMultiplicationTable(int maxCol, int maxRow)
        {
            echo("<table>\r\n");
            for (int rowIdx = 1; rowIdx <= maxRow; rowIdx++)
            {
                echo("<tr>\r\n");
                for (int ColIdx = 1; ColIdx <= maxCol; ColIdx++)
                {
                    echo("<td>");
                    Response.Echo(rowIdx * ColIdx);
                    echo("</td>\r\n");
                }
                echo("</tr>\r\n");
            }
            echo("</table>\r\n");
        }
    }
}
