var app;
(function (app) {
    (function (importers) {
        "use strict";
        var ImporterFactory = (function () {
            function ImporterFactory(content) {
                this.content = content;
                this.constructorImporter = new importers.ConstructorImporter();
                this.functionImporter = new importers.FunctionImporter();
                this.instanceImporter = new importers.InstanceImporter();
                this.objectImporter = new importers.ObjectImporter();
                this.packageImporter = new importers.PackageImporter();
            }
            ImporterFactory.prototype.importObject = function (data, callback) {
                var _this = this;
                var result;

                app.Status.requestsMade++;

                this.correctTypeIfNecessary(data);

                switch (data.type) {
                    case "constructor":
                        this.constructorImporter.importObject(data, function (result) {
                            _this.loadChildren(data, result);
                            callback(result);
                        });
                        break;
                    case "object":
                        this.objectImporter.importObject(data, function (result) {
                            _this.loadChildren(data, result);
                            callback(result);
                        });
                        break;
                    case "function":
                        this.functionImporter.importObject(data, function (result) {
                            _this.loadChildren(data, result);
                            callback(result);
                        });
                        break;
                    case "instance":
                        this.instanceImporter.importObject(data, function (result) {
                            _this.loadChildren(data, result);
                            callback(result);
                        });
                        break;
                    case "number":
                        app.Status.markRequestAsFilled();

                        break;
                    default:
                        this.packageImporter.importObject(data, function (result) {
                            _this.loadChildren(data, result);
                            callback(result);
                        });
                        break;
                }
            };

            ImporterFactory.prototype.correctTypeIfNecessary = function (data) {
                var toConstructor = [
                    "dojo/Deferred",
                    "dojo/promise/Promise",
                    "dojo/DeferredList",
                    "dojo/NodeList",
                    "dojo/_base/Color",
                    "dojo/_base/declare.__DeclareCreatedObject",
                    "dojo/_base/config",
                    "dojo/Evented"
                ];

                var changeToConstructor = toConstructor.some(function (fullname, index, array) {
                    return fullname == data.fullname;
                });

                if (changeToConstructor) {
                    data.type = "constructor";
                }
            };

            ImporterFactory.prototype.loadChildren = function (data, result) {
                var _this = this;
                if (data && data.children) {
                    data.children.forEach(function (value) {
                        _this.importObject(value, function (child) {
                            result.addChild(child);
                        });
                    });
                }
            };
            return ImporterFactory;
        })();
        importers.ImporterFactory = ImporterFactory;
    })(app.importers || (app.importers = {}));
    var importers = app.importers;
})(app || (app = {}));
//# sourceMappingURL=ImporterFactory.js.map
