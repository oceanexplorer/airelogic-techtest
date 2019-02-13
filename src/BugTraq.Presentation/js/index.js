var bugTraqHomeModule = (function($) {

    // Alerts
    var errorAlert = '#errorAlert'
    var errorMessage = '#errorMessage'

    // Buttons
    var newBugButton = '#newBugButton';
    var editBugButton = '#editBugButton';
    var deleteBugButton = '#deleteBugButton';
    var addBugButton = '#addBugButton';
    var updateBugButton = '#updateBugButton';

    // Dropdowns
    var newBugUserDropdown = '#newBugUserDropdown';
    var editBugUserDropdown = '#editBugUserDropdown';

    // Forms
    var newBugForm = '#newBugForm';

    // Hidden Fields
    var editBugId = '#editBugId';

    // Layout
    var alertsRow = '#alertsRow';
    var addBugAlertRow = '#addBugAlertRow';
    var editBugAlertRow = '#editBugAlertRow';

    // Modals
    var newBugModal = '#newBugModal';
    var editBugModal = '#editBugModal';

    // Tables
    var openBugsTable  = '#openBugsTable';

    // Textboxes
    var editBugTitle = '#editBugTitle';
    var editBugDescription = '#editBugDescription';


    var init = function() {
        
        _loadOpenBugs();
        _loadUsers();
        _wireUpEvents();        

    };

    var _addBug = function () {

        var newBugForm = document.getElementById('newBugForm')
        var isValid = newBugForm.reportValidity();

        if(!isValid) return;

        var form = $(newBugForm);
        var data = _getFormData(form); 

        $.ajax({
            'type': 'POST',
            'url': 'http://localhost:5000/api/bugs',
            'data': data,
            'success': function() {

                _showSuccessAlert('New bug succesfully added', alertsRow);
                $(newBugModal).modal('hide');
                $(openBugsTable).DataTable().ajax.reload();
                
            },
            'error': function () {
                
                _showErrorAlert('There was an error saving the bug', addBugAlertRow);

            }
        });
    }

    var _openEditBugModal = function() {

        document.getElementById('editBugForm').reset();
        $(editBugModal).modal('show');

        var data = _getSelectedBugData(openBugsTable);

        if(!data) return;

        $(editBugTitle).val(data.title);
        $(editBugDescription).val(data.description);
        $(editBugUserDropdown).val(data.userId);
        $(editBugId).val(data.bugId);
    }

    var _updateBug = function() {

        
        var editBugForm = document.getElementById('editBugForm')
        var isValid = editBugForm.reportValidity();
        
        if(!isValid) return;
        
        var form = $(editBugForm);
        var data = _getFormData(form); 
        var bugId = _getSelectedBugData(openBugsTable).bugId;

        if(!data) return;

        $.ajax({
            'type': 'PUT',
            'url': 'http://localhost:5000/api/bugs/' + bugId,
            'data': data,
            'success': function () {

                _showSuccessAlert('Bug successfully updated', alertsRow);
                $(openBugsTable).DataTable().ajax.reload();
                $(editBugModal).modal('hide');

            },
            'error': function () {

                _showErrorAlert('Error updating bug', editBugAlertRow);

            }
        })

    }

    var _loadOpenBugs = function() {

        $(openBugsTable).DataTable({
            'ajax': { 
                'url': 'http://localhost:5000/api/bugs',
                'dataSrc': ''
            },
            'columns': [{ 
                'data': 'bugId',
                'visible': false 
            },{
                'data': 'title',
                'title': 'Title'
            }, {
                'data': 'createdDate',
                'title': 'Created Date'
            }, {
                'data': 'status',
                'title': 'Status'
            }],
            'select': true
        });
    }

    var _loadUsers = function () {

        $.ajax({
            url: 'http://localhost:5000/api/users',
            success: function(result) {

                _appendUsersToDropdown(result, newBugUserDropdown, true);
                _appendUsersToDropdown(result, editBugUserDropdown, true);

            }
        });
    }

    var _appendUsersToDropdown = function(users, dropdown, addDefault) {

        if(addDefault) {
            $(dropdown).append(new Option('--- Select a user ---', ''));
        }
                
        $.each(users, function (i, user) {

            var fullName = user.firstName + " " + user.surname;
            $(dropdown).append(new Option(fullName, user.userId));
            
        });
    } 

    var _openNewBugModal = function () {

        document.getElementById('newBugForm').reset();
        $(newBugModal).modal('show');       

    }

    var _showErrorAlert = function (message, elementToAppendTo) {

        $(elementToAppendTo).html('');

        $('<div class="col-lg-12">' + 
            '<div class="alert alert-danger">' +
              '<button type="button" class="close" data-dismiss="alert">&times;</button>' +
              '<span>' + message + '</span>' +
            '</div>' + 
          '</div>').hide().appendTo(elementToAppendTo).fadeIn(1000);
    }

    var _showSuccessAlert = function (message, elementToAppendTo) {

        $(elementToAppendTo).html('');

        $('<div class="col-lg-12">' + 
            '<div class="alert alert-success">' +
              '<button type="button" class="close" data-dismiss="alert">&times;</button>' +
              '<span>' + message + '</span>' +
            '</div>' + 
          '</div>').hide().appendTo(elementToAppendTo).fadeIn(1000);
    }

    var _wireUpEvents = function ()
    {
        $(newBugButton).on('click', _openNewBugModal);
        $(addBugButton).on('click', _addBug);
        $(deleteBugButton).on('click', _deleteBug);
        $(editBugButton).on('click', _openEditBugModal);
        $(updateBugButton).on('click', _updateBug);

        var table = $(openBugsTable).DataTable();
 
        table.on('select', _rowSelected);
        table.on('deselect', _rowDeSelected);
    }

    var _rowSelected = function(e, dt, type, indexes) {
        
        _showButtons();
        
    }

    var _rowDeSelected = function(e, dt, type, indexes) {
        
        _hideButtons();
        
    }

    var _hideButtons = function() {

        $(deleteBugButton).css('display', 'none');
        $(editBugButton).css('display', 'none');

    }

    var _showButtons = function() {

        $(deleteBugButton).css('display', 'inline-block');
        $(editBugButton).css('display', 'inline-block');

    }

    var _getFormData = function ($form){

        var unindexed_array = $form.serializeArray();
        var indexed_array = {};
    
        $.map(unindexed_array, function(n, i){
            indexed_array[n['name']] = n['value'];
        });
    
        return indexed_array;

    }

    var _getSelectedBugData = function(selectedTable) {

        var table = $(selectedTable).DataTable();
        var row = $(selectedTable).find('tr.selected');

        if(row.length === 0) return null;

        var data = table.row(row).data();

        return data;
    }

    var _deleteBug = function() {

        var data = _getSelectedBugData(openBugsTable);

        if(!data) return;        

        $.ajax({
            'type': 'DELETE',
            'url': 'http://localhost:5000/api/bugs/' + data.bugId,
            'success': function () {

                _showSuccessAlert('Bug successfully deleted', alertsRow);
                $(openBugsTable).DataTable().ajax.reload();                

            },
            'error': function () {

                _showErrorAlert('Error deleting bug', alertsRow);

            }
        })
    }

    return {
        init: init
    }

})(jQuery);

$(document).ready(function (){
    bugTraqHomeModule.init();
})