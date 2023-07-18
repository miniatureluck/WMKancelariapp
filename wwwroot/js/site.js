/*!
* Start Bootstrap - Simple Sidebar v6.0.6 (https://startbootstrap.com/template/simple-sidebar)
* Copyright 2013-2023 Start Bootstrap
* Licensed under MIT (https://github.com/StartBootstrap/startbootstrap-simple-sidebar/blob/master/LICENSE)
*/
// 
// Scripts
// 

$('.filter-button').click(function () {
    $('.filter-section').slideToggle(500);
    $('.filter-button').toggleClass("filter-button-toggled")
});

//function dropdownToggle() {
//    document.getElementById("dropdown-content").classList.toggle("display");
//}

//function filterFunction() {
//    var input, filter, ul, li, a, i;
//    input = document.getElementById("dropdownInput");
//    filter = input.value.toUpperCase();
//    div = document.getElementById("dropdown-content");
//    a = div.getElementsByTagName("a");
//    for (i = 0; i < a.length; i++) {
//        txtValue = a[i].textContent || a[i].innerText;
//        if (txtValue.toUpperCase().indexOf(filter) > -1) {
//            a[i].style.display = "block";
//        } else {
//            a[i].style.display = "none";
//        }
//    }
//}

function filterNames() {

    var input, filter, table, tr, td, i, txtValue;
    input = document.getElementById("clientNameInput");
    filter = input.value.toUpperCase();
    table = document.getElementById("resultsTable");
    tr = table.getElementsByTagName("tr");

    for (i = 0; i < tr.length; i++) {
        td = tr[i].getElementsByTagName("td")[0];
        if (td) {
            txtValue = td.textContent || td.innerText;
            if (txtValue.toUpperCase().indexOf(filter) > -1) {
                tr[i].style.display = "";
            } else {
                tr[i].style.display = "none";
            }
        }
    }
};

window.addEventListener('DOMContentLoaded', event => {

    const sidebarToggle = document.body.querySelector('#sidebarToggle');
    if (sidebarToggle) {

         if (localStorage.getItem('sb|sidebar-toggle') === 'true') {
             document.body.classList.toggle('sb-sidenav-toggled');
         }
        sidebarToggle.addEventListener('click', event => {
            event.preventDefault();
            document.body.classList.toggle('sb-sidenav-toggled');
            localStorage.setItem('sb|sidebar-toggle', document.body.classList.contains('sb-sidenav-toggled'));
        });
    }

});
