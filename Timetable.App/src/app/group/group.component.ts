import { Component, OnInit, Input, Output, EventEmitter} from '@angular/core';
import { Group } from '@app/_models';
import { ModalService, UserService, GroupsService } from '@app/_services';

@Component({
  selector: 'app-group',
  templateUrl: './group.component.html',
  styleUrls: ['./group.component.css']
})
export class GroupComponent implements OnInit{
  @Output() showEditModalEvent = new EventEmitter<Group>();
  @Input() group!: Group;

  editMode: boolean;

  constructor(
    private modalService: ModalService,
    private groupService: GroupsService, 
    auth: UserService) {
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
    this.modalService.openConfirmModal(`Confirm Group Deletion`, 'Delating this group will remove all related lessons.').subscribe({next: confirmation => {
      if(confirmation)
        this.groupService.delete(this.group);
    }})
  }
}
