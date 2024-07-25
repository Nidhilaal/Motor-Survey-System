import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.css',
  encapsulation: ViewEncapsulation.None,

})
export class DashboardComponent implements OnInit{
  
  constructor(private router:Router){}

  ngOnInit(): void {
    if(sessionStorage.getItem('userType') === null){
      this.router.navigate(['/account/login']);
    }
  }

}
