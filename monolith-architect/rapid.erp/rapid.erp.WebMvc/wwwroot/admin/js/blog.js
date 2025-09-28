
function ShowHideCross() {
    if ($("#primaryImg").attr("src") != "/default/images/default-addphoto-image.jpg")
    {
        $("#crossImg").css("display", "block");
    }
    else
    {
        $("#crossImg").css("display", "none");
    }
}

function GetMediaWithPaging() {
    var name = $('#serchName').val();
    var date = $("#date").val();
    var page = $('#page').val();

    $(document).unbind(".firstCall");
    $(document).on("ajaxStart.firstCall", function () {
        App.LoaderShow();
    });
    $(document).on("ajaxStop.firstCall", function () {
        App.LoaderHide();
    });

    $.ajax({
        type: "POST",
        url: "/Blog/GetMediaWithPaging",
        data: { name: name, date: date, page: page },
        dataType: "html",
        success: function (msg) {
            if (msg == '')
                $('#page').val('end');
            else
                $("#allMedia").append(msg);
        },
        error: function (req, status, error) {
            alert(error);
        }
    });
    return false;
}

function Initialize() {
    $('#page').val('1');
    $('#allMedia').html('');
}

var dataTableObjData;

var Blog = function () {

    var loadDataTables = function (dataTableId, iDisplayLength, sAjaxSourceUrl) {

        $.fn.dataTable.ext.errMode = () => alert('We are facing some problem while processing the current request. Please try again later.');

        dataTableObjData = $('#' + dataTableId).DataTable({
            "bJQueryUI": true,
            "bAutoWidth": true,
            "sPaginationType": "full_numbers",
            "bPaginate": true,
            "iDisplayLength": iDisplayLength,
            "bSort": false,
            "bFilter": true,
            "bSortClasses": false,
            "lengthChange": false,
            "oLanguage": {
                "sLengthMenu": "Display _MENU_ records per page",
                "sZeroRecords": "Data not found.",
                "sInfo": "Page _START_ to _END_ (about _TOTAL_ results)",
                "sInfoEmpty": "Page 0 to 0 (about 0 results)",
                "sInfoFiltered": ""
            },
            "bProcessing": true,
            "bServerSide": true,
            "initComplete": function (settings, json) {
                App.SetDataTableSearch(dataTableId);
            },
            "drawCallback": function (settings) {
            },

            ajax: sAjaxSourceUrl,
            columns: [
                {
                    name: 'Id',
                    data: 'id',
                    title: "Id",
                    sortable: false,
                    searchable: false,
                    visible: false
                },
                {
                    name: 'Name',
                    data: 'name',
                    title: "Name",
                    sortable: false,
                    searchable: false
                },
                {
                    name: 'Url',
                    data: 'url',
                    title: "Url",
                    sortable: false,
                    searchable: false
                },
                {
                    name: 'MetaTitle',
                    data: 'metaTitle',
                    title: "Meta Title",
                    sortable: false,
                    searchable: false
                },
                {
                    name: 'MetaKeyword',
                    data: 'metaKeyword',
                    title: "Meta Keyword",
                    sortable: false,
                    searchable: false
                },
                {
                    name: 'MetaDescription',
                    data: 'metaDescription',
                    title: "Meta Description",
                    sortable: false,
                    searchable: false
                },
                {
                    name: 'Id',
                    data: "id",
                    title: "Actions",
                    sortable: false,
                    searchable: false,
                    className: "w-auto text-center" ,
                    "mRender": function (data, type, row) {
                        return ' <a href="/Blog/Add/' + row.id + '\" data-href=\"/Blog/Add/' + row.id + '\" data-name="' + row.siteTitle + '" data-id="' + row.id + '" title="Edit" class="btn btn-warning ml-2">Edit</a>'
                            + ' <button data-href=\"/Blog/Delete/' + row.id + '\" data-name="' + row.siteTitle + '" data-id="' + row.id + '" title="Delete" onclick="AppModal.DeleteCommon(this)" class="btn btn-danger ml-2">Delete</button>';
                        //return data;

                    },
                }
            ]

        });

    };

    var initBlog = function () {

        ShowHideCross();

        $("#primaryImg").click(function () {
            $("#myModal").modal("show");
            if ($('#page').val() != 'end') {
                Initialize();
                GetMediaWithPaging();
            }
        });

        $("#primaryImg").click(function () {
            $("#myModal").modal("show");
            if ($('#page').val() != 'end') {
                Initialize();
                GetMediaWithPaging();
            }
        });

        $("#crossImg").click(function () {
            $("#primaryImg").attr("src", "/default/images/default-addphoto-image.jpg");
            $("#PrimaryImageId").val("");
            $("#PrimaryImageUrl").val("");

            $(this).hide();
        });

        $(".modal-footer > button").click(function () {
            var imageId = $("#allMedia div input[type='checkbox']:checked").attr("data-img-id");
            var imageUrl = $("#allMedia div input[type='checkbox']:checked").attr("data-img-url");

            $("#primaryImg").attr("src", imageUrl);
            $("#PrimaryImageUrl").val(imageUrl);
            $("#PrimaryImageId").val(imageId);

            ShowHideCross();

        });

        $("#allMedia").on("change", "input[type='checkbox']", function () {
            if ($(this).prop('checked')) {
                $("#allMedia li input[type='checkbox']").prop("checked", false);
                $("#allMedia li").removeClass("selected");
                $(this).prop("checked", true);
                $(this).parent().addClass("selected");
            }
            else
            {
                $(this).parent().removeClass("selected");
            }
        });

        $('.modal-body').scroll(function () {
            var div = $(this);
            if (div[0].scrollHeight - div.scrollTop() - 100 <= div.height()) {
                //alert('Reached the bottom!');
                if ($('#page').val() != 'end') {
                    var page = parseInt($('#page').val());
                    page = page + 1;
                    $('#page').val(page);
                    GetMediaWithPaging();
                }
            }
        });

        $("#serchName").keyup(function () {
            Initialize();
            GetMediaWithPaging();
        });

        $('#date').change(function () {
            Initialize();
            GetMediaWithPaging();
        });

    };

    return {
        LoadDataTables: loadDataTables,
        InitBlog: initBlog
    };
}();