var __extends = this.__extends || function (d, b) {
    for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p];
    function __() { this.constructor = d; }
    __.prototype = b.prototype;
    d.prototype = new __();
};
var app;
(function (app) {
    (function (importers) {
        "use strict";

        var FunctionImporter = (function (_super) {
            __extends(FunctionImporter, _super);
            function FunctionImporter() {
                _super.apply(this, arguments);
            }
            FunctionImporter.prototype.importObject = function (data, callback) {
                var _this = this;
                var result = new app.model.DojoFunction();

                app.Loader.load("1.9/" + data.fullname + ".html", function (text) {
                    var div = document.createElement("div");
                    div.innerHTML = text;
                    var name = $(".module-title", div)[0].firstChild.textContent;
                    var description = $(".module-title ~ .jsdoc-full-summary", div).text();
                    var permalink = $(".jsdoc-permalink", div)[0].innerText;
                    var usage = _this.importUsage($(".jsdoc-function-information ~ .jsdoc-parameters", div)[0], name);
                    var properties = _this.importProperties(div, name);
                    var methods = _this.importMethods(div, name, "Methods");

                    result.setPermalink(permalink);
                    result.setName(name);
                    result.setDescription(description);
                    result.setUsage(usage);

                    properties.forEach(function (property) {
                        result.addProperty(property);
                    });
                    methods.forEach(function (method) {
                        result.addMethod(method);
                    });
                    callback(result);
                    app.Status.markRequestAsFilled();
                });
            };
            return FunctionImporter;
        })(importers.BaseImporter);
        importers.FunctionImporter = FunctionImporter;
    })(app.importers || (app.importers = {}));
    var importers = app.importers;
})(app || (app = {}));
//# sourceMappingURL=FunctionImporter.js.map
