(async function (window) {

    var list;
    var decoder = {};

    var getList = async function () {
        if (!list) {
            console.log("fetching");
            var response;
            try {
                response = await fetch("api/unicode/characters");
            } catch (e) {
                // maybe not found because of caching
                console.log("error: " + e);
                response = await fetch("api/unicode/characters?" + new Date());
            }
            
            list = await response.json();
            console.log("got the list");
            for (var cp in list.characters) {
                var c = list.characters[cp];
                c.NAME = c.name.toUpperCase();
            }

            console.log("uppercased all the names");
        }

        return list;
    };

    var getChar = async function (cp, fillMissing) {
        if (cp <= 0) {
            return null;
        }

        var l = await getList();

        var c = l.characters[cp];
        var c2;
        if (c) {
            c2 = {
                codepoint: cp,
                hex: c.hex,
                name: c.name,
                block: l.blocks[c.block],
                category: l.categories[c.category]
            };

            return c2;
        }

        if (fillMissing) {
            c2 = {
                codepoint: cp,
                hex: 'FFFD', // replacement char
                name: 'Unknown',
                block: '?',
                category: 'Private Use'
            };

            return c2;
        }

        return null;
    };

    var makeInt = function (value, radix) {
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
        for (var i = 0; i < text.length; i++) {
            var cp = text.codePointAt(i);
            var c2 = await getChar(cp, true);

            characters.push(c2);
            if (cp > 65536) i++;
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
            for (var i = cp - 5; i <= cp + 5; i++) {
                c = await getChar(i, false);
                if (c) {
                    //console.log("got (dec) " + i);
                    characters.push(c);
                }
            }
        }

        cp = makeInt(text, 16);
        if (cp) {
            for (var j = cp - 5; j <= cp + 5; j++) {
                c = await getChar(j);
                if (c) {
                    //console.log("got (hex) " + i);
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
                    if (c.NAME.indexOf(t) < 0) {
                        c = null;
                        break;
                    }
                }

                if (c) {
                    var c2 = {
                        codepoint: cp,
                        hex: c.hex,
                        name: c.name,
                        block: list.blocks[c.block],
                        category: list.categories[c.category]
                    };
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

        for (cp in list.characters) {
            var c = list.characters[cp];
            if (c.block === blockId) {
                chars.push({
                    codepoint: cp,
                    hex: c.hex,
                    name: c.name,
                    block: l.blocks[c.block],
                    category: l.categories[c.category]
                });
            }
        }

        return chars;
    };

    window.decoder = decoder;
})(window);

