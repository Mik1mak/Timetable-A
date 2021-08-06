import { Component, OnInit } from '@angular/core';
import { Group } from '@app/_models';
import { GroupsService } from '@app/_services/groups.service';
import { ToasterService } from '@app/_services/toaster.service';

@Component({
  selector: 'app-groups',
  templateUrl: './groups.component.html'
})
export class GroupsComponent implements OnInit {
  loading = false;
  groups!: Group[];

  constructor(private groupsService: GroupsService, private toaster: ToasterService) { }

  ngOnInit() {
    this.loading = true;
    this.groupsService.getAll().subscribe({
      next: groups => {
        this.loading = false;
        this.groups = groups;
      },
      error: err => this.toaster.add(err)
    });
  }
}
