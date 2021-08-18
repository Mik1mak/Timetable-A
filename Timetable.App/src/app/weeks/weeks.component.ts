import { Component, OnInit } from '@angular/core';
import { Day, Week } from '@app/_models';
import { TimetableService, ToasterService, UserService } from '@app/_services';

@Component({
  selector: 'app-weeks',
  templateUrl: './weeks.component.html',
  styleUrls: ['./weeks.component.css']
})
export class WeeksComponent implements OnInit {

  weeks?: Week[];
  loading = true;

  constructor(
    private timetableService: TimetableService,
    private userService: UserService,
    private toaster: ToasterService) { }

  ngOnInit(): void {
    this.timetableService.get().subscribe({
      next: t => {
        this.loading = false;
        this.weeks = t.weeks;

        this.userService.currentUser.subscribe({
          next: newUser => {
            this.refillWeeks();
            if(newUser.showWeekend)
              this.refillDays();
            else
              this.removeEmptyDays();
          }
        });
      },
      error: err => {   
        this.loading = false;
        this.toaster.add(err);
      }
    });

    
  }

  refillWeeks() {
    for (let i = 1; i <= this.userService.currentUserValue.cycles!; i++) {
      const week = this.weeks![i-1];
      
      if(week)
        if(week.number == i)
          continue;

      const newWeek = new Week();
      newWeek.number = i;
      newWeek.days = [];
      this.weeks!.splice(i-1, 0, newWeek);
    }

    this.weeks!.length = this.userService.currentUserValue.cycles!;
  }

  refillDays() {
    for (const week of this.weeks!) {
      for (let i = 1; i <= 7; i++) {
        const element = week.days![i-1];
        if(element?.dayOfWeek != i)
        {
          const newDay = new Day();
          newDay.dayOfWeek = i;
          newDay.lessons = [];
          week.days!.splice(i-1, 0, newDay);
        }
      } 
    }
  }

  removeEmptyDays() {
    for (const week of this.weeks!) {
      var i = 0;
      while (i < week.days!.length) {
        if (week.days![i].lessons.length == 0) {
          week.days!.splice(i, 1);
        } else {
          ++i;
        }
      }
    }
  }
}
