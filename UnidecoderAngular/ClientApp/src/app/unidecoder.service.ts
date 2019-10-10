import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../environments/environment';
import { Basics } from './models/basics';

@Injectable({
  providedIn: 'root'
})
export class UnidecoderService {
    private basicInfo: Observable<object>;

    constructor(
        private http: HttpClient
    ) { }


    getBasics(): Observable<object> {
        if (!this.basicInfo) {
            const url = environment.api + '/api/GetBasicInfo';
            console.log("GETting " + url);
            this.basicInfo = this.http.get(url);
        }

        return this.basicInfo;
    }
}
