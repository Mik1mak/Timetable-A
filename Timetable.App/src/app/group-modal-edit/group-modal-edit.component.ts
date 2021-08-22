import { Component, Input, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Group } from '@app/_models';
import { GroupsService } from '@app/_services';
import Modal from 'bootstrap/js/dist/modal';

@Component({
  selector: 'app-group-modal-edit',
  templateUrl: './group-modal-edit.component.html'
})
export class GroupModalEditComponent implements OnInit, OnChanges {

  @Input() groupToEdit?: Group;

  editGroupForm!: FormGroup;
  submitted = false;
  
  constructor(
    private formBuilder: FormBuilder,
    private groupsService: GroupsService) { }

    get f() {
      return this.editGroupForm.controls;
    }

  ngOnInit(): void {
      this.editGroupForm = this.formBuilder.group({
        name: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(64)]],
        color: ['', []]
    });
  }

  ngOnChanges(changes: SimpleChanges): void {
    if(this.editGroupForm) {
      this.f.name.setValue(this.groupToEdit?.name);
      this.f.color.setValue(this.groupToEdit?.hexColor);
    }
  }

  edit() {
    this.submitted = true;

    if(this.editGroupForm.invalid)
      return;

    let name = this.f.name.value;
    let color = this.f.color.value;

    this.groupsService.update(this.groupToEdit!.id, name, color);
      
    this.close();   
  }

  close() {
    this.submitted = false;
    let modalElement = <Element>document.getElementById('group-modal-edit');
    let modal = Modal.getInstance(modalElement);
    modal?.hide();
  }
}
