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
var param = {
    isReg: false //初始化是否注册
};
$(function ()
{


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

    function handleFileprogress(e)
    {
        progress++;
        if (progress > (fest.length - 1))
        {
            progress = (fest.length - 1);
        }
        var nums = parseInt(progress / (fest.length - 1) * 100);
        if (nums < 10)
        {
            nums = "0" + nums;
        };
        var numsHtml = "";
        if (nums.toString().length >= 3)
        {
            numsHtml += '<em class="nums' + nums.toString().split("")[0] + '"></em>';
            numsHtml += '<em class="nums' + nums.toString().split("")[1] + '"></em>';
            numsHtml += '<em class="nums' + nums.toString().split("")[2] + '"></em>';
        } else
        {
            numsHtml += '<em class="nums' + nums.toString().split("")[0] + '"></em>';
            numsHtml += '<em class="nums' + nums.toString().split("")[1] + '"></em>';
        };
        $(".page0 .nums").html(numsHtml + '<em class="numss"></em>');
    }

    function handleComplete() { }


    // handleComplete();


    var game = {


        init: function ()
        {

            var _this = this;

            // 所有事件
            _this.addEvent();

            // 正常进入展示
            _this.curPage1();

            // 分享出去展示
            // _this.curPage2();
            if ($(".result").height() - $(".result-mid").height() >= -10)
            {
                $("body").addClass("less")
            } else
            {
                $("body").removeClass("less")
            }


        },
        seelist: function ()
        {
            $(".goods").fadeIn();
            $.ajax({ url: "./home/getlist", type: "post", dataType: "json", success: function (r)
            {
                if (r.Success)
                {
                    var html = "";
                    for (var i = 0; i < r.Source.length; i++)
                    {
                        var item = r.Source[i];
                        html += "<div class=\"tr\">";
                        html += "<div class=\"td\">" + item.I + "</div>";
                        html += "<div class=\"td\"><img src=\"" + item.HeadImage + "\" /></div>";
                        html += "<div class=\"td\">" + item.NickName + "</div>";
                        html += "<div class=\"td\">" + item.PerValue + "</div></div>";
                    }
                    $("#x_ulist").html(html);
                    $("#x_mytop").text(r.Code);
                }
            }, error: function (e)
            {
                alert("系统繁忙，请稍候再试！");
            }
            });
        },
        addEvent: function ()
        {
            var _this = this;
            // 点击马上测试
            $(".btn-start").on("click", function ()
            {

                // 如果没注册 弹出注册
                if (!param.isReg)
                {
                    $(".reg").fadeIn();
                } else
                {
                    // 显示上传弹窗
                    _this.pupLoad();
                }
            })
            $(".my-goods").on("click", function ()
            {

                _this.seelist();
            })
            $(".my-saying").on("click", function ()
            {
                $(".saying").fadeIn();
            });
            $(".btn-next").on("click", function ()
            {
                if (param.isReg)
                {
                    // 显示上传弹窗
                    _this.pupLoad();
                }
                else
                {
                    var xname = $("#x_realname").val();
                    var xphone = $("#x_phone").val();
                    var xages = $("#x_ages").val();
                    var xjob = $("#x_jobon").val();
                    if (xname == "") { alert("请输入您的姓名"); return; }
                    if (xphone == "") { alert("请输入您的电话"); return; }
                    if (xages == "") { alert("请输入您的年龄"); return; }
                    if (isNaN(xages)) { alert("年龄必须是整数"); return; }
                    $.ajax({
                        url: "./home/setlink", data: $("#form1").serialize(), dataType: "json", type: "post", success: function (r)
                        {
                            if (r.Success)
                            {
                                param.isReg = true;
                                // 隐藏注册
                                $(".reg").fadeOut();
                                // 显示上传弹窗
                                _this.pupLoad();
                            }
                            else
                            {
                                alert(r.Msg);
                            }
                        },
                        error: function (e)
                        {
                            alert("系统繁忙，请稍候再试！");
                        }
                    });
                }
            });

            // 查看结果
            $(".btn-go").on("click", function ()
            {
                $.ajax({
                    url: "./home/getlast", dataType: "json", type: "post",
                    success: function (r)
                    {
                        if (r.Success)
                        {
                            $("#x_fimg").attr("src", r.Source.FatherPhoto);
                            $("#x_cimg").attr("src", r.Source.ChildPhoto);
                            var pv = r.Source.PerValue;
                            if (pv > 0)
                            {
                                $("#x_perv").text(r.Source.PerValue + "% 匹配度");
                            }
                            else
                            {
                                $("#x_perv2").empty();
                            }
                            _this.curPage2();
                        }
                        else
                        {
                            alert(r.Msg);
                        }
                    },
                    error: function (e)
                    {
                        alert("系统繁忙，请稍候再试！");
                    }
                });

            })


            // 查看排行
            $(".btn-see").on("click", function ()
            {
                _this.seelist();
            })

            // 重新测试
            $(".btn-again").on("click", function ()
            {
                _this.curPage1();
            })


            // 我也要玩
            $(".btn-meto").on("click", function ()
            {
                _this.curPage1();
            })


            $("a").on("touchstart", function ()
            {
                $(this).addClass("on");
            });
            $("a").on("touchend", function ()
            {
                $(this).removeClass("on");
            });
            $(".close").on("click", function ()
            {
                $(this).closest(".layer").fadeOut();
            });


        },

        // 页面1
        curPage1: function ()
        {
            pageHide($(".page"));
            pageShow($(".page1"));
            $(".upload1 img").attr("src", "");
            $(".upload2 img").attr("src", "");
        },

        // 上传弹窗
        pupLoad: function ()
        {
            $(".upload").fadeIn();
        },


        // 页面2
        curPage2: function ()
        {
            pageHide($(".page"));
            pageShow($(".page2"));
            $(".upload").fadeOut();
        },

        // 页面3 分享出去显示的页面
        curPage3: function ()
        {
            pageHide($(".page"));
            pageShow($(".page3"));
            $(".upload").fadeOut();
        }

    }
    setTimeout(function ()
    {
        game.init();
    }, 200)



})