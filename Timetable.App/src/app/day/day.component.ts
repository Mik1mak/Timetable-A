import { Component, Input, OnInit } from '@angular/core';
import { Day, UserIdentity } from '@app/_models';
import { GroupsService, UserService } from '@app/_services';

@Component({
  selector: 'app-day',
  templateUrl: './day.component.html',
  styleUrls: ['./day.component.css']
})
export class DayComponent implements OnInit {

  @Input() day?: Day;
  @Input() pxPerMinute: number = 1.1;
  @Input() minStartOfWeek: number = 480;
  @Input() maxStopOfWeek: number = 960;

  hasVisibleLessons: boolean = true;

  private readonly fullEn = [
    'Monday',
    'Tuesday',
    'Wednesday',
    'Thursday',
    'Friday',
    'Saturday',
    'Sunday'
  ];

  get dayOfWeekFull() {
      if(this.day)
          return this.fullEn[this.day.dayOfWeek!-1];
      return ''
  }

  get height() {
    return `${((this.maxStopOfWeek - this.minStartOfWeek) * this.pxPerMinute) + 40}px`;
  }

  constructor(private groupService: GroupsService, private userService: UserService) { }

  ngOnInit(): void { 
    this.groupService.selected.subscribe({next: newSelected => 
      this.refreshDayVisibility(newSelected, this.userService.currentUserValue)});

    this.userService.currentUser.subscribe(newUser => 
      this.refreshDayVisibility(this.groupService.selectedValue, newUser));
  }

  private refreshDayVisibility(currentSelected: number[], currentUser: UserIdentity) {
    if(currentUser.showWeekend) {
      this.day!.isVisible = true;
      return;
    }
    
    let hasVisible = false;

    for (const lesson of this.day!.lessons)
      if(currentSelected.includes(lesson.groupId))
        hasVisible = true;
    
    this.day!.isVisible = hasVisible;
  }
}
