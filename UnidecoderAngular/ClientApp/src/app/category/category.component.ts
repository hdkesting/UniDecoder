import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Observable, of, fromEvent } from 'rxjs';
import { map, debounceTime, distinctUntilChanged, flatMap } from 'rxjs/operators';
import { UnidecoderService } from '../unidecoder.service';
import { Charinfo } from '../models/charinfo';
import { BlockDef } from '../models/blockdef';

@Component({
  selector: 'app-category',
  templateUrl: './category.component.html',
  styleUrls: ['./category.component.css']
})
export class CategoryComponent implements OnInit {
    categories: Observable<BlockDef[]>;
    categoryName: string;
    result: Observable<Charinfo[]>;
    dropdown: HTMLSelectElement;
    getting: boolean;

    constructor(
        private route: ActivatedRoute,
        private unidecoder: UnidecoderService) { }

    ngOnInit() {
        const eventName = 'change';
        this.dropdown = document.getElementById('catSelect') as HTMLSelectElement;

        this.categories = this.unidecoder.getCategoryList();
        this.categories.subscribe({
            next: () => setTimeout(() => {
                this.dropdown.selectedIndex = 0;
                setTimeout(() => this.dropdown.dispatchEvent(new Event(eventName)), 100);
            }, 100)
        });

        // when the query param changes (a link was clicked), set the dropdown and trigger the change event
        this.route.queryParamMap.subscribe(parms => {
            this.categoryName = parms.get('cat');
            console.log("got NEW name param: '" + this.categoryName + "'")
            if (this.categoryName) {
                this.dropdown.value = this.categoryName;
                setTimeout(() => this.dropdown.dispatchEvent(new Event(eventName)), 100);
            }
        });

        this.result = fromEvent(this.dropdown, eventName).pipe(
            map(e => (e.target as HTMLSelectElement).value),
            debounceTime(300),
            distinctUntilChanged(),
            flatMap((cat: string) => this.findCharacters(cat))
        );
    }

    findCharacters(category: string): Observable<Charinfo[]> {
        if (category) {
            console.log("key up, value=" + category);
            this.getting = true;
            let obs = this.unidecoder.findCharsByCategory(category);
            obs.subscribe({ next: () => this.getting = false });
            return obs;
        } else {
            return of([]);
        }
    }
}
