var bugTraqUsersModule = (function($) {

    // Buttons
    var newUserButton = '#newUserButton';
    var addUserButton = '#addUserButton';

    // Layout
    var alertsRow = '#alertsRow';
    var addUserAlertRow = '#addUserAlertRow';

    // Tables
    var usersTable = '#usersTable';

    // Modal
    var newUserModal = '#newUserModal';

    // Locks
    var addUserAlreadyInProcess = false;

    var init = function() {

        _loadUsers();
        _wireUpEvents();

    };

    var _addUser = function () {

        var newUserForm = document.getElementById('newUserForm');
        var isValid = newUserForm.reportValidity();
        
        if(!isValid) return;
        
        var form = $(newUserForm);
        var data = _getFormData(form);
        
        $(addUserButton).prop('disabled', true);
        
        if(addUserAlreadyInProcess) return;

        addUserAlreadyInProcess = true;
        
        $.ajax({
            'type': 'POST',
            'url': 'http://localhost:5000/api/users',
            'data': data,
            'success': function() {

                _showSuccessAlert('New user succesfully added', alertsRow);
                $(newUserModal).modal('hide');
                $(usersTable).DataTable().ajax.reload();
                
            },
            'error': function () {
                
                _showErrorAlert('There was an error saving the bug', addUserAlertRow);

            },
            'complete': function () {

                $(addUserButton).prop('disabled', false);
                addUserAlreadyInProcess = false;

            }
        });
    };

    var _getFormData = function ($form){

        var unindexed_array = $form.serializeArray();
        var indexed_array = {};
    
        $.map(unindexed_array, function(n, i){
            indexed_array[n['name']] = n['value'];
        });
    
        return indexed_array;

    };

    var _loadUsers = function() {

        $(usersTable).DataTable({
            'ajax': { 
                'url': 'http://localhost:5000/api/users',
                'dataSrc': ''
            },
            'columns': [{ 
                'data': 'userId',
                'visible': false 
            },{
                'data': 'firstName',
                'title': 'First Name'
            }, {
                'data': 'surname',
                'title': 'Surname'
            }]
        });
    };

    var _openNewUserModal = function () {

        document.getElementById('newUserForm').reset();
        $(newUserModal).modal('show');

    };

    var _showErrorAlert = function (message, elementToAppendTo) {

        $(elementToAppendTo).html('');

        $('<div class="col-lg-12">' + 
            '<div class="alert alert-danger">' +
              '<button type="button" class="close" data-dismiss="alert">&times;</button>' +
              '<span>' + message + '</span>' +
            '</div>' + 
          '</div>').hide().appendTo(elementToAppendTo).fadeIn(1000);
    };

    var _showSuccessAlert = function (message, elementToAppendTo) {

        $(elementToAppendTo).html('');

        $('<div class="col-lg-12">' + 
            '<div class="alert alert-success">' +
              '<button type="button" class="close" data-dismiss="alert">&times;</button>' +
              '<span>' + message + '</span>' +
            '</div>' + 
          '</div>').hide().appendTo(elementToAppendTo).fadeIn(1000);
    };
    
    var _wireUpEvents = function ()
    {
       $(newUserButton).on('click', _openNewUserModal);
       $(addUserButton).on('click', _addUser);
    };

    return {
        init: init
    };

})(jQuery);

$(document).ready(function (){
    bugTraqUsersModule.init();
});