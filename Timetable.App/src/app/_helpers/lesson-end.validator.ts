import { AbstractControl, ValidationErrors, ValidatorFn } from "@angular/forms";

export function lessonEndValidator(): ValidatorFn {
    return (control: AbstractControl): ValidationErrors | null => {
        const duration: number = control.get('duration')!.value;
        const start: string = control.get('start')!.value;

        if(!duration || !start)
            return {};
            
        const startValues = start.split(':').map(val => parseInt(val));
        const end = startValues[0] * 60 + startValues[1] + duration;

        return end > 1440 ? {error: 'Lesson must end in the same day'} : null;
      };
}