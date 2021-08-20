import { Injectable } from '@angular/core';
import { Lesson } from '@app/_models';
import Modal from 'bootstrap/js/dist/modal';
import { Subject } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class LessonsModalService {

    private defaultLessonValuesSubject = new Subject<any>();
    defaultLessonValues = this.defaultLessonValuesSubject.asObservable();

    openAddModal(defaultWeekNumber: number, defaultDayOfWeek?: number, defaultValues?: Lesson) {
        const modalElement = <Element>document.getElementById('weeks-modal-add-lesson');
        const editModal = new Modal(modalElement, {});
        this.defaultLessonValuesSubject.next({
            week: defaultWeekNumber,
            day: defaultDayOfWeek,
            name: defaultValues?.name,
            group: defaultValues?.groupId,
            classroom: defaultValues?.classroom,
            link: defaultValues?.link,
            duration: defaultValues?.duration,
            start: defaultValues?.start.toLocaleTimeString('en-GB').substr(0, 5),
        });
        editModal.show();
    }
    
}