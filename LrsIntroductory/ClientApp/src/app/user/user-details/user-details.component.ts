import { UserDeleteModalComponent } from './../user-delete/user-delete-modal.component';
import { MatDialog } from '@angular/material/dialog';
import { User } from '../../models/User';
import { Component } from "@angular/core";
import { ActivatedRoute, Router } from '@angular/router';
import { UserService } from '../user.service';
import { Location } from '@angular/common';

@Component({
    templateUrl: './user-details.component.html'
})

export class UserDetailsComponent{
    pageTitle: string = 'Product ';
    user: User;

    constructor(private route: ActivatedRoute,
                private userService: UserService,
                private location: Location,
                private router: Router,
                public dialog: MatDialog){}

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

    goToEditPage():void{
        this.router.navigate(['user-update', this.user.id]);
    }

    goBack(): void
    {
        this.location.back();
    }

    openDeleteDialog() {
       this.dialog.open(UserDeleteModalComponent,{
           data: {
               userId: this.user.id
           }
       });
    }
}
