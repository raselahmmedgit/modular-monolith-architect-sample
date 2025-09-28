
var dataTableObjData;

var Media = function () {

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
                "sLengthMedia": "Display _MENU_ records per page",
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
                    name: 'Title',
                    data: 'title',
                    title: "Title",
                    sortable: false,
                    searchable: false
                },
                {
                    name: 'Id',
                    data: "id",
                    title: "Actions",
                    sortable: false,
                    searchable: false,
                    className: "w-20 text-center",
                    "mRender": function (data, type, row) {
                        return ' <a href="/Media/Edit/' + row.id + '\" data-href=\"/Media/Edit/' + row.id + '\" data-name="' + row.siteTitle + '" data-id="' + row.id + '" title="Edit" class="btn btn-warning ml-2">Edit</a>'
                            + ' <button data-href=\"/Media/Delete/' + row.id + '\" data-name="' + row.siteTitle + '" data-id="' + row.id + '" title="Delete" onclick="AppModal.DeleteCommon(this)" class="btn btn-danger ml-2">Delete</button>';
                        //return data;

                    },
                }
            ]

        });

    };

    var initMedia = function () {
    };

    return {
        LoadDataTables: loadDataTables,
        InitMedia: initMedia
    };
}();