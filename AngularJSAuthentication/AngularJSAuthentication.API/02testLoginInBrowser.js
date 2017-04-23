var url = "/token";
var params = "grant_type=password&username=alex&password=dsadsa&client_id=ngAuthApp";
var xhr = new XMLHttpRequest();
xhr.open("POST", url, true);

//Send the proper header information along with the request
xhr.setRequestHeader("Content-type", "application/x-www-form-urlencoded");

xhr.send(params);

// result:
//{"access_token":
//    "AnI_TcLhvuGr1uPfIjezImMMkYpWGNrPUZJuldgc5QLZTNQywYkvnAQebfU5htofXM0dwDpC_cGrNnr_R2P-Frmbd4ZnTJC4Pgo5_4Y8MA1CFGDIIwdPw7Z9rbXRYNHLv7PekwhJvLroL678vbjTkSW0UzXNSoG4JlDzGEfNkUJxFT3vIRFrkSHMTakZ-vAy76CiZZqpQBiccS8fr-CodIZjrH3uOVwAu9CIw0h2EDY",
//    "token_type":"bearer","expires_in":86399}