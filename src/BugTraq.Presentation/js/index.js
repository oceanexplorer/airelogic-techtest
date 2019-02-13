var bugTraqHomeModule = (function($) {

    // Buttons
    var addBugButton = '#addBugButton';
    var editBugButton = '#editBugButton';
    var deleteBugButton = '#deleteBugButton';

    // Modals
    var addBugModal = '#addBugModal';

    // Tables
    var openBugsTable  = '#openBugsTable';

    var init = function() {
        
        _loadOpenBugs();
        _wireUpEvents();

    };

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
            }]
        });

    }

    var _openAddBugModal = function () {

        $(addBugModal).modal('show');

    }

    var _wireUpEvents = function ()
    {
        $(addBugButton).on('click', _openAddBugModal);
    }

    return {
        init: init
    }

})(jQuery);

$(document).ready(function (){
    bugTraqHomeModule.init();
})