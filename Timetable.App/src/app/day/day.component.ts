import { Component, Input, OnInit } from '@angular/core';
import { TotalTime } from '@app/_helpers';
import { Day, UserIdentity } from '@app/_models';
import { GroupsService, LessonsService, ToasterService, UserService } from '@app/_services';
import { first } from 'rxjs/operators';

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
  
  get dayOfWeekFull() {
      if(this.day)
          return TotalTime.dayOfWeeksArray[this.day.dayOfWeek!-1];
      return ''
  }

  get height() {
    return `${((this.maxStopOfWeek - this.minStartOfWeek) * this.pxPerMinute) + 40}px`;
  }

  constructor(
    private groupService: GroupsService, 
    private userService: UserService,
    private lessonService: LessonsService,
    private toaster: ToasterService) { }

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

  removeLesson(lessonId: number) {
    this.lessonService.delete(lessonId).pipe(first()).subscribe({
      next: () => {
        this.day!.lessons.forEach((val, index) => {
          if(val.id == lessonId) {
            this.day!.lessons.splice(index, 1);
            this.refreshDayVisibility(this.groupService.selectedValue, this.userService.currentUserValue);
            this.groupService.refreshSelectable();
          }
        })
      },
      error: err => this.toaster.add(err)
    });
  }

}
