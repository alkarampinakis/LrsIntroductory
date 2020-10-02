import { ErrorPageComponent } from './error-pages/error-page.component';
import { UserUpdateComponent } from './user/user-update/user-update.component';
import { UserInsertComponent } from './user/user-insert/user-insert.component';
import { UserDetailsComponent } from './user/user-details/user-details.component';
import { UserListComponent } from './user/user-list/user-list.component';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { DatePipe } from '@angular/common';
import { MatDialogModule } from '@angular/material/dialog';
import { UserDeleteModalComponent } from './user/user-delete/user-delete-modal.component';
import { MapComponent } from './map/map.component';
import { ErrorPageNotFoundComponent } from './error-pages/error-page-not-found.component';
import { HttpErrorInterceptor } from './http-error.interceptor';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    UserListComponent,
    UserDetailsComponent,
    UserInsertComponent,
    UserUpdateComponent,
    UserDeleteModalComponent,
    MapComponent,
    ErrorPageComponent,
    ErrorPageNotFoundComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: UserListComponent, pathMatch: 'full' },
      { path: 'user-list', component: UserListComponent },
      { path: 'user/:id', component: UserDetailsComponent },
      { path: 'user-insert', component: UserInsertComponent },
      { path: 'user-update/:id', component: UserUpdateComponent },
      { path: 'map', component: MapComponent },
      { path: 'error', component: ErrorPageComponent ,data:{error: "yo"}},
      { path: '**', component: ErrorPageNotFoundComponent }
    ]),
    BrowserAnimationsModule,
    MatDialogModule
  ],
  providers: [DatePipe,
    {

      provide: HTTP_INTERCEPTORS,
 
      useClass: HttpErrorInterceptor,
 
      multi: true
 
    }],
  bootstrap: [AppComponent]
})
export class AppModule { }
