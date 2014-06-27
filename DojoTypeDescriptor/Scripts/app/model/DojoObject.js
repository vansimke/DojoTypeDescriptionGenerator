var app;
(function (app) {
    (function (model) {
        "use strict";
        var DojoObject = (function () {
            function DojoObject() {
                this.type = "DojoObject";
                this.children = [];
                this.properties = [];
                this.methods = [];
            }
            DojoObject.prototype.getPermalink = function () {
                return this.permalink;
            };
            DojoObject.prototype.setPermalink = function (value) {
                this.permalink = value;
            };
            DojoObject.prototype.getName = function () {
                return this.name;
            };
            DojoObject.prototype.setName = function (value) {
                this.name = value;
            };
            DojoObject.prototype.getDescription = function () {
                return this.description;
            };
            DojoObject.prototype.setDescription = function (value) {
                this.description = value;
            };
            DojoObject.prototype.getChildren = function () {
                return this.children;
            };
            DojoObject.prototype.addChild = function (child) {
                this.children.push(child);
            };
            DojoObject.prototype.getProperties = function () {
                return this.properties;
            };
            DojoObject.prototype.addProperty = function (child) {
                this.properties.push(child);
            };
            DojoObject.prototype.getMethods = function () {
                return this.methods;
            };
            DojoObject.prototype.addMethod = function (child) {
                this.methods.push(child);
            };
            return DojoObject;
        })();
        model.DojoObject = DojoObject;
    })(app.model || (app.model = {}));
    var model = app.model;
})(app || (app = {}));
//# sourceMappingURL=DojoObject.js.map
