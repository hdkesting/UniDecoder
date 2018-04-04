﻿(async function (window) {

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
            if (c) {
                characters.push(c);
            }
            if (cp > 65536) i++;
        }

        return characters;
    };

    window.decoder = decoder;

})(window);