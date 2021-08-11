import { Component, Input, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Group } from '@app/_models';
import { GroupsService } from '@app/_services/groups.service';
import Modal from 'bootstrap/js/dist/modal';

@Component({
  selector: 'app-group-modal-edit',
  templateUrl: './group-modal-edit.component.html'
})
export class GroupModalEditComponent implements OnInit, OnChanges {

  @Input() groupToEdit?: Group;

  editGroupForm!: FormGroup;
  
  constructor(
    private formBuilder: FormBuilder,
    private groupsService: GroupsService) { }

  ngOnInit(): void {
      this.editGroupForm = this.formBuilder.group({
        name: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(64)]],
        color: ['', []]
    });
  }

  ngOnChanges(changes: SimpleChanges): void {
    if(this.editGroupForm) {
      this.editGroupForm.controls.name.setValue(this.groupToEdit?.name);
      this.editGroupForm.controls.color.setValue(this.groupToEdit?.hexColor);
    }
  }

  edit() {
    if(this.editGroupForm.invalid)
      return;

    let name = this.editGroupForm.controls.name.value;
    let color = this.editGroupForm.controls.color.value;

    this.groupsService.update(this.groupToEdit!.id, name, color);
      
    this.close();   
  }

  close() {
    let modalElement = <Element>document.querySelector('#group-modal-edit');
    let modal = Modal.getInstance(modalElement);
    modal?.hide();
  }
}
