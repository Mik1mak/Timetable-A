import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserIdentity } from '@app/_models';
import { ModalService, UserService, ToasterService } from '@app/_services';
import { first } from 'rxjs/operators';

@Component({
  selector: 'app-settings',
  templateUrl: './settings.component.html',
  styleUrls: ['./settings.component.css']
})
export class SettingsComponent implements OnInit {

  name!: string;
  cycles!: number;
  displayEmptyDays!: boolean;
  maxCycles!: number;

  constructor(
    private router: Router,
    private userService: UserService,
    private toaster: ToasterService,
    private modalService: ModalService,
    private authenticationService: UserService) {
       
    this.setUser(userService.currentUserValue);
  }

  ngOnInit(): void {
    this.userService.currentUser.subscribe(newUser => this.setUser(newUser));
  }

  update() {
    this.userService.update(this.name, this.cycles, this.displayEmptyDays)
      .pipe(first())
      .subscribe({error: err => {
        this.toaster.add(err);
        this.setUser(this.userService.currentUserValue);
      }});
  }

  setUser(user: UserIdentity) {
    this.name = user.name!;
    this.cycles = user.cycles!;
    this.displayEmptyDays = user.displayEmptyDays!;
    this.maxCycles = user.maxCyclesPerTimetable!;
  }

  deleteTimetable() {
    this.modalService.openConfirmModal('Confirm Timetable Deletion', 'This action will permanently delete the timetable with all groups and lessons.')
      .subscribe({next: confirmation => {
        if(confirmation) {
          this.userService.deleteUser().subscribe({
            next: () => {
              this.authenticationService.logout();
              this.router.navigate(['/create']);
            },
            error: err => this.toaster.add(err)
          });
        }
      }});
  }
}