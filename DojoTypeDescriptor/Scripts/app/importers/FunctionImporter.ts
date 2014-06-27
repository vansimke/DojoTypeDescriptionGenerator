module app.importers {
    "use strict";

    export class FunctionImporter extends BaseImporter implements IImporter {
        importObject(data: model.IObjectTree, callback: { (data: model.DojoFunction): void }): void {
            var result: model.DojoFunction = new model.DojoFunction();

            app.Loader.load("1.9/" + data.fullname + ".html", (text: string): void => {
                var div: HTMLDivElement = document.createElement("div");
                div.innerHTML = text;
                var name: string = $(".module-title", div)[0].firstChild.textContent;
                var description: string = $(".module-title ~ .jsdoc-full-summary", div).text();
                var permalink: string = (<HTMLAnchorElement> $(".jsdoc-permalink", div)[0]).innerText;
                var usage: model.DojoFunction = this.importUsage($(".jsdoc-function-information ~ .jsdoc-parameters", div)[0], name);
                var properties: model.DojoVariable[] = this.importProperties(div, name);
                var methods: model.DojoFunction[] = this.importMethods(div, name, "Methods");

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
                callback(result);
                app.Status.markRequestAsFilled();

            });
        }
    }
} 