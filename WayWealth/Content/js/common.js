$(function() {
	var swiper = new Swiper('.swiper-banner', {
		pagination: {
			el: '.swiper-pagination',
			clickable: true,
		},
		speed: 500
	});
	var swiper2 = new Swiper('.swiper-about', {
		pagination: {
			el: '.swiper-pagination',
			clickable: true,
		},
		speed: 500
	});
	var swiper3 = new Swiper('.swiper-invest', {
		pagination: {
			el: '.swiper-pagination',
			clickable: true,
		},
		speed: 500
	});
	// var swiper4 = new Swiper('.swiper-partnership', {
	// 	pagination: {
	// 		el: '.swiper-pagination',
	// 		clickable: true,
	// 	},
	// 	speed: 500
	// });
	// var swiper5 = new Swiper('.swiper-partnership2', {
	// 	pagination: {
	// 		el: '.swiper-pagination',
	// 		clickable: true,
	// 	},
	// 	speed: 500
	// });
	var swiper6 = new Swiper('.swiper-news-m', {
		spaceBetween: 10,
		pagination: {
			el: '.swiper-pagination',
			clickable: true,
		},
	});
	var swiper6 = new Swiper('.swiper-reviews-m', {
		spaceBetween: 10,
		pagination: {
			el: '.swiper-pagination',
			clickable: true,
		},
	});
	var swiper7 = new Swiper('.swiper-icons-m', {
		pagination: {
			el: '.swiper-pagination',
			clickable: true,
		},
	});
	var swiper8 = new Swiper('.swiper-invest-block-3-m', {
		pagination: {
			el: '.swiper-pagination',
			clickable: true,
		},
	});
	var swiper9 = new Swiper('.swiper-invest-step-m', {
		pagination: {
			el: '.swiper-pagination',
			clickable: true,
		},
	});
	var swiper10 = new Swiper('.swiper-invest-block-5-m', {
		pagination: {
			el: '.swiper-pagination',
			clickable: true,
		},
	});
	//$('.select-range select').change(function () {
	//    $('#Sum').val($(this).val());
	//});
    //$("#amount-range-base")
    //    .slider({
    //        min: 1000,
    //        max: 4500,
    //        step: 500,
    //        value: 1000,
    //        create: function(event, ui) {
    //            var v = $(this).slider('value');
    //            setTimeout(function() {
    //                    $(".ui-slider-tip").attr('data-text', v);
    //                },
    //                50);
    //            $('#Sum').val(v);
    //        },
    //        slide: function(event, ui) {
    //            $(".ui-slider-tip").attr('data-text', ui.value);
    //            $('#Sum').val(ui.value);
    //        }
    //    })
    //    .slider("pips");

	//$("#amount-range-optimal").slider({
	//    min: 5000,
	//    max: 9500,
	//    step: 500,
	//    value: 5000,
	//    create: function (event, ui) {
	//        var v = $(this).slider('value');
	//        setTimeout(function () {
	//            $(".ui-slider-tip").attr('data-text', v);
	//        }, 50);
	//        $('#Sum').val(v);
	//    },
	//    slide: function (event, ui) {
	//        $(".ui-slider-tip").attr('data-text', ui.value);
	//        $('#Sum').val(ui.value);
	//    }
	//})
	//.slider("pips");

	//$("#amount-range-profi").slider({
	//    min: 10000,
	//    max: 20000,
	//    step: 1000,
	//    value: 10000,
	//    create: function (event, ui) {
	//        var v = $(this).slider('value');
	//        setTimeout(function () {
	//            $(".ui-slider-tip").attr('data-text', v);
	//        }, 50);
	//        $('#Sum').val(v);
	//    },
	//    slide: function (event, ui) {
	//        $(".ui-slider-tip").attr('data-text', ui.value);
	//        $('#Sum').val(ui.value);
	//    }
	//})
	//.slider("pips");

	//var yearLbl = [
	//"<span>1 год</span>", 
	//"<span>2 года</span>", 
	//"<span>3 года</span>"
	//];

	//$("#year-pips")
	//.slider({
	//	min: 1,
	//	max: 3,
	//	step: 1,
	//	value: 2
	//})
	//.slider("pips", {
	//	rest: "label",
	//	labels: yearLbl
	//});
	
	$('#myclock1').thooClock({
		size:200,
		showNumerals:true,
		brandText:'NEW-YORK',
		hourCorrection:'-7',
		brandText2:'',
		onEverySecond:function(){}
	});
	$('#myclock2').thooClock({
		size:200,
		showNumerals:true,
		brandText:'MOSCOW',
		hourCorrection:'0',
		brandText2:'',
		onEverySecond:function(){}
	});
	$('#myclock3').thooClock({
		size:200,
		showNumerals:true,
		brandText:'BERLIN',
		hourCorrection:'-1',
		brandText2:'',
		onEverySecond:function(){}
	});
	$('#myclock4').thooClock({
		size:200,
		showNumerals:true,
		brandText:'HONG KONG',
		hourCorrection: '+5',
		brandText2:'',
		onEverySecond:function(){}
	});
	$('#myclock5').thooClock({
		size:200,
		showNumerals:true,
		brandText:'TOKYO',
		hourCorrection: '+6',
		brandText2:'',
		onEverySecond:function(){}
	});
	$('#myclock6').thooClock({
		size:200,
		showNumerals:true,
		brandText:'LONDON',
		hourCorrection: '-2',
		brandText2:'',
		onEverySecond:function(){}
	});

	$('.lk-lang-block .lang a.active').click(function(event) {
		$(this).parent('div').addClass('active');
	});

	$('.answer-open').click(function(event) {
		$(this).parent('li').toggleClass('active');
	});
	// $('.answer-close').click(function(event) {
	// 	$(this).parent().parent('li').removeClass('active');
	// });
	$('.faq-page .faq-flex .faq-menu li a').click(function(event) {
		var id = $(this).data('id');
		$('.faq-page .faq-flex .faq-menu li').removeClass('active');
		$(this).parent('li').addClass('active');
		$('.faq-page .faq-flex .faq-cont .answer-block').removeClass('active');
		$('.' + id).addClass('active');
	});
	$('.invest-block-2 .btn-block a').click(function(event) {
		var block = $(this).data('id');
		$('.invest-block .btn1-block, .invest-block .btn2-block, .invest-block .btn3-block').removeClass('active');
		$('.' + block).addClass('active');
	});

	$('.open-partnership-1').click(function(event) {
		$(this).addClass('active');
		$(this).parent().children('a.hide-block').addClass('active');
		$('.partnership-4, .partnership-5').slideDown(500);
		$('.partnership-5 .hide-block').addClass('active');
		var swiper4 = new Swiper('.swiper-partnership', {
			pagination: {
				el: '.swiper-pagination',
				clickable: true,
			},
			speed: 500
		});
	});
	$('.hide-partnership-1').click(function(event) {
		var id = "#partnership-3"
		var top = $(id).offset().top;
		$('body,html').animate({scrollTop: top}, 500);
		$(this).removeClass('active');
		$('.partnership-4, .partnership-5').slideUp('500');
		$('.partnership-3 .hide-block').removeClass('active');
		$('.partnership-3 .page-link').removeClass('active');
	});
	$('.open-partnership-2').click(function(event) {
		$(this).addClass('active');
		$(this).parent().children('a.hide-block').addClass('active');
		$('.partnership-7, .partnership-8, .partnership-9, .partnership-10').slideDown('500');
		$('.partnership-10 .hide-block').addClass('active');
		var swiper5 = new Swiper('.swiper-partnership2', {
			pagination: {
				el: '.swiper-pagination',
				clickable: true,
			},
			speed: 500
		});
	});
	$('.hide-partnership-2').click(function(event) {
		var id = "#partnership-6"
		var top = $(id).offset().top;
		$('body,html').animate({scrollTop: top}, 500);
		$(this).removeClass('active');
		$('.partnership-7, .partnership-8, .partnership-9, .partnership-10').slideUp('500');
		$('.partnership-6 .hide-block').removeClass('active');
		$('.partnership-6 .page-link').removeClass('active');
	});
	$('.menu-btn').click(function(event) {
		$(this).toggleClass('active');
		$('header .menu').slideToggle(300);
	});
	$('header .bottom-block i').click(function(event) {
		$(this).toggleClass('active');
		$(this).parent().toggleClass('active');
		$(this).parent().children('p').slideToggle(500);
	});
	$('.banner-block').waypoint(function(){
		$('.banner-block .swiper-slide .info').addClass('on');
	}, {
		offset:'100%'
	});
	$('.calc-block').waypoint(function(){
		$('.calc-block .calc').addClass('on');
	}, {
		offset:'80%'
	});
	$('.about-block').waypoint(function(){
		$('.about-block ul').addClass('on');
	}, {
		offset:'80%'
	});
	$('.news-block').waypoint(function(){
		$('.news-block ul').addClass('on');
		$('.news-block .swiper-news-m').addClass('on');
	}, {
		offset:'80%'
	});
	$('.reviews-block').waypoint(function(){
		$('.reviews-block ul').addClass('on');
		$('.reviews-block .swiper-reviews-m').addClass('on');
	}, {
		offset:'80%'
	});
	$('.time-block').waypoint(function(){
		$('.time-block .times').addClass('on');
		$('.time-block .icons').addClass('on');
	}, {
		offset:'80%'
	});
	$('.about-page .about-block-0').waypoint(function(){
		$('.about-page .about-block-0').addClass('on');
	}, {
		offset:'80%'
	});
	$('.about-page .about-block-1').waypoint(function(){
		$('.about-page .about-block-1').addClass('on');
	}, {
		offset:'80%'
	});
	$('.about-page .about-block-2').waypoint(function(){
		$('.about-page .about-block-2').addClass('on');
	}, {
		offset:'60%'
	});
	$('.about-page .about-block-3').waypoint(function(){
		$('.about-page .about-block-3').addClass('on');
	}, {
		offset:'80%'
	});
	$('.about-page .about-block-4').waypoint(function(){
		$('.about-page .about-block-4').addClass('on');
	}, {
		offset:'80%'
	});
	$('.about-page .about-block-5').waypoint(function(){
		$('.about-page .about-block-5').addClass('on');
	}, {
		offset:'80%'
	});
	$('.invest-page .invest-block-0').waypoint(function(){
		$('.invest-page .invest-block-0').addClass('on');
	}, {
		offset:'80%'
	});
	$('.invest-page .invest-block-1').waypoint(function(){
		$('.invest-page .invest-block-1').addClass('on');
	}, {
		offset:'80%'
	});
	$('.invest-page .invest-block-2').waypoint(function(){
		$('.invest-page .invest-block-2').addClass('on');
	}, {
		offset:'80%'
	});
	$('.invest-page .invest-block-3').waypoint(function(){
		$('.invest-page .invest-block-3').addClass('on');
	}, {
		offset:'80%'
	});
	$('.invest-page .invest-block-4 .invest-step').waypoint(function(){
		$('.invest-page .invest-block-4 .invest-step').addClass('on');
		$('.invest-page .invest-block-4 .swiper-invest-step-m').addClass('on');
	}, {
		offset:'80%'
	});
	$('.invest-page .invest-block-5').waypoint(function(){
		$('.invest-page .invest-block-5').addClass('on');
	}, {
		offset:'80%'
	});
	$('.invest-page .invest-block-6').waypoint(function(){
		$('.invest-page .invest-block-6').addClass('on');
	}, {
		offset:'80%'
	});
	$('.invest-page .invest-block-7').waypoint(function(){
		$('.invest-page .invest-block-7').addClass('on');
	}, {
		offset:'80%'
	});
	$('.partnership-page .partnership-0').waypoint(function(){
		$('.partnership-page .partnership-0').addClass('on');
	}, {
		offset:'80%'
	});
	$('.partnership-page .partnership-1').waypoint(function(){
		$('.partnership-page .partnership-1').addClass('on');
	}, {
		offset:'80%'
	});
	$('.partnership-page .partnership-1_2').waypoint(function(){
		$('.partnership-page .partnership-1_2').addClass('on');
	}, {
		offset:'80%'
	});
	$('.partnership-page .partnership-1_3').waypoint(function(){
		$('.partnership-page .partnership-1_3').addClass('on');
	}, {
		offset:'80%'
	});
	$('.partnership-page .partnership-2').waypoint(function(){
		$('.partnership-page .partnership-2').addClass('on');
	}, {
		offset:'60%'
	});
	$('.partnership-page .partnership-3').waypoint(function(){
		$('.partnership-page .partnership-3').addClass('on');
	}, {
		offset:'60%'
	});
	$('.partnership-page .partnership-6').waypoint(function(){
		$('.partnership-page .partnership-6').addClass('on');
	}, {
		offset:'60%'
	});
	$('.news-page').waypoint(function(){
		$('.news-page').addClass('on');
	}, {
		offset:'80%'
	});
	$('.faq-page').waypoint(function(){
		$('.faq-page').addClass('on');
	}, {
		offset:'80%'
	});
	$('.reviews-page .reviews-content').waypoint(function(){
		$('.reviews-page .reviews-content').addClass('on');
	}, {
		offset:'80%'
	});
	function animationSize(){
		if ($(window).width() > '992'){
		}
	}
	$(window).load(animationSize);
	$(window).resize(animationSize);

	$('.scroll_up').click(function(event) {
		$('body,html').animate({
			scrollTop: 0
		}, 500);
		return false;
	});
	$('.partnership_anchor1').click(function(event) {
		event.preventDefault();
		var id = $(this).attr('href');
		var top = $(id).offset().top;
		$('body,html').animate({scrollTop: top}, 500);
	});
});