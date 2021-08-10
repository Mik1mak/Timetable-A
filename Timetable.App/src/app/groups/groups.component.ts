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
  loading = false;
  groupToEdit?: Group;
  groups!: Group[];

  constructor(private groupsService: GroupsService, private toaster: ToasterService) { }

  ngOnInit() {
    this.loading = true;
    this.groupsService.getAll().subscribe({
      next: groups => {
        this.loading = false;
        this.groups = groups;
      },
      error: err => {
        this.loading = false;
        this.toaster.add(err);
      }
    });
  }

  public get groupsLength() {
    if(this.groups)
      return this.groups.length;
    return 0;
  }

  showAddModal() {
    this.groupToEdit = undefined;
    this.showModal();
  }

  showEditModal(group: Group) {
    this.groupToEdit = group;
    this.showModal();
  }

  private showModal() {
    let modalElement = <Element>document.getElementById('group-modal-add');
    let modal = new Modal(modalElement, {});
    modal.show();
  }
}