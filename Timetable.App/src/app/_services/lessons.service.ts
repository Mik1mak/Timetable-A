import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { environment } from '@environments/environment';
import { Lesson as any } from '@app/_models';

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
}