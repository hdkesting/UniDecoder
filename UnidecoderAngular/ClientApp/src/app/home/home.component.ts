import { Component, OnInit } from '@angular/core';
import { Charinfo, getCharSample } from '../models/charinfo';


@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent implements OnInit {
    sampleChar: Charinfo;

    constructor() {
        this.sampleChar = getCharSample();
    }

    ngOnInit() {
    }
}
