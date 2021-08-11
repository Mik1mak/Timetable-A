import { Lesson } from "./lesson";

export class Group {
    id: number;
    name: string;
    hexColor: string;
    lessons: Array<Lesson>;
    collidingGroups: number[];

    constructor(data: any)
    {
        this.id = data.id;
        this.name = data.name;
        this.hexColor = data.hexColor;
        
        if(Array.isArray(data.lessons) && data.lessons.length)
            this.lessons = data.lessons.Map((lesson: any) => new Lesson(lesson));
        else
            this.lessons = [];

        if(data.collidingGroups)
            this.collidingGroups = data.collidingGroups;
        else
            this.collidingGroups = [];
    }
}