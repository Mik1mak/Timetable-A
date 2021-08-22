import { Day } from ".";

export class Week {
    number?: number;
    minDuration?: number;
    minStart?: Date;
    maxStop?: Date;
    days?: Array<Day> = [];
}