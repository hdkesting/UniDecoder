import { Component, OnInit } from '@angular/core';
import { Charinfo } from '../models/charinfo';
import { Observable, of, fromEvent } from 'rxjs';
import { map, filter, debounceTime, distinctUntilChanged, switchMap, flatMap } from 'rxjs/operators';
import { ActivatedRoute } from '@angular/router';
import { UnidecoderService } from '../unidecoder.service';

@Component({
    selector: 'app-name',
    templateUrl: './name.component.html',
    styleUrls: ['./name.component.less']
})
export class NameComponent implements OnInit {
    result: Observable<Charinfo[]>;
    activeCallback;
    searchBox: HTMLInputElement;
    getter$: Observable<Charinfo[]>;
    getting: boolean;

    constructor(
        private route: ActivatedRoute,
        private unidecoder: UnidecoderService) { }

    ngOnInit() {
        this.searchBox = document.getElementById('searchString') as HTMLInputElement;

        // keep reacting to changes in ('name') param
        this.route.queryParamMap.subscribe(parms => {
            const paramname = parms.get('name');
            if (paramname) {
                console.log("got NEW name param: '" + paramname + "'")
                this.searchBox.value = paramname;
                this.result = this.findCharacters(paramname);
            }
        });

        this.result = fromEvent(this.searchBox, 'input').pipe(
            map((e: KeyboardEvent) => {
                return (e.target as HTMLInputElement).value;
            }),
            debounceTime(500),
            distinctUntilChanged(),
            flatMap((text: string) => this.findCharacters(text)),
          );

        //this.getter$.subscribe(this.processResults);
    }

    /*
    private processResults(data: Charinfo[]) {
        this.getting = false;
        this.result = data;

        console.log("got " + this.result.length + " characters");
    }
    */

    findCharacters(text: string): Observable<Charinfo[]> {
        console.log('getting characters for "' + text + '"');

        if (text) {
            this.getting = true;
            return this.unidecoder.findCharacters(text);
        } else {
            return of([]);
        }
    }
}
