import { Component, Input, OnInit } from '@angular/core';
import { Day } from '@app/_models';

@Component({
  selector: 'app-day',
  templateUrl: './day.component.html',
  styleUrls: ['./day.component.css']
})
export class DayComponent implements OnInit {

  @Input() day?: Day;

  private readonly fullEn = [
    'Monday',
    'Tuesday',
    'Wednesday',
    'Thursday',
    'Friday',
    'Saturday',
    'Sunday'
];

get dayOfWeekFull() {
    if(this.day)
        return this.fullEn[this.day.dayOfWeek!-1];
    return ''
}

  constructor() { }

  ngOnInit(): void {
  }

}
