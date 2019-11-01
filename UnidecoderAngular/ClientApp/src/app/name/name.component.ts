import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Observable, of, fromEvent } from 'rxjs';
import { map, debounceTime, distinctUntilChanged, flatMap } from 'rxjs/operators';
import { Charinfo } from '../models/charinfo';
import { UnidecoderService } from '../unidecoder.service';

@Component({
    selector: 'app-name',
    templateUrl: './name.component.html',
    styleUrls: ['./name.component.less']
})
export class NameComponent implements OnInit {
    result: Observable<Charinfo[]>;
    searchBox: HTMLInputElement;
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
            map((e: KeyboardEvent) => (e.target as HTMLInputElement).value),
            debounceTime(500),
            distinctUntilChanged(),
            flatMap((text: string) => this.findCharacters(text)),
        );

        this.result.subscribe({ next: () => this.getting = false });
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
