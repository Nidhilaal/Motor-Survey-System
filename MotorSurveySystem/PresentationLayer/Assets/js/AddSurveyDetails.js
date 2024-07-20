function FnGetCurrencyExchange() {
    try {
        var qty = $("#txtSurdQty").val();
        var unitPrice = $("#txtSurdUnitPrice").val();
        var currency = $("#txtSurrCurr").val();

        var foreigncurrency = qty * unitPrice;
        $("#ContentPlaceHolder1_txtSurdFcAmt").val(foreigncurrency.toFixed(2));

        if (currency && foreigncurrency) {
            $.ajax({
                type: "POST",
                url: "../../WebServices/CurrencyConversion.asmx/Conversion",
                data: JSON.stringify({ "pForeignCurrency": foreigncurrency, "pCurrencyValue": currency }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {
                    if (result.d !== 0) {
                        var lcAmt = result.d;
                        $("#ContentPlaceHolder1_txtSurdLcAmt").val(lcAmt.toFixed(2));
                    }
                    else {
                        alert("No Value ");
                    }
                },
                error: function (xhr, status, error) {
                    alert("Error : " + error);
                }
            });
        }
    } catch (ex) {
        alert("An error occurred: " + ex);
    }
}