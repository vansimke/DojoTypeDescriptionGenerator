var app;
(function (app) {
    (function (model) {
        "use strict";
        var DojoFunction = (function () {
            function DojoFunction() {
                this.type = "DojoFunction";
                this.children = [];
                this.properties = [];
                this.methods = [];
                this.parameters = [];
                this.returnTypes = [];
            }
            DojoFunction.prototype.getPermalink = function () {
                return this.permalink;
            };
            DojoFunction.prototype.setPermalink = function (value) {
                this.permalink = value;
            };
            DojoFunction.prototype.getName = function () {
                return this.name;
            };
            DojoFunction.prototype.setName = function (value) {
                this.name = value;
            };
            DojoFunction.prototype.getDescription = function () {
                return this.description;
            };
            DojoFunction.prototype.setDescription = function (value) {
                this.description = value;
            };
            DojoFunction.prototype.getUsage = function () {
                return this.usage;
            };
            DojoFunction.prototype.setUsage = function (value) {
                this.usage = value;
            };

            DojoFunction.prototype.getChildren = function () {
                return this.children;
            };
            DojoFunction.prototype.addChild = function (child) {
                this.children.push(child);
            };
            DojoFunction.prototype.getProperties = function () {
                return this.properties;
            };
            DojoFunction.prototype.addProperty = function (child) {
                this.properties.push(child);
            };
            DojoFunction.prototype.getMethods = function () {
                return this.methods;
            };
            DojoFunction.prototype.addMethod = function (child) {
                this.methods.push(child);
            };
            DojoFunction.prototype.getParameters = function () {
                return this.parameters;
            };
            DojoFunction.prototype.addParameter = function (parameter) {
                this.parameters.push(parameter);
            };
            DojoFunction.prototype.getReturnTypes = function () {
                return this.returnTypes;
            };
            DojoFunction.prototype.addReturnType = function (returnType) {
                this.returnTypes.push(returnType);
            };
            return DojoFunction;
        })();
        model.DojoFunction = DojoFunction;
    })(app.model || (app.model = {}));
    var model = app.model;
})(app || (app = {}));
//# sourceMappingURL=DojoFunction.js.map
