import { Component, OnInit } from '@angular/core';
import { Group } from '@app/_models';
import { GroupsService } from '@app/_services/groups.service';
import { ToasterService } from '@app/_services/toaster.service';
import Modal from 'bootstrap/js/dist/modal';

@Component({
  selector: 'app-groups',
  templateUrl: './groups.component.html'
})
export class GroupsComponent implements OnInit {
  groupToEdit?: Group;
  groups!: Group[];
  groupsService: GroupsService;

  constructor(groupsService: GroupsService, private toaster: ToasterService) { 
    this.groupsService = groupsService;
  }

  ngOnInit() {
    this.groupsService.groups.subscribe({
      next: (groups: Group[]) => {
        this.groups = groups;
      },
      error: err => {
        this.toaster.add(err);
      }
    })

    this.groupsService.refreshGroups();
  }

  public get groupsLength() {
    if(this.groups)
      return this.groups.length;
    return 0;
  }

  showAddModal() {
    if(this.groupsLength >= 10)
    {
      this.toaster.add('max count of groups is 10');
      return;
    }

    let modalElement = <Element>document.getElementById('group-modal-add');
    let modal = new Modal(modalElement, {});
    modal.show();
  }

  showEditModal(group: Group) {
    this.groupToEdit = group;
    let modalElement = <Element>document.getElementById('group-modal-edit');
    let modal = new Modal(modalElement, {});
    modal.show();
  }
}