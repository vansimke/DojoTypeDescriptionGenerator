module app.model {
    "use strict";
    export interface IObjectTree {
        id: string;
        type: string;
        fullname: string;
        name: string;
        children: IObjectTree[];
    }
} 