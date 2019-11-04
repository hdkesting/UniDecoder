import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Observable, of, fromEvent } from 'rxjs';
import { map, debounceTime, distinctUntilChanged, flatMap } from 'rxjs/operators';
import { UnidecoderService } from '../unidecoder.service';
import { Charinfo } from '../models/charinfo';
import { BlockDef } from '../models/blockdef';

@Component({
    selector: 'app-block',
    templateUrl: './block.component.html',
    styleUrls: ['./block.component.css']
})
export class BlockComponent implements OnInit {
    blocks: Observable<BlockDef[]>;
    blockName: string;
    result: Observable<Charinfo[]>;
    dropdown: HTMLSelectElement;
    getting: boolean;

    constructor(
        private route: ActivatedRoute,
        private unidecoder: UnidecoderService) { }

    ngOnInit() {
        const eventName = 'change';
        this.dropdown = document.getElementById('blockSelect') as HTMLSelectElement;

        this.blocks = this.unidecoder.getBlockList();
        this.blocks.subscribe({
            next: () => setTimeout(() => {
                this.dropdown.selectedIndex = 0;
                setTimeout(() => this.dropdown.dispatchEvent(new Event(eventName)), 100);
              }, 100)
            });

        // when the query param changes (a link was clicked), set the dropdown and trigger the change event
        this.route.queryParamMap.subscribe(parms => {
            this.blockName = parms.get('block');
            console.log("got NEW block param: '" + this.blockName + "'")
            if (this.blockName) {
                this.dropdown.value = this.blockName;
                setTimeout(() => this.dropdown.dispatchEvent(new Event(eventName)), 100);
            }
        });

        // on change event, get characters for the selected block
        this.result = fromEvent(this.dropdown, eventName).pipe(
            map(e => (e.target as HTMLSelectElement).value),
            debounceTime(300),
            distinctUntilChanged(),
            flatMap((block: string) => this.findCharacters(block))
        );
    }

    findCharacters(block: string): Observable<Charinfo[]> {
        if (block) {
            this.getting = true;
            this.blockName = block;
            console.log("changed to value=" + block);
            let obs = this.unidecoder.findCharsByBlock(block);
            obs.subscribe({ next: () => this.getting = false });
            return obs;
        } else {
            return of([]);
        }
    }

}
