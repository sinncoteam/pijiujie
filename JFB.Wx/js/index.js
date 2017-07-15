function pageShow(e, b) {
    if (!b) {
        e.css("z-index", "1");
        e.css("display", "block");
    } else {
        if (b == 1) {
            e.css("z-index", "0");
            e.css("display", "block");
            e.css("transition", "");
            e.css("transform", "scale(1.3) translate(0,500px) translateZ(0px)");
            e.css("opacity", "0");
            setTimeout(function() {
                e.css("transition", "all 0.5s");
                e.css("transform", "scale(1) translate(0,0) translateZ(0px)");
                e.css("opacity", "1");
            }, 50)
        } else {
            e.css("z-index", "0");
            e.css("display", "block");
            e.css("transition", "");
            e.css("transform", "scale(.8) translate(0,-200px) translateZ(0px)");
            e.css("opacity", "0");
            setTimeout(function() {
                e.css("transition", "all 0.5s");
                e.css("transform", "scale(1) translate(0,0) translateZ(0px)");
                e.css("opacity", "1");
            }, 50)
        }
        setTimeout(function() {
            e.siblings().removeClass("active");
            e.addClass("active");
        }, 600)
    }
}

function pageHide(e, b) {
    e.removeClass("active");
    if (!b) {
        e.css("z-index", "0");
        e.css("display", "none");
    } else {
        if (b == 1) {
            e.css("z-index", "1");
            e.css("transition", "");
            e.css("transform", "scale(1) translate(0 0) translateZ(0px)");
            e.css("opacity", "1");
            setTimeout(function() {
                e.css("transition", "all 0.5s");
                e.css("transform", "scale(.8) translate(0,-200px) translateZ(0px)");
                e.css("opacity", "0");
            }, 50)
            setTimeout(function() {
                e.css("display", "none");
            }, 550);
        } else {
            e.css("z-index", "1");
            e.css("transition", "");
            e.css("transform", "scale(1) translate(0 0) translateZ(0px)");
            e.css("opacity", "1");
            setTimeout(function() {
                e.css("transition", "all 0.5s");
                e.css("transform", "scale(1.3) translate(0,500px) translateZ(0px)");
                e.css("opacity", "0");
            }, 50)
            setTimeout(function() {
                e.css("display", "none");
            }, 550);
        }
    }
}

$(function() {


    // 加载
    // var queue = new createjs.LoadQueue(true);
    // var fest = [
    //     "images/bg.png",
    //     "images/down.png",
    //     "images/logo.png",
    //     "images/normalmusic.png",
    // ]
    // queue.loadManifest(fest);
    // queue.on("fileload", handleFileprogress, this);
    // queue.on("complete", handleComplete, this);
    var progress = 0;
    var sounds = [];

    function handleFileprogress(e) {
        progress++;
        if (progress > (fest.length - 1)) {
            progress = (fest.length - 1);
        }
        var nums = parseInt(progress / (fest.length - 1) * 100);
        if (nums < 10) {
            nums = "0" + nums;
        };
        var numsHtml = "";
        if (nums.toString().length >= 3) {
            numsHtml += '<em class="nums' + nums.toString().split("")[0] + '"></em>';
            numsHtml += '<em class="nums' + nums.toString().split("")[1] + '"></em>';
            numsHtml += '<em class="nums' + nums.toString().split("")[2] + '"></em>';
        } else {
            numsHtml += '<em class="nums' + nums.toString().split("")[0] + '"></em>';
            numsHtml += '<em class="nums' + nums.toString().split("")[1] + '"></em>';
        };
        $(".page0 .nums").html(numsHtml + '<em class="numss"></em>');
    }

    function handleComplete() {}


    // handleComplete();

    var param = {
        isReg: false //初始化是否注册
    };
    var game = {


        init: function() {

            var _this = this;

            // 所有事件
            _this.addEvent();

            // 正常进入展示
            _this.curPage1();

            // 分享出去展示
            // _this.curPage2();
            if ($(".result").height() - $(".result-mid").height() >= -10) {
                $("body").addClass("less")
            } else {
                $("body").removeClass("less")
            }


        },

        addEvent: function() {
            var _this = this;
            // 点击马上测试
            $(".btn-start").on("click", function() {

                // 如果没注册 弹出注册
                if (!param.isReg) {
                    $(".reg").fadeIn();
                } else {
                    // 显示上传弹窗
                    _this.pupLoad();
                }
            })
            $(".my-goods").on("click", function() {
                $(".goods").fadeIn();
            })
            $(".my-saying").on("click", function() {
                $(".saying").fadeIn();
            });
            $(".btn-next").on("click", function() {
                param.isReg = true;
                // 隐藏注册
                $(".reg").fadeOut();
                // 显示上传弹窗
                _this.pupLoad();
            });

            // 查看结果
            $(".btn-go").on("click", function() {
                _this.curPage2();
            })


            // 查看排行
            $(".btn-see").on("click", function() {
                _this.curPage1();
            })

            // 重新测试
            $(".btn-again").on("click", function() {
                _this.curPage1();
            })


            // 我也要玩
            $(".btn-meto").on("click", function() {
                _this.curPage1();
            })


            $("a").on("touchstart", function() {
                $(this).addClass("on");
            });
            $("a").on("touchend", function() {
                $(this).removeClass("on");
            });
            $(".close").on("click", function() {
                $(this).closest(".layer").fadeOut();
            });


        },

        // 页面1
        curPage1: function() {
            pageHide($(".page"));
            pageShow($(".page1"));
            $(".upload1 img").attr("src", "");
            $(".upload2 img").attr("src", "");
        },

        // 上传弹窗
        pupLoad: function() {
            $(".upload").fadeIn();
        },


        // 页面2
        curPage2: function() {
            pageHide($(".page"));
            pageShow($(".page2"));
            $(".upload").fadeOut();
        },

        // 页面3 分享出去显示的页面
        curPage3: function() {
            pageHide($(".page"));
            pageShow($(".page3"));
            $(".upload").fadeOut();
        }

    }
    setTimeout(function() {
        game.init();
    }, 200)



})