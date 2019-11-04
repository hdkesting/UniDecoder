import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Observable, of, fromEvent } from 'rxjs';
import { map, debounceTime, distinctUntilChanged, flatMap } from 'rxjs/operators';
import { SingleLine } from '../models/single-line';
import { Charinfo } from '../models/charinfo';
import { UnidecoderService } from '../unidecoder.service';

@Component({
  selector: 'app-text',
  templateUrl: './text.component.html',
  styleUrls: ['./text.component.css']
})
export class TextComponent implements OnInit {
    result: Observable<Charinfo[]>;
    readonly model: SingleLine = new SingleLine();
    searchBox: HTMLInputElement;
    searchname: string;
    getting: boolean;

    constructor(
        private route: ActivatedRoute,
        private unidecoder: UnidecoderService) { }

    ngOnInit() {
        const eventName = 'input';
        this.searchBox = document.getElementById('text') as HTMLInputElement;

        // keep reacting to changes in the searchbox
        this.result = fromEvent(this.searchBox, eventName).pipe(
            map((e: Event) => (e.target as HTMLInputElement).value.trim()),
            debounceTime(500),
            distinctUntilChanged(),
            flatMap((text: string) => this.findCharacters(text)),
        );

        // keep reacting to changes in ('text') param
        this.route.queryParamMap.subscribe(parms => {
            this.searchname = parms.get('text');
            if (this.searchname) {
                this.searchBox.value = this.searchname;
                this.searchBox.dispatchEvent(new Event(eventName));
            }
        });

        // extra (delayed) dispatch for on-start event
        setTimeout(() => this.searchBox.dispatchEvent(new Event(eventName)), 100);
    }

    findCharacters(text: string): Observable<Charinfo[]> {
        this.searchname = text;

        if (text) {
            console.log("key up, value=" + text);
            this.getting = true;
            let obs = this.unidecoder.listCharacters(text);
            obs.subscribe({ next: () => this.getting = false });
            return obs;
        } else {
            return of([]);
        }
    }
}
