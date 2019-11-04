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
    searchname: string;

    constructor(
        private route: ActivatedRoute,
        private unidecoder: UnidecoderService) { }

    ngOnInit() {
        const eventName = 'input';
        this.searchBox = document.getElementById('searchString') as HTMLInputElement;

        // keep reacting to changes in the searchbox
        this.result = fromEvent(this.searchBox, eventName).pipe(
            map((e: Event) => (e.target as HTMLInputElement).value.trim()),
            debounceTime(500),
            distinctUntilChanged(),
            flatMap((text: string) => this.findCharacters(text)),
        );

        // keep watching for changes in 'name' parameter (if present)
        this.route.queryParamMap.subscribe(parms => {
            const paramname = parms.get('name');
            if (paramname) {
                console.log("got NEW name param: '" + paramname + "'")
                this.searchBox.value = paramname;
                this.searchBox.dispatchEvent(new Event(eventName));
            }
        });

        // extra (delayed) dispatch for on-start event
        setTimeout(() => this.searchBox.dispatchEvent(new Event(eventName)), 100);
    }

    findCharacters(text: string): Observable<Charinfo[]> {
        console.log('getting characters for "' + text + '"');
        this.searchname = text;

        if (text) {
            this.getting = true;
            let obs = this.unidecoder.findCharacters(text);
            obs.subscribe({ next: () => this.getting = false });
            return obs;
        } else {
            return of([]);
        }
    }
}
