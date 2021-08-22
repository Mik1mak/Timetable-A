import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { GroupsService } from '@app/_services';
import Modal from 'bootstrap/js/dist/modal';

@Component({
  selector: 'app-group-modal-add',
  templateUrl: './group-modal-add.component.html'
})
export class GroupModalAddComponent implements OnInit {
  addGroupForm!: FormGroup;
  submitted = false;

  constructor(
    private formBuilder: FormBuilder,
    private groupsService: GroupsService) { }

    get f() {
      return this.addGroupForm.controls;
    }

  ngOnInit(): void {
    this.addGroupForm = this.formBuilder.group({
      name: ['', [Validators.required, Validators.minLength(1), Validators.maxLength(32)]],
      color: ['', []]
    });
    this.f.color.setValue('#ffffff');
  }

  add() {
    this.submitted = true;

    if(this.addGroupForm.invalid)
      return;

    let name = this.f.name.value;
    let color = this.f.color.value;

    this.groupsService.create(name, color);

    this.f.name.setValue('');
    this.f.color.setValue('#ffffff');
      
    this.close();    
  }

  close() {
    this.submitted = false;
    let modalElement = <Element>document.getElementById('group-modal-add');
    let modal = Modal.getInstance(modalElement);
    modal?.hide();
  }
}
