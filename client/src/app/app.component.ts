import { OnInit } from '@angular/core';
import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { User } from './_models/user';
import { AccountService } from './_services/account.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent implements OnInit {
  opened: boolean = true;
  model: any = {};

  constructor(public accountService: AccountService, private router: Router) {}

  ngOnInit(): void {
    this.setCurrentUser();
  }

  login() {
    this.accountService.login(this.model).subscribe((response) => {
      this.router.navigateByUrl('/budget');
    });
  }

  logout() {
    this.accountService.logout();
    this.router.navigateByUrl('/');
  }

  setCurrentUser() {
    const user: User = JSON.parse(localStorage.getItem('user'));
    this.accountService.setCurrentUser(user);
  }

  toggleSidebar() {
    this.opened = !this.opened;
  }
}
