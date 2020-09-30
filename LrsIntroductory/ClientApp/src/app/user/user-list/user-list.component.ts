import { IUser } from '../../models/IUser';
import { Component, OnInit } from "@angular/core";
import { UserService } from './user.service';

@Component({
    selector : 'user-list-component',
    templateUrl:'./user-list.component.html'
})

export class UserListComponent implements OnInit{

    title:string = 'To Fovero User List';
    _listFilter: string;
    fitleredUsers: IUser[];
    users: IUser[] = [];
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

    constructor(private userService: UserService) { }

    ngOnInit(): void {
        this.getUsers();
    }

    getUsers():void {
        this._listFilter = "filter";
        this.userService.getUsers(this.includeInactive).subscribe({
        next: users =>
        {
            this.users = users,
            this.fitleredUsers = this.users
        },
        error: err => this.errorMessage = err
        });
    }
  
    perfomFilter(filterBy: string): IUser[]
    {
      filterBy = filterBy.toLocaleLowerCase();
      return this.users.filter((user: IUser) =>
      (user.surname +" "+ user.name).toLocaleLowerCase().indexOf(filterBy) !== -1) 
    }

    toggleInactive() : void{
        this.includeInactive = !this.includeInactive;
        this.getUsers();
    }
}