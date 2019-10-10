"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var Charinfo = /** @class */ (function () {
    function Charinfo() {
    }
    Charinfo.prototype.isLatin = function () {
        return this.name.indexOf('Latin') !== -1;
    };
    return Charinfo;
}());
exports.Charinfo = Charinfo;
function getCharSample() {
    var ci = new Charinfo();
    ci.character = "a";
    ci.name = "Latin Small Letter A";
    ci.block = "Basic Latin";
    ci.codepoint = 97;
    ci.codepointHex = "0061";
    ci.categoryName = "Lowercase Letter";
    return ci;
}
exports.getCharSample = getCharSample;
//# sourceMappingURL=charinfo.js.map