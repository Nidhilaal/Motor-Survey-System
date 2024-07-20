$(function () {
    $("#txtClaimFromDate").datepicker(
        {
            changeMonth: true,
            changeYear: true,
            dateFormat: 'dd-mm-yy',
            maxDate: 0
        });
    $("#txtClaimFromDate").attr('readonly', 'readonly');
});

$(function () {
    $("#txtClaimPolicyLossDate").datepicker({
        changeMonth: true,
        changeYear: true,
        dateFormat: 'dd-mm-yy',
        maxDate: 0
    });
    $("#txtClaimPolicyLossDate").attr('readonly', 'readonly');
});

$(function () {
    $("#txtClaimIntmDate").datepicker({
        changeMonth: true,
        changeYear: true,
        dateFormat: 'dd-mm-yy',
        maxDate: 0
    });
    $("#txtClaimIntmDate").attr('readonly', 'readonly');
});
$(function () {
    $("#txtClaimRegnDate").datepicker({
        changeMonth: true,
        changeYear: true,
        dateFormat: 'dd-mm-yy',
        maxDate: 0
    });
    $("#txtClaimRegnDate").attr('readonly', 'readonly');
});