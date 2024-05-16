
$(document).ready(function () {


    var PageNumber = 1;
    var PageSize = 10;
    var CurrentPage = 0;
    var TotalRecord = 0;
    var counter;

    var CustomerName = sessionStorage.getItem("CustomerNamekey");
    $('#CustomerName').val(CustomerName);

    var ProductId = sessionStorage.getItem("ProductIdkey");
    $('#ProductId').val(ProductId);

    var ProductCategoriesId = sessionStorage.getItem("ProductCategoriesIdkey");
    $('#ProductCategoriesId').val(ProductCategoriesId);

   
      

    function GetList() {
        var formData = $('#searchForm').serialize();
        formData += '&PageNumber=' + PageNumber;
        formData += '&PageSize=' + PageSize;
        console.log(formData);
          
        $.ajax({
            url: "/ShopMaster/GetList",
            method: "GET",
            data: formData,
            dataType: "json",
            success: function (data) {
                RenderList(data.MasterList)
                renderPagination(data.TotalCount, data.PageNumber);
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

    function Delete(id, CustomerName) {
        CustomerName

        Swal.fire({
            title: "Are you sure?",
            html: "To Delete <b>" + CustomerName + "</b> record.",
          //  text: "To Delete " + CustomerName + " record.",
            icon: "warning",
            showCancelButton: true,
            confirmButtonColor: "#3085d6",
            cancelButtonColor: "#d33",
            confirmButtonText: "Yes, delete it!"
        }).then((result) => {
            if (result.isConfirmed) {
                Swal.fire({
                    title: "Deleted!",
                    text: "Your file has been deleted.",
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
        if (list.length != 0)
        {

        $.each(list, function (index, item) {
            var row = '<tr align="center" style="border: 1px solid #807d78;">' +
                '<td scope="col" style="border: 1px solid #807d78;">' + item.CustomerName + '</td>' +
                '<td scope="col" style="border: 1px solid #807d78;">' + item.DateSaleOrders + '</td>' +
                '<td scope="col" style="border: 1px solid #807d78;">' + item.ProductName + '</td>' +
                '<td scope="col" style="border: 1px solid #807d78;">' + item.ProductCategory + '</td>' +
                '<td scope="col" style="border: 1px solid #807d78;">' + item.Quantity + '</td>' +
                '<td scope="col" style="border: 1px solid #807d78;">' + item.ProductPrice + '</td>' +
                '<td scope="col" style="border: 1px solid #807d78;">' + item.TotalAmmount + '</td>' +
                '<td scope="col" style="border: 1px solid #807d78;">' + '<button id="EditSaleOrdersDetails" value="' + item.SaleOrdersDetailsId + '" class="btn ">Edit</button>' +
                '&nbsp;&nbsp;' + '<button id = "deleteSaleOrder" data-name="' + item.CustomerName + '" value="' + item.SaleOrdersDetailsId + '" class="btn" >Delete</button>' + '</td>'
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
           

        var CustomerName = $('#CustomerName').val();
        sessionStorage.setItem('CustomerNamekey', CustomerName);

        var ProductId = $('#ProductId').val();
        sessionStorage.setItem('ProductIdkey', ProductId);

        var ProductCategoriesId = $('#ProductCategoriesId').val();
        sessionStorage.setItem('ProductCategoriesIdkey', ProductCategoriesId);
        console.log("ProductCategoriesId" + ProductCategoriesId);

        PageNumber = 1;
          
        GetList();
    }



    $('#reset').click(function () {
     
        sessionStorage.clear();
        $('#searchForm').find('form')[0].reset();
                             
        GetList();
          
    })



    $('#PageSize').change(function () {
        PageSize = $(this).val();
        PageNumber = 1;
    
          
        GetList();
    })


    $(document).on('click', '#insert', function () {
          
        Insert();  
    });


    $(document).on('click', '#searchbtn', function () {
          
        event.preventDefault(); // Prevent default form submission
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

    //function oldEdit(id) {
    //    console.log("pagenumber=>" + PageNumber);
    //    window.location.href = '/ShopMaster/Edit?SaleOrdersDetailsId=' + id + '&PageNumber=' + PageNumber;
    //}



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
        PageSize = $('#PageSize').val();
        sessionStorage.setItem('PageNumberkey', PageNumber);
      
          
        GetList();

    })





    function editSave() {
        var formData = $('#form').serialize();
        PageSize = $('#PageSize').val();
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



    $(document).on('click', '#saveBtn', function () {
        editSave();

    })
    function display() {
        debugger;
        $('#displayPager').empty();
        var StartRecord = (PageNumber - 1) * PageSize + 1;
        var EndRecord = (PageNumber * PageSize);

        if (EndRecord > TotalRecord) {
            EndRecord = TotalRecord;
        }
        $('#displayPager').html('<h5><strong>Showing</strong> '+StartRecord+' - '+EndRecord+' <strong> of </strong> '+TotalRecord+'<strong> records </strong> </h5>');
       
    }


    GetList();

})
