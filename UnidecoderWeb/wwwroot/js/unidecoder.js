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
        return this.substr(!pos || pos < 0 ? 0 : +pos, search.length) == search;
    };
}
class CharDef {
}
class DisplayChar {
}
class CharacterList {
}
class Decoder {
    constructor() {
        /** Get the local list if available, else get from server (and store locally).
         */
        this.getList = function () {
            return __awaiter(this, void 0, void 0, function* () {
                if (!this.list) {
                    console.log("fetching");
                    var response;
                    try {
                        let versionResponse = yield fetch("api/unicode/version");
                        let version = yield versionResponse.text();
                        console.log("unicode version: " + version);
                        response = yield fetch("api/unicode/characters?v=" + version);
                    }
                    catch (e) {
                        // maybe not found because of caching
                        console.log("error: " + e);
                        response = yield fetch("api/unicode/characters?v=" + new Date());
                    }
                    this.list = yield response.json();
                    console.log("got the list");
                    // make an uppercase copy of all names, for easier searching
                    for (let cp in this.list.characters) {
                        var c = this.list.characters[cp];
                        c.uname = c.name.toUpperCase();
                    }
                    console.log("uppercased all the names");
                }
                return this.list;
            });
        };
        /** Convert a char from the (local) list to a display value.
         * params: cp = codepoint (int), c = char from list (optional, object)
         */
        this.convertChar = function (cp, c) {
            if (!c) {
                c = this.list[cp];
            }
            return {
                codepoint: cp,
                hex: c.hex,
                name: c.name,
                block: this.list.blocks[c.block],
                blockId: c.block,
                category: this.list.categories[c.category],
                isLatin: this.list.blocks[c.block].indexOf("Latin") >= 0
            };
        };
        /** Try and get a character from the list.
         * params: cp = codepoint (int), fillMissing = when true, create a "missing value" (bool), else use null
         */
        this.getChar = function (cp, fillMissing) {
            return __awaiter(this, void 0, void 0, function* () {
                if (cp <= 0) {
                    return null;
                }
                var l = yield this.getList();
                var c = l.characters[cp];
                if (c) {
                    return this.convertChar(cp, c);
                }
                if (fillMissing) {
                    return {
                        codepoint: cp,
                        hex: 'FFFD',
                        name: 'Unknown',
                        block: '?',
                        blockId: 0,
                        category: 'Private Use',
                        isLatin: false
                    };
                }
                return null;
            });
        };
        /** Converts a value to an int, using the supplied radix.
         * params: value = value to convert (string), radix = base to use for conversion (10 or 16)
         */
        this.makeInt = function (value, radix) {
            // remove any leading 0's
            while (value.length && value[0] === '0') {
                value = value.substr(1);
            }
            var i = parseInt(value, radix);
            if (isNaN(i))
                return 0;
            // parseInt already succeeds when *some* characters can be parsed, it will ignore an unparsable rest. I want a *full* parse.
            if (i.toString(radix).toUpperCase() === value.toUpperCase()) {
                return i;
            }
            return 0;
        };
        /** Get all characters in the supplied text.
         */
        this.expandToChars = function (text) {
            return __awaiter(this, void 0, void 0, function* () {
                let characters = new Array();
                var list = yield this.getList();
                for (let i = 0; i < text.length; i++) {
                    var cp = text.codePointAt(i);
                    var c2 = yield this.getChar(cp, true);
                    characters.push(c2);
                    if (cp > 65536)
                        i++; // skip other half of surrogate pair
                }
                return characters;
            });
        };
        /** Find all (max 80) characters whose name contains the words in the supplied text or match around a numerical value.
         */
        this.findChars = function (text) {
            return __awaiter(this, void 0, void 0, function* () {
                console.log("finding " + text);
                if (!text) {
                    return [];
                }
                var characters = new Array();
                var c;
                // try integer value
                var cp = this.makeInt(text, 10);
                if (cp) {
                    for (let i = cp - 5; i <= cp + 5; i++) {
                        c = yield this.getChar(i, false);
                        if (c) {
                            characters.push(c);
                        }
                    }
                }
                // try hex value
                var cleaned = text;
                if (cleaned.startsWith("0x") || cleaned.startsWith("U+")) {
                    cleaned = cleaned.substr(2);
                }
                cp = this.makeInt(cleaned, 16);
                if (cp) {
                    for (let i = cp - 5; i <= cp + 5; i++) {
                        c = yield this.getChar(i);
                        if (c) {
                            characters.push(c);
                        }
                    }
                }
                // plain search
                var words = text.toUpperCase().split(' ');
                var list = yield this.getList();
                for (cp in list.characters) {
                    if (list.characters.hasOwnProperty(cp)) {
                        let c2 = list.characters[cp];
                        for (var t of words) {
                            var idx = c2.uname.indexOf(t);
                            /* eslint no-extra-parens: ["error", "all", { "nestedBinaryExpressions": false }] */
                            if (idx < 0 || (idx > 0 && c2.uname[idx - 1] !== ' ')) {
                                // either not found at all, or the character just before (if any) is not a space: no match
                                c2 = null;
                                break;
                            }
                        }
                        if (c2) {
                            c = this.convertChar(cp, c2);
                            characters.push(c2);
                            if (characters.length > 80) {
                                console.log("found enough");
                                break;
                            }
                        }
                    }
                }
                return characters;
            });
        };
        /** Get a sorted list of blocknames + indices.
         */
        this.getBlockList = function () {
            return __awaiter(this, void 0, void 0, function* () {
                var l = yield this.getList();
                var blocks = [];
                for (var b = 0; b < l.blocks.length; b++) {
                    blocks.push({ "index": b, "name": l.blocks[b] });
                }
                // sort the Latin ones first, then alphabetically
                blocks.sort(function (a, b) {
                    // a first: return -1; b first: return 1; equal: return 0 (but I don't expect that)
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
        /** Find all characters in the specified block.
         */
        this.findCharsByBlock = function (blockId) {
            return __awaiter(this, void 0, void 0, function* () {
                var l = yield this.getList();
                var chars = [];
                for (let cp in l.characters) {
                    var c = l.characters[cp];
                    if (c && c.block === blockId) {
                        chars.push(this.convertChar(cp, c));
                    }
                }
                return chars;
            });
        };
        /** Count the number of characters in the list.
         */
        this.getCharCount = function () {
            return __awaiter(this, void 0, void 0, function* () {
                var l = yield this.getList();
                let count = 0;
                // "characters" is not an array, but an object with numerically named properties.
                for (let cp in l.characters) {
                    if (l.characters.hasOwnProperty(cp)) {
                        count++;
                    }
                }
                return count;
            });
        };
    }
}
(function (window) {
    'use strict';
    return __awaiter(this, void 0, void 0, function* () {
        var decoder = new Decoder();
        window.decoder = decoder;
    });
})(window);
//# sourceMappingURL=unidecoder.js.map