module app.importers {
    "use strict";
    export interface IImporter {
        importObject(data: model.IObjectTree, callback: { (data: model.IDojoObject): void });
    }
}