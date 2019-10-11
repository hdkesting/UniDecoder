import { Component, OnInit, Input } from '@angular/core';
import { Charinfo } from '../models/charinfo';

@Component({
  selector: 'app-char-list',
  templateUrl: './char-list.component.html',
  styleUrls: ['./char-list.component.less']
})
export class CharListComponent implements OnInit {
    @Input() characters: Charinfo[]; 

  constructor() { }

  ngOnInit() {
  }

}
