import { Injectable } from '@angular/core';

import { Group } from '@app/_models';
import { BehaviorSubject, Observable, throwError } from 'rxjs';
import { ToasterService } from './toaster.service';
import { GroupsCrudService } from './groups-crud.service';
import { ActivatedRoute, Router } from '@angular/router';
import { first } from 'rxjs/operators';

@Injectable({ providedIn: 'root' })
export class GroupsService {

    private selectedSubject: BehaviorSubject<number[]>;
    selected: Observable<number[]>;
    
    private groupsSubject: BehaviorSubject<Group[]>;
    groups: Observable<Group[]>;

    loading = true;
    queryParamsUpdateSubscribed = false;

    constructor(
        private crud: GroupsCrudService,
        private router: Router,
        private route: ActivatedRoute,
        private toaster: ToasterService) {

        this.selectedSubject = new BehaviorSubject(new Array<number>());
        this.selected = this.selectedSubject.asObservable();
        this.groupsSubject = new BehaviorSubject(new Array<Group>());
        this.groups = this.groupsSubject.asObservable();        
    }


    get selectedValue() {
        return this.selectedSubject.value;
    }

    loadGroups() {
        this.loading = true;

        let toSelect: number[] = [];
        this.route.queryParamMap.pipe(first()).subscribe(params => {
            toSelect = params.getAll('g').map(x => parseInt(x));
        });

        this.crud.getAll().pipe(first()).subscribe({
            next: groups => {
                this.loading = false;
                this.groupsSubject.next(groups);

                for(let id of toSelect) {
                    let group = groups.find(g => g.id == id)
                    if(group)
                        this.select(group);
                }

                if(!this.queryParamsUpdateSubscribed) {
                    this.selected.subscribe({
                        next: selected => this.router.navigate([], {queryParams: {g: selected}, queryParamsHandling: 'merge'})
                    });
                    this.queryParamsUpdateSubscribed = true;
                }
            },
            error: err => {
                this.loading = false;
                this.toaster.add(err)
            }
        });
    }

    refreshSelectable() {
        for (let group of this.groupsSubject.value) {
            this.crud.getCollidingGroupsIds(group.id).pipe(first())
                .subscribe(g => group.collidingGroups = g);
        }
    }

    select(group: Group): boolean {
        if(this.isSelectable(group))
        {
            this.selectedSubject.next(this.selectedSubject.getValue().concat([group.id]));
            return true;
1       }
        return false;
    }

    unselect(group: Group)
    {
        this.selectedValue.forEach((val, index) => {
            if(val == group.id) {
                this.selectedValue.splice(index, 1);
                this.selectedSubject.next(this.selectedValue);
            }
        });
    }

    isSelectable(group: Group) {
        return !this.arraysHasSameMember(group.collidingGroups, this.selectedValue);
    }

    private arraysHasSameMember(arr1: number[], arr2: number[]): boolean {
       for(let val of arr1)
            if(arr2.includes(val))
                return true;
        return false;
    }

    create(name: string, hexColor: string) {
        return this.crud.create(name, hexColor).pipe(first()).subscribe({
            next: group => {
                this.groupsSubject.next(this.groupsSubject.getValue().concat(group));
            },
            error: err => this.toaster.add(err)
        });
    }

    update(id: number, name: string, hexColor: string) {
       this.crud.update(id, name, hexColor).pipe(first()).subscribe({
           next: () => {
                this.groupsSubject.value.forEach(g => {
                    if(g.id == id) {
                        g.name = name;
                        g.hexColor = hexColor;
                    }
                this.groupsSubject.next(this.groupsSubject.value);
                });
                
           },
           error: err => this.makeToastAndRefresh(err)
       })
    }

    delete(group: Group) {
        this.crud.delete(group.id).pipe(first()).subscribe({
            next: () => {
                this.unselect(group);
                if(this.groupsSubject.value.includes(group))
                this.groupsSubject.value.forEach((val, index) => {
                    if(val.id == group.id) {
                        this.groupsSubject.value.splice(index, 1);
                    }
                });
            },
            error: err => this.makeToastAndRefresh(err)
        });
    }

    private makeToastAndRefresh(errorMsg: string) {
        this.toaster.add(errorMsg);
        this.loadGroups();
    }
}