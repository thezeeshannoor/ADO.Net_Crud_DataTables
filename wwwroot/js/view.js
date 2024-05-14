//// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
//// for details on configuring this project to bundle and minify static web assets.

//// Write your JavaScript code.
//$(document).ready(function () {
//    //index view ajax call for db


//    $.ajax({
//        type: "Get",
//        url: "Home/Indexx",
//        contenttype: "application/json; charset=utf-8",
//        datatype: "json",
//        success: function (data) {
//            console.log(data);
//            //for (let i = 0; i<data.length; i++) {
//            //    $("#tablebody").append(`<tr><td>${data[i].id}</td><td>${data[i].fname}</td><td>${data[i].lname}</td><td>${data[i].clas}</td><td>${data[i].contact}</td><td>${data[i].address}</td></tr>`);
//            //};

//            $('#myTable').DataTable({
//                "responsive": true,
//                //"processing": true,
//                "lengthChange": true,
//                /*  "serverSide": true,*/

//                "lengthMenu": [[5, 10, 100, -1], [5, 10, 100, "All"]],

//                columns: [
//                    { "data": "id", autoWidth: true },
//                    { "data": "fname", autoWidth: true },
//                    { "data": "lname", autoWidth: true },
//                    { "data": "clas", autoWidth: true },
//                    { "data": "contact", autoWidth: true },
//                    { "data": "address", autoWidth: true }
//                ]
//            });

//        },
//        error: function (xhr, status, error) {
//            console.error("Error occurred:", error);
//            console.error("Status:", status);
//            console.error("Error:", error);
//        }

//    });



//});
////Without Data Table View 


$(document).ready(function () {
    window.Delete = function (id) {
        $.ajax({
            type: "POST",
            url: "Home/DeleteField",
            data: { id },
            success: function () {
                console.log("Student deleted");
            },
            error: function (err) {
                console.log(err);
            }
        });
    }
    window.updateField = function (ele, fieldName, id) {
        let val = $(ele).text();
        $.ajax({
            
            url: "Home/UpdateField",
            type: "POST",
            data: { val, fieldName, id },
            success: function () {
                $('#myTable').DataTable().ajax.reload();
            },
            error: function (error) {
                console.log(error);
            }
        });
    }
      $('#myTable').DataTable({

        "serverSide": true,
          "processing": true,
          "pagingType": "full_numbers",
          "lengthMenu": [[1,5, 10, 100, -1], [1,5, 10, 100, "All"]],
        "ajax": {
            "url": "/Home/Indexx",
            "type": "Post",
            "datatype": "json"
        },
        "columns": [

            { "data": "fname", "render": function (data, type, row) { return `<div  contenteditable="true" onBlur="updateField(this,'fname',${row.id})" >${data}</div>` } },
            { "data": "lname", "render": function (data, type, row) { return `<div  contenteditable="true" onBlur="updateField(this,'lname',${row.id})" >${data}</div>` } },
            { "data": "clas", "render": function (data, type, row) { return `<div  contenteditable="true" onBlur="updateField(this,'class',${row.id})" >${data}</div>` } },
            { "data": "contact", "render": function (data, type, row) { return `<div  contenteditable="true" onBlur="updateField(this,'contact',${row.id})" >${data}</div>` } },
            { "data": "address", "render": function (data, type, row) { return `<div  contenteditable="true" onBlur="updateField(this,'address',${row.id})" >${data}</div>` } },
            { "data": null, "render": function (data, type, row) { return `<button class="btn btn-danger" onclick="Delete(${row.id})" >Delete</button>` } },
            
            
           
         
            
        ]
      });
   
    
});