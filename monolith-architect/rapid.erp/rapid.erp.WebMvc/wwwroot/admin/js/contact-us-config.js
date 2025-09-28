var ContactUsConfig = function () {

    var loadDataTables = function (dataTableId, iDisplayLength, sAjaxSourceUrl) {

        $.fn.dataTable.ext.errMode = () => alert('We are facing some problem while processing the current request. Please try again later.');

        $('#' + dataTableId).DataTable({
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

            "ajax": {
                "url": sAjaxSourceUrl,
                
            },
            "columns": [
                {
                    name: 'Id',
                    data: 'id',
                    title: "Id",
                    sortable: false,
                    searchable: false,
                    visible: false
                },
                {
                    name: 'ContactUsConfigType',
                    data: 'contactUsConfigType',
                    title: "ContactUsConfigType",
                    sortable: false,
                    searchable: false
                },
                {
                    name: 'FromEmailAddress',
                    data: 'fromEmailAddress',
                    title: "FromEmailAddress",
                    sortable: false,
                    searchable: false
                },
                {
                    name: 'FromEmailAddressDisplayName',
                    data: 'fromEmailAddressDisplayName',
                    title: "FromEmailAddressDisplayName",
                    sortable: false,
                    searchable: false
                },
                /*{
                    name: 'PhoneNumber',
                    data: 'phoneNumber',
                    title: "PhoneNumber",
                    sortable: false,
                    searchable: false
                },
                {
                    name: 'PhoneNumberDisplayName',
                    data: 'phoneNumberDisplayName',
                    title: "PhoneNumberDisplayName",
                    sortable: false,
                    searchable: false
                },*/
                {
                    name: 'Id',
                    data: "id",
                    title: "Actions",
                    sortable: false,
                    searchable: false,
                    className: "w-20",
                    "mRender": function (data, type, row) {
                        return '<a href="/ContactUsConfig/Details/' + row.id + '\" data-href=\"/ContactUsConfig/Details/' + row.id + '\" data-name="' + row.contactUsConfigType + '" data-id="' + row.id + '" title="Details" class="btn btn-success">Details</a>'
                            + ' <a href="/ContactUsConfig/Edit/' + row.id + '\" data-name="" data-href=\"/ContactUsConfig/Edit/' + row.id + '\" data-name="' + row.contactUsConfigType + '" data-id="' + row.id + '" title="Edit" class="btn btn-warning ml-2">Edit</a>'
                            //+ ' <button data-href=\"/ContactUsConfig/Delete/' + row.id + '\" data-name="' + row.contactUsConfigType + '" data-id="' + row.id + '" title="Delete" onclick="AppModal.DeleteCommon(this)" class="btn btn-danger ml-2">Delete</button>';
                        //return data;

                    }
                }
            ]

        });

    };


    return {
        LoadDataTables: loadDataTables
    };
}();