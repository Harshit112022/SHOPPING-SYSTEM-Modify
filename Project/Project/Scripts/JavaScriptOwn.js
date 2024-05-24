var expanded = false;
function showCheckboxes() {
    var checkboxes = document.getElementById("checkboxes");
    if (!expanded) {
        checkboxes.style.display = "block";
        expanded = true;
    } else {
        checkboxes.style.display = "none";
        expanded = false;
    }
}

var PageNumber;
var PageSize ;
var TotalRecord = 0;
var counter;
debugger;


function GetList() {
    var formData = $('#searchForm').serialize();
    formData += '&PageNumber=' + PageNumber ;
    formData += '&PageSize=' + PageSize;
    debugger;
    expanded = false;
    console.log(formData);
    $.ajax({
        url: "/ShopMaster/GetList",
        method: "GET",
        data: formData,
        dataType: "json",
        success: function (data) {
            RenderList(data.MasterList)
            renderPagination(data.TotalCount, data.PageNumber);
            debugger;
            TotalRecord = data.TotalRecord;    
            display();
        },
        error: function (err) {
            console.log(err);
        }
    });
}


function Insert() {

    $.ajax({
        url: "/ShopMaster/Insert",
        method: "GET",
        success: function (response) {
            $('.modal-body').html(response);
        },
        error: function (xhr, status, error) {
            console.error(xhr.responseText);
        }
    });
}



function display() {
   
    $('#displayPager').empty();
    var StartRecord = (PageNumber - 1) * PageSize + 1;
    var EndRecord = (PageNumber * PageSize);
    debugger;
    if (EndRecord > TotalRecord) {
        EndRecord = TotalRecord;
    }
    $('#displayPager').html('<h5><strong>Showing</strong> ' + StartRecord + ' - ' + EndRecord + ' <strong> of </strong> ' + TotalRecord + '<strong> records </strong> </h5>');

}



function Delete(id, CustomerName) {
    CustomerName
    debugger;

    Swal.fire({
       // title: "Are you sure? ",
        html: " Are you sure to Delete <b>" + CustomerName + "</b> record.",
        //  text: "To Delete " + CustomerName + " record.",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes",
        cancelButtonText :"No"
    }).then((result) => {
        if (result.isConfirmed) {
            Swal.fire({
                title: "Deleted!",
                text: "Your recode has been deleted.",
                icon: "success"
            });
            var obj = { id: id };
            $.ajax({
                url: "/ShopMaster/Delete",

                type: 'POST',
                data: obj,
                success: function () {
                    console.log('Success..')
                    GetList(null, null, null, null, null, PageNumber, PageSize);
                },
                error: function () {
                    console.log(error);
                }
            })
        }
    })
}



function Edit(id) {

    var obj = {
        SaleOrdersDetailsId: id,
        // PageNumber: PageNumber
    }
    $.ajax({
        url: '/ShopMaster/Edit/ ' + id,
        method: 'GET',
        data: obj,
        success: function (response) {
            $('.modal').modal();
            $('.modal-body').html(response);
            console.log('Edit Working... 2');
        },
        eror: function () {
            console.log(error);
        }
    })
}



function RenderList(list) {
    var tableBody = $('#tblMasterShop tbody');
    tableBody.empty();
    if (list.length != 0) {

        $.each(list, function (index, item) {
            var row = '<tr align="center" style="border: 1px solid #807d78;">' +
                '<td scope="col" style="border: 1px solid #807d78;">' + item.CustomerName + '</td>' +
                '<td scope="col" style="border: 1px solid #807d78;">' + item.DateSaleOrders + '</td>' +
                '<td scope="col" style="border: 1px solid #807d78;">' + item.ProductName + '</td>' +
                '<td scope="col" style="border: 1px solid #807d78;">' + item.ProductCategory + '</td>' +
                '<td scope="col" style="border: 1px solid #807d78;">' + item.Quantity + '</td>' +
                '<td scope="col" style="border: 1px solid #807d78;">' + item.ProductPrice + '</td>' +
                '<td scope="col" style="border: 1px solid #807d78;">' + item.TotalAmmount + '</td>' +
                '<td scope="col" style="border: 1px solid #807d78;">'
                + '<a href="/ShopMaster/Edit?SaleOrdersDetailsId=' + item.SaleOrdersDetailsId + '" role="button" class="btn ">Edit</a>' + '&nbsp;&nbsp;' +
                '<button id="EditSaleOrdersDetails" value="' + item.SaleOrdersDetailsId + '" class="btn ">Edit</button>' +
                '&nbsp;&nbsp;' + '<button id = "deleteSaleOrder" data-name="' + item.CustomerName + '" value="' + item.SaleOrdersDetailsId + '" class="btn" >Delete</button>' +
                '</td>'
            tableBody.append(row);
        }
        )
    }
    else {
        row = '<tr><td colspan="8" class="text-center text-danger font-weight-bold h3" style="vertical-align:middle;" scope="row">No Records Found</td></tr>'
        tableBody.append(row);
    }
}


