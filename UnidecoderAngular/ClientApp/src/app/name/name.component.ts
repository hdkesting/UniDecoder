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
    result: Charinfo[];
    activeCallback;
    searchBox: HTMLElement;
    getter$: Observable<Charinfo[]>;
    getting: boolean;

    constructor(
        private route: ActivatedRoute,
        private unidecoder: UnidecoderService) { }

    ngOnInit() {
        // keep reacting to changes in ('name') param
        this.route.queryParamMap.subscribe(parms => {
            const paramname = parms.get('name');
            if (paramname) {
                console.log("got NEW name param: '" + paramname + "'")
                this.findCharacters(paramname);
            }
        });

        this.searchBox = document.getElementById('searchString');
        console.log("Got searchbox? " + this.searchBox);

        this.getter$ = fromEvent(this.searchBox, 'input').pipe(
            map((e: KeyboardEvent) => {
                return e.target.value;
            }),
            filter(text => text.length > 1),
            debounceTime(500),
            distinctUntilChanged(),
            flatMap((text: string) => this.findCharacters(text)),
          );

        this.getter$.subscribe(data => {
            this.getting = false;
            console.log("got " + data.length + " characters");
            this.result = data;
        });
     }

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
