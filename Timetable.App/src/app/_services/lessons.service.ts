import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { environment } from '@environments/environment';
import { map } from 'rxjs/operators';
import { ValidationErrors } from '@angular/forms';

@Injectable({ providedIn: 'root' })
export class LessonsService {
    private readonly url = `${environment.apiUrl}/api/Lesson`;

    constructor(private http: HttpClient) {}

    add(lesson: any) {
        return this.http.post<any>(`${this.url}/${lesson.groupId}`, {
            name: lesson.name,
            start: lesson.start,
            duration: lesson.duration,
            classroom: lesson.classroom,
            link: lesson.link,
        });
    }

    delete(lessonId: number) {
        return this.http.delete(`${this.url}/${lessonId}`);
    }

    verify(lesson: any, selectedGroupsIds: number[]) {
        let requestBody = {
            lesson: {
                name: 'foo',
                start: lesson.start,
                duration: lesson.duration
            },
            groupIds: selectedGroupsIds
        };

        return this.http.post<ValidationErrors>(`${this.url}/Verify/${lesson.groupId}`, requestBody).pipe(map(response => {
            if(response)
                return null;
            
            return {error: 'Colliding with other lessons in group or in other selected groups'};
        }));
    }
}