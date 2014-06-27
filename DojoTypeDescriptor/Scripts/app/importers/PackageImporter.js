var app;
(function (app) {
    (function (importers) {
        "use strict";
        var PackageImporter = (function () {
            function PackageImporter() {
            }
            PackageImporter.prototype.importObject = function (data, callback) {
                var result = new app.model.DojoPackage();
                result.setName(data.fullname);

                callback(result);
                app.Status.markRequestAsFilled();
            };
            return PackageImporter;
        })();
        importers.PackageImporter = PackageImporter;
    })(app.importers || (app.importers = {}));
    var importers = app.importers;
})(app || (app = {}));
//# sourceMappingURL=PackageImporter.js.map
