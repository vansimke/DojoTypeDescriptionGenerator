var app;
(function (app) {
    (function (model) {
        "use strict";
        var DojoInstance = (function () {
            function DojoInstance() {
                this.type = "DojoInstance";
                this.children = [];
                this.properties = [];
                this.methods = [];
                this.events = [];
                this.mixins = [];
            }
            DojoInstance.prototype.getPermalink = function () {
                return this.permalink;
            };
            DojoInstance.prototype.setPermalink = function (value) {
                this.permalink = value;
            };
            DojoInstance.prototype.getName = function () {
                return this.name;
            };
            DojoInstance.prototype.setName = function (value) {
                this.name = value;
            };
            DojoInstance.prototype.getDescription = function () {
                return this.description;
            };
            DojoInstance.prototype.setDescription = function (value) {
                this.description = value;
            };
            DojoInstance.prototype.getUsage = function () {
                return this.usage;
            };
            DojoInstance.prototype.setUsage = function (value) {
                this.usage = value;
            };
            DojoInstance.prototype.getChildren = function () {
                return this.children;
            };
            DojoInstance.prototype.addChild = function (child) {
                this.children.push(child);
            };
            DojoInstance.prototype.getProperties = function () {
                return this.properties;
            };
            DojoInstance.prototype.addProperty = function (child) {
                this.properties.push(child);
            };
            DojoInstance.prototype.getMethods = function () {
                return this.methods;
            };
            DojoInstance.prototype.addMethod = function (child) {
                this.methods.push(child);
            };
            DojoInstance.prototype.getEvents = function () {
                return this.events;
            };
            DojoInstance.prototype.addEvent = function (child) {
                this.events.push(child);
            };
            DojoInstance.prototype.getBaseClass = function () {
                return this.baseClass;
            };
            DojoInstance.prototype.setBaseClass = function (value) {
                this.baseClass = value;
            };
            DojoInstance.prototype.getMixins = function () {
                return this.mixins;
            };
            DojoInstance.prototype.addMixin = function (value) {
                this.mixins.push(value);
            };
            return DojoInstance;
        })();
        model.DojoInstance = DojoInstance;
    })(app.model || (app.model = {}));
    var model = app.model;
})(app || (app = {}));
//# sourceMappingURL=DojoInstance.js.map
