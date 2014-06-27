module app.model {
    "use strict";
    export class DojoInstance implements IDojoObject {
        private type: string = "DojoInstance";
        private permalink: string;
        private name: string;
        private description: string;
        private usage: DojoFunction;
        private baseClass: string;
        private mixins: string[];
        private children: IDojoObject[];
        private properties: DojoVariable[];
        private methods: DojoFunction[];
        private events: DojoFunction[];

        constructor() {
            this.children = [];
            this.properties = [];
            this.methods = [];
            this.events = [];
            this.mixins = [];
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
        getEvents(): DojoFunction[] {
            return this.events;
        }
        addEvent(child: DojoFunction): void {
            this.events.push(child);
        }
        getBaseClass(): string {
            return this.baseClass;
        }
        setBaseClass(value: string): void {
            this.baseClass = value;
        }
        getMixins(): string[] {
            return this.mixins;
        }
        addMixin(value: string): void {
            this.mixins.push(value);
        }
    }
} 