import { IUserType } from './../../models/IUserType';
import { IUserTitle } from './../../models/IUserTitle';
import { IUser } from './../../models/IUser';
import { Component, OnInit } from "@angular/core";
import { UserService } from '../user-list/user.service';
import { Location } from '@angular/common';

@Component({
    templateUrl: './user-insert.component.html'
})

export class UserInsertComponent implements OnInit{
    submitted = false;
    userTitles: IUserTitle[];
    userTypes: IUserType[];
    errorMessage: string;
    originalUser: IUser={
        id: null,
        name: null,
        surname:null,
        birthDate: null,
        emailAddress: null,
        userType : null, 
        userTypeId : null,  
        userTitle : null,
        userTitleId : null,
        isActive : null, 
      };
      user: IUser = {... this.originalUser };
      
    constructor(private userService: UserService,
                private location: Location){}

    ngOnInit(): void {
        this.populateDropdowns();
    }

    populateDropdowns(): void{
        this.userService.getUserTypes().subscribe({
            next: userTypes =>
            {
                this.userTypes = userTypes
            },
            error: err => this.errorMessage = err
        });

        this.userService.getUserTitles().subscribe({
            next: userTitles =>
            {
                this.userTitles = userTitles
            },
            error: err => this.errorMessage = err
        });
    }

    insertUser(): void{
        this.userService.insertUser(this.user);
    }

    goBack(): void
    {
        this.location.back();
    }
}