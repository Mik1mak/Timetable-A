import { Component, OnInit } from '@angular/core';
import { TotalTime } from '@app/_helpers';
import { Day, Lesson, Week } from '@app/_models';
import { GroupsService, TimetableService, ToasterService, UserService } from '@app/_services';

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
    private groupService: GroupsService,
    private userService: UserService,
    private toaster: ToasterService) { }

  ngOnInit(): void {
    this.timetableService.get().subscribe({
      next: t => {
        this.loading = false;
        this.weeks = t.weeks;

        this.userService.currentUser.subscribe({
          next: newUser => {
            this.refillWeeks().then(() =>{
              this.refillDays();
            });          
          }
        });
      },
      error: err => {   
        this.loading = false;
        this.toaster.add(err);
      }
    });
  }

  private async refillWeeks() {
    for (let i = 1; i <= this.userService.currentUserValue.cycles!; i++) {
      const week = this.weeks![i-1];
      
      if(week)
        if(week.number == i)
          continue;

      const newWeek = await this.timetableService.getWeek(i).toPromise();
      this.weeks!.splice(i-1, 0, newWeek);
    }

    this.weeks!.length = this.userService.currentUserValue.cycles!;
  }

  private refillDays() {
    for (const week of this.weeks!) {
      for (let i = 1; i <= 7; i++) {
        const element = week.days![i-1];
        if(element?.dayOfWeek != i) 
          this.addEmptyDay(week, i);
      } 
    }
  }

  private addEmptyDay(week: Week, dayNumber: number): Day {
    const newDay = new Day();
    newDay.dayOfWeek = dayNumber;
    newDay.lessons = [];
    week.days!.splice(dayNumber-1, 0, newDay);
    return newDay;
  }

  addLesson(lessonAndWeekNum: any) {
    const lesson: Lesson = lessonAndWeekNum.lesson;
    const weekIndex = lessonAndWeekNum.week - 1;

    const week = this.weeks![weekIndex];
    const dayIndex = (lesson.start.getDay()+6) % 7;
    const day = week.days![dayIndex];

    if(day) {
      day.lessons.push(lesson);
      if(this.groupService.selectedValue.includes(lesson.groupId))
        day.isVisible = true;
    }

    this.refreshWeekExtremes(week);
    this.groupService.refreshSelectable();
  }

  private refreshWeekExtremes(week: Week) {
    week.days!.forEach(day => day.lessons.forEach(lesson => {
      if(lesson.duration < week.minDuration!)
      week.minDuration = lesson.duration;

      let lessonStart = new Date(lesson.start);

      if(TotalTime.minutesInDay(lessonStart) < TotalTime.minutesInDay(week.minStart!))
        week.minStart = lessonStart;

      if(TotalTime.minutesInDay(lessonStart)+lesson.duration > TotalTime.minutesInDay(week.maxStop!))
      {
        lessonStart.setMinutes(lessonStart.getMinutes() + lesson.duration);
        week.maxStop = lessonStart;
      }
    }))
  }

}
