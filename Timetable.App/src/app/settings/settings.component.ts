import { Component, OnInit } from '@angular/core';
import { UserIdentity } from '@app/_models';
import { UserService } from '@app/_services';
import { ToasterService } from '@app/_services/toaster.service';
import { first } from 'rxjs/operators';

@Component({
  selector: 'app-settings',
  templateUrl: './settings.component.html',
  styleUrls: ['./settings.component.css']
})
export class SettingsComponent implements OnInit {

  name!: string;
  cycles!: number;
  showWeekend!: boolean;

  constructor(
    private userService: UserService,
    private toaster: ToasterService) { 
    this.setUser(userService.currentUserValue);
  }

  ngOnInit(): void {
    this.userService.currentUser.subscribe(newUser => this.setUser(newUser));
  }

  update() {
    this.userService.update(this.name, this.cycles, this.showWeekend)
      .pipe(first())
      .subscribe({error: err => {
        this.toaster.add(err);
        this.setUser(this.userService.currentUserValue);
      }});
  }

  setUser(user: UserIdentity) {
    this.name = user.name!;
    this.cycles = user.cycles!;
    this.showWeekend = user.showWeekend!;
  }
}