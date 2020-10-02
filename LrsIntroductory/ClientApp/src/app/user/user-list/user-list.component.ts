import { User } from '../../models/User';
import { Component, OnInit } from "@angular/core";
import { UserService } from '../user.service';
import { UserDeleteModalComponent } from '../user-delete/user-delete-modal.component';
import { MatDialog } from '@angular/material/dialog';

@Component({
    selector : 'user-list-component',
    templateUrl:'./user-list.component.html'
})

export class UserListComponent implements OnInit{

    title:string = 'To Fovero User List';
    _listFilter: string;
    fitleredUsers: User[];
    users: User[] = [];
    errorMessage: string;
    includeInactive:boolean = false;
        
    get listFilter(): string
    {
        return this._listFilter;
    }

    set listFilter(value: string)
    {
      this._listFilter = value;
      this.fitleredUsers = this._listFilter ? this.perfomFilter(this.listFilter) : this.users;
    }

    constructor(private userService: UserService,
                private dialog: MatDialog) { }

    ngOnInit(): void {
        this.getUsers();
    }

    getUsers():void {
        this.userService.getUsers(this.includeInactive).subscribe({
        next: users =>
        {
            this.users = users,
            this.fitleredUsers = this.users
        },
        error: err => this.errorMessage = err
        });
    }
  
    perfomFilter(filterBy: string): User[]
    {
      filterBy = filterBy.toLocaleLowerCase();
      return this.users.filter((user: User) =>
      (user.surname +" "+ user.name).toLocaleLowerCase().indexOf(filterBy) !== -1) 
    }

    toggleInactive() : void{
        this.includeInactive = !this.includeInactive;
        this.getUsers();
    }

    openDeleteDialog(userId: number) {
        this.dialog.open(UserDeleteModalComponent,{
            data: {
                userId: userId
            }
        });
     }
}