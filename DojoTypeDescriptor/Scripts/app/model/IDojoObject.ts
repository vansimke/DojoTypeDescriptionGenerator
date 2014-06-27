module app.model {
    "use strict";
    export interface IDojoObject {
        getChildren(): IDojoObject[]
        addChild(child: IDojoObject): void
    }
} 