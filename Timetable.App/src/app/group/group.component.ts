import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { Group } from '@app/_models';
import { UserService } from '@app/_services';
import { GroupsService } from '@app/_services/groups.service';
import { ToasterService } from '@app/_services/toaster.service';

@Component({
  selector: 'app-group',
  templateUrl: './group.component.html',
  styleUrls: ['./group.component.css']
})
export class GroupComponent implements OnInit {
  @Output() showEditModalEvent = new EventEmitter<Group>();
  @Input() group!: Group;

  disabled = true;
  selected = false;
  editMode: boolean;

  constructor(private groupService: GroupsService, private toaster: ToasterService, auth: UserService) {
    this.editMode = auth.currentUserEditMode;
  }

  ngOnInit() 
  {
    this.groupService.selected.subscribe({
      next: () => this.refresh(),
      error: err => this.toaster.add(err)
    });
  }

  refresh() {
    this.disabled = !this.groupService.isSelectable(this.group);
    this.selected = this.groupService.selectedValue.includes(this.group.id);
  }

  toggle() {
    if(!this.disabled)
    {
      if(this.selected) {
        this.groupService.unselect(this.group);
        this.selected = false;
      }
      else
      {
        this.groupService.select(this.group);
        this.selected = true;
      }
    }
  }

  showEditModalOutput() {
    this.showEditModalEvent.emit(this.group);
  }

  remove() {
    this.groupService.delete(this.group);
  }
}
