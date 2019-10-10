import { Component, Input } from '@angular/core';
import { Charinfo } from '../models/charinfo';

@Component({
  selector: 'app-char-info',
  templateUrl: './char-info.component.html',
  styleUrls: ['./char-info.component.less']
})
export class CharInfoComponent {
   @Input() character: Charinfo; 

  constructor() { }

}
