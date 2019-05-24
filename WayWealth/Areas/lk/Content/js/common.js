$(function () {
    $(document).ready(function () {
        if ($(window).width() < 1200) {
            $('header .menu').append($('.toolbar'));
        }
        else {
            $('.toolbar').prependTo($('.wrap-content'));
        }
        $('.ui-state-free').each(function () {
            $(this).find('.item p').html($(this).find('.item').attr('data-name'));
        });
    });

    $(window).resize(function () {
        if ($(window).width() < 1200) {
            $('header .menu').append($('.toolbar'));
        }
        else {
            $('.toolbar').prependTo($('.wrap-content'));
        }
    });

    $(window).scroll(function () {
        $(document).scrollTop() > 1000 ? $(".scroll_up").addClass("active") : $(".scroll_up").removeClass("active");
    });

    $('.lk-lang-block .lang a.active').click(function () {
        $(this).parent('div').toggleClass('active');
    });

    $('body').click(function (e) {
        if (!($(e.target).closest('.lang').hasClass('lang') || $(e.target).hasClass('lang'))) {
            $('.lang').removeClass('active');
        }

        if (!($(e.target).closest('.menu').hasClass('menu') || $(e.target).hasClass('menu') || $(e.target).hasClass('menu-btn'))) {
            $('header .top-block').removeClass('active');
        }
    });

    $('.menu-btn').click(function () {
        $('header .top-block').toggleClass('active');
    });

    $('header .bottom-block i').click(function (event) {
        $(this).parent().toggleClass('active');
        $(this).siblings('p').stop().slideToggle();
    });

    function animationSize() {
        if ($(window).width() > '992') {
        }
    }

    $(window).load(animationSize);
    $(window).resize(animationSize);

    //$('.copy_link').click(function () {
    //    var copyText = $(this).data('href');
    //});

    $('.select-range select').change(function () {
        $('#Sum').val($(this).val());
    });

    //$("#amount-range-base").slider({
    //    min: 1000,
    //    max: 4500,
    //    step: 500,
    //    value: 1000,
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

    $("#scale-range").slider({
        min: 50,
        max: 100,
        step: 10,
        value: 100,
        create: function (event, ui) {
            var v = $(this).slider('value');
            setTimeout(function () {
                $(".tree2>ul").attr('class', 'scale-' + v);
            }, 50);
        },
        change: function (event, ui) {
            setTimeout(function () {
                $(".tree2>ul").attr('class', 'scale-' + ui.value);
            }, 50);
        },
        slide: function (event, ui) {
            $(".tree2>ul").attr('class', 'scale-' + ui.value);
        }
    })
        .slider("pips");

    $('.tree2-scroll-right').click(function () {
        $('.tree2').animate({
            scrollLeft: "+=200px"
        }, "300");
    });
    $('.tree2-scroll-left').click(function () {
        $('.tree2').animate({
            scrollLeft: "-=200px"
        }, "300");
    });

    //var yearLbl = ["<span>1 год</span>", "<span>2 года</span>", "<span>3 года</span>"];

    //$("#year-pips").slider({
    //    min: 1,
    //    max: 3,
    //    step: 1,
    //    value: 1
    //}).slider("pips", { rest: "label", labels: yearLbl });

    $('.subtable_btn').click(function (event) {
        $(this).toggleClass('on');
        var childT = $(this).parent().parent('tr').next('tr.children-table');
        if (childT.length > 0)
            childT.toggleClass('active');
    });



    $('.wallets ul li a').click(function (event) {
        $('.wallets ul li').removeClass('active');
        $(this).parent().toggleClass('active');
        var id = $(this).data('id');
        var v = $(id).val();
        $('.wallets-inp').val(v);
        $('.wallets-inp').data('id', id);
    });
    $('.wallets-inp').on('input change paste', function () {
        var v = $(this).val();
        var id = $(this).data('id');
        $(id).val(v);
    });

    $('.verification-block input[type="file"]').change(function (event) {
        var files = $(this).val();
        var img = $(this).closest('.form-group').find('img').attr('id');
        if (files.length > 0) {
            $(this).closest('.form-group').addClass('active');
            readURL(this, '#' + img);
        }
    });
    $('.cancel-fileupload').click(function (event) {
        $(this).closest('.form-group').removeClass('active');
        $(this).closest('.form-group').find('img').attr('src', '');
        $(this).closest('.form-group').find('input').val('');
    });
    function readURL(input, img) {
        if (input.files && input.files[0]) {
            var reader = new FileReader();
            reader.onload = function (e) {
                $(img).attr('src', e.target.result);
            }
            reader.readAsDataURL(input.files[0]);
        }
    }
    var swiper1 = new Swiper('.swiper-charity', {
        slidesPerView: 3,
        //loop: true,
        navigation: {
            nextEl: '.swiper-button-next',
            prevEl: '.swiper-button-prev',
        },
        breakpoints: {
            1240: {
                slidesPerView: 2
            },
            650: {
                slidesPerView: 1
            }
        }
    });

    var swiper3 = new Swiper('.swiper-charity3', {
        //loop: true,
        slidesPerView: 4,
        pagination: {
            el: '.swiper-pagination',
            clickable: true
        },
        breakpoints: {
            1475: {
                slidesPerView: 3
            },
            810: {
                slidesPerView: 2
            },
            575: {
                slidesPerView: 1
            }
        }
    });
    var swiper5 = new Swiper('.swiper-news', {
        autoHeight: true,
        slidesPerView: 3,
        pagination: {
            el: '.swiper-pagination',
            clickable: true
        },
        breakpoints: {
            768: {
                slidesPerView: 1
            }
        }
    });
    var swiper6 = new Swiper('.swiper-question', {
        autoHeight: true,
        pagination: {
            el: '.swiper-pagination',
            clickable: true
        },
    });
    var swiper7 = new Swiper('.swiper-invest-packages', {
        pagination: {
            el: '.swiper-pagination-packages',
            clickable: true,
        },
        autoplay: {
            delay: 2500,
            disableOnInteraction: false,
        },
        slidesPerView: 'auto',
        speed: 500
    });
    $(".swiper-invest-packages").hover(function () {
        if (!$(this).hasClass('deautoplay')) {
            this.swiper.autoplay.stop();
        }
    }, function () {
        if (!$(this).hasClass('deautoplay')) {
            this.swiper.autoplay.start();
        }
    });
    $('.btn-invest-show').click(function () {
        $(".swiper-invest-packages").each(function () {
            this.swiper.autoplay.stop();
        });
    });
    $('.btn-invest-hide').click(function () {
        $(".swiper-invest-packages").each(function () {
            this.swiper.autoplay.start();
        });
    });
    $('.charity-btn').click(function (event) {
        $('.charity-btn').removeClass('active');
        $(this).addClass('active');
        $('#fondid').val($(this).data("id"));
    });
    if ($('div').is('#lists')) {
        $('#lists').jstree();
        $('#lists').addClass('open_all');
        $('#lists').find('a[href="#"]').removeAttr('href');
    }
    $('.all_tree_btn').click(function (event) {

        if ($('#lists').hasClass("open_all")) {
            $('#lists').jstree("open_all");
            $('#lists').removeClass('open_all');
        } else {
            $('#lists').jstree("close_all");
            $('#lists').addClass('open_all');
        }

        if ($('.tree2>ul').hasClass("open_ul")) {
            $('.tree2>ul').removeClass('open_ul');
            $('.tree2>ul li ul, .tree2 .item i').removeClass('active');
        } else {
            $('.tree2>ul').addClass('open_ul');
            $('.tree2>ul li ul, .tree2 .item i').addClass('active');
        }
    });

    $('body').on('click', '.close-modal-btn', function (e) {
        $('.wrap-modal').fadeOut();
        setTimeout(function () {
            $('#searchResults').empty().removeClass('search-table');
        }, 600);

        if ($(".swiper-invest-packages").length) {
            $('.invest-package').removeClass("selected");
            $(".swiper-invest-packages").removeClass('deautoplay');
            $(".swiper-invest-packages").each(function () {
                this.swiper.autoplay.start();
            });
        }
    });

    $('.wrap-modal').click(function (e) {
        if ($(e.target).hasClass('wrap-modal')) {
            $('.wrap-modal').fadeOut();
            setTimeout(function () {
                $('#searchResults').empty().removeClass('search-table');
            }, 600);

            if ($(".swiper-invest-packages").length) {
                $('.invest-package').removeClass("selected");
                $(".swiper-invest-packages").removeClass('deautoplay');
                $(".swiper-invest-packages").each(function () {
                    this.swiper.autoplay.start();
                });
            }
        }
    });

    $('.tree2 .item i').click(function () {
        $(this).toggleClass('active');
        $(this).parent().parent().children('ul').toggleClass('active');
    });
    $('.tree2 .item-inner').hover(function () {
        if (!$(this).closest('.ui-state-free').hasClass('ui-state-free')) {
            fillTreeInfo(this);
        }
    }, function () {
        $('.tree-info').removeClass('active');
        });
    $('.tree2 .item-inner').click(function () {
        if (!$(this).closest('.ui-state-free').hasClass('ui-state-free')) {
            $('.tree-info').addClass('stuck');
        }
    });
    /*$('#lists').on("hover_node.jstree", function (e, data) {
        fillTreeInfo(document.getElementById(data.node.a_attr.id));
        $('#lists').find('a[href="#"]').removeAttr('href');
    }).on("dehover_node.jstree", function (e, data) {
        $('.tree-info').removeClass('active');
    });*/
    $('#lists').on('mouseenter', '.jstree-anchor', function (e) {
        if ($(this).hasClass('jstree-anchor')) {
            fillTreeInfo(this);
            $('#lists').find('a[href="#"]').removeAttr('href');
        }
    }).on('mouseleave', '.jstree-anchor', function () {
        $('.tree-info').removeClass('active');
    }).on('click', function (e) {
        if (!$(e.target).hasClass('jstree-anchor')) {
            $('.tree-info').removeClass('active');
        }
        else {
            $('.tree-info').addClass('stuck');
        }
        });
    $('body').click(function (e) {
        if (!$(e.target).hasClass('jstree-anchor') && !$(e.target).hasClass('item-inner') && !$(e.target).closest('.item-inner').hasClass('item-inner') || $(e.target).closest('li').hasClass('ui-state-free')) {
            $('.tree-info').removeClass('stuck');
        }
    });
    function fillTreeInfo(item) {
        $('.tree-info').addClass('active');

        var name = $(item).parent().data('name');
        var deep = $(item).parent().data('deep');
        var filled = $(item).parent().data('filled');
        var loginorid = $(item).parent().data('loginorid');
        var levelvalue = $(item).parent().data('level-value');
        var structvalue = $(item).parent().data('struct-value')
        var investments = $(item).parent().data('investments');
        var rewards = $(item).parent().data('rewards');
        var structrewards = $(item).parent().data('struct-rewards');
        var leftShoulderSum = $(item).parent().data('left-sum');
        var rightShoulderSum = $(item).parent().data('right-sum');
        //var position = $(item).parent().data('pos');
        //var parentId = $(item).parent().data('parent');

        $('.tree-info .name, .tree-info .deep, .tree-info .investments, .tree-info .rewards, .tree-info .filled').empty();
        $('.tree-info .levelvalue').empty();
        $('.tree-info .structvalue').empty();
        $('.tree-info .structrewards').empty();
        $('.tree-info .left-sum').empty();
        $('.tree-info .right-sum').empty();
        $('.tree-info .loginorid').empty();


        $('.tree-info .name').append(name);
        $('.tree-info .deep').append(deep);
        $('.tree-info .loginorid').append(loginorid);
        $('.tree-info .filled').append(filled + ' / 62');
        $('.tree-info .levelvalue').append('$ ' + levelvalue);
        $('.tree-info .structvalue').append('$ ' + structvalue);
        $('.tree-info .investments').append('$ ' + investments);
        $('.tree-info .rewards').append('$' + rewards);
        $('.tree-info .structrewards').append('$' + structrewards);
        $('.tree-info .right-sum').append('$' + rightShoulderSum);
        $('.tree-info .left-sum').append('$' + leftShoulderSum);
    }

    $('.filter-block-btn').click(function (event) {
        $('.filter-block .form_flex').slideToggle(300);
    });
    $('.scroll_up').click(function (event) {
        $('body,html').animate({
            scrollTop: 0
        }, 500);
        return false;
    });

    $('.question-block .ul1 li a').click(function (event) {
        pollVariantId = $(this).data("poll-variant-id");

        $.ajax({
            url: "/lk/SelectQuestion",
            type: "POST",
            data: {
                pollVariantId: pollVariantId
            },
            success: function () {
                location.reload();
            }
        });
    });

    $('.tab-btn-account').click(function (event) {
        $('.tab-btn-partner').removeClass('active');
        $('.tab-partner').removeClass('active');
        $(this).addClass('active');
        $('.tab-account').addClass('active');
    });
    $('.tab-btn-partner').click(function (event) {
        $('.tab-btn-account').removeClass('active');
        $('.tab-account').removeClass('active');
        $(this).addClass('active');
        $('.tab-partner').addClass('active');
    });

    $('.upload_ava').change(function (event) {
        var uploadfile = $(this).prop('files')[0];

        var data = new FormData();

        data.append("file", uploadfile);

        $.ajax({
            type: "POST",
            url: "/lk/UploadAvatar",
            contentType: false,
            processData: false,
            data: data,
            success: function () {
                location.reload();
            }
        });
    });

    $('.delete_ava').click(function () {
        $.ajax({
            type: "POST",
            url: "/lk/DeleteAvatar",
            success: function () {
                location.reload();
            }
        });
    });


    if ($('span').is('#reminder'))
        $('#reminder').text(Number(balances["Остаток.ВнутреннийСчет"].replace(/\,/g, '.')).toFixed(2));

    if ($('input').is('#account_hidden'))
        $('#account_hidden').val("Остаток.Проценты");

    $('.js_transfer_all_sum').click(function () {
        $('.sum_input').val($('.reminder').text().replace('.', ','));
    });

    $('#gentp').click(function () {
        
        $.ajax({
            type: "POST",
            url: "/lk/GenerateTransactionPassword",
            data: {},
            success: function () {
                console.log("test");   
                
            }
        });


        // Set the date we're counting down to
        var countDownDate = new Date(new Date().getTime() + 5 * 60000).getTime();
        //var countDownDate = new Date(new Date().getTime() + 10000).getTime();
        var btn = this;
        $(btn).css('visibility', 'hidden');
        $('.sms').fadeIn();
        
        // Update the count down every 1 second
        var x = setInterval(function () {
            
            // Get todays date and time
            var now = new Date().getTime();

            // Find the distance between now and the count down date
            var distance = countDownDate - now;

            // Time calculations for days, hours, minutes and seconds
            //var days = Math.floor(distance / (1000 * 60 * 60 * 24));
            var hours = Math.floor((distance % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60));
            var minutes = Math.floor((distance % (1000 * 60 * 60)) / (1000 * 60));
            var seconds = Math.floor((distance % (1000 * 60)) / 1000);

            if (hours < 10) { hours = "0" + hours; }
            if (minutes < 10) { minutes = "0" + minutes; }
            if (seconds < 10) { seconds = "0" + seconds; }

            $('.timer').text(hours + ":" + minutes + ":" + seconds);
            
            // If the count down is finished, write some text 
            if (distance < 0) {
                clearInterval(x);
                $('.timer').text("00:00:00");
                $(btn).css('visibility', 'visible');
            }
        }, 1000);
    });

    //$('#crypptoaccount').change(function () {
    //    var v = $('.sum_input').val();
    //    $('.crypptoconvert').html(v + ' ' + '<span>' + $(this).val() + '</span>');
    //    if (addresses) {
    //        $('#inp_address').val(addresses[$(this).val()]);
    //    }
    //});

    //$('.jstree-select').change(function () {
    //    if ($(this).val() === "List") {
    //        location.href = "/lk/structure2";
    //    } else if ($(this).val() === "Tree") {
    //        location.href = "/lk/structure";
    //    }
    //});

    $('.js_search_input').click(function () {
        var searchText = $('.search_input').val();
        $.ajax({
            type: "POST",
            url: "/lk/_SearchMarketingPlaces",
            data: {
                searchText: searchText
            },
            success: function (data) {
                $('.wrap-modal').fadeIn();
                $('#searchResults').empty();
                $('#searchResults').append(data).addClass('search-table');
            }
        });
    });


    $('.invest-block table .checkbox input[type="checkbox"]').change(checkInvestments);

    function checkInvestments() {
        var val = "";
        $('.invest-block table .checkbox input[type="checkbox"]')
            .each(function () {
                if ($(this).is(':checked')) {
                    val = val + $(this).val() + ";";
                }
                console.log(val);
            });
        $('.invest-block .add_sum input[name="investments"]').val(val);
    }
    //$('.calc-block-btn').click(function () {
    //    var program = getUrlParameter('program');
    //    var sum = $('.amount-block .ui-slider-pip-selected .ui-slider-label').data('value');
    //    var time = $('.year-block .ui-slider-pip-selected .ui-slider-label').data('value');
    //    var isProlonged = $('.avtoprodleniye input[name="IsProlonged"]').is(':checked');
    //    var isCreateNewPlace = $('.avtoprodleniye input[name="IsCreateNewPlace"]').is(':checked');

    //    if (program === 'camulative')
    //        sum = 300;

    //    $.ajax({
    //        type: "POST",
    //        url: "/lk/InvestPost",
    //        data: {
    //            program: program,
    //            sum: sum,
    //            time: time,
    //            IsProlonged: isProlonged,
    //            IsCreateNewPlace: isCreateNewPlace,
    //        },
    //        success: function () {
    //            window.location("/lk/index")
    //        }
    //    });
    //});

    $('.sum_input').on('input', function () {
        $(this).val($(this).val().replace('.', ','));
    });

    /*$('.tree-sandbox ul li').draggable({ revert: "invalid" });
    $(".tree-block .tree2 li.ui-state-free").droppable({
        drop: function (event, ui) {
            if (!$(this).hasClass('ui-state-free')) {
                return;
            }
            if (confirm($("#confirm-set-partner").val())) {
                var partner_id = ui.draggable.attr('data-id'); 
                $(this).find('a p').html(ui.draggable.html());
                ui.draggable.remove();
                $(this).removeClass('ui-state-free');
                var data = { place_id: $(this).find('a').data('parent'), partner_id: partner_id, pos: $(this).find('a').data('pos') };                
                $.post('/lk/SetPlace', data, function (response) {
                    if (response.error) {
                        $('.wrap-modal').fadeIn();
                        $('#searchResults').html(response.message);
                    }
                    else {
                        $('.wrap-modal').fadeIn();
                        $('#searchResults').html(response.message);
                    }

                    var content = '<i class="ion-ios-' + (response.error ? 'close' : 'checkmark') + ' modal-icon"></i><p class="m-b-30">' + response.message +'</p><a class="btn btn-bordered btn-sm close-modal-btn" >'+response.close+'</a >';

                }, 'json');
            }
        }
    });*/

    $('.tree-sandbox ul li').click(function () {
        var tooltip = '', old_tooltip = '', tooltip_cancel = '', partner_id = '';
        old_tooltip = $('body').find('.tree2-set-tooltip');
        $(old_tooltip).fadeOut(300);
        setTimeout(function () {
            $(old_tooltip).remove();
        }, 350);
        if (!$(this).hasClass('active')) {
            $('.tree-sandbox ul li').removeClass('active');
            $(this).addClass('active');
            partner_id = $(this).find('span').html();
        }
        if ($('.tree-block-lang').hasClass('ru')) {
            tooltip_cancel = $('<button type="button">Отмена</button>');
            tooltip = $('<div class="tree2-set-tooltip"><p>' + partner_id + '</p>Выберите <strong>свободное место</strong> для распределения.</div>');
            $(tooltip).prepend(tooltip_cancel);
        }
        else {
            tooltip_cancel = $('<button type="button">Cancel</button>');
            tooltip = $('<div class="tree2-set-tooltip"><p>' + partner_id + '</p>Select <strong>free place</strong> for distribution.</div>');
            $(tooltip).prepend(tooltip_cancel);
        }
        $(tooltip_cancel).click(function () {
            old_tooltip = $('body').find('.tree2-set-tooltip');
            $(old_tooltip).fadeOut(300);
            setTimeout(function () {
                $(old_tooltip).remove();
            }, 350);
            $('.tree-sandbox ul li').removeClass('active');
        });
        $('body').append(tooltip);
        $('body').find('.tree2-set-tooltip').last().fadeIn(300);
    });

    $('.ui-state-free .item').bind('click', function () {
        var item = $(this);
        if ($('body').find('.tree2-set-tooltip').length) {
            if (confirm($("#confirm-set-partner").val())) {
                var partner_id = $('.tree-sandbox ul li.active').attr('data-id');
                var data = { place_id: $(item).data('parent'), partner_id: partner_id, pos: $(item).data('pos') };
                $.post('/lk/SetPlace', data, function (response) {
                    var content = '<i class="ion-ios-' + (response.error == 'true' ? 'close' : 'checkmark') + ' modal-icon"></i><p class="m-b-30">' + response.message + '</p><a class="btn btn-bordered btn-sm close-modal-btn">' + response.close + '</a>';
                    $('#searchResults').html(content);
                    $('.wrap-modal').fadeIn();
                    if (response.error == 'true') {
                        $('body').find('.tree2-set-tooltip').fadeOut(300);
                        $('.tree-sandbox ul li').removeClass('active');
                        setTimeout(function () {
                            $('body').find('.tree2-set-tooltip').remove();
                        }, 350);
                    }
                    else {
                        $(item).find('p').html($('.tree-sandbox ul li.active span').html());
                        $(item).unbind('click');
                        var item_img = $(item).find('img');
                        $(item_img).attr('src', $(item_img).attr('src').replace('tree-logo.png', 'tree-logo-dark.png'));
                        $(item).parent().removeClass('ui-state-free').addClass('ui-state-filled');
                        $('body').find('.tree2-set-tooltip').fadeOut(300);
                        $('.tree-sandbox ul li.active').fadeOut(300);
                        setTimeout(function () {
                            $('body').find('.tree2-set-tooltip').remove();
                            $('.tree-sandbox ul li.active').remove();
                            if (!$('.tree-sandbox ul li').length) {
                                $('.tree-sandbox').remove();
                            }
                        }, 350);
                    }


                }, 'json');
            }
        }
    });

});

//var getUrlParameter = function getUrlParameter(sParam) {
//    var sPageURL = decodeURIComponent(window.location.search.substring(1)),
//        sURLVariables = sPageURL.split('&'),
//        sParameterName,
//        i;

//    for (i = 0; i < sURLVariables.length; i++) {
//        sParameterName = sURLVariables[i].split('=');

//        if (sParameterName[0] === sParam) {
//            return sParameterName[1] === undefined ? true : sParameterName[1];
//        }
//    }
//};
