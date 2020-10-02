import { catchError, retry } from 'rxjs/operators';
import { UserTitle } from '../models/UserTitle';
import { UserType } from '../models/UserType';
import { User } from '../models/User';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
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
    
    httpOptions = {
        headers: new HttpHeaders({
          'Content-Type': 'application/json; charset=utf-8'
        })
    };

    constructor(private http:HttpClient){}

    // Gets the list of users.
    getUsers(includeInactive:boolean): Observable<User[]>{
        return this.http.get<User[]>(this.baseUrl + this.usersEndpoint + "?includeInactive=" + includeInactive);
    }

    // Gets a user by user identifier .
    getUser(userId : number): Observable<User>{
        return this.http.get<User>(this.baseUrl + this.userEndpoint + "?userId=" + userId);
    }

    // Gets all user types.
    getUserTypes(): Observable<UserType[]>{
        return this.http.get<UserType[]>(this.baseUrl + this.typesEndpoint);
    }
   
    // Gets all user titles.
    getUserTitles(): Observable<UserTitle[]>{
        return this.http.get<UserTitle[]>(this.baseUrl + this.titlesEndpoint);
    }

    // Insert a new user.
    insertUser(user : User) :Observable<any>{
        var data = {
            "id": user.id,
            "name":user.name,
            "surname":user.surname,
            "birthDate":(user.birthDate != null) ? new Date(user.birthDate):null,
            "emailAddress":user.emailAddress,
            "userTitleId":+user.userTitleId,
            "userTypeId":+user.userTypeId,
            "isActive":user.isActive,
            "userType":user.userType,
            "userTitle":user.userTitle
        }

        return this.http.post(
                this.baseUrl + this.usersEndpoint, 
                JSON.stringify(data),
                this.httpOptions)
            .pipe(
                catchError((err) => {
                    console.error(err);
                    throw err;
                })
            );
    }

    // Updates a user.
    updateUser(user : User) :Observable<any>{
        var data = {
            "id": user.id,
            "name":user.name,
            "surname":user.surname,
            "birthDate":(user.birthDate != null) ? new Date(user.birthDate):null,
            "emailAddress":user.emailAddress,
            "userTitleId":+user.userTitleId,
            "userTypeId":+user.userTypeId,
            "isActive":user.isActive,
            "userType":user.userType,
            "userTitle":user.userTitle
        }

        return this.http.put(
                this.baseUrl + this.usersEndpoint, 
                JSON.stringify(data),
                this.httpOptions)
            .pipe(
                catchError((err) => {
                    console.error(err);
                    throw err;
                })
            );
    }

    // Deletes a user by user identifier.
    deleteUser(userId : number) :Observable<any>{

        return this.http.delete(
                this.baseUrl + this.usersEndpoint+"?userId="+userId)
            .pipe(
                catchError((err) => {
                    console.error(err);
                    throw err;
                })
            );
    }

}