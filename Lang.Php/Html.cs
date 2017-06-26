using Lang.Php.Runtime;
using System;
using System.Text;

namespace Lang.Php
{
    public class Html
    {

        [AsValue]
        public const string BR_NL = "<br />\r\n";
        public static string Pixels(object px)
        {
            throw new NotSupportedException();
        }
        public static string Percent(object px)
        {
            throw new NotSupportedException();
        }
        public static string Mm(object px)
        {
            throw new NotSupportedException();
        }


        public static string Css(params object[] atts)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 1; i < atts.Length; i += 2)
            {
                var k = PhpValues.ToPhpCodeValue(atts[i - 1]);
                var v = PhpValues.ToPhpCodeValue(atts[i]);
                sb.AppendFormat("{0}: {1};", k, v);
            }
            return sb.ToString();
        }

        public static string CssBorder(object width, object style, object color)
        {
            width = PhpValues.ToPhpCodeValue(width,true);
            style = PhpValues.ToPhpCodeValue(style, true);
            color = PhpValues.ToPhpCodeValue(color, true);
            return string.Format("{0} {1} {2}", width, style, color);
        }
        public static string TagOpen(object tagname, params object[] atts)
        {
            PhpStringBuilder sb = new PhpStringBuilder();
            sb.Add("<" + PhpValues.ToPhpCodeValue(tagname));
            for (int i = 1; i < atts.Length; i += 2)
                sb.AddFormat(" {0}=\"{1}\"", atts[i - 1], atts[i]);
            sb.Add(">");
            return sb.ToString(); throw new MockMethodException();
        }
        public static string TagBound(object tagname, object inside, params object[] atts)
        {
            return TagOpen(tagname, atts) + PhpValues.ToPhpCodeValue(inside) + TagClose(tagname);
        }
        public static void EchoTagBound(object tagname, object inside, params object[] atts)
        {
            PhpDummy.echo(TagBound(tagname, inside, atts));
        }



        public static void EchoTagOpen(object tagname, params object[] atts)
        {
            PhpDummy.echo(TagOpen(tagname, atts));
        }
        public static string TagOpenOpen(object tagname, params object[] atts)
        {
            PhpStringBuilder sb = new PhpStringBuilder();
            sb.Add("<" + PhpValues.ToPhpCodeValue(tagname));
            for (int i = 1; i < atts.Length; i += 2)
                sb.AddFormat(" {0}=\"{1}\"", atts[i - 1], atts[i]);
            // sb.Add(">");
            return sb.ToString();
        }
        public static void EchoTagOpenOpen(object tagname, params object[] atts)
        {
            PhpDummy.echo(TagOpenOpen(tagname, atts));
        }

        public static string Attributes(params object[] atts)
        {
            throw new MockMethodException();
        }

        public static void EchoAttributes(params object[] atts)
        {
            throw new MockMethodException();
        }

        public static void EchoTagClose(object tagname, bool addNL =false)
        {
            PhpDummy.echo(TagClose(tagname));
            if (addNL)
                PhpDummy.echo(PhpDummy.PHP_EOL);
        }
        public static string TagClose(object tagname)
        {
            return string.Format("</{0}>", PhpValues.ToPhpCodeValue(tagname));
        }

        public static void EchoComment(object comment)
        {
            throw new MockMethodException();
        }

        public static string Comment(string comment)
        {
            throw new MockMethodException();
        }

        /// <summary>
        /// like 'br' or 'img'
        /// </summary>
        /// <param name="tagname"></param>
        /// <param name="atts"></param>
        /// <returns></returns>
        public static void EchoTagSingle(object tagname, params object[] atts)
        {
            PhpDummy.echo(TagSingle(tagname, atts));
        }

        /// <summary>
        /// like 'br' or 'img'
        /// </summary>
        /// <param name="tagname"></param>
        /// <param name="atts"></param>
        /// <returns></returns>
        public static string TagSingle(object tagname, params object[] atts)
        {
            PhpStringBuilder sb = new PhpStringBuilder();
            sb.Add("<" + PhpValues.ToPhpCodeValue(tagname));
            for (int i = 1; i < atts.Length; i += 2)
                sb.AddFormat(" {0}=\"{1}\"", atts[i - 1], atts[i]);
            sb.Add(" />");
            return sb.ToString();
        }
    }
}
