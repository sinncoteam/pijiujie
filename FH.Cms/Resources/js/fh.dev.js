var sysevt =
{
    layerin: 0,
    showtip: function (content, title, callback)
    {
        $("#x_tip_content").html(content);
        if (undefined != title && title != "")
        {
            $("#x_tip_title").text(title);
        }
        else
        {
            $("#x_tip_title").text("提示");
        }
        $('#x_tip').modal().off("hidden");
        if ("function" == typeof (callback))
        {
            $('#x_tip').on('hidden', function ()
            {
                callback();
            });
        }
    },
    showcontent: function (ue, content, title)
    {
        var cont = "<div class=\"alert alert-block clear\"> <a class=\"close\" data-dismiss=\"alert\" href=\"javascript:;\" onclick='javascript:$(this).parent().hide();'>×</a>";
        if (undefined != title && title != "")
        {
            cont += "<h4 class=\"alert-heading\" >" + title + "</h4>";
        }
        else
        {
            cont += "<h4 class=\"alert-heading\" >提示</h4>";
        }
        cont += content + "</div>";
        $(ue).html(cont);
    },
//    showinput: function (v)
//    {
//        var vtmp = "";
//        if ("undefined" != typeof (v))
//        {
//            vtmp = v;
//        }
//        this.layerin++;
//        var vid = "x_modal_" + this.layerin;
//        var sput = " <div class=\"modal hide\" id=\"" + vid + "\">              <div class=\"modal-header\">                <button data-dismiss=\"modal\"  class=\"close\" type=\"button\">×</button>                <h3 id=\"x_tip_title\">请输入</h3>              </div>              <div class=\"modal-body\">                <p id=\"x_tip_content\"> 名称：<input type='input' value='" + vtmp + "' /> <input type=\"button\" onclick=\"javascript:sysevt.showinputcall();\" class=\"btn btn-success btn-mini\" value=\"确 定\" data-dismiss=\"modal\"/>   </p>              </div>            </div>";
//        $(document.body).append(sput);
//        $("#"+ vid).modal().off("hidden");
//        return "#"+vid;
//    },
//    showinputgetval: function (vid)
//    {
//        return $(vid).find("input[type=input]").val();
//    },
//    showinputcall: function () { },
    showinputclose: function (vid)
    {
        $("#" + vid).remove();
    },
    showloading: function (ue)
    {
        var cont = "<div class=\"progress progress-striped active\" style=\"height:8px;\">              <div class=\"bar\" style=\"width: 100%;\"></div>            </div>";
        $(ue).html(cont).show();
    },
    closeloading: function (ue)
    {
        setTimeout(function ()
        {
            $(ue).empty().hide();
        }, 500);
    },
    UnixToDate: function (unixTime, isFull, timeZone)
    {
        if (unixTime != undefined)
        {
            unixTime = unixTime.replace("/Date(", "").replace(")/", "");
        }
        if (typeof (timeZone) == 'number')
        {
            unixTime = parseInt(unixTime) + parseInt(timeZone) * 60 * 60;
        }
        var time = new Date(unixTime * 1);
        var ymdhis = "";
        ymdhis += time.getFullYear() + "-";
        ymdhis += (time.getMonth() + 1) + "-";
        ymdhis += time.getDate();
        if (ymdhis == "1-1-1")
        {
            return "";
        }
        if (isFull === true)
        {
            ymdhis += " " + time.getHours() + ":";
            ymdhis += time.getMinutes() + ":";
            ymdhis += time.getSeconds();
        }

        return ymdhis;
    }
}