module app.importers {
    "use strict";
    export class PackageImporter implements IImporter {
        importObject(data: model.IObjectTree, callback:{ (data: model.IDojoObject): void }):void {
            var result: model.DojoPackage = new model.DojoPackage();
            result.setName(data.fullname);

            callback(result);
            app.Status.markRequestAsFilled();
        }
    }
} 