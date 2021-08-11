import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { environment } from '@environments/environment';
import { Group } from '@app/_models';

@Injectable({ providedIn: 'root' })
export class GroupsCrudService {
    private readonly url = `${environment.apiUrl}/api/Group`;

    constructor(private http: HttpClient) {}

    getAll() {
        return this.http.get<Group[]>(this.url);
    }

    get(id: number) {
        return this.http.get<Group>(`${this.url}/${id}`);
    }

    create(name: string, hexColor: string) {
        return this.http.post<Group>(this.url, {name, hexColor});
    }

    update(id: number, name: string, hexColor: string) {
        return this.http.put(`${this.url}/${id}`, {name, hexColor});
    }

    getCollidingGroupsIds(id: number) {
        return this.http.get<number[]>(`${this.url}/${id}/CollidingGroups`);
    }

    delete(group: Group) {
        return this.http.delete(`${this.url}/${group.id}`);
    }
}