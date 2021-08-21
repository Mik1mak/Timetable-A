import { Injectable } from '@angular/core';
import { Lesson } from '@app/_models';
import Modal from 'bootstrap/js/dist/modal';
import { Subject } from 'rxjs';
import { first } from 'rxjs/operators';

@Injectable({ providedIn: 'root' })
export class ModalService {

    private defaultLessonValuesSubject = new Subject<any>();
    defaultLessonValues = this.defaultLessonValuesSubject.asObservable();

    private confirmationContentSubject = new Subject<any>();
    confirmationContent = this.confirmationContentSubject.asObservable();

    private confirmationSubject = new Subject<boolean>();
    private confirmation = this.confirmationSubject.asObservable();

    openAddLessonModal(defaultWeekNumber: number, defaultDayOfWeek?: number, defaultValues?: Lesson) {
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

    openConfirmModal(title: string, content: string) {
        const modalElement = <Element>document.getElementById('confirm-modal');
        const confirmaModal = new Modal(modalElement, {keyboard: false});
        this.confirmationContentSubject.next({title, content});
        confirmaModal.show();        
        return this.confirmation.pipe(first());
    }
    
    confirm(confirmationState: boolean) {
        this.confirmationSubject.next(confirmationState);
    }
}