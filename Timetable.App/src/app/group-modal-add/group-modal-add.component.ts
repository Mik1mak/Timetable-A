import { Component, Input, OnInit } from '@angular/core';
import { Group } from '@app/_models';
import * as bootstrap from 'bootstrap';

@Component({
  selector: 'app-group-modal-add',
  templateUrl: './group-modal-add.component.html',
  styleUrls: ['./group-modal-add.component.css']
})
export class GroupModalAddComponent implements OnInit {

  @Input() groupToEdit?: Group;

  constructor() { }

  ngOnInit(): void {}

}
