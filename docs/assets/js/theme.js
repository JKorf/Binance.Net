/*
================================================================
* Template:  	 iDocs - One Page Documentation HTML Template
* Written by: 	 Harnish Design - (http://www.harnishdesign.net)
* Description:   Main Custom Script File
================================================================
*/


(function ($) {
	"use strict";

// Preloader
$(window).on('load', function () {
	$('.lds-ellipsis').fadeOut(); // will first fade out the loading animation
	$('.preloader').delay(333).fadeOut('slow'); // will fade out the white DIV that covers the website.
	$('body').delay(333);
});

/*-------------------------------
    Primary Menu
--------------------------------- */

// Dropdown show on hover
$('.primary-menu ul.navbar-nav li.dropdown, .login-signup ul.navbar-nav li.dropdown').on("mouseover", function() {
	if ($(window).width() > 991) {
		$(this).find('> .dropdown-menu').stop().slideDown('fast');
		$(this).bind('mouseleave', function() {
		$(this).find('> .dropdown-menu').stop().css('display', 'none'); 
		});
	}
});

// When dropdown going off to the out of the screen.
$('.primary-menu ul.navbar-nav .dropdown-menu').each(function() {
		var menu = $('#header .container-fluid').offset();
		var dropdown = $(this).parent().offset();

		var i = (dropdown.left + $(this).outerWidth()) - (menu.left + $('#header .container-fluid').outerWidth());

		if (i > 0) {
			$(this).css('margin-left', '-' + (i + 5) + 'px');
		}
	});
$(function () {
    $(".dropdown li").on('mouseenter mouseleave', function (e) {
		if ($(window).width() > 991) {
            var elm = $('.dropdown-menu', this);
            var off = elm.offset();
            var l = off.left;
            var w = elm.width();
            var docW = $(window).width();
            var isEntirelyVisible = (l + w + 30 <= docW);
            if (!isEntirelyVisible) {
                $(elm).addClass('dropdown-menu-right');
            } else {
                $(elm).removeClass('dropdown-menu-right');
            }
			}
    });
});

// DropDown Arrow
$('.primary-menu ul.navbar-nav').find('a.dropdown-toggle').append($('<i />').addClass('arrow'));


// Mobile Collapse Nav
$('.primary-menu .navbar-nav .dropdown-toggle[href="#"], .primary-menu .dropdown-toggle[href!="#"] .arrow').on('click', function(e) {
	if ($(window).width() < 991) {
        e.preventDefault();
        var $parentli = $(this).closest('li');
        $parentli.siblings('li').find('.dropdown-menu:visible').slideUp();
        $parentli.find('> .dropdown-menu').stop().slideToggle();
        $parentli.siblings('li').find('a .arrow.show').toggleClass('show');
		$parentli.find('> a .arrow').toggleClass('show');
	}
});


// Mobile Menu
$('.navbar-toggler').on('click', function() {
	$(this).toggleClass('show');
});


/*------------------------
   Side Navigation
-------------------------- */

$('#sidebarCollapse').on('click', function () {
	$('#sidebarCollapse span:nth-child(3)').toggleClass('w-50');
	$('.idocs-navigation').toggleClass('active');
});


/*------------------------
   Sections Scroll
-------------------------- */

$('.smooth-scroll,.idocs-navigation a').on('click', function() {
	event.preventDefault();
    var sectionTo = $(this).attr('href');
	$('html, body').stop().animate({
      scrollTop: $(sectionTo).offset().top - 120}, 1000, 'easeInOutExpo');
});

/*-----------------------------
    Magnific Popup
------------------------------- */

// Image on Modal
$('.popup-img').each(function() {
$(this).magnificPopup({
	type: "image",
	tLoading: '<div class="preloader"><div class="lds-ellipsis"><div></div><div></div><div></div><div></div></div></div>',
    closeOnContentClick: !0,
    mainClass: "mfp-fade",
	
});
});

// YouTube/Viemo Video & Gmaps
$('.popup-youtube, .popup-vimeo, .popup-gmaps').each(function() {
$(this).magnificPopup({
        type: 'iframe',
		mainClass: 'mfp-fade',
});
});


/*------------------------
   Highlight Js
-------------------------- */

hljs.initHighlightingOnLoad();


/*------------------------
   tooltips
-------------------------- */
$('[data-toggle=\'tooltip\']').tooltip({container: 'body'});


/*------------------------
   Scroll to top
-------------------------- */
$(function () {
		$(window).on('scroll', function(){
			if ($(this).scrollTop() > 400) {
				$('#back-to-top').fadeIn();
			} else {
				$('#back-to-top').fadeOut();
			}
		});
		});
$('#back-to-top').on("click", function() {
	$('html, body').animate({scrollTop:0}, 'slow');
	return false;
});


})(jQuery)