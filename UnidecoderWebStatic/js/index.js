'use strict';

/*
 * This javascript code has a direct interaction with the page. It uses code in unicoder.js.
 * Because of the "async/await", the VS minifier errors out. https://github.com/madskristensen/BundlerMinifier/issues/311
 */

var resultTemplate;
var activeCallback;
const delay = 500;
const selectDelay = 300;

function setResultTemplate() {
    if (!resultTemplate) {
        var templateElement = document.getElementById('result-template');
        var templateSource = templateElement.innerHTML;
        resultTemplate = Handlebars.compile(templateSource);
    }
}

function setLink(linkid, tag, value) {
    if (linkid) {
        var link = document.getElementById(linkid);
        link.href = document.location.href.split('#')[0];
        link.href += `#${tag}=${encodeURI(value)}`;
        //"#" + tag + "=" + encodeURI(source.value);
    }
}

// find all characters in the string
function processDecoder(source, targetId, linkId, skipdelay) {
    setResultTemplate();
    if (typeof source === "string") {
        source = document.getElementById(source);
    }

    if (activeCallback) {
        console.log("cancelling previous search");
        clearTimeout(activeCallback);
    }

    activeCallback = setTimeout(async function () {
        // property(-name) "characters" of "data" matches {{#each characters}} in template
        var data = { characters: await decoder.expandToChars(source.value) };

        var targetElement = document.getElementById(targetId);
        var content = resultTemplate(data);
        targetElement.innerHTML = content;

        setLink(linkId, 's', source.value);
        activeCallback = null;
    }, skipdelay ? 1 : delay);
}

// search for characters by name
function processSearch(source, targetId, linkId, skipdelay) {
    setResultTemplate();
    if (typeof source === "string") {
        source = document.getElementById(source);
    }

    if (activeCallback) {
        console.log("cancelling previous search");
        clearTimeout(activeCallback);
    }

    activeCallback = setTimeout(async function () {
        // property(-name) "characters" of "data" matches {{#each characters}} in template
        var data = { characters: await decoder.findChars(source.value) };

        var targetElement = document.getElementById(targetId);
        var content = resultTemplate(data);
        targetElement.innerHTML = content;

        setLink(linkId, 'q', source.value);
        activeCallback = null;
    }, skipdelay ? 1 : delay);

}

// block dropdown was changed, handle that.
function processBlockSearch(source, targetId, linkId, skipdelay) {
    setResultTemplate();
    if (typeof source === "string") {
        source = document.getElementById(source);
    }

    if (activeCallback) {
        console.log("cancelling previous search");
        clearTimeout(activeCallback);
    }

    var block = source.value;

    activeCallback = setTimeout(async function () {
        console.log("finding block " + block);

        var data = { characters: await decoder.findCharsByBlock(block) };
        console.log("got block " + block);

        var targetElement = document.getElementById(targetId);
        var content = resultTemplate(data);
        targetElement.innerHTML = content;

        setLink(linkId, 'b', block);
        activeCallback = null;
    }, skipdelay ? 1 : selectDelay);
}

// block dropdown was changed, handle that.
function processCategorySearch(source, targetId, linkId, skipdelay) {
    setResultTemplate();
    if (typeof source === "string") {
        source = document.getElementById(source);
    }

    if (activeCallback) {
        console.log("cancelling previous search");
        clearTimeout(activeCallback);
    }

    var category = source.value;

    activeCallback = setTimeout(async function () {
        console.log("finding category " + category);

        var data = { characters: await decoder.findCharsByCategory(category) };
        console.log("got category " + category);

        var targetElement = document.getElementById(targetId);
        var content = resultTemplate(data);
        targetElement.innerHTML = content;

        setLink(linkId, 'c', category);
        activeCallback = null;
    }, skipdelay ? 1 : selectDelay);
}


