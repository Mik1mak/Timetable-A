import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { TotalTime } from '@app/_helpers';
import { Lesson } from '@app/_models';
import { GroupsService, ModalService, UserService } from '@app/_services';

@Component({
  selector: 'app-lesson',
  templateUrl: './lesson.component.html',
  styleUrls: ['./lesson.component.css']
})
export class LessonComponent implements OnInit {

  @Input() lesson?: Lesson;
  @Input() pxPerMin: number = 1.1;
  @Input() minStart: number = 480;
  @Output() removeEvent = new EventEmitter<number>()

  editMode = false;

  constructor(private groupsService: GroupsService, 
    userService: UserService, 
    private lessonsModalService: ModalService) {
    this.editMode = userService.currentUserEditMode;
  }

  get endDate(): Date {
    const endTime = new Date(this.lesson!.start.getTime());
    endTime.setMinutes(endTime.getMinutes() + this.lesson!.duration);
    return endTime;
  }

  get height() {
    return `${this.lesson!.duration * this.pxPerMin}px`;
  }

  get offset() {
    return `${(TotalTime.minutesInDay(this.lesson!.start) - this.minStart) * this.pxPerMin}px`;
  }

  ngOnInit(): void {
    this.groupsService.selected.subscribe({
      next: newSelected => {
        this.lesson!.isVisible = newSelected.includes(this.lesson!.groupId);
      }
    });

    this.groupsService.groups.subscribe({
      next: newGroups => {
        for (const group of newGroups) {
          if(group.id == this.lesson!.groupId) {
            if(group.hexColor != this.lesson!.hexColor)
              this.lesson!.hexColor = group.hexColor;
          }
        }
      }
    });
  }

  remove() {
    this.removeEvent.emit(this.lesson!.id);
  }

  copy() {
    this.lessonsModalService.openAddLessonModal(1, (this.lesson!.start.getDay() + 6) % 7 + 1, this.lesson);
  }
}
