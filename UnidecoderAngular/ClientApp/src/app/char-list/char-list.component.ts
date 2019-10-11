import { Component, OnInit, Input } from '@angular/core';
import { Charinfo } from '../models/charinfo';
import { UnidecoderService } from '../unidecoder.service';

@Component({
    selector: 'app-char-list',
    templateUrl: './char-list.component.html',
    styleUrls: ['./char-list.component.less']
})
export class CharListComponent implements OnInit {
    @Input() characters: Charinfo[];

    constructor(private unidecoder: UnidecoderService) { }

    ngOnInit() {
    }

    public isLatin(char: Charinfo): boolean {
        return char.name.indexOf('Latin') !== -1;
    }

    public async categoryName(char: Charinfo): Promise<string> {
        //return await this.unidecoder.getCategoryById(char.categoryId);

        return "n/a";
    }
}
