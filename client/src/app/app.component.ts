import { OnInit } from '@angular/core';
import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { User } from './_models/user';
import { UsersService } from './_services/users.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent implements OnInit {
  opened: boolean = true;
  model: any = {};

  constructor(public usersService: UsersService, private router: Router) {}

  ngOnInit(): void {
    this.setCurrentUser();
  }

  login() {
    this.usersService.login(this.model).subscribe((response) => {
      this.router.navigateByUrl('/budget');
    });
  }

  logout() {
    this.usersService.logout();
    this.router.navigateByUrl('/');
  }

  setCurrentUser() {
    const user: User = JSON.parse(localStorage.getItem('user'));
    this.usersService.setCurrentUser(user);
  }

  toggleSidebar() {
    this.opened = !this.opened;
  }
}
