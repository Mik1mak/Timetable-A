import { Component } from '@angular/core';
import { Router } from '@angular/router';

import { AuthenticationService } from './_services';
import { UserIdentity } from './_models';
import { environment } from '@environments/environment';

@Component({ selector: 'app', templateUrl: 'app.component.html' })
export class AppComponent {
    currentUser?: UserIdentity;

    constructor(
        private router: Router,
        private authenticationService: AuthenticationService) 
    {
        this.authenticationService.currentUser.subscribe(x => this.currentUser = x);
    }

    logout() {
        this.authenticationService.logout();
        this.router.navigate(['/login']);
    }
}