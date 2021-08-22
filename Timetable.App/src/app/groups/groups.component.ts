import { Component, OnInit } from '@angular/core';
import { Group } from '@app/_models';
import { GroupsService, ToasterService, UserService } from '@app/_services';
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
  maxCountOfGroups?: number;

  constructor(
    private toaster: ToasterService,
    private userService: UserService,
    groupsService: GroupsService) {
    
    this.editMode = userService.currentUserEditMode;
    this.maxCountOfGroups = userService.currentUserValue.maxGroupsPerTimetable;
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

    this.groupsService.loadGroups();    
  }

  public get groupsLength() {
    if(this.groups)
      return this.groups.length;
    return 0;
  }

  showAddModal() {
    if(this.groupsLength >= 20)
    {
      this.toaster.add('Max count of groups is 10');
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