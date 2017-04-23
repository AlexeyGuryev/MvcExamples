var url = "/token";
var params = "grant_type=refresh_token&refresh_token=63bd8b6348f54b3c985369ad7080c6e7&client_id=ngAuthApp";
var xhr = new XMLHttpRequest();
xhr.open("POST", url, true);

//Send the proper header information along with the request
xhr.setRequestHeader("Content-type", "application/x-www-form-urlencoded");

xhr.send(params);