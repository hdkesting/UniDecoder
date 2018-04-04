(async function (window) {

    var list;
    var decoder = {};

    decoder.getList = async function () {
        if (!list) {
            console.log("fetching");
            var response = await fetch("api/unicode/characters");
            list = await response.json();
        }

        return list;
    };

    decoder.getChars = async function (text) {
        var characters = [];
        var list = await this.getList();
        for (var i = 0; i < text.length; i++) {
            var cp = text.codePointAt(i);
            var c = list[cp];
            var c2;
            if (c) {
                c2 = {
                    codepoint: cp,
                    hex: c.hex,
                    name: c.name,
                    block: list.blocks[c.block],
                    category: list.categories[c.category]
                };
            } else {
                c2 = {
                    codepoint: cp,
                    hex: 'FFFD', // replacement char
                    name: 'Unknown',
                    block: '?',
                    category: 'Private Use'
                };
            }

            characters.push(c2);
            if (cp > 65536) i++;
        }

        return characters;
    };

    window.decoder = decoder;

})(window);
