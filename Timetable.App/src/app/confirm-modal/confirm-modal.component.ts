import { Component, OnInit } from '@angular/core';
import { ModalService } from '@app/_services';
import Modal from 'bootstrap/js/dist/modal';

@Component({
  selector: 'app-confirm-modal',
  templateUrl: './confirm-modal.component.html',
  styleUrls: ['./confirm-modal.component.css']
})
export class ConfirmModalComponent implements OnInit {

  constructor(private modalService: ModalService) { }

  title?: string;
  content?: string;

  ngOnInit(): void {
    this.modalService.confirmationContent.subscribe({next: val => {
      this.title = val.title;
      this.content = val.content;
    }})
  }

  confirm() {
    this.modalService.confirm(true);
    this.close();
  }

  cancel() {
    this.modalService.confirm(false);
    this.close();
  }

  private close() {
    let modalElement = <Element>document.getElementById('confirm-modal');
    let modal = Modal.getInstance(modalElement);
    modal?.hide();
  }
}
