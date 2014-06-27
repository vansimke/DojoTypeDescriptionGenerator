var app;
(function (app) {
    "use strict";
    var importers;
    (function (importers) {
        var FolderImporter = (function () {
            function FolderImporter(content) {
                this.content = content;
            }
            FolderImporter.prototype.import = function (folder) {
                var result = new app.model.DojoPackage();
                result.SetName(folder.fullname);

                return result;
            };
            return FolderImporter;
        })();
        importers.FolderImporter = FolderImporter;
    })(importers || (importers = {}));
})(app || (app = {}));
//# sourceMappingURL=FolderImporter.js.map
