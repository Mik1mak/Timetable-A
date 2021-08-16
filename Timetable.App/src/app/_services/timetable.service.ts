import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { environment } from '@environments/environment';
import { Timetable } from '@app/_models';
import { GroupsService } from './groups.service';

@Injectable({ providedIn: 'root' })
export class TimetableService {
    private readonly url = `${environment.apiUrl}/api/Timetable/simple`;

    constructor(
        private http: HttpClient,
        private groupService: GroupsService) { }

    get() {
        return this.http.get<Timetable>(this.url);
    }
    
    getCurrentGroups() {
        return this.http.post<Timetable>(this.url, this.groupService.selectedValue);
    }
}