import { Component, OnInit } from '@angular/core';
import { Observable, of } from 'rxjs';
import { ActivatedRoute } from '@angular/router';
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
    activeCallback;
    categoryName: string;
    result: Observable<Charinfo[]>;

    constructor(
        private route: ActivatedRoute,
        private unidecoder: UnidecoderService) { }

    ngOnInit() {
        this.categories = this.unidecoder.getCategoryList();
        this.categories.subscribe({ next: cats => this.categoryName = cats[0].name });
//        this.categoryName = this.categories[0].name;

        this.route.queryParamMap.subscribe(parms => {
            this.categoryName = parms.get('cat');
            console.log("got NEW name param: '" + this.categoryName + "'")
            this.findCharacters(this.categoryName);
        });
    }

    processChange() {
        if (this.activeCallback) {
            console.log("cancelling previous search");
            clearTimeout(this.activeCallback);
            this.activeCallback = null;
        }

        if (this.categoryName) {
            // get results, but wait after half a second for further changes
            this.activeCallback = setTimeout(() => this.findCharacters(this.categoryName), 500);
        }
    }

    findCharacters(category: string): void {
        if (category) {
            console.log("key up, value=" + category);
            this.result = this.unidecoder.findCharsByCategory(category);
            console.log("got results");
        } else {
            this.result = of([]);
        }
    }
}
