import { UserService } from '../user.service';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Component, Inject, Input } from "@angular/core";
import { Router } from '@angular/router';

@Component({
    selector: 'user-delete-modal',
    templateUrl: './user-delete-modal.component.html'
  })
  export class UserDeleteModalComponent {
    constructor(@Inject(MAT_DIALOG_DATA) public data,
                private userService: UserService,
                private router : Router ){}

    deleteUser(userId: number){
        this.userService.deleteUser(userId).subscribe(res=>{
          if(this.router.url == '/user-list'){
            location.reload();
          }
          else{
            this.router.navigate(['user-list']);
          }
      }),(err: any)=>{
          console.log("Error Inserting user: ", err);
      }
    }
  }