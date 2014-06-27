module app.model {
    "use strict";
    export class DojoObject implements IDojoObject {
        private type: string = "DojoObject";
        private permalink: string;
        private name: string;
        private description: string;
        private children: IDojoObject[];
        private properties: DojoVariable[];
        private methods: DojoFunction[];

        constructor() {
            this.children = [];
            this.properties = [];
            this.methods = [];
        }

        getPermalink(): string {
            return this.permalink;
        }
        setPermalink(value: string): void {
            this.permalink = value;
        }
        getName(): string {
            return this.name;
        }
        setName(value: string): void {
            this.name = value;
        }
        getDescription(): string {
            return this.description;
        }
        setDescription(value: string): void {
            this.description = value;
        }
        getChildren(): IDojoObject[] {
            return this.children;
        }
        addChild(child: IDojoObject): void {
            this.children.push(child);
        }
        getProperties(): DojoVariable[] {
            return this.properties;
        }
        addProperty(child: DojoVariable): void {
            this.properties.push(child);
        }
        getMethods(): DojoFunction[] {
            return this.methods;
        }
        addMethod(child: DojoFunction): void {
            this.methods.push(child);
        }
    }
} 