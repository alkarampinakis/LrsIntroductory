import { Component, OnInit } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import { UserService } from "../user.service";
import {  Location } from '@angular/common';
import { User } from "src/app/models/User";
import { UserTitle } from "src/app/models/UserTitle";
import { UserType } from "src/app/models/UserType";
import { NgForm } from "@angular/forms";
import { DatePipe } from '@angular/common';
import * as _ from 'lodash';

@Component({
    templateUrl: './user-update.component.html'
})

export class UserUpdateComponent implements OnInit{
    originalUser: User;
    user: User;
    userTitles: UserTitle[];
    userTypes: UserType[];      
    errorMessage: string;
    date:string;
    noChangesDetected = false;

    constructor(private userService: UserService,
        private location: Location,
        private router : Router,
        private route: ActivatedRoute,
        private datepipe: DatePipe){}


    ngOnInit(): void {
        this.populateDropdowns();
        this.getUser();
    }

    getUser(): void
    {
        const id = +this.route.snapshot.paramMap.get('id');
        this.userService.getUser(id)
            .subscribe(
            user => {
                this.originalUser = user
                this.user = {... this.originalUser }
                // couldnt load date on date input cause in its format it had
                // time too and couldnt put it in date input, so i made an extra var that takes 
                // only the date string and link this to the input. Then on updateUser i put it at user.birthDate
                this.date= (this.user.birthDate != null) ?
                                     this.user.birthDate.toString().split('T')[0]
                                     : null;
            },
            error => console.log(error)
            );
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

    updateUser(form : NgForm): void{
        if(form.valid){
            //passing the date temp var back to user.birthdate and making the originalUser.birthdate the same format
            this.user.birthDate = this.date != "" ? new Date(this.datepipe.transform(new Date(this.date),'yyyy-MM-dd')) : null;
            this.originalUser.birthDate = new Date(this.datepipe.transform(this.originalUser.birthDate,'yyyy-MM-dd'));
  
            if(_.isEqual( this.user,  this.originalUser)){
               this.noChangesDetected = true;
            }else{
                this.userService.updateUser(this.user).subscribe(res=>{
                    this.router.navigate(['user-list']);
                }),(err: any)=>{
                    console.log("Error Updating user: ", err);
                }
            }
        }
    }

    goBack(): void
    {
        this.location.back();
    }
}