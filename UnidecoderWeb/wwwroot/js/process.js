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
        var characters = [];
        var cp = makeInt(text, 10);
        var c = await getChar(cp, false);
        if (c) {
            console.log("got (dec) " + cp);
            characters.push(c);
        }

        cp = makeInt(text, 16);
        c = await getChar(cp);
        if (c) {
            console.log("got (hex) " + cp);
            characters.push(c);
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
                    if (characters.length > 50) {
                        console.log("found enough");
                        break;
                    }
                }
            }
        }

        return characters;
    };

    window.decoder = decoder;

    // pre-fetch list, fire-and-forget
    await getList();
})(window);

