import { Injectable } from '@angular/core';
import { AbstractControl, AsyncValidator, AsyncValidatorFn, ValidationErrors, ValidatorFn } from '@angular/forms';
import { Week } from '@app/_models';
import { GroupsService, LessonsService } from '@app/_services';
import { Observable } from 'rxjs';
import { first } from 'rxjs/operators';
import { TotalTime } from './total-time';

export function lessonCollisionValidator(lessonsService: LessonsService, groupService: GroupsService): AsyncValidatorFn {
    return (control: AbstractControl): Promise<ValidationErrors | null> | Observable<ValidationErrors | null> => {
        const groupId: number = control.get('group')!.value;
        const duration: number = control.get('duration')!.value;
        const start: string = control.get('start')!.value;
        const day: number = control.get('day')!.value;
        const weekNumber: number = control.get('week')!.value;
        
        return lessonsService.verify({
            groupId: groupId,
            duration: duration,
            start: TotalTime.createDateTimeIso(weekNumber, day, start)
        }, groupService.selectedValue).pipe(first());
      };
}