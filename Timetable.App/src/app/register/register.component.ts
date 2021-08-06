import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthenticationService } from '@app/_services';
import { first } from 'rxjs/operators';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html'
})
export class RegisterComponent implements OnInit {
  registerForm!: FormGroup;
  loading = false;
  submitted = false;
  error = '';

  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private authenticationService: AuthenticationService) {}

  ngOnInit(): void {
    this.registerForm = this.formBuilder.group({
      name: ['', Validators.required],
      cycles: ['', Validators.required]
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
    this.authenticationService.register(this.f.name.value, this.f.cycles.value)
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
