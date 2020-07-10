$(document).ready(function () {
    $(".opcion").click(function () {
        $(".opcion").removeClass("active");

        $(this).addClass("active");
    });

    $(".rslides").responsiveSlides({
        auto: true,
        speed: 1000,
        random: true
    });

});