module app.model {
    "use strict";
    export class DojoPackage implements IDojoObject {
        private type: string = "DojoPackage";
        private name: string;
        private children: IDojoObject[];

        constructor() {
            this.children = [];
        }

        getName(): string {
            return this.name;
        }
        setName(value: string): void {
            this.name = value;
        }

        getChildren(): IDojoObject[] {
            return this.children;
        }
        addChild(child: IDojoObject):void {
            this.children.push(child);
        }
    }
} 