using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace CurrencyConversion
{
    /// <summary>
    /// Summary description for Conversion
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class Conversion : System.Web.Services.WebService
    {

        [WebMethod]
        public void CurrencyConversion(decimal amount, string fromCurrency, string toCurrency)
        {
            //WebClient web = new WebClient();
            //string url = string.Format("https://www.google.com/finance/converter?fromCurrency={0}&toCurrency={1}", fromCurrency.ToUpper(), toCurrency.ToUpper(), amount);
            //string response = web.DownloadString(url);
            //Regex regex = new Regex(@":(?<rhs>.+?),");
            //string[] arrDigits = regex.Split(response);
            //string rate = arrDigits[3];
            //return rate;
        }
    }
}
