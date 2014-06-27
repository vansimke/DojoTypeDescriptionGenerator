var app;
(function (app) {
    (function (importers) {
        "use strict";
        var NumberImporter = (function () {
            function NumberImporter() {
            }
            NumberImporter.prototype.importObject = function (data, callback) {
                return null;
            };
            return NumberImporter;
        })();
        importers.NumberImporter = NumberImporter;
    })(app.importers || (app.importers = {}));
    var importers = app.importers;
})(app || (app = {}));
//# sourceMappingURL=NumberImporter.js.map
