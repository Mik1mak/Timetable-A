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
        const requestBody = {
            name: lesson.name,
            start: lesson.start,
            duration: lesson.duration,
            classroom: lesson.classroom,
            link: lesson.link,
        };

        return this.http.post<any>(`${this.url}/${lesson.groupId}`, requestBody);
    }

    delete(lessonId: number) {
        return this.http.delete(`${this.url}/${lessonId}`);
    }

    verify(lesson: any, selectedGroupsIds: number[]) {
        const requestBody = {
            lesson: {
                name: 'foo',
                start: lesson.start,
                duration: lesson.duration
            },
            groupIds: selectedGroupsIds
        };

        return this.http.post<ValidationErrors>(`${this.url}/Verify/${lesson.groupId}`, requestBody);
    }
}