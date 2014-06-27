var app;
(function (app) {
    (function (model) {
        "use strict";
        var DojoNumber = (function () {
            function DojoNumber() {
                this.children = [];
            }
            DojoNumber.prototype.getChildren = function () {
                return this.children;
            };
            DojoNumber.prototype.addChild = function (child) {
                this.children.push(child);
            };
            return DojoNumber;
        })();
        model.DojoNumber = DojoNumber;
    })(app.model || (app.model = {}));
    var model = app.model;
})(app || (app = {}));
//# sourceMappingURL=DojoNumber.js.map
