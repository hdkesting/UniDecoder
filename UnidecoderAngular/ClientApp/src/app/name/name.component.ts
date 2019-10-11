import { Component, OnInit } from '@angular/core';
import { Charinfo } from '../models/charinfo';
import { Observable, of } from 'rxjs';
import { ActivatedRoute } from '@angular/router';
import { SingleLine } from '../models/single-line';
import { UnidecoderService } from '../unidecoder.service';

@Component({
    selector: 'app-name',
    templateUrl: './name.component.html',
    styleUrls: ['./name.component.less']
})
export class NameComponent implements OnInit {
    result: Observable<Charinfo[]>;
    readonly model: SingleLine = new SingleLine();
    activeCallback;

    constructor(
        private route: ActivatedRoute,
        private unidecoder: UnidecoderService) { }

    ngOnInit() {
        // keep reacting to changes in ('name') param
        this.route.queryParamMap.subscribe(parms => {
            this.model.value = parms.get('name');
            console.log("got NEW name param: '" + this.model.value + "'")
            this.findCharacters(this.model);
        });
     }

    keyup(): void {
        if (this.activeCallback) {
            console.log("cancelling previous search");
            clearTimeout(this.activeCallback);
            this.activeCallback = null;
        }

        if (this.model.value) {
            // get results, but wait after half a second for further changes
            this.activeCallback = setTimeout(() => this.findCharacters(this.model), 500);
        }
    }

    findCharacters(model: SingleLine): void {
        if (model.value) {
            console.log("key up, value=" + model.value);
            this.result = this.unidecoder.findCharacters(model.value);
            console.log("got results");
        } else {
            this.result = of([]);
        }
    }

}
