import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Group } from '@app/_models';
import { UserService } from '@app/_services';
import { GroupsService } from '@app/_services/groups.service';
import { ToasterService } from '@app/_services/toaster.service';
import Modal from 'bootstrap/js/dist/modal';

@Component({
  selector: 'app-groups',
  templateUrl: './groups.component.html',
  styleUrls: ['./groups.component.css']
})
export class GroupsComponent implements OnInit {
  groupToEdit?: Group;
  groups!: Group[];
  groupsService: GroupsService;
  editMode: boolean;

  constructor(
    private toaster: ToasterService,
    groupsService: GroupsService, 
    auth: UserService) {

    this.editMode = auth.currentUserEditMode;

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
    });

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
    let addModal = new Modal(modalElement, {});
    addModal.show();
  }

  showEditModal(group: Group) {
    this.groupToEdit = group;
    let modalElement = <Element>document.getElementById('group-modal-edit');
    let editModal = new Modal(modalElement, {});
    editModal.show();
  }
}