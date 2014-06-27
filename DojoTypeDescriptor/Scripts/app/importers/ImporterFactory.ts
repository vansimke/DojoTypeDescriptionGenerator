module app.importers {
    "use strict";
    export class ImporterFactory {
        private constructorImporter: ConstructorImporter = new ConstructorImporter();
        private functionImporter: FunctionImporter = new FunctionImporter();
        private instanceImporter: InstanceImporter = new InstanceImporter();
        private objectImporter: ObjectImporter = new ObjectImporter();
        private packageImporter: PackageImporter = new PackageImporter();

        constructor(private content: HTMLDivElement) { }

        importObject(data: model.IObjectTree, callback: { (result: model.IDojoObject): void }):void {
            var result: model.IDojoObject;

            app.Status.requestsMade++;

            this.correctTypeIfNecessary(data);

            switch (data.type) {

                case "constructor":
                    this.constructorImporter.importObject(data, (result: model.DojoConstructor):void => {
                        this.loadChildren(data, result);
                        callback(result);
                    });
                    break;
                case "object":
                    this.objectImporter.importObject(data, (result: model.DojoObject): void => {
                        this.loadChildren(data, result);
                        callback(result);
                    });
                    break;
                case "function":
                    this.functionImporter.importObject(data, (result: model.DojoFunction): void => {
                        this.loadChildren(data, result);
                        callback(result);
                    });
                    break;
                case "instance":
                    this.instanceImporter.importObject(data, (result: model.DojoInstance): void => {
                        this.loadChildren(data, result);
                        callback(result);
                    });
                    break;
                case "number":
                    app.Status.markRequestAsFilled();
                    //this space intentionally left blank...
                    break;
                default:
                    this.packageImporter.importObject(data, (result: model.DojoPackage): void => {
                        this.loadChildren(data, result);
                        callback(result);
                    });
                    break;

            }

        }

        private correctTypeIfNecessary(data: model.IObjectTree): void {
            var toConstructor: string[] = [
                "dojo/Deferred",
                "dojo/promise/Promise",
                "dojo/DeferredList",
                "dojo/NodeList",
                "dojo/_base/Color",
                "dojo/_base/declare.__DeclareCreatedObject",
                "dojo/_base/config",
                "dojo/Evented"
            ]

            var changeToConstructor: boolean = toConstructor.some((fullname, index, array) => {
                return fullname == data.fullname;
            });

            if (changeToConstructor) {
                data.type = "constructor";
            }
        }

        private loadChildren(data: model.IObjectTree, result: model.IDojoObject): void {
            if (data && data.children) {
                data.children.forEach((value) => {
                    this.importObject(value, (child: model.IDojoObject): void => {
                        result.addChild(child);
                    });
                });
            }
        }

    }
} 