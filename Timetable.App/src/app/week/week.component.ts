import { Component, Input, OnInit } from '@angular/core';
import { TotalTime } from '@app/_helpers';
import { Day, Week } from '@app/_models';
import { LessonsModalService, UserService } from '@app/_services';

@Component({
  selector: 'app-week',
  templateUrl: './week.component.html',
  styleUrls: ['./week.component.css']
})
export class WeekComponent implements OnInit {
  @Input() week?: Week;
  editMode: boolean;

  get pxPerMin(): number {
    if(!this.week)
      return 1.1;

    return 66 / this.week.minDuration!;
  }
  
  get TotalMinutesOfminStart() {
    return TotalTime.minutesInDay(this.week!.minStart!);
  }

  get TotalMinutesOfmaxStop() {
    return TotalTime.minutesInDay(this.week!.maxStop!);
  }

  constructor(userService: UserService, private lessonsModalService: LessonsModalService) { 
    this.editMode = userService.currentUserEditMode;
  }

  ngOnInit(): void {
  }

  display(day: Day) {
    if(day.isVisible)
      return 'block';
    return 'none';
  }

  openAddModal() {
    this.lessonsModalService.openAddModal(this.week!.number!);
  }
}
