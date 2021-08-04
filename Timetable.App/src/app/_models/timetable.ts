import { Group } from "./group";

export class Timetable {
    name: string;
    cycles: number;
    groups: Array<Group>;

    constructor(data: any)
    {
        this.name = data.name;
        this.cycles = data.cycles;
        if(data.groups)
            this.groups = data.groups.map((group : any)=> new Group(group));
        else
            this.groups = [];
    }
}