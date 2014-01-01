using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lang.Php.Mpdf
{
    /// <summary>
    /// Only simple facade....
    /// </summary>    
    [Module("mpdf")]
    [IgnoreNamespace]
    [ScriptName("mPDF")]
    public class Mpdf
    {
        #region Constructors

        public Mpdf(string mode, string format, double fontSize, string fontFamily, double mLeft, double mRight, double mTop, double mBottom, double mHeader, double mFooter, PageOrientation orientation)
        {

        }

        #endregion Constructors

        #region Methods

        // Public Methods 

        public void Output(string name = "", string dest = "")
        {

        }

        public void SetWatermarkText(string txt = "", double alpha = -1)
        {
            if (alpha >= 0) watermarkTextAlpha = alpha;
            watermarkText = txt;
        }

        public void WriteHTML(string html, int sub = 0, bool init = true, bool close = true)
        {

        }

        #endregion Methods

        #region Fields

        public string author;
        [AsValue]
        public const string htmlpagefooter = "htmlpagefooter"; 
        [AsValue]
        public const string sethtmlpagefooter = "sethtmlpagefooter";
        public bool showWatermarkText;
        public string subject;
        public string title;
        public string watermark_font;
        public string watermarkText = "";
        public double watermarkTextAlpha = 0;

        #endregion Fields
    }
}
/*
 * 1	$mpdf = new mPDF('',    // mode - default ''
2	 '',    // format - A4, for example, default ''
3	 0,     // font size - default 0
4	 '',    // default font family
5	 15,    // margin_left
6	 15,    // margin right
7	 16,     // margin top
8	 16,    // margin bottom
9	 9,     // margin header
10	 9,     // margin footer
11	 'L');  // L - landscape, P - portrait
 */
