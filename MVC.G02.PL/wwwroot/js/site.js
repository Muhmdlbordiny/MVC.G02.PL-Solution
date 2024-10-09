// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
let element = document.getElementById('id');
let tbl = document.getElementById('tble');
element.addEventListener("key", () => {
    //send Request to the backend
    let xhr = XMLHttpRequest();
    let url = `https://localhost:44393/Employee/Index?Inputsearch=${element.value}`;
        xhr.open("post", url, true);
        xhr.onreadystatechange = function () {
            if (this.readyState == 4 && this.readyState == 200) {
                element.innerHTML = tbl.value;
                console.log(this.responseText);
            }
        }
        xhr.send();
    

})
