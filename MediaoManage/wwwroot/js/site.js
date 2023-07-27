// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function uploadMedia() {
    var x = document.getElementById("uploadfile_id");    
    if (x.files.length > 0) {
        const myArray = x.files[0].type.split("/");
        if (myArray[0] != "video") {
            alert("you can only upload video!");
            x.value = "";
            return;
        }
        var filename = document.getElementById("filename_id");
        var filetype = document.getElementById("filetype_id");
        var mediaurl = document.getElementById("mediaurl_id");
        filename.value = x.files[0].name;
        mediaurl.value = Date.now().toString() + x.files[0].name;
        filetype.value = x.files[0].type;
    }
}
function uploadThumbnail() {
    var x = document.getElementById("uploadthumbnail_id");
    if (x.files.length > 0) {
        const myArray = x.files[0].type.split("/");
        if (myArray[0] != "image") {
            alert("you can only upload image!");
            x.value = "";
            return;
        }
        var thumbnail_url = document.getElementById("thumbnailurl_id");      
        thumbnail_url.value = Date.now().toString() + x.files[0].name;
    }
}