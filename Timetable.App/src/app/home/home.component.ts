import { Component, HostListener } from '@angular/core';

@Component({ templateUrl: 'home.component.html', styleUrls: ['./home.component.css'] })
export class HomeComponent {
    width!: number;

    constructor() { }

    ngOnInit() {
        this.width = window.innerWidth;
    }

    @HostListener('window:resize', ['$event'])
    onResize(event: any) {
        this.width = window.innerWidth;
    }
}