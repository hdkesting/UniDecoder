import { Component, OnInit } from '@angular/core';
import { Charinfo, getCharSample } from '../models/charinfo';
import { Observable } from 'rxjs';
import { Basics } from '../models/basics';
import { UnidecoderService } from '../unidecoder.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent implements OnInit {
    sampleChars: Charinfo[];
    basicInfo: Observable<object>;
    characterCount: string = '❝a lot of❞';
    versionTag: string;
    ready: boolean;
    getting: boolean;

    constructor(
        private unidecoder: UnidecoderService
    ) {
        //this.sampleChars = [getCharSample()];
    }

    ngOnInit() {
        setTimeout(() => this.getBasics(this.unidecoder), 500);
    }

    getBasics(svc: UnidecoderService): void {
        console.log("getBasics - start");
        this.getting = true;
        const myObserver = {
            next: (x: Basics) => {
                this.characterCount = x.charCount.toLocaleString();
                this.versionTag = " of Unicode version " + x.unicodeVersion;
            },
            error: err => { console.error('Observer got an error: ' + err); console.dir(err); },
            complete: () => {
                this.ready = true;
                this.getting = false;
                this.sampleChars = [getCharSample()];
                console.log("getBasics - done")
            },
        };
        this.basicInfo = svc.getBasics();
        this.basicInfo.subscribe(myObserver);
    }
}