function renderPagination(TotalCount, PageNumber) {
    $(".pagination").empty()
    if (PageNumber > 1) {
        $(".pagination").append(`<li class="page-item "  value=${PageNumber - 1} id="pagenumber"><a class="page-link" href="#" tabindex="-1">Previous</a></li>`);
    }
    for (var i = 1; i <= TotalCount; i++) {
        if (PageNumber == i) {
            $(".pagination").append(` <li class="page-item active" value=${i} id="pagenumber" ><a class="page-link" href="#">${i}</a></li>`)
        } else {
            $(".pagination").append(` <li class="page-item "   value=${i} id="pagenumber"><a class="page-link" href="#">${i}</a></li>`)
        }
    }
    if (TotalCount != PageNumber) {
        $(".pagination").append(`<li class="page-item"   value=${PageNumber + 1} id="pagenumber"><a class="page-link" href="#">Next</a></li>`)

    }
}


function search() {
    //var CustomerName = $('#CustomerName').val();
    //sessionStorage.setItem('CustomerNamekey', CustomerName);    
    PageNumber = 1;
    GetList();
}


function Pagesize() {
    PageSize = $('#PageSize').val();
    PageNumber = 1;
    GetList();
}


function editSave() {
    var formData = $('#form').serialize();
 
    debugger;
    $.ajax({
        url: "/ShopMaster/Edit",
        method: "POST",
        data: formData,
        dataType: "json",
        success: function (response) {
            console.log("hello.....");
            GetList();
        },
        error: function (err) {
            console.log(err);
        }
    });
}




$(document).ready(function () {
    if (sessionStorage.getItem('boxes')) {
        let checkBoxArray = JSON.parse(sessionStorage.getItem('boxes'));
        $("#checkboxes").find('input:checkbox').each(function () {
            $(this).prop("checked", ($.inArray($(this).val(), checkBoxArray) !== -1));
        });
    }


    PageNumber = document.getElementById('xyz').getAttribute('data-page-number');;
    console.log(PageNumber)
    PageSize = $('#PageSize').val();
    debugger;
    GetList();
    debugger;
   
    //var CustomerName = sessionStorage.getItem("CustomerNamekey");
    //$('#CustomerName').val(CustomerName);     
    
    $(document).on('click', '#reset', function () {
        var checkboxes = document.getElementById("checkboxes");
        checkboxes.style.display = "none";
        $.ajax({
            url: '/ShopMaster/ResetSession/ ',            
            success: function (response) {
                $('#CustomerName').val(null);
                $('#ProductCategoriesId').val(null);
                $('#FromDate').val(null);
                $('#ToDate').val(null);
                $('input[name="ProductIdList"]').prop('checked', false);
                sessionStorage.clear();
                GetList();
               debugger;                  
            },
            eror: function () {
                console.log(error);
            }
        })
    })
  
    $(document).on('change', '#PageSize', function () {
        Pagesize()
    })


    $(document).on('click', '#insert', function () {
                 Insert();  

    });


    $(document).on('click', '#searchbtn', function () {
        event.preventDefault(); // Prevent default form submission
        var checkboxes = document.getElementById("checkboxes");
        checkboxes.style.display = "none";
        expanded = false;
        let checkboxArray = [];
        $('input:checkbox:checked').each(function () {
            checkboxArray.push($(this).val());;
        });
        sessionStorage.setItem('boxes', JSON.stringify(checkboxArray));       
        search();
    })
    $(document).on('click', '#EditSaleOrdersDetails', function () {
        var id = $(this).val();
        Edit(id);
    })

    $(document).on('click', '#OldEditSaleOrdersDetails', function () {
        var id = $(this).val();
        oldEdit(id);
    })

    
    $(document).on('click', '#deleteSaleOrder', function () {
        debugger
        var id = $(this).val();
        var CustomerName = $(this).data('name');
        console.log(CustomerName);
        Delete(id, CustomerName);
    })

    $(document).on('click', '#pagenumber', function () {
        console.log("Function get clicked..")
        PageNumber = $(this).val();
        debugger;
        PageSize = $('#PageSize').val();
        //sessionStorage.setItem('PageNumberkey', PageNumber);
        GetList();

    })

    $(document).on('click', '#saveBtn', function () {
 
            editSave();
    })

    $(document).on('click', '#InsertSave', function () {
        debugger;
        //$(document).off('click', '#InsertSave').on('click', '#InsertSave', function () {
        event.preventDefault();
        var formData = $('#formInsert').serialize();
        var isValid = true;      
        var CustomerId = $('#CustomerIdInsert').val();
        var ProductId = $('#productId2').val();
        var Quantity = $('#QuentityId').val();
        var Date = $('#dateInsert').val(); 
        if (CustomerId <= 0) {
            $('#CustomerIdErrorMassage').html("Please select Customer.");
            isValid = false;
        } else {
            $('#CustomerIdErrorMassage').html("It is valid !");
        }      
        if (ProductId <= 0) {
            $('#ProductIdErrorMassage').html("Please select Product.");
            isValid = false;
        } else {
            $('#ProductIdErrorMassage').html("It is valid !");
        }   
        if (Quantity <= 0) {
            $('#QuantityErrorMassage').html("Quantity should be greater than Zero '0'.");
            isValid = false;
        } else {
            $('#QuantityErrorMassage').html("It is valid !");
        }    
        if (Date === "") {
            $('#DateErrorMassage').html("Please select Date.");
            isValid = false;
        } else {
            $('#DateErrorMassage').html("It is valid !");
        }   
        if (isValid) {
            debugger;
            $.ajax({
                url: "/ShopMaster/Insert",
                method: "POST",
                data: formData,
                success: function (response) {
                    $('.modal').modal().hide();
                    $('.modal-backdrop').remove();
                   
                    GetList();
                },
                error: function (err) {
                    console.log(err);
                }
            });
        }
    });

})
