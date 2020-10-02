import { Component, Input } from "@angular/core";

@Component({
    selector : 'error-page',
    templateUrl:'./error-page.component.html'
})

export class ErrorPageComponent{
    @Input() err : string;

}