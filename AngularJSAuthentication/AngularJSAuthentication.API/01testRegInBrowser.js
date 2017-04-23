var url = "/api/Account/Register";
var params = "userName=alex&password=dsadsa&confirmPassword=dsadsa";
var xhr = new XMLHttpRequest();
xhr.open("POST", url, true);

//Send the proper header information along with the request
xhr.setRequestHeader("Content-type", "application/x-www-form-urlencoded");

xhr.send(params);