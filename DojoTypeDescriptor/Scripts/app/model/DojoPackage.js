var app;
(function (app) {
    (function (model) {
        "use strict";
        var DojoPackage = (function () {
            function DojoPackage() {
                this.type = "DojoPackage";
                this.children = [];
            }
            DojoPackage.prototype.getName = function () {
                return this.name;
            };
            DojoPackage.prototype.setName = function (value) {
                this.name = value;
            };

            DojoPackage.prototype.getChildren = function () {
                return this.children;
            };
            DojoPackage.prototype.addChild = function (child) {
                this.children.push(child);
            };
            return DojoPackage;
        })();
        model.DojoPackage = DojoPackage;
    })(app.model || (app.model = {}));
    var model = app.model;
})(app || (app = {}));
//# sourceMappingURL=DojoPackage.js.map
