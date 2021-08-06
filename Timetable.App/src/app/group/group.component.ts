import { Component, OnInit, Input } from '@angular/core';
import { Group } from '@app/_models';
import { GroupsService } from '@app/_services/groups.service';

@Component({
  selector: 'app-group',
  templateUrl: './group.component.html'
})
export class GroupComponent implements OnInit {
  @Input() group!: Group;

  selectable = true;
  selected = false;

  constructor(private groupService: GroupsService) {
    this.groupService.selected.subscribe(async () => {
      this.selectable = await this.groupService.isSelectable(this.group.id);
    });
  }

  ngOnInit() {}

}
