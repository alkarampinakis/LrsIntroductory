import { IUserTitle } from './../../models/IUserTitle';
import { IUserType } from './../../models/IUserType';
import { IUser } from '../../models/IUser';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Injectable } from '@angular/core';

@Injectable({
    providedIn: 'root'
})

export class UserService{
    private baseUrl ='https://localhost:44366';
    private usersEndpoint ='/users';
    private userEndpoint ='/user';
    private typesEndpoint ='/types';
    private titlesEndpoint ='/titles';

    constructor(private http:HttpClient){}

    getUsers(includeInactive:boolean): Observable<IUser[]>{
        return this.http.get<IUser[]>(this.baseUrl + this.usersEndpoint + "?includeInactive=" + includeInactive);
    }

    getUser(userId : number): Observable<IUser>{
        return this.http.get<IUser>(this.baseUrl + this.userEndpoint + "?userId=" + userId);
    }

    getUserTypes(): Observable<IUserType[]>{
        return this.http.get<IUserType[]>(this.baseUrl + this.typesEndpoint);
    }
   
    getUserTitles(): Observable<IUserTitle[]>{
        return this.http.get<IUserTitle[]>(this.baseUrl + this.titlesEndpoint);
    }

    insertUser(user : IUser): Observable<any> {
        return this.http.post(this.baseUrl + this.userEndpoint, user);
    }
}