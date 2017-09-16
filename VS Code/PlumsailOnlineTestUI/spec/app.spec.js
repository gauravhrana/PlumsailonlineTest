
var appData = angular.module('Data', [])
var appHome = angular.module('Home', ['Data'])
var appSave = angular.module('Save', ['Data'])

angular.module('SimpleWebApiApp', [ 
    'ngRoute',        
    'Home',
    'Save',
    'Data',
    'ngAnimate',
    'ngSanitize',
    'ui.bootstrap',
])

