// startsWith polyfill
if (!String.prototype.startsWith) {
    String.prototype.startsWith = function (search: string, pos: number) {
        return this.substr(!pos || pos < 0 ? 0 : +pos, search.length) === search;
    };
}

// definition of a character, as received from the function.
class CharDef {
    character: string;
    name: string; // name with mixed casing
    block: string; // NAME of block
    codepoint: number;
    codepointHex: string;
    categoryId: number; // id of category
}

// definition of a character for display purposes.
class DisplayChar {
    codepoint: number;
    hex: string;
    name: string;
    block: string;
    // blockId: number;
    category: string;
    isLatin: boolean;
}

class BlockDef {
    index: number;
    name: string;
}

class BasicInformation {
    categories: Map<number, string>;
    blocks: Map<number, string>;
    charCount: number;
    unicodeVersion: string;
}

class Decoder {
    private basics: BasicInformation;
    private functionUrl = "https://unidecoderfunctions.azurewebsites.net";
    // private functionUrl = "http://localhost:7071";

    /** Get the local list if available, else get from server (and store locally).
        * @async
        * @returns {array} the full character list.
        */
    private getBasics = async function (): Promise<BasicInformation> {
        if (!this.basics) {
            console.log("fetching basics");
            var response: Response;
            try {
                response = await fetch(this.functionUrl + "/api/GetBasicInfo");
            } catch (e) {
                // maybe not found because of caching??
                console.log("error: " + e);
                response = await fetch(this.functionUrl + "/api/GetBasicInfo?v=" + new Date());
            }

            this.basics = await response.json();
            console.log("got the basics");
            // console.dir(this.basics);
        }

        return this.basics;
    }

    /** Convert a char from the (remote) list to a display value.
        * @param {int} cp - codepoint value
        * @param {object} c - char from list (optional)
        * @returns {object} - the codepoint description object
        */
    private convertChar = function (cp: number, c: CharDef): DisplayChar {
        return {
            codepoint: cp,
            hex: c.codepointHex,
            name: c.name,
            block: c.block,
            category: this.basics.categories[c.categoryId],
            isLatin: c.block.indexOf("Latin") >= 0
        };
    }

    /** Converts a value to an int, using the supplied radix.
        * @param {string} value - value to convert
        * @param {int} radix - base to use for conversion (10 or 16).
        * @returns {int} - the converted value.
        */
    private makeInt = function (value: string, radix: number): number {
        // remove any leading 0's
        while (value.length && value[0] === '0') {
            value = value.substr(1);
        }

        var i = parseInt(value, radix);

        if (isNaN(i)) return 0;

        // parseInt already succeeds when *some* characters can be parsed, it will ignore an unparsable rest. I want a *full* parse.
        if (i.toString(radix).toUpperCase() === value.toUpperCase()) {
            return i;
        }

        return 0;
    };

    /** Get all characters in the supplied text.
        * @async
        * @param {string} text - the text to convert.
        * @returns {array} - an array with codepoint descriptions.
        */
    public expandToChars = async function (text: string): Promise<DisplayChar[]> {
        console.log("Calling function ListCharacters to parse: " + text);
        if (!text) {
            return [];
        }

        // ensure basics
        await this.getBasics();

        var response: Response;

        try {
            response = await fetch(this.functionUrl + "/api/ListCharacters?text=" + encodeURIComponent(text));
        }
        catch (e) {
            console.log("error: " + e);
            return null;
        }

        var chars: CharDef[] = await response.json();
        return chars.map(c => this.convertChar(c.codepoint, c));
    };


    /** Find all (max 80) characters whose name contains the words in the supplied text or match around a numerical value.
        * @async
        * @param {string} text - the partial name(s) to search for.
        * @returns {array} - an array of codepoint descriptions.
        */
    public findChars = async function (text: string): Promise<DisplayChar[]> {
        console.log("Calling function FindCharacters to search for: " + text);
        if (!text) {
            return [];
        }

        // ensure basics
        await this.getBasics();

        var response: Response;

        try {
            response = await fetch(this.functionUrl + "/api/FindCharacters?search=" + encodeURIComponent(text));
        }
        catch (e) {
            console.log("error: " + e);
            return null;
        }

        var chars: CharDef[] = await response.json();
        var res = chars.map(c => this.convertChar(c.codepoint, c));
        return res;
    };

    /** Get a sorted list of blocknames + indices.
        * @async
        * @returns {array} - a sorted list of block names.
        */
    public getBlockList = async function (): Promise<BlockDef[]> {

        await this.getBasics();

        var l = this.basics.blocks;
        var blocks = [];
        for (var b in l) {
            blocks.push({ "index": b, "name": l[b] });
        }

        // in-place sort: the Latin ones first, then alphabetically
        blocks.sort(function (a: BlockDef, b: BlockDef) {
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
            } else if (b.name.indexOf("Latin") >= 0) {
                return 1; // latin b before non-latin a
            } else {
                // both "non-Latin"
                return a.name < b.name ? -1 : 1;
            }
        });

        return blocks;
    };

    /** Get a list of categories
     * @returns {array} - a list of categories
     */
    public getCategoryList = async function (): Promise<BlockDef[]> {
        // just to be sure
        await this.getBasics();

        var l = this.basics.categories;
        var cats = [];
        for (var c in l) {
            cats.push({ "index": c, "name": l[c] });
        }

        return cats;
    }

    /** Find all characters in the specified block.
        * @async
        * @param {int} blockName - the full name of the block.
        * @returns {array} - a list of characters in the block.
        */
    public findCharsByBlock = async function (blockName: string): Promise<DisplayChar[]> {
        await this.getBasics();
        var response: Response;

        try {
            response = await fetch(this.functionUrl + "/api/GetCharactersByType?block=" + encodeURIComponent(blockName));
        }
        catch (e) {
            console.log("error: " + e);
            return null;
        }

        var chars: CharDef[] = await response.json();
        var res = chars.map(c => this.convertChar(c.codepoint, c));
        return res;
    };

    public findCharsByCategory = async function (categoryName: string): Promise<DisplayChar[]> {
        await this.getBasics();
        var response: Response;

        try {
            response = await fetch(this.functionUrl + "/api/GetCharactersByType?category=" + encodeURIComponent(categoryName));
        }
        catch (e) {
            console.log("error: " + e);
            return null;
        }

        var chars: CharDef[] = await response.json();
        var res = chars.map(c => this.convertChar(c.codepoint, c));
        return res;
    }

    /** Count the number of characters in the list.
        * @async
        * @returns {int} the total number of character descriptions available.
        */
    public getCharCount = async function () {
        var l = await this.getBasics();

        return l.charCount;
    };
}

// make sure TS knows that I can set it
interface Window {
    decoder: Decoder;
}

(function (window) {
    'use strict'

    var decoder = new Decoder();

    window.decoder = decoder;
})(window);

