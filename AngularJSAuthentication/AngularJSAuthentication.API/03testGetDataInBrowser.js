var url = "/api/Orders";
var xhr = new XMLHttpRequest();
xhr.open("GET", url, true);

//Send the proper header information along with the request
xhr.setRequestHeader("Authorization", "Bearer AnI_TcLhvuGr1uPfIjezImMMkYpWGNrPUZJuldgc5QLZTNQywYkvnAQebfU5htofXM0dwDpC_cGrNnr_R2P-Frmbd4ZnTJC4Pgo5_4Y8MA1CFGDIIwdPw7Z9rbXRYNHLv7PekwhJvLroL678vbjTkSW0UzXNSoG4JlDzGEfNkUJxFT3vIRFrkSHMTakZ-vAy76CiZZqpQBiccS8fr-CodIZjrH3uOVwAu9CIw0h2EDY");

xhr.send(params);