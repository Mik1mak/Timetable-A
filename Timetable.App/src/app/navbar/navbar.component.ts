import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserIdentity } from '@app/_models';
import { ModalService, UserService } from '@app/_services';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html'
})
export class NavbarComponent implements OnInit {
  currentUser?: UserIdentity;

  constructor(
      private router: Router,
      private authenticationService: UserService,
      private modalService: ModalService) 
  {
      this.authenticationService.currentUser.subscribe(x => {
          this.currentUser = x;
      });
  }

  logout() {
      this.modalService.openConfirmModal('Confirm Closing', 'Make sure you save link to this timetable from shere tab.').subscribe({next: confirmation => {
        if(confirmation) {
          this.authenticationService.logout();
          this.router.navigate(['/create']);
        }
      }});
  }

  ngOnInit(): void {
  }

}
