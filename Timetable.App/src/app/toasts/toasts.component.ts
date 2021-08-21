import { Component, OnInit } from '@angular/core';
import { ToasterService } from '@app/_services/toaster.service';

@Component({
  selector: 'app-toasts',
  templateUrl: './toasts.component.html',
  styleUrls: ['./toasts.component.css']
})
export class ToastsComponent implements OnInit {
  toaster: ToasterService;

  constructor(toaster: ToasterService) {
    this.toaster = toaster;
  }

  ngOnInit(): void 
  {
  }
}
