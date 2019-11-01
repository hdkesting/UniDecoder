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
        this.dropdown = document.getElementById('blockSelect') as HTMLSelectElement;

        this.blocks = this.unidecoder.getBlockList();
        this.blocks.subscribe({ next: x => this.blockName = x[0].name });

        this.route.queryParamMap.subscribe(parms => {
            this.blockName = parms.get('block');
            console.log("got NEW block param: '" + this.blockName + "'")
            this.result = this.findCharacters(this.blockName);
        });

        this.result = fromEvent(this.dropdown, 'change').pipe(
            map(e => (e.target as HTMLSelectElement).value),
            debounceTime(300),
            distinctUntilChanged(),
            flatMap((block: string) => this.findCharacters(block))
        );
    }

    findCharacters(block: string): Observable<Charinfo[]> {
        if (block) {
            this.getting = true;
            console.log("key up, value=" + block);
            let obs = this.unidecoder.findCharsByBlock(block);
            obs.subscribe({ next: () => this.getting = false });
            return obs;
        } else {
            return of([]);
        }
    }

}
