export class UserIdentity {
    id?: number;
    name?: string;
    editKey?: string;
    readKey?: string;
    createDate?: Date;
    token?: string;

    public get editMode() 
    {
        if(this.editKey)
            return true;
        return false;
    }
}