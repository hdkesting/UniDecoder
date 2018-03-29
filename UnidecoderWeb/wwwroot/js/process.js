function process(inputElement, targetElement, templateElement) {
    var text = inputElement.value;
    console.log("processing '" + text + "'");

    fetch("api/unicode/characters").then(response => {
        console.log("got char list");
        response.json().then(list => {
            console.log("known chars = " + Object.keys(list).length);
            var result = "";
            var data = { characters:[] };
            for (var i = 0; i < text.length; i++) {
                var cp = text.codePointAt(i);
                var c = list[cp];
                if (c) {
                    data.characters.push(c);
                }
                if (cp > 65536) i++;
            }

            console.log("found chars in text: " + data.characters.length);
            console.dir(data);
            var templateSource = templateElement.innerHTML;
            var template = Handlebars.compile(templateSource);

            // property(-name) "characters" of "data" matches {{#each characters}} in template
            var content = template(data);
            targetElement.innerHTML = content;

            //console.log(content);
            console.log("data bound?");
        });
    });
}