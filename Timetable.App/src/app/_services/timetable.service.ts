import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { environment } from '@environments/environment';
import { Timetable, Week } from '@app/_models';
import { GroupsService } from './groups.service';
import { map } from 'rxjs/operators';

@Injectable({ providedIn: 'root' })
export class TimetableService {
    private readonly url = `${environment.apiUrl}/api/AltTimetable`;

    constructor(
        private http: HttpClient) { }

    get() {
        return this.http.get<Timetable>(this.url)
            .pipe(map(timetabe => {
                timetabe.weeks?.forEach(week => this.constructDates(week));
                return timetabe;
            }));
    }
    
    getWeek(weekIndex: number) {
        return this.http.get<Week>(`${this.url}/Week/${weekIndex}`)
            .pipe(map(week => {
                this.constructDates(week);
                return week;
            }))
    }

    private constructDates(week: Week) {
        week.days?.forEach(day => day.lessons
            ?.forEach(lesson => {
                lesson.start = new Date(lesson.start);
            }));

        if(week.minStart)
            week.minStart = new Date(week.minStart);
        if(week.maxStop)
            week.maxStop = new Date(week.maxStop);
    }
}