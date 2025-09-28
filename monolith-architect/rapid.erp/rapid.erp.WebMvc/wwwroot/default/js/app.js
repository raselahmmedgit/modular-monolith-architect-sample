
var appMessage = {
    Error: 'We are facing some problem while processing the current request. Please try again later.',
    NotFound: 'Requested object not found.',
    SaveSuccess: 'Save successfully.',
    UpdateSuccess: 'Update successfully.',
    DeleteSuccess: 'Delete successfully.'
};

var App = function () {
    var appAreaName = {
        Admin: 'Admin'
    };
    var appCopyUrl = function () {
        $(".app-copy-url").select();
        document.execCommand("copy");
    };
    var loaderShow = function () {
        $("#appPreLoader").addClass("appPreLoaderLight");
        $("#appPreLoader").fadeIn(100);
    };

    var loaderHide = function () {
        $("#appPreLoader").remove("appPreLoaderLight");
        $("#appPreLoader").fadeOut(100);
    };

    var sendAjaxRequest = function (url, data, isPost, callback, isAsync, isJson, target) {
        isJson = typeof (isJson) == 'undefined' ? true : isJson;
        var contentType = (isJson) ? "application/json" : "text/plain";
        var dataType = (isJson) ? "json" : "html";
        if (!isAsync) {
            App.LoaderShow();
        }

        return $.ajax({
            type: isPost ? "POST" : "GET",
            url: url,
            data: isPost ? JSON.stringify(data) : data,
            contentType: contentType,
            dataType: dataType,
            beforeSend: function (xhr) {
                App.LoaderShow();
            },
            success: function (successData) {
                if (!isAsync) {
                    App.LoaderHide();
                }
                return typeof (callback) == 'function' ? callback(successData) : successData;
            },
            complete: function (xhr, status) {
                App.LoaderHide();
            },
            error: function (exception) {
                return false;
            },
            async: isAsync
        });
    };

    var arrayToTree = function (arr, parent) {
        //arr.sort(function (a, b) { return parseInt(b.Level) - parseInt(a.Level) });
        var out = [];
        for (var i in arr) {
            if (arr[i].ParentId == parent) {
                var data = new Object();
                data.text = arr[i].Name;
                if (arr[i].Level == 3) {
                    data.id = arr[i].Id;
                } else {
                    var children = arrayToTree(arr, arr[i].Id);
                    if (children.length) {
                        data.children = children;
                    }
                }
                out.push(data);
            }
        }
        return out;
    };

    var loadDropdown = function (targetDropdown, dataSourceUrl, filterByValue) {

        App.SendAjaxRequest(dataSourceUrl, filterByValue, false, function (options) {
            var optionHtml = '';

            if ($.isArray(options) && (options.length > 0)) {

                $(options).each(function (index, option) {
                    if (option.selected == true) {
                        optionHtml += '<option value="' + option.value + '" selected>' + option.text + '</option>';
                    }
                    else {
                        optionHtml += '<option value="' + option.value + '" >' + option.text + '</option>';
                    }
                });

            }

            $('#' + targetDropdown).html(optionHtml);

        }, true);

        $('#' + targetDropdown).val(0);

    };

    var toastrNotifier = function (error, errortype) {
        toastr.clear();
        toastr.options = {
            "closeButton": true,
            "debug": false,
            "positionClass": "toast-top-center",
            "onclick": null,
            "showDuration": "1000",
            "hideDuration": "1000",
            "timeOut": "5000",
            "extendedTimeOut": "1000",
            "showEasing": "swing",
            "hideEasing": "linear",
            "showMethod": "fadeIn",
            "hideMethod": "fadeOut"
        };

        if (errortype == "info")
        {
            toastr.info(error, 'Info !');
        }
        else if (errortype == "warning")
        {
            toastr.warning(error, 'Warning !');
        }
        else if (errortype == "success") {
            toastr.success(error, 'Success !');
        }
        else if (errortype == "danger") {
            toastr.error(error, 'Error !');
        }
    };

    var toastrNotifierError = function (error) {
        toastr.clear();
        toastr.options = {
            "closeButton": true,
            "debug": false,
            "positionClass": "toast-top-center",
            "onclick": null,
            "showDuration": "1000",
            "hideDuration": "1000",
            "timeOut": "5000",
            "extendedTimeOut": "1000",
            "showEasing": "swing",
            "hideEasing": "linear",
            "showMethod": "fadeIn",
            "hideMethod": "fadeOut"
        };

        toastr.error(error, 'Error !');
    };

    var toastrNotifierInfo = function (error) {
        toastr.clear();
        toastr.options = {
            "closeButton": true,
            "debug": false,
            "positionClass": "toast-top-center",
            "onclick": null,
            "showDuration": "1000",
            "hideDuration": "1000",
            "timeOut": "5000",
            "extendedTimeOut": "1000",
            "showEasing": "swing",
            "hideEasing": "linear",
            "showMethod": "fadeIn",
            "hideMethod": "fadeOut"
        };

        toastr.info(error, 'Info !');
    };

    var toastrNotifierSuccess = function (message) {
        toastr.clear();
        toastr.options = {
            "closeButton": true,
            "debug": false,
            "positionClass": "toast-top-center",
            "onclick": null,
            "showDuration": "1000",
            "hideDuration": "1000",
            "timeOut": "5000",
            "extendedTimeOut": "1000",
            "showEasing": "swing",
            "hideEasing": "linear",
            "showMethod": "fadeIn",
            "hideMethod": "fadeOut"
        };

        toastr.success(message, 'Success !');
    };

    var toUpperCase = function (strText) {

        var _strText;
        _strText = strText.toLowerCase().replace(/\b[a-z]/g, function (letter) {
            return letter.toUpperCase();
        });

        return _strText;
    };

    var displayLength = function () {
        var _displayLength = 10;
        return _displayLength;
    };

    var actionHandler = function () {

    };

    var initializeApp = function () {
        actionHandler();
    };

    var getAjax = function (getUrl, param) {

        $.getJSON(getUrl, param).done(function (data) {
            return data;
        }).fail(function (jqxhr, textStatus, error) {
            var msg = textStatus + ", " + error;
            toastr['error'](msg, "Error !");
        });

    };

    var getDataTableConfiguration = function (dataTableId, iDisplayLength, sAjaxSourceUrl,columns) {
        return {
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
                var keys = Object.keys(json.data[0]);
                tableColumns = keys;
                App.SetDataTableSearch(dataTableId);
            },
            "drawCallback": function (settings) {
                
            },
            ajax: sAjaxSourceUrl,
            columns: generateDataTableColumns(columns)
        };
    };

    var generateDataTableColumns = function (columns) {
        var tableColumns = [];
        if (columns.length > 0) {
            for (var i = 0; i < columns.length; i++) {
                var column = {
                    name: toCamelCase(columns[i]),
                    data: toCamelCase(columns[i]),
                    title: columns[i],
                    sortable: false,
                    searchable: false
                };
                tableColumns.push(column);
            }
        }
        var actionColumn = {
            title: "Actions",
            "mRender": function (data, type, row) {

                return '<a href="javascript.void(0);" data-typename="' + row.typeName + '" data-typeid="' + row.typeId + '" class="btn btn-success lnkAppModal">Details</a>'
                    + ' <a href="javascript.void(0);" data-typename="' + row.typeName + '" data-typeid="' + row.typeId + '" style="margin-left: 5px;" class="btn btn-warning lnkAppModal">Edit</a>'
                    + ' <a href="javascript.void(0);" data-typename="' + row.typeName + '" data-typeid="' + row.typeId + '" style="margin-left: 5px;" class="btn btn-danger lnkAppModal">Delete</a>';
                
            },
            width: "20%"
        };
        tableColumns.push(actionColumn);
        return tableColumns;
    }

    var setDataTableSearch = function (dataTableId) {
        //App.LoaderShow();
        var filterInput = '#' + dataTableId + '_filter label input';
        $(filterInput).attr("placeholder", "Search by keyword");
        //App.LoaderHide();
    };

    var toCamelCase = function (str) {
        return str.replace(/(?:^\w|[A-Z]|\b\w)/g, function (word, index) {
            return index == 0 ? word.toLowerCase() : word.toUpperCase();
        }).replace(/\s+/g, '');
    };


    //-----------------------------------------------------
    //start Ajax Get Methods

    var ajaxJsonGet = function (getUrl) {

        $.ajax({
            url: getUrl,
            type: 'GET',
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            beforeSend: function () {
                OpenAppProgressModal();
            },
            success: function (result) {
                var messageType = result.messageType;
                var messageText = result.messageText;
                LoadAppMessageModal(messageType, messageText);
            },
            error: function (objAjaxRequest, strError) {
                var respText = objAjaxRequest.responseText;
                var messageText = respText;
                LoadErrorAppMessageModalWithText(messageText);
            }

        });

    }

    var ajaxJsonGetWithParam = function (getUrl, paramValue) {

        $.ajax({
            url: getUrl,
            type: 'GET',
            dataType: 'json',
            data: paramValue,
            contentType: 'application/json; charset=utf-8',
            beforeSend: function () {
                OpenAppProgressModal();
            },
            success: function (result) {
                var messageType = result.messageType;
                var messageText = result.messageText;
                LoadAppMessageModal(messageType, messageText);
            },
            error: function (objAjaxRequest, strError) {
                var respText = objAjaxRequest.responseText;
                var messageText = respText;
                LoadErrorAppMessageModalWithText(messageText);
            }

        });

    }

    //end Ajax Get Methods
    //-----------------------------------------------------

    //-----------------------------------------------------
    //start Ajax Post Methods

    var ajaxJsonPost = function (postUrl) {

        $.ajax({
            url: postUrl,
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            beforeSend: function () {
                OpenAppProgressModal();
            },
            success: function (result) {
                var messageType = result.messageType;
                var messageText = result.messageText;
                LoadAppMessageModal(messageType, messageText);
            },
            error: function (objAjaxRequest, strError) {
                var respText = objAjaxRequest.responseText;
                var messageText = respText;
                LoadErrorAppMessageModalWithText(messageText);
            }

        });

    }

    var ajaxJsonPostForDelete = function (postUrl) {

        $.ajax({
            url: postUrl,
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            beforeSend: function () {
                OpenAppProgressModal();
            },
            success: function (result) {
                var messageType = result.messageType;
                var messageText = result.messageText;
                LoadAppMessageModal(messageType, messageText);
                DataTableRefreshInIndexPage();
            },
            error: function (objAjaxRequest, strError) {
                var respText = objAjaxRequest.responseText;
                var messageText = respText;
                LoadErrorAppMessageModalWithText(messageText);
            }

        });

    }

    var ajaxJsonPostWithParam = function (postUrl, paramValue) {

        $.ajax({
            url: postUrl,
            type: 'POST',
            dataType: 'json',
            data: paramValue,
            contentType: 'application/json; charset=utf-8',
            beforeSend: function () {
                OpenAppProgressModal();
            },
            success: function (result) {
                var messageType = result.messageType;
                var messageText = result.messageText;
                LoadAppMessageModal(messageType, messageText);
            },
            error: function (objAjaxRequest, strError) {
                var respText = objAjaxRequest.responseText;
                var messageText = respText;
                LoadErrorAppMessageModalWithText(messageText);
            }

        });

    }

    var ajaxJsonPostForDeleteWithParam = function (postUrl, paramValue) {

        $.ajax({
            url: postUrl,
            type: 'POST',
            dataType: 'json',
            data: paramValue,
            contentType: 'application/json; charset=utf-8',
            beforeSend: function () {
                OpenAppProgressModal();
            },
            success: function (result) {
                var messageType = result.messageType;
                var messageText = result.messageText;
                LoadAppMessageModal(messageType, messageText);
                DataTableRefreshInIndexPage(); //Have to add index page
            },
            error: function (objAjaxRequest, strError) {
                var respText = objAjaxRequest.responseText;
                var messageText = respText;
                LoadErrorAppMessageModalWithText(messageText);
            }

        });

    }

    //end Ajax Post Methods
    //-----------------------------------------------------

    return {
        Init: initializeApp,
        LoaderShow: loaderShow,
        LoaderHide: loaderHide,

        SendAjaxRequest: sendAjaxRequest,
        LoadDropdown: loadDropdown,

        ToastrNotifier: toastrNotifier,
        ToastrNotifierInfo: toastrNotifierInfo,
        ToastrNotifierError: toastrNotifierError,
        ToastrNotifierSuccess: toastrNotifierSuccess,

        DisplayLength: displayLength,

        GetAjax: getAjax,
        GetDataTableConfiguration: getDataTableConfiguration,
        SetDataTableSearch: setDataTableSearch,

        AjaxJsonGet: ajaxJsonGet,
        AjaxJsonGetWithParam: ajaxJsonGetWithParam,
        AjaxJsonPost: ajaxJsonPost,
        AjaxJsonPostForDelete: ajaxJsonPostForDelete,
        AjaxJsonPostWithParam: ajaxJsonPostWithParam,
        AjaxJsonPostForDeleteWithParam: ajaxJsonPostForDeleteWithParam,
        ToCamelCase: toCamelCase,
        AppAreaName: appAreaName,
        AppCopyUrl: appCopyUrl
    };
}();

$(document).ready(function () {

    App.Init();

});