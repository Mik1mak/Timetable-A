import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserIdentity } from '@app/_models';
import { AuthenticationService } from '@app/_services';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html'
})
export class NavbarComponent implements OnInit {
  currentUser?: UserIdentity;

  constructor(
      private router: Router,
      private authenticationService: AuthenticationService) 
  {
      this.authenticationService.currentUser.subscribe(x => {
          this.currentUser = x;
      });
  }

  logout() {
      this.authenticationService.logout();
      this.router.navigate(['/login']);
  }

  ngOnInit(): void {
  }

}
