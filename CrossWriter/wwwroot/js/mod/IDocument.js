// mko, 28.12.2023
// Allgemeine Dokumentstruktur
define(["require", "exports", "./SiegelAndSowilo"], function (require, exports, SiegelAndSowilo_1) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
    exports.CreateDocument = void 0;
    // Class Factory for Documents
    function CreateDocument(authorUserId, documentName, text, siegel, sowilo) {
        const fname = "CreateDocument";
        let doc = {
            documentName: documentName,
            autorUserId: authorUserId,
            textLines: [""],
            LineCount: () => 0
        };
        let res = "";
        if (documentName === undefined) {
            res = sowilo.apply(null, (0, SiegelAndSowilo_1.ArgumentValidationFailedDescriptor)(text, fname, "documentName", "undefined", "documentName must be defined"));
        }
        else if (!documentName.toLocaleLowerCase().endsWith(".cwf") && !documentName.toLocaleLowerCase().endsWith(".md")) {
            res = sowilo.apply(null, (0, SiegelAndSowilo_1.ArgumentValidationFailedDescriptor)(text, fname, "documentName", documentName, "documentName ends with '.cwf' or '.md'"));
        }
        else if (text === undefined) {
            res = siegel(doc);
        }
        else {
            let lines = text.split(/\s*\\n+\s*/);
            doc.textLines = lines;
            doc.LineCount = () => lines.length;
            res = siegel(doc);
        }
        return res;
    }
    exports.CreateDocument = CreateDocument;
});
//# sourceMappingURL=IDocument.js.map