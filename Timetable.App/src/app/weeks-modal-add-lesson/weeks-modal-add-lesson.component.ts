import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { lessonCollisionValidator, lessonEndValidator, TotalTime } from '@app/_helpers';
import { Group } from '@app/_models';
import { GroupsService, ModalService, LessonsService, UserService, ToasterService } from '@app/_services';
import Modal from 'bootstrap/js/dist/modal';

@Component({
  selector: 'app-weeks-modal-add-lesson',
  templateUrl: './weeks-modal-add-lesson.component.html',
  styleUrls: ['./weeks-modal-add-lesson.component.css']
})
export class WeeksModalAddLessonComponent implements OnInit {
  @Output() saveEvent = new EventEmitter<any>();

  readonly daysOfweek = TotalTime.dayOfWeeksArray;
  groups!: Group[];
  weeksNumbers: number[] = [];
  addLessonForm!: FormGroup;
  submitted = false;
  loading = false;

  constructor(
    private formBuilder: FormBuilder,
    private groupService: GroupsService,
    private lessonsService: LessonsService,
    private userService: UserService,
    private lessonsModalService: ModalService,
    private toaster: ToasterService) { }

  ngOnInit(): void {

    this.addLessonForm = this.formBuilder.group({
      group: ['', [Validators.required]],
      week: ['', [Validators.required, Validators.min(1), Validators.max(10)]],
      day: ['', [Validators.required]],
      start: ['', [Validators.required]],
      duration: ['', [Validators.required, Validators.min(15), Validators.max(1440)]],
      name: ['', [Validators.required, Validators.minLength(1), Validators.maxLength(32)]],
      classroom: ['', [Validators.maxLength(32)]],
      link: ['', [Validators.maxLength(512)]],
    }, 
    {
      updateOn: 'blur',
      validators: lessonEndValidator(),
      asyncValidators: lessonCollisionValidator(this.lessonsService, this.groupService),
    });

    this.groupService.groups.subscribe({next: groups => this.groups = groups});
    this.userService.currentUser.subscribe({next: user => this.weeksNumbers = Array.from({length: user.cycles!}, (_, i) => i + 1)});

    this.lessonsModalService.defaultLessonValues.subscribe({next: defaultValues => {

      for(const [key, value] of Object.entries(defaultValues)) {
        if(value)
          this.addLessonForm.controls[key].setValue(value);
      }

      this.addLessonForm.updateValueAndValidity();
    }});
  }

  get f() {
    return this.addLessonForm.controls;
  }

  add() {
    this.submitted = true;

    if(this.addLessonForm.invalid)
      return;

    this.loading = true;

    const day: number = this.f.day.value;
    const week: number = this.f.week.value;
    let startTime = this.f.start.value;

    let newLesson = {
      groupId: this.f.group.value,
      duration: this.f.value,
      start: TotalTime.createDateTimeIso(week, day, startTime),
      name: this.f.name.value,
      classroom: this.f.classroom.value,
      link: this.f.link.value,
    };

    this.lessonsService.add(newLesson).subscribe({next: (lesson: any) => {
      lesson.start = new Date(lesson.start); 
      this.saveEvent.emit({lesson: lesson, week: week});
      this.close();
    },
    error: err => {
      this.toaster.add(err);
      this.close();
    }})
  }

  reset() {
    this.addLessonForm.reset();
    this.close();
  }

  close() {
    this.loading = false;
    this.submitted = false;
    let modalElement = <Element>document.getElementById('weeks-modal-add-lesson');
    let modal = Modal.getInstance(modalElement);
    modal!.hide();
  }
}
