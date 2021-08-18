import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { environment } from '@environments/environment';
import { Timetable } from '@app/_models';
import { GroupsService } from './groups.service';
import { map } from 'rxjs/operators';

@Injectable({ providedIn: 'root' })
export class TimetableService {
    private readonly url = `${environment.apiUrl}/api/Timetable/simple`;

    constructor(
        private http: HttpClient,
        private groupService: GroupsService) { }

    get() {
        return this.http.get<Timetable>(this.url)
            .pipe(map(timetabe => {
                timetabe.weeks?.forEach(week => {
                    week.days
                        ?.forEach(day => day.lessons
                            ?.forEach(lesson => lesson.start = new Date(lesson.start)));
                    if(week.minStart)
                        week.minStart = new Date(week.minStart);
                    if(week.maxStop)
                        week.maxStop = new Date(week.maxStop);
                });

                return timetabe;
            }));
    }
    
    getCurrentGroups() {
        return this.http.post<Timetable>(this.url, this.groupService.selectedValue);
    }
}