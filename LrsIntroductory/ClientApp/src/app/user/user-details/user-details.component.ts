import { IUser } from '../../models/IUser';
import { Component } from "@angular/core";
import { ActivatedRoute } from '@angular/router';
import { UserService } from '../user-list/user.service';
import { Location } from '@angular/common';

@Component({
    templateUrl: './user-details.component.html'
})

export class UserDetailsComponent{
    pageTitle: string = 'Product ';
    user: IUser;

    constructor(private route: ActivatedRoute,
                private userService: UserService,
                private location: Location){}

    ngOnInit(): void
    {
        this.getUser();
    }
    
    getUser(): void
    {
        const id = +this.route.snapshot.paramMap.get('id');
        this.userService.getUser(id)
            .subscribe(
            user => this.user = user,
            error => console.log(error)
            );
    }

    goBack(): void
    {
        this.location.back();
    }
}