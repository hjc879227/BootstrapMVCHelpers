function CheckForClientValidation() {
    var elems = $('.field-validation-valid.text-error');
    elems.removeClass('text-error');
    elems.parents('.control-group').removeClass('error');

    elems = $('.field-validation-error').not('.text-error');
    elems.addClass('text-error');
    elems.parents('.control-group').addClass('error');

    elems = $('.validation-summary-errors').not('.text-error');
    elems.addClass('alert').addClass('alert-error');

    setTimeout(CheckForClientValidation, 300);
}