var app;
(function (app) {
    (function (model) {
        "use strict";
        var DojoVariable = (function () {
            function DojoVariable() {
                this.type = "DojoVariable";
                this.children = [];
                this.types = [];
            }
            DojoVariable.prototype.getIsOptional = function () {
                return this.isOptional;
            };
            DojoVariable.prototype.setIsOptional = function (value) {
                this.isOptional = value;
            };
            DojoVariable.prototype.getName = function () {
                return this.name;
            };
            DojoVariable.prototype.setName = function (value) {
                this.name = value;
            };
            DojoVariable.prototype.getDescription = function () {
                return this.description;
            };
            DojoVariable.prototype.setDescription = function (value) {
                this.description = value;
            };
            DojoVariable.prototype.getTypes = function () {
                return this.types;
            };
            DojoVariable.prototype.addType = function (value) {
                this.types.push(value);
            };

            DojoVariable.prototype.getChildren = function () {
                return this.children;
            };
            DojoVariable.prototype.addChild = function (child) {
                this.children.push(child);
            };
            return DojoVariable;
        })();
        model.DojoVariable = DojoVariable;
    })(app.model || (app.model = {}));
    var model = app.model;
})(app || (app = {}));
//# sourceMappingURL=DojoVariable.js.map
