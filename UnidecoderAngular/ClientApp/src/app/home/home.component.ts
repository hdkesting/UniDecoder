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
    characterCount: string = '\"a lot of\"';
    versionTag: string;

    constructor(
        private unidecoder: UnidecoderService 
    ) {
        this.sampleChars = [getCharSample()];
    }

    ngOnInit() {
        this.basicInfo = this.unidecoder.getBasics();
        const myObserver = {
            next: (x: Basics) => {
                this.characterCount = x.charCount.toLocaleString();
                this.versionTag = "of Unicode version " + x.unicodeVersion;
            },
            error: err => { console.error('Observer got an error: ' + err); console.dir(err); },
            complete: () => console.log('Observer got a complete notification'),
        };
        this.basicInfo.subscribe(myObserver);
    }
}
