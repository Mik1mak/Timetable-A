import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { environment } from '@environments/environment';
import { Timetable, UserIdentity } from '@app/_models';

@Injectable({ providedIn: 'root' })
export class UserService {
    private currentUserSubject: BehaviorSubject<UserIdentity>;
    public currentUser: Observable<UserIdentity>;
    private readonly timetableUrl = `${environment.apiUrl}/api/Timetable`;

    constructor(private http: HttpClient) {
        let currentUser: string = localStorage.getItem('currentUser')!;
        this.currentUserSubject = new BehaviorSubject<UserIdentity>(JSON.parse(currentUser));
        this.currentUser = this.currentUserSubject.asObservable();
    }

    public get currentUserValue(): UserIdentity {
        return this.currentUserSubject.value;
    }

    public get currentUserEditMode(): boolean {
        if(this.currentUserValue.editKey)
            return true;
        return false;
    }

    login(id: number, key: string) {
        return this.getUser(environment.apiUrl+'/Authenticate/Auth', { id, key });
    }

    register(name: string, cycles: number){
        return this.getUser(this.timetableUrl, {name, cycles});
    }

    update(name: string, cycles: number, showWeekend: boolean) {
        return this.http.put<UserIdentity>(this.timetableUrl, {name, cycles, showWeekend})
            .pipe(map(timetableUser => {
                let currentUser = this.currentUserValue;
                currentUser.name = timetableUser.name;
                currentUser.cycles = timetableUser.cycles;
                currentUser.showWeekend = timetableUser.showWeekend;
                this.setUser(currentUser);
                return timetableUser;
            }));
    }

    private getUser(url: string, request: any) {
        return this.http.post<any>(url, request)
            .pipe(map(user => {
                this.setUser(user);
                return user;
            }));
    }

    private setUser(user: UserIdentity) {
        this.currentUserSubject.next(user);
        localStorage.setItem('currentUser', JSON.stringify(user));
    }

    logout() {
        localStorage.removeItem('currentUser');
        this.currentUserSubject.next(new UserIdentity);
    }
}