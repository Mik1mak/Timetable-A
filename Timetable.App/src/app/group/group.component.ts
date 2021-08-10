import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { Group } from '@app/_models';
import { GroupsService } from '@app/_services/groups.service';
import { ToasterService } from '@app/_services/toaster.service';

@Component({
  selector: 'app-group',
  templateUrl: './group.component.html'
})
export class GroupComponent implements OnInit {
  @Input() group!: Group;
  @Output() showEditModalEvent = new EventEmitter<Group>();

  disabled = true;
  selected = false;

  constructor(private groupService: GroupsService, private toaster: ToasterService) {}

  ngOnInit() 
  {
    this.groupService.selected.subscribe({
      next: async () => await this.isDisabled(),
      error: err => this.toaster.add(err)
    });
    this.isDisabled();
  }

  async isDisabled() {
    this.disabled = !(await this.groupService.isSelectable(this.group.id));
  }

  toggle() {
    if(!this.disabled)
    {
      if(this.selected) {
        this.groupService.unselect(this.group.id);
        this.selected = false;
      }
      else
      {
        this.groupService.select(this.group.id);
        this.selected = true;
      }
    }
  }

  showEditModalOutput() {
    this.showEditModalEvent.emit(this.group);
  }

}
