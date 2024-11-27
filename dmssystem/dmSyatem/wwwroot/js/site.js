
    $(document).ready(function() {
        // Limit the 'shorttext' class to 20 characters (max length already set in HTML)
        $(".shorttext").on("input", function () {
            var value = $(this).val();

            // Remove any characters that are not alphabetic (or spaces)
            var cleanValue = value.replace(/[^a-zA-Z\s]/g, '');

            // Update the input value with cleaned string and truncate to 20 characters if necessary
            $(this).val(cleanValue.slice(0, 20));
        });


        // Allow only numeric input for the 'numb' class
        $(".numb").on("input", function() {
            var value = $(this).val();
            // Remove any non-numeric characters
            $(this).val(value.replace(/[^0-9]/g, ''));
        });
    });