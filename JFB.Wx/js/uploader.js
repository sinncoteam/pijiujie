var fctype = 1;
function fileSelected(t, type)
{
    fctype = type;
    var file = t.files[0];
    if (file)
    {
        var fileSize = 0;
        if (file.size > 1024 * 1024)
            fileSize = (Math.round(file.size * 100 / (1024 * 1024)) / 100).toString() + 'MB';
        else
            fileSize = (Math.round(file.size * 100 / 1024) / 100).toString() + 'KB';
        if (file.size > 1024 * 1024 * 5)
        {
            alert("请上传低于5MB的图片！");
            return;
        }
        uploadFile(t, type);
//        document.getElementById('fileName').innerHTML = 'Name: ' + file.name;
//        document.getElementById('fileSize').innerHTML = 'Size: ' + fileSize;
//        document.getElementById('fileType').innerHTML = 'Type: ' + file.type;
    }
}

function uploadFile(t, type)
{
    var fd = new FormData();
    fd.append("ft", t.files[0]);
    var xhr = new XMLHttpRequest();
    //xhr.upload.addEventListener("progress", uploadProgress, false);
    xhr.addEventListener("load", uploadComplete, false);
    xhr.addEventListener("error", uploadFailed, false);
    xhr.addEventListener("abort", uploadCanceled, false);
    xhr.open("POST", "/pjj/home/upfile");
    xhr.send(fd);
    $("#x_up_gress").html("正在上传照片，请稍候... ");
}

function uploadProgress(evt)
{
    if (evt.lengthComputable)
    {
        var percentComplete = Math.round(evt.loaded * 100 / evt.total);
        document.getElementById('progressNumber').innerHTML = percentComplete.toString() + '%';
    }
    else
    {
        document.getElementById('progressNumber').innerHTML = 'unable to compute';
    }
}

function uploadComplete(evt)
{
    $("#x_up_gress").text("上传成功");
    setTimeout(function ()
    {
        $("#x_up_gress").text("若两张图片都已上传完成，无需等待图片完全显示出来，可先点击“查看结果”查看匹配度");
    }, 1000);
    var sour = $.parseJSON(evt.target.responseText);
    $(".upimg" + fctype + ">img").attr("src", sour.Source).css("width","100%").css("height","100%");
}

function uploadFailed(evt)
{
    alert("文件上传失败");
}

function uploadCanceled(evt)
{
    //alert("The upload has been canceled by the user or the browser dropped the connection.");
}