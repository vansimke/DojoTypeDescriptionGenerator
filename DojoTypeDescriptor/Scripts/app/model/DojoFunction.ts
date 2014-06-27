module app.model {
    "use strict";
    export class DojoFunction implements IDojoObject {
        private type: string = "DojoFunction";
        private permalink: string;
        private name: string;
        private description: string;
        private usage: DojoFunction;
        private children: IDojoObject[];
        private properties: DojoVariable[];
        private methods: DojoFunction[];

        private returnTypes: DojoVariable[]; 
        private parameters: DojoVariable[];

        constructor() {
            this.children = [];
            this.properties = [];
            this.methods = [];
            this.parameters = [];
            this.returnTypes = [];
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
        getUsage(): DojoFunction {
            return this.usage;
        }
        setUsage(value: DojoFunction): void {
            this.usage = value;
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
        getParameters(): DojoVariable[] {
            return this.parameters;
        }
        addParameter(parameter: DojoVariable): void {
            this.parameters.push(parameter);
        }
        getReturnTypes(): DojoVariable[] {
            return this.returnTypes;
        }
        addReturnType(returnType: DojoVariable): void {
            this.returnTypes.push(returnType);
        }
    }
} 