using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace PresentationLayer.WebServices
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]
    public class CurrencyConversion : System.Web.Services.WebService
    {

        [WebMethod]
        public double Conversion(double pForeignCurrency, double pCurrencyValue)
        {
            double localCurrency = pForeignCurrency * pCurrencyValue;
            return localCurrency;
        }
    }
}
