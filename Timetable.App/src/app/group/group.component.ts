import { Component, OnInit, Input, Output, EventEmitter, OnChanges, SimpleChanges } from '@angular/core';
import { Group } from '@app/_models';
import { UserService } from '@app/_services';
import { GroupsService } from '@app/_services/groups.service';
import { ToasterService } from '@app/_services/toaster.service';

@Component({
  selector: 'app-group',
  templateUrl: './group.component.html',
  styleUrls: ['./group.component.css']
})
export class GroupComponent implements OnInit{
  @Output() showEditModalEvent = new EventEmitter<Group>();
  @Input() group!: Group;

  editMode: boolean;

  constructor(private groupService: GroupsService, private toaster: ToasterService, auth: UserService) {
    this.editMode = auth.currentUserEditMode;
  }

  ngOnInit() {}

  get disabled() {
    return !this.groupService.isSelectable(this.group);
  }

  get selected() {
    return this.groupService.selectedValue.includes(this.group.id);
  }

  toggle() {
    if(!this.disabled)
    {
      if(this.selected) 
        this.groupService.unselect(this.group);
      else
        this.groupService.select(this.group);
    }
  }

  showEditModalOutput() {
    this.showEditModalEvent.emit(this.group);
  }

  remove() {
    this.groupService.delete(this.group);
  }
}
