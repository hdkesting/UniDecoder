export class Charinfo {
    character: string;
    name: string;
    block: string;
    codepoint: number;
    codepointHex: string;
    categoryId: number;
    categoryName: string;
}

export function getCharSample(): Charinfo {
    let ci = new Charinfo();
    ci.character = "a";
    ci.name = "Latin Small Letter A";
    ci.block = "Basic Latin";
    ci.codepoint = 97;
    ci.codepointHex = "0061";
    ci.categoryName = "Lowercase Letter";

    return ci;
}
