import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from '../environments/environment';
import { Basics } from './models/basics';
import { Charinfo } from './models/charinfo';
import { BlockDef } from './models/blockdef';

@Injectable({
    providedIn: 'root'
})
export class UnidecoderService {
    private static basicInfo: Basics;
    private basicGetter: Observable<Basics>;

    constructor(
        private http: HttpClient
    ) {
        console.log("UnidecoderService ctor");
    }


    getBasics(): Observable<Basics> {
        if (!UnidecoderService.basicInfo && !this.basicGetter) {
            const url = environment.api + '/api/GetBasicInfo';
            console.log("getBasics GET " + url);
            this.basicGetter = this.http.get<Basics>(url);

            const myObserver = {
                next: (x: Basics) => {
                    UnidecoderService.basicInfo = x;
                    console.log("Got basic info");
                }
            };

            this.basicGetter.subscribe(myObserver);
            return this.basicGetter;
        }

        return of(UnidecoderService.basicInfo);
    }

    listCharacters(text: string): Observable<Charinfo[]> {
        if (!text) {
            return of([]);
        }

        const uri = environment.api + '/api/ListCharacters?text=' + encodeURIComponent(text);
        console.log("listCharacters GET " + uri);
        return this.getCharacters(uri);
    }

    findCharacters(search: string): Observable<Charinfo[]> {
        if (!search) {
            return of([]);
        }

        const uri = environment.api + '/api/FindCharacters?search=' + encodeURIComponent(search);
        console.log("findCharacters GET " + uri);
        return this.getCharacters(uri);
    }

    findCharsByBlock(blockname: string): Observable<Charinfo[]> {
        if (!blockname) {
            return of([]);
        }

        const uri = environment.api + '/api/GetCharactersByType?block=' + encodeURIComponent(blockname);
        console.log("findCharsByBlock GET " + uri);
        return this.getCharacters(uri);
    }

    findCharsByCategory(categoryname: string): Observable<Charinfo[]> {
        if (!categoryname) {
            return of([]);
        }

        const uri = environment.api + '/api/GetCharactersByType?category=' + encodeURIComponent(categoryname);
        console.log("findCharsByCategory GET " + uri);
        return this.getCharacters(uri);
    }

    async getCategoryById(id: number): Promise<string> {
        console.log("getCategoryById: Getting category name for " + id);
        const info = await this.getBasics().toPromise();
        console.log("getCategoryById: got basics")
        if (info && info.categories && info.categories.get) {
            const name = info.categories.get(id);
            console.log("getCategoryById: found " + name);
            return name;
        }

        console.log("getCategoryById: no info or categories");
        return null;
    }

    private getCharacters(uri: string): Observable<Charinfo[]> {
        return this.http.get<Charinfo[]>(uri)
            .pipe(map((cia: Charinfo[]) => {
                for (let ci of cia) {
                    ci.categoryName = UnidecoderService.basicInfo.categories[ci.categoryId];
                    console.log(ci.categoryId + "=" + ci.categoryName);
                }
                return cia;
            }));
    }

    async getBlockList(): Promise<BlockDef[]> {
        const basics = await this.getBasics().toPromise();

        var l = basics.blocks;
        var blocks: BlockDef[] = [];
        for (var b in l) {
            blocks.push(new BlockDef(Number(b), l[b]));
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
    }

    async getCategoryList(): Promise<BlockDef[]> {
        const basics = await this.getBasics().toPromise();

        var l = basics.categories;
        var categories: BlockDef[] = [];
        for (var b in l) {
            categories.push(new BlockDef(Number(b), l[b]));
        }

        return categories;
    }
}
