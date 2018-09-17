'use strict';

/*
 * This javascript code has a direct interaction with the page. It uses code in unicoder.js.
 */

var resultTemplate;
var activeCallback;
const delay = 500;

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

function processDecoder(source, targetId, linkId) {
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
    }, delay);
}

function processSearch(source, targetId, linkId) {
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
    }, delay);

}

async function processBlockSearch(source, targetId, linkId) {
    setResultTemplate();
    if (typeof source === "string") {
        source = document.getElementById(source);
    }

    var blockId = 1 * source.value; // make sure it's a number
    console.log("finding block " + blockId);

    var data = { characters: await decoder.findCharsByBlock(blockId) };
    console.log("got block " + blockId);

    var targetElement = document.getElementById(targetId);
    var content = resultTemplate(data);
    targetElement.innerHTML = content;

    setLink(linkId, 'b', source.value);
}

async function setBlockSelect() {
    var templateElement = document.getElementById('blockselect-template');
    var templateSource = templateElement.innerHTML;
    var blockTemplate = Handlebars.compile(templateSource);

    var blocklist = await decoder.getBlockList();
    var data = { blocks: blocklist };
    var content = blockTemplate(data);

    var targetElement = document.getElementById("blockSelect");
    targetElement.innerHTML = content;
}

async function setCharCount() {
    var count = await decoder.getCharCount();
    var targetElement = document.getElementById("charcount");
    targetElement.innerHTML = count.toLocaleString(); // current locale, default options
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
                processDecoder('sampleString', 'result2', 'link2');
                break;
            case 'q':
                console.log("search chars by " + srch[1]);
                opentab('tab3', 'tabcontent3');
                document.getElementById("searchString").value = decodeURI(srch[1]);
                processSearch('searchString', 'result3', 'link3');
                break;
            case 'b':
                console.log("show block " + srch[1]);
                opentab('tab4', 'tabcontent4');
                // value is internal ID of block
                document.getElementById("blockSelect").value = decodeURI(srch[1]);
                processBlockSearch('blockSelect', 'result4', 'link4');
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
