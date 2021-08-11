import { Component, Input, OnInit, EventEmitter, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Group } from '@app/_models';
import { GroupsService } from '@app/_services/groups.service';
import { ToasterService } from '@app/_services/toaster.service';
import Modal from 'bootstrap/js/dist/modal';

@Component({
  selector: 'app-group-modal-add',
  templateUrl: './group-modal-add.component.html'
})
export class GroupModalAddComponent implements OnInit {
  addGroupForm!: FormGroup;

  constructor(
    private formBuilder: FormBuilder,
    private groupsService: GroupsService) { }

  ngOnInit(): void {
    this.addGroupForm = this.formBuilder.group({
      name: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(64)]],
      color: ['', []]
    });
    this.addGroupForm.controls.color.setValue('#ffffff');
  }

  add() {
    if(this.addGroupForm.invalid)
      return;

    let name = this.addGroupForm.controls.name.value;
    let color = this.addGroupForm.controls.color.value;

    this.groupsService.create(name, color);

    this.addGroupForm.controls.name.setValue('');
    this.addGroupForm.controls.color.setValue('#ffffff');
      
    this.close();    
  }

  close() {
    let modalElement = <Element>document.querySelector('#group-modal-add');
    let modal = Modal.getInstance(modalElement);
    modal?.hide();
  }
}
