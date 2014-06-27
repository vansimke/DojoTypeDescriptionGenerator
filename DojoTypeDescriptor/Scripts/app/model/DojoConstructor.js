var app;
(function (app) {
    (function (model) {
        "use strict";
        var DojoConstructor = (function () {
            function DojoConstructor() {
                this.type = "DojoConstructor";
                this.children = [];
                this.properties = [];
                this.methods = [];
                this.events = [];
                this.mixins = [];
            }
            DojoConstructor.prototype.getPermalink = function () {
                return this.permalink;
            };
            DojoConstructor.prototype.setPermalink = function (value) {
                this.permalink = value;
            };
            DojoConstructor.prototype.getName = function () {
                return this.name;
            };
            DojoConstructor.prototype.setName = function (value) {
                this.name = value;
            };
            DojoConstructor.prototype.getBaseClass = function () {
                return this.baseClass;
            };
            DojoConstructor.prototype.setBaseClass = function (value) {
                this.baseClass = value;
            };
            DojoConstructor.prototype.getDescription = function () {
                return this.description;
            };
            DojoConstructor.prototype.setDescription = function (value) {
                this.description = value;
            };
            DojoConstructor.prototype.getUsage = function () {
                return this.usage;
            };
            DojoConstructor.prototype.setUsage = function (value) {
                this.usage = value;
            };

            DojoConstructor.prototype.getMixins = function () {
                return this.mixins;
            };
            DojoConstructor.prototype.addMixin = function (value) {
                this.mixins.push(value);
            };
            DojoConstructor.prototype.getChildren = function () {
                return this.children;
            };
            DojoConstructor.prototype.addChild = function (child) {
                this.children.push(child);
            };
            DojoConstructor.prototype.getProperties = function () {
                return this.properties;
            };
            DojoConstructor.prototype.addProperty = function (child) {
                this.properties.push(child);
            };
            DojoConstructor.prototype.getMethods = function () {
                return this.methods;
            };
            DojoConstructor.prototype.addMethod = function (child) {
                this.methods.push(child);
            };
            DojoConstructor.prototype.getEvents = function () {
                return this.events;
            };
            DojoConstructor.prototype.addEvent = function (child) {
                this.events.push(child);
            };
            return DojoConstructor;
        })();
        model.DojoConstructor = DojoConstructor;
    })(app.model || (app.model = {}));
    var model = app.model;
})(app || (app = {}));
//# sourceMappingURL=DojoConstructor.js.map
