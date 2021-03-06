import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of, pipe } from 'rxjs';
import { map, flatMap } from 'rxjs/operators';
import { environment } from '../environments/environment';
import { Basics } from './models/basics';
import { Charinfo } from './models/charinfo';
import { BlockDef } from './models/blockdef';

@Injectable({
    providedIn: 'root'
})
export class UnidecoderService {
    private static basicInfo: Basics;
    private basicInfo$: Observable<Basics>;

    constructor(
        private http: HttpClient
    ) {
        console.log("UnidecoderService ctor");
    }

    // get basic information: stats and lists for dropdowns
    getBasics(): Observable<Basics> {
        // TODO always invoke to make sure the basicInfo exists
        if (!UnidecoderService.basicInfo && !this.basicInfo$) {
            const url = environment.api + '/api/GetBasicInfo';
            console.log("getBasics GET " + url);
            this.basicInfo$ = this.http.get<Basics>(url);

            const myObserver = {
                next: (x: Basics) => {
                    UnidecoderService.basicInfo = x;
                    console.log("Got basic info");
                }
            };

            this.basicInfo$.subscribe(myObserver);
            return this.basicInfo$;
        }

        return of(UnidecoderService.basicInfo);
    }

    // list all characters in the supplied text
    listCharacters(text: string): Observable<Charinfo[]> {
        if (!text) {
            return of([]);
        }

        const uri = environment.api + '/api/ListCharacters?text=' + encodeURIComponent(text);
        console.log("listCharacters GET " + uri);
        return this.getCharacters(uri);
    }

    // find chanracters by (partial) name
    findCharacters(search: string): Observable<Charinfo[]> {
        if (!search) {
            return of([]);
        }

        const uri = environment.api + '/api/FindCharacters?search=' + encodeURIComponent(search);
        console.log("findCharacters GET " + uri);
        return this.getCharacters(uri);
    }

    // find all chars of the supplied block
    findCharsByBlock(blockname: string): Observable<Charinfo[]> {
        if (!blockname) {
            return of([]);
        }

        const uri = environment.api + '/api/GetCharactersByType?block=' + encodeURIComponent(blockname);
        console.log("findCharsByBlock GET " + uri);
        return this.getCharacters(uri);
    }

    // find all characters of the supplied category
    findCharsByCategory(categoryname: string): Observable<Charinfo[]> {
        if (!categoryname) {
            return of([]);
        }

        const uri = environment.api + '/api/GetCharactersByType?category=' + encodeURIComponent(categoryname);
        console.log("findCharsByCategory GET " + uri);
        return this.getCharacters(uri);
    }

    private getCharacters(uri: string): Observable<Charinfo[]> {
        return this.getBasics().pipe(
            flatMap(localbasics => this.http.get<Charinfo[]>(uri)
                .pipe(map((cia: Charinfo[]) => {
                    for (let ci of cia) {
                        ci.categoryName = localbasics.categories[ci.categoryId];
                        // console.log(ci.categoryId + "=" + ci.categoryName);
                    }
                    return cia;
                }))
            ));
    }

    getBlockList(): Observable<BlockDef[]> {
        return this.getBasics().pipe(
            map(localbasics => {
                var l = localbasics.blocks;
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
            }));
    }

    getCategoryList(): Observable<BlockDef[]> {
        return this.getBasics().pipe(
            map(localbasics => {
                var l = localbasics.categories;
                var categories: BlockDef[] = [];
                for (var b in l) {
                    categories.push(new BlockDef(Number(b), l[b]));
                }
                return categories;
           }));
    }
}
