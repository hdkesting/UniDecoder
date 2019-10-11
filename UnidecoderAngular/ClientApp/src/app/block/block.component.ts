import { Component, OnInit } from '@angular/core';
import { BlockDef } from '../models/blockdef';
import { Observable, of } from 'rxjs';
import { ActivatedRoute } from '@angular/router';
import { UnidecoderService } from '../unidecoder.service';
import { Charinfo } from '../models/charinfo';

@Component({
    selector: 'app-block',
    templateUrl: './block.component.html',
    styleUrls: ['./block.component.css']
})
export class BlockComponent implements OnInit {
    blocks: BlockDef[];
    activeCallback;
    blockName: string;
    result: Observable<Charinfo[]>;

    constructor(
        private route: ActivatedRoute,
        private unidecoder: UnidecoderService) { }

    async ngOnInit() {
        this.blocks = await this.unidecoder.getBlockList();
        this.blockName = this.blocks[0].name;

        this.route.queryParamMap.subscribe(parms => {
            this.blockName = parms.get('block');
            console.log("got NEW name param: '" + this.blockName + "'")
            this.findCharacters(this.blockName);
        });

    }

    processChange() {
        if (this.activeCallback) {
            console.log("cancelling previous search");
            clearTimeout(this.activeCallback);
            this.activeCallback = null;
        }

        if (this.blockName) {
            // get results, but wait after half a second for further changes
            this.activeCallback = setTimeout(() => this.findCharacters(this.blockName), 500);
        }
    }

    findCharacters(block: string): void {
        if (block) {
            console.log("key up, value=" + block);
            this.result = this.unidecoder.findCharsByBlock(block);
            console.log("got results");
        } else {
            this.result = of([]);
        }
    }

}
