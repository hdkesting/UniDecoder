var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : new P(function (resolve) { resolve(result.value); }).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
// startsWith polyfill
if (!String.prototype.startsWith) {
    String.prototype.startsWith = function (search, pos) {
        return this.substr(!pos || pos < 0 ? 0 : +pos, search.length) === search;
    };
}
// definition of a character, as received from the function.
class CharDef {
}
// definition of a character for display purposes.
class DisplayChar {
}
class BlockDef {
}
class BasicInformation {
}
class Decoder {
    constructor() {
        this.functionUrl = "https://unidecoderfunctions.azurewebsites.net";
        // private functionUrl = "http://localhost:7071";
        /** Get the local list if available, else get from server (and store locally).
            * @async
            * @returns {array} the full character list.
            */
        this.getBasics = function () {
            return __awaiter(this, void 0, void 0, function* () {
                if (!this.basics) {
                    console.log("fetching basics");
                    var response;
                    try {
                        response = yield fetch(this.functionUrl + "/api/GetBasicInfo");
                    }
                    catch (e) {
                        // maybe not found because of caching??
                        console.log("error: " + e);
                        response = yield fetch(this.functionUrl + "/api/GetBasicInfo?v=" + new Date());
                    }
                    this.basics = yield response.json();
                    console.log("got the basics");
                }
                return this.basics;
            });
        };
        /** Convert a char from the (remote) list to a display value.
        * @param {int} cp - codepoint value
        * @param {object} c - char from list (optional)
        * @returns {object} - the codepoint description object
        */
        this.convertChar = function (cp, c) {
            return {
                codepoint: cp,
                hex: c.codepointHex,
                name: c.name,
                block: c.block,
                category: this.basics.categories[c.categoryId],
                isLatin: c.block.indexOf("Latin") >= 0
            };
        };
        /** Get all characters in the supplied text.
            * @async
            * @param {string} text - the text to convert.
            * @returns {array} - an array with codepoint descriptions.
            */
        this.expandToChars = function (text) {
            return __awaiter(this, void 0, void 0, function* () {
                console.log("Calling function ListCharacters to parse: " + text);
                if (!text) {
                    return [];
                }
                // ensure basics
                yield this.getBasics();
                var chars = yield this.fetchJson("/api/ListCharacters?text=", text);
                return chars.map(c => this.convertChar(c.codepoint, c));
            });
        };
        /** Find all (max 80) characters whose name contains the words in the supplied text or match around a numerical value.
            * @async
            * @param {string} text - the partial name(s) to search for.
            * @returns {array} - an array of codepoint descriptions.
            */
        this.findChars = function (text) {
            return __awaiter(this, void 0, void 0, function* () {
                console.log("Calling function FindCharacters to search for: " + text);
                if (!text) {
                    return [];
                }
                // ensure basics
                yield this.getBasics();
                var chars = yield this.fetchJson("/api/FindCharacters?search=", text);
                var res = chars.map(c => this.convertChar(c.codepoint, c));
                return res;
            });
        };
        /** Get a sorted list of blocknames + indices.
            * @async
            * @returns {array} - a sorted list of block names.
            */
        this.getBlockList = function () {
            return __awaiter(this, void 0, void 0, function* () {
                yield this.getBasics();
                var l = this.basics.blocks;
                var blocks = [];
                for (var b in l) {
                    blocks.push({ "index": b, "name": l[b] });
                }
                // in-place sort: the Latin ones first, then alphabetically
                blocks.sort(function (a, b) {
                    // a first: return -1; b first: return 1; equal: return 0 (but I don't expect that)
                    if (a.name === b.name) {
                        return 0;
                    }
                    if (a.name.indexOf("Latin") >= 0) {
                        if (b.name.indexOf("Latin") >= 0) {
                            // both "Latin"
                            return a.name < b.name ? -1 : 1;
                        }
                        else {
                            return -1; // latin a before non-latin b
                        }
                    }
                    else if (b.name.indexOf("Latin") >= 0) {
                        return 1; // latin b before non-latin a
                    }
                    else {
                        // both "non-Latin"
                        return a.name < b.name ? -1 : 1;
                    }
                });
                return blocks;
            });
        };
        /** Get a list of categories
         * @returns {array} - a list of categories
         */
        this.getCategoryList = function () {
            return __awaiter(this, void 0, void 0, function* () {
                // just to be sure
                yield this.getBasics();
                var l = this.basics.categories;
                var cats = [];
                for (var c in l) {
                    cats.push({ "index": c, "name": l[c] });
                }
                return cats;
            });
        };
        /** Find all characters in the specified block.
            * @async
            * @param {int} blockName - the full name of the block.
            * @returns {array} - a list of characters in the block.
            */
        this.findCharsByBlock = function (blockName) {
            return __awaiter(this, void 0, void 0, function* () {
                yield this.getBasics();
                var chars = yield this.fetchJson("/api/GetCharactersByType?block=", blockName);
                var res = chars.map(c => this.convertChar(c.codepoint, c));
                return res;
            });
        };
        this.findCharsByCategory = function (categoryName) {
            return __awaiter(this, void 0, void 0, function* () {
                yield this.getBasics();
                var chars = yield this.fetchJson("/api/GetCharactersByType?category=", categoryName);
                var res = chars.map(c => this.convertChar(c.codepoint, c));
                return res;
            });
        };
        /** Count the number of characters in the list.
            * @async
            * @returns {int} the total number of character descriptions available.
            */
        this.getCharCount = function () {
            return __awaiter(this, void 0, void 0, function* () {
                var l = yield this.getBasics();
                return l.charCount;
            });
        };
        this.fetchJson = function (url, value) {
            return __awaiter(this, void 0, void 0, function* () {
                var response;
                try {
                    response = yield fetch(this.functionUrl + url + encodeURIComponent(value));
                }
                catch (e) {
                    console.log("error fetching json from: " + url + "; error: " + e);
                    return null;
                }
                return yield response.json();
            });
        };
    }
}
(function (window) {
    'use strict';
    var decoder = new Decoder();
    window.decoder = decoder;
})(window);
//# sourceMappingURL=unidecoder.js.map