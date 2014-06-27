declare var contextRoot: string;
module app {
    "use strict";
    export class Loader {
        static load(path: string, callback: ICallbackFunction): void {
            $.get(contextRoot + "api/dojopage", { url: path }, (data: string, status: string): void => {
                callback(data);
            }).fail(() => {
                    app.Status.markRequestAsFilled();
                });
        }

        static generateApi(data:string, packageName:string): void {
            var form: FormData = new FormData();

            var file: Blob = new Blob([data], { type: 'application/json' });
            form.append("data", file, "data.txt");

            var request: XMLHttpRequest = new XMLHttpRequest();
            request.open("POST", contextRoot + "typedescriptor/" + packageName);
            request.send(form);
        }
    }
    
}