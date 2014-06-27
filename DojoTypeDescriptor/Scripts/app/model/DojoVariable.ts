module app.model {
    "use strict";
    export class DojoVariable implements IDojoObject {
        private type: string = "DojoVariable";
        private isOptional: boolean;
        private name: string;
        private description: string;
        private types: string[];

        private children: IDojoObject[];

        constructor() {
            this.children = [];
            this.types = [];
        }

        getIsOptional(): boolean {
            return this.isOptional;
        }
        setIsOptional(value: boolean): void {
            this.isOptional = value;
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
        getTypes(): string[] {
            return this.types;
        }
        addType(value: string): void {
            this.types.push(value);
        }

        getChildren(): IDojoObject[] {
            return this.children;
        }
        addChild(child: IDojoObject): void {
            this.children.push(child);
        }
    }
} 