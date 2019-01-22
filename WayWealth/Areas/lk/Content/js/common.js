$(function () {
    $('.lk-lang-block .lang a.active').click(function (event) {
        $(this).parent('div').addClass('active');
    });
    $('.menu-btn').click(function (event) {
        $(this).toggleClass('active');
        $('header .menu').slideToggle(300);
        $('.toolbar').slideToggle(300);
    });
    $('header .bottom-block i').click(function (event) {
        $(this).toggleClass('active');
        $(this).parent().toggleClass('active');
        $(this).parent().children('p').slideToggle(500);
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
        min: 10,
        max: 100,
        step: 10,
        value: 100,
        create: function (event, ui) {
            var v = $(this).slider('value');
            setTimeout(function () {
                $(".tree2>ul").attr('class', 'scale-' + v);
            }, 50);
        },
        slide: function (event, ui) {
            $(".tree2>ul").attr('class', 'scale-' + ui.value);
        }
    })
    .slider("pips");

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
        var img = $(this).parent().parent().children('img').attr('id');
        if (files.length > 0) {
            $(this).parent().parent().parent().addClass('active');
            readURL(this, '#' + img);
        }
    });
    $('.cancel-fileupload').click(function (event) {
        $(this).parent().parent().removeClass('active');
        $(this).parent().children('img').attr('src', '');
        $(this).parent().children('div').children('input').val('');
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
        slidesPerView: 3,
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
    var swiper4 = new Swiper('.swiper-bills', {
        slidesPerView: 2,
        pagination: {
            el: '.swiper-pagination',
        },
        breakpoints: {
            640: {
                slidesPerView: 1
            }
        }
    });
    var swiper5 = new Swiper('.swiper-news', {
        slidesPerView: 2,
        pagination: {
            el: '.swiper-pagination',
        },
        breakpoints: {
            740: {
                slidesPerView: 1
            }
        }
    });
    var swiper6 = new Swiper('.swiper-question', {
        pagination: {
            el: '.swiper-pagination',
            clickable: true
        },
    });
    $('.charity-btn').click(function (event) {
        $('.charity-btn').removeClass('active');
        $(this).addClass('active');
        $('#fondid').val($(this).data("id"));
    });
    if ($('div').is('#lists')) {
        $('#lists').jstree();
        $('#lists').addClass('open_all');
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
            $('.tree2>ul li ul').removeClass('active');
        } else {
            $('.tree2>ul').addClass('open_ul');
        }
    });

    $('.close-modal-btn').click(function (event) {
        $(this).closest('.modal').remove();
        if ($('.wrap-modal .modal').length == 0)
        {
            $('.wrap-modal').removeClass('active');
            $('#searchResults').empty();
            $('body').removeClass('overflow');
        }
    });

    /*$('.close-modal-btn').click(function (event) {
        $('.wrap-modal').removeClass('active');
        $('#searchResults').empty();
        $('body').removeClass('overflow');
    });*/
    $('.tree2 .item .flex').click(function (event) {
        $(this).toggleClass('active');
        $(this).parent().parent().children('ul').toggleClass('active');
    });
    $('.tree2 .item p').click(function (event) {
        $('.tree2 .item').removeClass('active');
        $(this).parent().toggleClass('active');
        
        fillTreeInfo(this);
    });
    $('#lists').on("select_node.jstree", function (e, data) {
        //alert("node_id: " + data.node.id);
        fillTreeInfo(document.getElementById(data.node.a_attr.id));
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

        $('.tree-info .name, .tree-info .deep, .tree-info .investments, .tree-info .rewards, .tree-info .filled').empty();
        $('.tree-info .levelvalue').empty();
        $('.tree-info .structvalue').empty();
        $('.tree-info .structrewards').empty();
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
    }

  
    $('.scroll-btn-right').click(function (event) {
        $('.tree2').animate({
            scrollLeft: "+=200px"
        }, "300");
    });
    $('.scroll-btn-left').click(function (event) {
        $('.tree2').animate({
            scrollLeft: "-=200px"
        }, "300");
    });
    $('.scroll-btn-down').click(function (event) {
        $('html, body').animate({
            scrollTop: "+=200px"
        }, "300");
    });
    $('.scroll-btn-up').click(function (event) {
        $('html, body').animate({
            scrollTop: "-=200px"
        }, "300");
    });
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



    //$('#account').change(function () {
    //    if (balances) {
    //        $('#reminder').text(balances[$(this).val()]);
    //    }
    //    $('#account_hidden').val($(this).val());
    //    $('.sum_input').val("");
    //});

    if ($('span').is('#reminder'))
        $('#reminder').text(balances["Остаток.Проценты"]);

    if ($('input').is('#account_hidden'))
        $('#account_hidden').val("Остаток.Проценты");

    $('.js_transfer_all_sum').click(function () {
        $('.sum_input').val($('.reminder').text().replace('.',','));
    });

    $('#gentp').click(function () {
        
        $.ajax({
            type: "POST",
            url: "/lk/GenerateTransactionPassword",
            data: {},
            success: function () {
                // ввывод таймера   
                
            }
        });


        // Set the date we're counting down to
        var countDownDate = new Date(new Date().getTime() + 5 * 60000).getTime();
        //var countDownDate = new Date(new Date().getTime() + 10000).getTime();
        var btn = this;
        $(btn).css('visibility', 'hidden');
        
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
                $('body').addClass('overflow');
                $('.wrap-modal').addClass('active');
                $('#searchResults').empty();
                $('#searchResults').append(data);
            }
        });
    });


    $('.invest-block table .checkbox input[type="checkbox"]').change(checkInvestments);

    $('.tree-info').click(function() {
        $(this).toggleClass('on');
    });

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
