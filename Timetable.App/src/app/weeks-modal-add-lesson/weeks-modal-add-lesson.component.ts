import { Component, EventEmitter, Input, OnChanges, OnInit, Output, SimpleChanges } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { TotalTime } from '@app/_helpers';
import { Group } from '@app/_models';
import { GroupsService, LessonsService, UserService } from '@app/_services';
import Modal from 'bootstrap/js/dist/modal';

@Component({
  selector: 'app-weeks-modal-add-lesson',
  templateUrl: './weeks-modal-add-lesson.component.html',
  styleUrls: ['./weeks-modal-add-lesson.component.css']
})
export class WeeksModalAddLessonComponent implements OnInit, OnChanges {
  @Input() defaultWeekNumber?: number;
  @Output() saveEvent = new EventEmitter<any>();

  readonly daysOfweek = TotalTime.dayOfWeeksArray;
  groups!: Group[];
  weeksNumbers: number[] = [];
  addLessonForm!: FormGroup;

  constructor(
    private formBuilder: FormBuilder,
    private groupService: GroupsService,
    private lessonsService: LessonsService,
    private userService: UserService) { }

  ngOnInit(): void {

    this.addLessonForm = this.formBuilder.group({
      group: ['', [Validators.required]],
      week: ['', [Validators.min(1), Validators.max(10)]],
      day: ['', [Validators.required]],
      start: ['', [Validators.required]],
      duration: ['', [Validators.required, Validators.min(15), Validators.max(1440)]],
      name: ['', [Validators.required, Validators.minLength(1), Validators.maxLength(32)]],
      classroom: ['', [Validators.maxLength(32)]],
      link: ['', [Validators.maxLength(512)]]
    });


    this.groupService.groups.subscribe({next: groups => this.groups = groups});
    this.userService.currentUser.subscribe({next: user => this.weeksNumbers = Array.from({length: user.cycles!}, (_, i) => i + 1)})
  }

  ngOnChanges(changes: SimpleChanges): void {
    if(this.addLessonForm)
      this.addLessonForm.controls.week.setValue(this.defaultWeekNumber);
  }

  add() {
    if(this.addLessonForm.invalid)
      return;

    const day: number = this.addLessonForm.controls.day.value;
    const week: number = this.addLessonForm.controls.week.value;
    let startTime = this.addLessonForm.controls.start.value;

    let startDate: Date = new Date(Date.UTC(1, 0, (week-1) * 7 + day, 0, 0, 0, 0));
    startDate.setUTCFullYear(1);

    let newLesson = {
      groupId: this.addLessonForm.controls.group.value,
      duration: this.addLessonForm.controls.duration.value,
      start: `${startDate.toISOString().split('T')[0]}T${startTime}:00.000`,
      name: this.addLessonForm.controls.name.value,
      classroom: this.addLessonForm.controls.classroom.value,
      link: this.addLessonForm.controls.link.value,
    };

    this.lessonsService.add(newLesson).subscribe({next: (lesson: any) => {
      lesson.start = new Date(lesson.start); 
      this.saveEvent.emit({lesson: lesson, week: week});
    }})

    this.close();    
  }   

  close() {
    let modalElement = <Element>document.querySelector('#weeks-modal-add-lesson');
    let modal = Modal.getInstance(modalElement);
    modal?.hide();
  }
}
