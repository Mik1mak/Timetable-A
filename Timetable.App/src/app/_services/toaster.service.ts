import { Injectable } from '@angular/core';

@Injectable({ providedIn: 'root' })
export class ToasterService {
    toasts: any[] = [];

    constructor() { }

    add(message: string) {
        this.toasts.push({message, visible: true});
    }

    remove(message: any) {
        this.toasts.forEach((val, index) => {
            if(val == message)
                this.toasts.splice(index, 1);
        });
    }

    clear() {
        this.toasts = [];
    }
}