import { Lesson } from "./lesson";

export class Group {
    id: string;
    name: string;
    hexColor: string;
    lessons: Array<Lesson>

    constructor(data: any)
    {
        this.id = data.id;
        this.name = data.name;
        this.hexColor = data.hexColor;
        
        if(Array.isArray(data.lessons) && data.lessons.length)
            this.lessons = data.lessons.Map((lesson: any) => new Lesson(lesson));
        else
            this.lessons = [];
    }
}