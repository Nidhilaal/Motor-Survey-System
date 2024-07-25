import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { Router } from '@angular/router';
import { Session } from 'inspector';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrl: './home.component.css',
  encapsulation: ViewEncapsulation.None,
})
export class HomeComponent implements OnInit {

  constructor(private router: Router){}

  ngOnInit(): void {

    if(sessionStorage.getItem('userType') === null){
      this.router.navigate(['/account/login']);
    }
  }

}
