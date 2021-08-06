import { Component, OnInit } from '@angular/core';
import { UserIdentity } from '@app/_models';
import { AuthenticationService } from '@app/_services';

@Component({
  selector: 'app-shere',
  templateUrl: './shere.component.html'
})
export class ShereComponent implements OnInit {
  private currentUser?: UserIdentity;

  constructor(private authenticationService: AuthenticationService) {
    this.authenticationService.currentUser.subscribe(x => this.currentUser = x);
  }

  ngOnInit(): void {
  }

}
