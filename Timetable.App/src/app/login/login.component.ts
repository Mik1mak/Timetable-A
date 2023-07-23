import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { UntypedFormBuilder, UntypedFormGroup, Validators } from '@angular/forms';
import { first } from 'rxjs/operators';

import { UserService } from '@app/_services';

@Component({ templateUrl: 'login.component.html' })
export class LoginComponent implements OnInit {
    loginForm!: UntypedFormGroup;
    loading = false;
    submitted = false;
    error = '';

    constructor(
        private formBuilder: UntypedFormBuilder,
        private route: ActivatedRoute,
        private router: Router,
        private authenticationService: UserService) {}

    ngOnInit() {
        this.loginForm = this.formBuilder.group({
            username: ['', Validators.required],
            password: ['', [Validators.required, Validators.minLength(16), Validators.maxLength(16)]]
        });
        
        this.route.queryParams.subscribe(params => {
            const id = params['id'];
            const key = params['key'];
            if(id && key)
            {
                this.f.username.setValue(id); 
                this.f.password.setValue(key);
                this.onSubmit();
            }
        });
    }

    get f() { return this.loginForm.controls; }

    onSubmit() {
        this.submitted = true;

        if (this.loginForm.invalid) {
            return;
        }

        this.loading = true;
        this.authenticationService.login(this.f.username.value, this.f.password.value)
            .pipe(first())
            .subscribe({
                next: () => {
                    const returnUrl = this.route.snapshot.queryParams['returnUrl'];

                    if(
                        returnUrl) {
                        this.router.navigateByUrl(returnUrl);
                    }
                    else
                        this.router.navigate(['/']);
                },
                error: error => {
                    this.error = error;
                    this.loading = false;
                }
            });
    }
}