import { Component, OnInit } from '@angular/core';
import { UntypedFormBuilder, UntypedFormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { UserService } from '@app/_services';
import { first } from 'rxjs/operators';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html'
})
export class RegisterComponent implements OnInit {
  registerForm!: UntypedFormGroup;
  loading = false;
  submitted = false;
  error = '';

  constructor(
    private formBuilder: UntypedFormBuilder,
    private router: Router,
    private authenticationService: UserService) {}

  ngOnInit(): void {
    this.registerForm = this.formBuilder.group({
      name: ['', [Validators.required, Validators.maxLength(32)]],
    });

    this.f.cycles.setValue(1);
  }

  get f() { return this.registerForm.controls; }

  onSubmit() {
    this.submitted = true;

    if (this.registerForm.invalid) {
        return;
    }

    this.loading = true;
    this.authenticationService.register(this.f.name.value, 1)
        .pipe(first())
        .subscribe({
            next: () => {
                this.router.navigate(['/']);
            },
            error: error => {
                this.error = error;
                this.loading = false;
            }
        });
}
}
