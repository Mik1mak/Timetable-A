import { Component, Input, OnInit } from '@angular/core';
import { Week } from '@app/_models';
import { UserService } from '@app/_services';

@Component({
  selector: 'app-week',
  templateUrl: './week.component.html',
  styleUrls: ['./week.component.css']
})
export class WeekComponent implements OnInit {

  @Input() week?: Week;
  editMode: boolean

  constructor(private userService: UserService) { 
    this.editMode = userService.currentUserEditMode;
  }

  ngOnInit(): void {
  }

}
