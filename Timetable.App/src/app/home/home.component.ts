import { Component } from '@angular/core';
import { first } from 'rxjs/operators';

import { TimetableService } from '@app/_services';
import { Group, Timetable } from '@app/_models';

@Component({ templateUrl: 'home.component.html' })
export class HomeComponent {
    loading = false;
    timetable?: Timetable;
    groups?: Group[];

     constructor(private timetableService: TimetableService) { }

    ngOnInit() {
        this.loading = true;
        this.timetableService.get().pipe(first()).subscribe(timetable => {
            this.loading = false;
            this.timetable = timetable;
            this.groups = timetable.groups;
        });
    }
}