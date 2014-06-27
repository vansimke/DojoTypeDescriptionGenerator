var app;
(function (app) {
    "use strict";
    var Loader = (function () {
        function Loader() {
        }
        Loader.load = function (path, callback) {
            $.get(contextRoot + "api/dojopage", { url: path }, function (data, status) {
                callback(data);
            }).fail(function () {
                app.Status.markRequestAsFilled();
            });
        };

        Loader.generateApi = function (data, packageName) {
            var form = new FormData();

            var file = new Blob([data], { type: 'application/json' });
            form.append("data", file, "data.txt");

            var request = new XMLHttpRequest();
            request.open("POST", contextRoot + "typedescriptor/" + packageName);
            request.send(form);
        };
        return Loader;
    })();
    app.Loader = Loader;
})(app || (app = {}));
//# sourceMappingURL=Loader.js.map
