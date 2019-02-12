// formserver functions to get the data
(function (window) {

    var dcrFormServerUrl = '';

    // add any instance
    function getFormServerUrl(callback, errorCallback) {
        API.serviceGET('services/GetDCRFormServerURL')
            .done(function (response) {
                formserver.dcrFormServerUrl = response;
                callback(response);
            })
            .fail(function (e) {
                App.showExceptionErrorMessage(e);
            });
    }

    function getFormServerJS(data, callback, errorCallback) {
        API.getJSFile(data + '/Scripts/dynamicform/CallBackHandler.js')
            .done(function (response) {
            })
            .fail(function (e) {
                App.showExceptionErrorMessage(e);
            });
    }

    function getFormServerIFrame(data, callback, errorCallback) {
        API.getJSFile(response + '/Scripts/dynamicform/CallBackHandler.js')
            .done(function (response) {
            })
            .fail(function (e) {
                App.showExceptionErrorMessage(e);
            });
    }

    // formserver library
    var formserver = function () {
        this.getFormServerUrl = getFormServerUrl;
        this.getFormServerJS = getFormServerJS;
        this.getFormServerIFrame = getFormServerIFrame;
        this.dcrFormServerUrl = dcrFormServerUrl;
    };

    return window.FormServer = new formserver;
}(window));

// document get ready
$(document).ready(function () {
    FormServer.getFormServerUrl(FormServer.getFormServerJS);
});
