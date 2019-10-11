import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { environment } from '../environments/environment';
import { Basics } from './models/basics';
import { Charinfo } from './models/charinfo';

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
        return this.http.get<Charinfo[]>(uri);
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

    findCharacters(search: string): Observable<Charinfo[]> {
        if (!search) {
            return of([]);
        }

        const uri = environment.api + '/api/FindCharacters?search=' + encodeURIComponent(search);
        console.log("findCharacters GET " + uri);
        return this.http.get<Charinfo[]>(uri);
    }
}
