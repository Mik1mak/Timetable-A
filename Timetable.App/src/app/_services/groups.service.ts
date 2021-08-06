import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { environment } from '@environments/environment';
import { Group } from '@app/_models';
import { BehaviorSubject, Observable } from 'rxjs';
import { ToasterService } from './toaster.service';

@Injectable({ providedIn: 'root' })
export class GroupsService {
    private readonly url = `${environment.apiUrl}/api/Group`;

    private selectedSubject: BehaviorSubject<number[]>;
    selected: Observable<number[]>;
    
    constructor(private http: HttpClient, private toaster: ToasterService) {
        this.selectedSubject = new BehaviorSubject(new Array<number>());
        this.selected = this.selectedSubject.asObservable();
    }

    get selectedValue() {
        return this.selectedSubject.value;
    }

    async select(id: number) {
        if(await this.isSelectable(id))
        {
            this.selectedSubject.next(this.selectedSubject.getValue().concat([id]));
        }
    }

    unselect(id: number)
    {
        if(this.selectedValue.includes(id))
            this.selectedValue.forEach((val, index) => {
                if(val == id)
                    this.selectedSubject.next(this.selectedValue.splice(index, 1));
            });
    }

    async isSelectable(id: number) {
        let colliding = await this.getCollidingGroupsIds(id).toPromise();
        return !this.arraysHasSameMember(colliding, this.selectedValue);
    }

    private arraysHasSameMember(arr1: number[], arr2: number[]): boolean {
       for(let val of arr1)
            if(arr2.includes(val))
                return true;

        return false;
    }

    getAll() {
        return this.http.get<Group[]>(this.url);
    }

    get(id: number) {
        return this.http.get<Group>(`${this.url}/${id}`);
    }

    create(group: Group) {
        return this.http.put<Group>(this.url, group);
    }

    update(id: number, group: Group) {
        this.http.put(`${this.url}/${id}`, group);
    }

    getCollidingGroupsIds(id: number) {
        return this.http.get<number[]>(`${this.url}/${id}/CollidingGroups`);
    }

    delete(id: number) {
        this.unselect(id);
        this.http.delete(`${this.url}/${id}`);
    }
}