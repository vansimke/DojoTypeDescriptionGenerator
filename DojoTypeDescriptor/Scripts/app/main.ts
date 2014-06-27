var r: any;
function findIt(query: Object, result: any[], source: any): any[] {
    "use strict";
    result = result || []; 
    var children = [];
    
    source.children.forEach((child: any) => {
        for (var i in query) {
            if (child[i] && child[i].match(new RegExp(query[i]))) {
                result.push(child);
            }
        }
        if (child.children) {
            child.children.forEach((subChild): void => {
                findIt(query, result, subChild);
            });
        }

    });

    return result;
}
module app {
    "use strict";

    export class Status {
        public static requestsMade: number = 0;
        private static requestsFilled: number = 0;
        private static callback: Function;
        public static setCallback(callback: Function) {
            Status.callback = callback;
        }
        public static markRequestAsFilled(): void {
            Status.requestsFilled++;
            console.log("Requests:", Status.requestsMade, "Filled:", Status.requestsFilled);
            if (Status.requestsMade == Status.requestsFilled) {
                Status.callback();
            }
        }
    }

    export function main(): void {

        var content: HTMLDivElement = <HTMLDivElement>document.getElementById("content");
        var importer = new importers.ImporterFactory(content);
        $("#generateDojoApi").css({ disabled: true });
        $("#generateDohApi").css({ disabled: true });
        $("#generateDojoxApi").css({ disabled: true });
        $("#generateDijitApi").css({ disabled: true });

        var tree: model.IObjectTree;

        app.Loader.load("data/1.9/tree.json", (data: string): void => {
            tree = JSON.parse(data);
            importer.importObject(tree, (result: model.IDojoObject) => {
                Status.setCallback(() => {
                    r = result;
                    $("#generateDojoApi").click(() => {
                        doExtractComplete(JSON.stringify(result), 'dojo');
                    });
                    $("#generateDojoApi").css({ disabled: false });
                    $("#generateDojoxApi").click(() => {
                        doExtractComplete(JSON.stringify(result), 'dojox');
                    });
                    $("#generateDojoxApi").css({ disabled: false });
                    $("#generateDohApi").click(() => {
                        doExtractComplete(JSON.stringify(result), 'doh');
                    });
                    $("#generateDohApi").css({ disabled: false });
                    $("#generateDijitApi").click(() => {
                        doExtractComplete(JSON.stringify(result), 'dijit');
                    });
                    $("#generateDijitApi").css({ disabled: false });
                });
            });
        });

    }

    function doExtractComplete(data, packageName): void {

        
        Loader.generateApi(data, packageName);
    }
}

$(app.main);