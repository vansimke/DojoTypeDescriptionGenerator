var r;
function findIt(query, result, source) {
    "use strict";
    result = result || [];
    var children = [];

    source.children.forEach(function (child) {
        for (var i in query) {
            if (child[i] && child[i].match(new RegExp(query[i]))) {
                result.push(child);
            }
        }
        if (child.children) {
            child.children.forEach(function (subChild) {
                findIt(query, result, subChild);
            });
        }
    });

    return result;
}
var app;
(function (app) {
    "use strict";

    var Status = (function () {
        function Status() {
        }
        Status.setCallback = function (callback) {
            Status.callback = callback;
        };
        Status.markRequestAsFilled = function () {
            Status.requestsFilled++;
            console.log("Requests:", Status.requestsMade, "Filled:", Status.requestsFilled);
            if (Status.requestsMade == Status.requestsFilled) {
                Status.callback();
            }
        };
        Status.requestsMade = 0;
        Status.requestsFilled = 0;
        return Status;
    })();
    app.Status = Status;

    function main() {
        var content = document.getElementById("content");
        var importer = new app.importers.ImporterFactory(content);
        $("#generateDojoApi").css({ disabled: true });
        $("#generateDohApi").css({ disabled: true });
        $("#generateDojoxApi").css({ disabled: true });
        $("#generateDijitApi").css({ disabled: true });

        var tree;

        app.Loader.load("data/1.9/tree.json", function (data) {
            tree = JSON.parse(data);
            importer.importObject(tree, function (result) {
                Status.setCallback(function () {
                    r = result;
                    $("#generateDojoApi").click(function () {
                        doExtractComplete(JSON.stringify(result), 'dojo');
                    });
                    $("#generateDojoApi").css({ disabled: false });
                    $("#generateDojoxApi").click(function () {
                        doExtractComplete(JSON.stringify(result), 'dojox');
                    });
                    $("#generateDojoxApi").css({ disabled: false });
                    $("#generateDohApi").click(function () {
                        doExtractComplete(JSON.stringify(result), 'doh');
                    });
                    $("#generateDohApi").css({ disabled: false });
                    $("#generateDijitApi").click(function () {
                        doExtractComplete(JSON.stringify(result), 'dijit');
                    });
                    $("#generateDijitApi").css({ disabled: false });
                });
            });
        });
    }
    app.main = main;

    function doExtractComplete(data, packageName) {
        app.Loader.generateApi(data, packageName);
    }
})(app || (app = {}));

$(app.main);
//# sourceMappingURL=main.js.map
