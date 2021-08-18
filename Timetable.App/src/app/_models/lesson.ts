export class Lesson {
    id: string;
    name: string;
    start: Date;
    duration: number;
    classroom: string;
    link: string;
    groupId: number;

    hexColor?: string;
    isVisible: boolean = true;

    constructor(data: any)
    {
        this.id = data.id;
        this.name = data.name;
        this.start = new Date(data.start);
        this.duration = data.duration;
        this.classroom = data.classroom;
        this.link = data.link;
        this.groupId = data.groupId;
    }
}