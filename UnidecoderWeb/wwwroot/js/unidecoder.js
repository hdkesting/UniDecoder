(async function (window) {
    'use strict'

    var list;
    var decoder = {};

    // get the local list if available, else get from server (and store)
    var getList = async function () {
        if (!list) {
            console.log("fetching");
            var response;
            try {
                var version = await fetch("api/unicode/version");
                version = await version.text();
                console.log("unicode version: " + version);
                response = await fetch("api/unicode/characters?" + version);
            } catch (e) {
                // maybe not found because of caching
                console.log("error: " + e);
                response = await fetch("api/unicode/characters?" + new Date());
            }
            
            list = await response.json();
            console.log("got the list");

            // make an uppercase copy of all names, for easier searching
            for (let cp in list.characters) {
                var c = list.characters[cp];
                c.NAME = c.name.toUpperCase();
            }

            console.log("uppercased all the names");
        }

        return list;
    };

    // convert a char from the (local) list to a display value
    // params: cp = codepoint (int), c = char from list (optional, object)
    var convertChar = function (cp, c) {
        if (!c) {
            c = list[cp];
        }

        return {
            codepoint: cp,
            hex: c.hex,
            name: c.name,
            block: list.blocks[c.block],
            blockId: c.block,
            category: list.categories[c.category],
            isLatin: list.blocks[c.block].indexOf("Latin") >= 0
        };
    };

    // try and get a character from the list
    // params: cp = codepoint (int), fillMissing = when true, create a "missing value" (bool), else use null
    var getChar = async function (cp, fillMissing) {
        if (cp <= 0) {
            return null;
        }

        var l = await getList();

        var c = l.characters[cp];

        if (c) {
            return convertChar(cp, c);
        }

        if (fillMissing) {
            return {
                codepoint: cp,
                hex: 'FFFD', // replacement char
                name: 'Unknown',
                block: '?',
                category: 'Private Use'
            };
        }

        return null;
    };

    // converts a value to an int, using the supplied radix
    // params: value = value to convert (string), radix = base to use for conversion (10 or 16)
    var makeInt = function (value, radix) {
        // remove any leading 0's
        while (value.length && value[0] === '0') {
            value = value.substr(1);
        }

        var i = parseInt(value, radix);

        if (isNaN(i)) return 0;

        // parseInt already succeeds when *some* characters can be parsed, it will ignore an unparsable rest.
        if (i.toString(radix).toUpperCase() === value.toUpperCase()) {
            return i;
        }

        return 0;
    };

    // get all characters in the supplied text
    decoder.expandToChars = async function (text) {
        var characters = [];
        var list = await getList();
        for (let i = 0; i < text.length; i++) {
            var cp = text.codePointAt(i);
            var c2 = await getChar(cp, true);

            characters.push(c2);
            if (cp > 65536) i++; // skip other half of surrogate pair
        }

        return characters;
    };

    // find all (max 50) characters whose name contains the words in the supplied text
    decoder.findChars = async function (text) {
        console.log("finding " + text);
        if (!text) {
            return [];
        }

        var characters = [];
        var c;

        var cp = makeInt(text, 10);
        if (cp) {
            for (let i = cp - 5; i <= cp + 5; i++) {
                c = await getChar(i, false);
                if (c) {
                    characters.push(c);
                }
            }
        }

        cp = makeInt(text, 16);
        if (cp) {
            for (let i = cp - 5; i <= cp + 5; i++) {
                c = await getChar(i);
                if (c) {
                    characters.push(c);
                }
            }
        }

        text = text.toUpperCase().split(' ');
        var list = await getList();

        for (cp in list.characters) {
            if (list.characters.hasOwnProperty(cp)) {
                c = list.characters[cp];
                for (var t of text) {
                    var idx = c.NAME.indexOf(t);
 /* eslint no-extra-parens: ["error", "all", { "nestedBinaryExpressions": false }] */
                   if (idx < 0 || (idx > 0 && c.NAME[idx - 1] !== ' ')) {
                        // either not found at all, or the character just before (if any) is not a space: no match
                        c = null;
                        break;
                    }
                }

                if (c) {
                    var c2 = convertChar(cp, c);
                    characters.push(c2);
                    if (characters.length > 80) {
                        console.log("found enough");
                        break;
                    }
                }
            }
        }

        return characters;
    };

    // get a sorted list of blocknames + indices
    decoder.getBlockList = async function () {
        var l = await getList();
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
            } else if (b.name.indexOf("Latin") >= 0) {
                return 1; // latin b before non-latin a
            } else {
                // both "non-Latin"
                return a.name < b.name ? -1 : 1;
            }
        });

        return blocks;
    };

    decoder.findCharsByBlock = async function (blockId) {
        var l = await getList();
        var chars = [];

        for (let cp in l.characters) {
            var c = l.characters[cp];
            if (c && c.block === blockId) {
                chars.push(convertChar(cp, c));
            }
        }

        return chars;
    };

    decoder.getCharCount = async function () {
        var l = await getList();
        let count = 0;
        // "characters" is not an array, but an object with numerically named properties.
        for (let cp in l.characters) {
            if (l.characters.hasOwnProperty(cp)) {
                count++;
            }
        }

        return count;
    };

    window.decoder = decoder;
})(window);