// fill the "block" dropdown
async function setBlockSelect() {
    var templateElement = document.getElementById('blockselect-template');
    var templateSource = templateElement.innerHTML;
    var blockTemplate = Handlebars.compile(templateSource);

    try {
        var blocklist = await decoder.getBlockList();
        var data = { blocks: blocklist };
        var content = blockTemplate(data);

        var targetElement = document.getElementById("blockSelect");
        targetElement.innerHTML = content;
    } catch (e) {
        console.log("An error occurred fetching the basic info");
        console.log(e);
        document.location.reload(true);
    }
}

// fill the "category" dropdown
async function setCategorySelect() {
    var templateElement = document.getElementById('categoryselect-template');
    var templateSource = templateElement.innerHTML;
    var categoryTemplate = Handlebars.compile(templateSource);

    var list = await decoder.getCategoryList();
    var data = { categories: list };
    var content = categoryTemplate(data);

    var targetElement = document.getElementById("categorySelect");
    targetElement.innerHTML = content;
}

async function setCharCount() {
    var count = await decoder.getCharCount();
    var targetElement = document.getElementById("charcount");
    targetElement.innerHTML = count.toLocaleString(); // current locale, default options

    targetElement = document.getElementById("version");
    var vers = await decoder.getVersion();
    targetElement.innerHTML = " of Unicode version " + vers;
}

function opentab(tabElement, tabContentName) {
    if (typeof tabElement === "string") {
        // supplied as id-string, convert to element
        tabElement = document.getElementById(tabElement);
    }

    var elts = document.getElementsByClassName("tab");
    for (let i = 0; i < elts.length; i++) {
        elts.item(i).classList.remove("opened");
    }

    tabElement.classList.add("opened");

    elts = document.getElementsByClassName("tabcontent");
    for (let i = 0; i < elts.length; i++) {
        elts.item(i).classList.remove("visible");
    }

    var tab = document.getElementById(tabContentName);
    tab.classList.add("visible");

    var inputs = tab.getElementsByTagName("input");
    if (inputs.length) {
        inputs[0].focus();
    } else {
        inputs = tab.getElementsByTagName("select");
        if (inputs.length) {
            inputs[0].focus();
        }
    }
}

function handleHash() {
    // window.location.hash is including '#'
    if (window.location.hash) {
        console.log("HASH: " + window.location.hash);
        var srch = window.location.hash.substr(1); // remove initial '#'
        srch = srch.split('=');  // variable = value
        switch (srch[0]) {
            case 's':
                console.log("show chars of " + srch[1]);
                opentab('tab2', 'tabcontent2');
                document.getElementById("sampleString").value = decodeURI(srch[1]);
                processDecoder('sampleString', 'result2', 'link2', true);
                break;
            case 'q':
                console.log("search chars by " + srch[1]);
                opentab('tab3', 'tabcontent3');
                document.getElementById("searchString").value = decodeURI(srch[1]);
                processSearch('searchString', 'result3', 'link3', true);
                break;
            case 'b':
                console.log("show block " + srch[1]);
                opentab('tab4', 'tabcontent4');
                // value is internal ID of block
                document.getElementById("blockSelect").value = decodeURI(srch[1]);
                processBlockSearch('blockSelect', 'result4', 'link4', true);
                break;
            case 'c':
                console.log("show category " + srch[1]);
                opentab('tab5', 'tabcontent5');
                document.getElementById("categorySelect").value = decodeURI(srch[1]); // set value of dropdown, has to be exact match
                processCategorySearch('categorySelect', 'result5', 'link5', true);
                break;
            default:
                console.log("show INTRO");
                opentab('tab1', 'tabcontent1');
                break;
        }
    } else {
        console.log("show INTRO (no #)");
        opentab('tab1', 'tabcontent1');
    }
}

function cp(cp, name) {
    let ch = String.fromCodePoint(cp);
    window.prompt("Use Ctrl+C to copy the character '" + name + "'", ch);
}
