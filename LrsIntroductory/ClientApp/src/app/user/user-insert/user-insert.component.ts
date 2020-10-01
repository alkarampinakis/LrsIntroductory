import { Router } from '@angular/router';
import { UserType } from '../../models/UserType';
import { UserTitle } from '../../models/UserTitle';
import { User } from '../../models/User';
import { Component, OnInit } from "@angular/core";
import { UserService } from '../user-list/user.service';
import { Location } from '@angular/common';
import { NgForm } from '@angular/forms';

@Component({
    templateUrl: './user-insert.component.html'
})

export class UserInsertComponent implements OnInit{
    userTitles: UserTitle[];
    userTypes: UserType[];
    errorMessage: string;
    originalUser: User={
        id: 0,
        name: null,
        surname:null,
        emailAddress: null,
        birthDate: null,
        userType : "", 
        userTypeId : null,  
        userTitle : "",
        userTitleId : null,
        isActive : null, 
      };
      user: User = {... this.originalUser };
      
    constructor(private userService: UserService,
                private location: Location,
                private router : Router  ){}

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

    insertUser(form : NgForm): void{
        if(form.valid){
            this.userService.insertUser(this.user).subscribe(res=>{
                this.router.navigate(['user-list']);
            }),(err: any)=>{
                console.log("Error Inserting user: ", err);
            }
        }
    }

    goBack(): void
    {
        this.location.back();
    }
}