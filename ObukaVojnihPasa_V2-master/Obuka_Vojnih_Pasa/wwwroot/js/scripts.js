

function Ok() {
    var x = document.getElementById("confirm");
    x.style.display = "none";
}




function Prikazi(id) {
    var x = document.getElementById(id);
    if (x.style.display === "none") {

        x.style.display = "block";

    } else {

        x.style.display = "none";
    }
}



