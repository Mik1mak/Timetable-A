import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { environment } from '@environments/environment';
import { UserIdentity } from '@app/_models';

@Injectable({ providedIn: 'root' })
export class AuthenticationService {
    private currentUserSubject: BehaviorSubject<UserIdentity>;
    public currentUser: Observable<UserIdentity>;

    constructor(private http: HttpClient) {
        let currentUser: string = localStorage.getItem('currentUser')!;
        this.currentUserSubject = new BehaviorSubject<UserIdentity>(JSON.parse(currentUser));
        this.currentUser = this.currentUserSubject.asObservable();
    }

    public get currentUserValue(): UserIdentity {
        return this.currentUserSubject.value;
    }

    login(id: number, key: string) {
        return this.getUser(environment.apiUrl+'/Authenticate/Auth', { id, key });
    }

    register(name: string, cycles: number){
        return this.getUser(environment.apiUrl+'/api/Timetable', {name, cycles});
    }

    private getUser(url: string, request: any) {
        return this.http.post<any>(url, request)
            .pipe(map(user => {
                localStorage.setItem('currentUser', JSON.stringify(user));
                this.currentUserSubject.next(user);
                return user;
            }));
    }

    logout() {
        localStorage.removeItem('currentUser');
        this.currentUserSubject.next(new UserIdentity);
    }
}