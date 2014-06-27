module app.importers {
    "use strict";

    export class ConstructorImporter extends BaseImporter implements IImporter {
        importObject(data: model.IObjectTree, callback: { (data: model.DojoConstructor): void }): void {
            var result: model.DojoConstructor = new model.DojoConstructor();

            app.Loader.load("1.9/" + data.fullname + ".html", (text: string):void => {
                var div: HTMLDivElement = document.createElement("div");
                div.innerHTML = text;
                var name: string = $(".module-title", div)[0].firstChild.textContent;
                var description: string = $(".module-title ~ .jsdoc-full-summary", div).text();
                var permalink: string = (<HTMLAnchorElement> $(".jsdoc-permalink", div)[0]).innerText;
                var usage: model.DojoFunction = this.importUsage($(".jsdoc-function-information ~ .jsdoc-parameters", div)[0], name);
                var properties: model.DojoVariable[] = this.importProperties(div, name);
                var methods: model.DojoFunction[] = this.importMethods(div, name, "Methods");
                var events: model.DojoFunction[] = this.importMethods(div, name, "Events");

                if (name.match(/onDijitClickMixin/)) {
                    var i: number = 1; 
                }

                this.importBaseClasses(result, div);

                result.setPermalink(permalink);
                result.setName(name);
                result.setDescription(description);
                result.setUsage(usage);

                properties.forEach((property: model.DojoVariable): void => {
                    result.addProperty(property);
                });
                methods.forEach((method: model.DojoFunction): void => {
                    result.addMethod(method);
                });
                events.forEach((event: model.DojoFunction): void => {
                    result.addEvent(event);
                });
                callback(result);

                app.Status.markRequestAsFilled();

            });
        }

        private importBaseClasses(result: model.DojoConstructor, content: HTMLElement): void {

            var baseClassAnchors:JQuery = $(".jsdoc-mixins a", content);
            for (var i: number = 0; i < baseClassAnchors.length; i++) {
                if (i == 0) {
                    result.setBaseClass(baseClassAnchors[i].innerText.replace(",","").trim());
                } else {
                    result.addMixin(baseClassAnchors[i].innerText.replace(",", "").trim());
                }
            }

        }
    }
} 