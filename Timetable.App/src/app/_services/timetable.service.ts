import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { environment } from '@environments/environment';
import { Timetable, UserIdentity } from '@app/_models';

@Injectable({ providedIn: 'root' })
export class TimetableService {
    private readonly url = `${environment.apiUrl}/api/Timetable`;

    constructor(private http: HttpClient) { }

    get() {
        return this.http.get<Timetable>(this.url);
    }

    update(timetable: Timetable) {
        this.http.put(this.url, timetable);
    }

    delete() {
        this.http.delete(this.url);
    }
}